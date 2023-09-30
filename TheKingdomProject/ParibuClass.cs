using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Windows.Forms.Design.AxImporter;
using static TheKingdomProject.CoinOperationUserControl;
using static TheKingdomProject.Form1;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using System.Collections.ObjectModel;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OpenQA.Selenium.Remote;

namespace TheKingdomProject
{
    internal class ParibuClass
    {
        public static IWebDriver driver;
        ChromeOptions options;
        public static int CurrentAnalyzedCoinIndex = 0;
        public static bool CurrentCoinPriceArrived = false;

        public static double total_try_amount = 0;
        public static double free_try_amount = 0;
        public static double total_usdt_amount = 0;
        public static double free_usdt_amount = 0;
        public static double total_assets_in_try = 0;
        public static double placed_orders_in_usdt = 0;

        public static string robot_currency = "TRY"; // default is TRY, it can be set to USDT in Form1

        public static bool robot_status_on = true;

        public static bool operation_exit_has_been_requested = false;

        public enum OrderTypeEnm
        {
            BUY_ORDER,
            SELL_ORDER
        };
        public class OpenOrderClass
        {
            public OrderTypeEnm order_type = OrderTypeEnm.BUY_ORDER;
            public string coin_name = string.Empty;
            public string price = string.Empty;
            public string amount = string.Empty;
            public string total_price = string.Empty;
        }
        public class CoinPriceEntry
        {
            public string price = "";
            public string amount = "";
            public string total_price = "";
        }
        public class CoinOperationInformationClass
        {
            public int coin_index;
            public string coin_name = "";
            public string coin_price_url = "";
            public List<CoinPriceEntry> coinSellPrices = new List<CoinPriceEntry>();
            public List<CoinPriceEntry> coinBuyPrices = new List<CoinPriceEntry>();
            public List<CoinPriceEntry> coinFilteredSellPrices = new List<CoinPriceEntry>();
            public List<CoinPriceEntry> coinFilteredBuyPrices = new List<CoinPriceEntry>();
            public List<OpenOrderClass> openOrders = new List<OpenOrderClass>();
            public string current_balance = "0";
            public int current_balance_in_try = 0;
            public int last_price_update_time = 0;
        }

        public static List<CoinOperationInformationClass> CoinOperationInformations = new List<CoinOperationInformationClass>();

        public void InitializeCoinOperationParameters(int index, string coin_name, string coin_url)
        {
            CoinOperationInformationClass coin_obj = new CoinOperationInformationClass();
            coin_obj.coin_index = index;
            coin_obj.coin_name = coin_name;
            coin_obj.coin_price_url = coin_url;
            CoinOperationInformations.Add(coin_obj);
        }

        public ParibuClass()
        {
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1]; ;
            string user_data_path = "--user-data-dir=C:/Users/" + username + "/ AppData/Local/Google/Chrome/User Data/Default";
            
            options = new ChromeOptions();
            options.AddArgument(user_data_path);
            options.AddArgument("--start-maximized"); // Maximize the browser window
            
            
            /*
            int desiredPort = 9515;
            options.AddArgument($"--remote-debugging-port={desiredPort}");
            
            try
            {
                // Try connecting to an existing ChromeDriver instance (assuming it's already running).
                driver = new RemoteWebDriver(new Uri("http://localhost:9515"), new ChromeOptions());
            }
            catch (WebDriverException)
            {
                driver = new ChromeDriver(options);
            }*/

            driver = new ChromeDriver(options);
        }

        public static void OpenParibuLoginPage()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl("https://www.paribu.com/auth/sign-in");
        }
        public static bool IsLoggedInPage()
        {
            try
            {
                IWebElement link = driver.FindElement(By.CssSelector("a[href='/account/user-info']"));
                return true;
            }
            catch
            {
                if (!IsLoginPage())
                {
                    driver.Navigate().GoToUrl("https://www.paribu.com/auth/sign-in");
                }
                return false;
            }
        }
        public static bool IsLoginPage()
        {
            try
            {
                var exists = driver.FindElement(By.Id("yourPhoneNumber")).Displayed;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static void LoginToParibu(string phone_number, string password)
        {
            var username_Tb = driver.FindElement(By.Id("yourPhoneNumber"));
            var password_Tb = driver.FindElement(By.Id("password"));

            username_Tb.SendKeys(phone_number);
            password_Tb.SendKeys(password);
            password_Tb.SendKeys(OpenQA.Selenium.Keys.Enter);
        }

        public static List<CoinPriceEntry> GetAllCoinPrices(OrderTypeEnm orderType)
        {
            List<CoinPriceEntry> retval = new List<CoinPriceEntry>();
            if (orderType == OrderTypeEnm.BUY_ORDER)
            {
                try
                {
                    string[] allinfo = driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[3]/div[2]/div[3]/div")).Text.Split("\r\n");
                    for (int i = 0; i < (allinfo.Count() / 3); i++)
                    {
                        CoinPriceEntry entry = new CoinPriceEntry();
                        entry.total_price = allinfo[3 * i];
                        entry.amount = allinfo[(3 * i) + 1];
                        entry.price = allinfo[(3 * i) + 2];
                        retval.Add(entry);
                    }
                }
                catch(Exception ex)
                {
                    //LOG("Unable to get the buy prices)
                }
            }
            else if (orderType == OrderTypeEnm.SELL_ORDER)
            {
                try
                {
                    string[] allinfo = driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[3]/div[2]/div[1]/div")).Text.Split("\r\n");
                    for (int i = (allinfo.Count() / 3) - 1; i > 0; i--)
                    {
                        CoinPriceEntry entry = new CoinPriceEntry();
                        entry.total_price = allinfo[3 * i];
                        entry.amount = allinfo[(3 * i) + 1];
                        entry.price = allinfo[(3 * i) + 2];
                        retval.Add(entry);
                    }
                }
                catch(Exception ex)
                {
                    //LOG("Unable to get the sell prices)
                }
            }

            return retval;
        }

        public static void SetRobotCurrency(string currency)
        {
            if (currency.Equals("USDT"))
                robot_currency = currency;
            else
                robot_currency = "TRY";
        }
        public static List<CoinPriceEntry> GetAllCoinPrices_Old(OrderTypeEnm orderType)
        {
            List<CoinPriceEntry> retval = new List<CoinPriceEntry>();

            string main_div_name = "";
            if (orderType == OrderTypeEnm.BUY_ORDER)
                main_div_name = "div.orderbook--classic-buy";
            else if (orderType == OrderTypeEnm.SELL_ORDER)
                main_div_name = "div.orderbook--classic-sell";

            IWebElement orderbookDiv = null;
            try
            {
                orderbookDiv = ParibuClass.driver.FindElement(By.CssSelector(main_div_name));
            }
            catch(Exception ex)
            {
                //LOG("Cannot get the orderbook.");
            }

            if (orderbookDiv != null)
            {
                //var divElements = orderbookDiv.FindElements(By.CssSelector("div.position-relative"));
                IReadOnlyCollection<IWebElement> divElements = orderbookDiv.FindElements(By.CssSelector("div.position-relative"));

                foreach (var divElement in divElements)
                {
                    try
                    {
                        var spanElements = divElement.FindElements(By.TagName("span"));
                        if (spanElements != null)
                        {
                            string price_amount_totalprice = "";
                            CoinPriceEntry entry = new CoinPriceEntry();

                            foreach (var spanElement in spanElements)
                            {
                                price_amount_totalprice += spanElement.Text;
                                price_amount_totalprice += "_";
                            }
                            string[] datas = price_amount_totalprice.Split('_');
                            entry.total_price = datas[0];
                            entry.amount = datas[1];
                            entry.price = datas[2];
                            retval.Add(entry);
                        }
                    }
                    catch (Exception ex)
                    {
                        // sometimes staleelementexception comes.
                        //MessageBox.Show(ex.Message);
                    }
                }
            }

            return retval;
        }

        public static bool PlaceOrder(OrderTypeEnm order_type, string price, string total_amount, int waitms)
        {
            string operation_tag_id = "";
            if (order_type == OrderTypeEnm.BUY_ORDER)
            {
                operation_tag_id = "buy";
            }
            else if (order_type == OrderTypeEnm.SELL_ORDER)
            {
                operation_tag_id = "sell";
            }

            try
            {
                var div_element = driver.FindElement(By.Id(operation_tag_id));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", div_element);

                var price_element = driver.FindElement(By.Name("price"));
                price_element.SendKeys(price);
                var amount_element = driver.FindElement(By.Name("total"));
                amount_element.SendKeys(total_amount);

                price_element.SendKeys(OpenQA.Selenium.Keys.Return);
            }
            catch(Exception ex)
            {
                //LOG("Cannot create an order);
            }

            Thread.Sleep(waitms);

            return true;
        }
        public static void AnalyzeWallet()
        {
            try
            {
                IWebElement element = driver.FindElement(By.XPath("//span[text()='Cüzdan']"));
                element.Click();
            }
            catch(Exception ex)
            {
                // There is a problem clicking the link. Reading the wallet further could be problematic. So, this function must be terminated here.
                return;
            }

            try
            {
                IWebElement valueElement = driver.FindElement(By.XPath("//span[@class='f-headline-medium f-headline-medium t-text-primary']"));
                total_assets_in_try = double.Parse(valueElement.Text);
            }
            catch(Exception ex)
            {
                // There is a problem reading the total assets value. Reading the wallet further could be problematic. So, this function must be terminated here.
                return;
            }

            IReadOnlyCollection<IWebElement> try_amounts_elements = driver.FindElements(By.XPath("//div[@class='p-table__data text-start']//span[@class='f-body-medium-bold t-text-primary']"));
            if (try_amounts_elements.Count == 0)
            {
                // There is a problem reading the total try and free try amounts. Reading the wallet further could be problematic. So, this function must be terminated here.
                return;
            }
            else
            {
                total_try_amount = double.Parse(try_amounts_elements.ElementAt(0).Text);
                free_try_amount = double.Parse(try_amounts_elements.ElementAt(1).Text);
            }

            /* Tum wallet okunmadan once calistigimiz coinlerin miktarlarini 0 yapalim oyle okuyalim. */
            for (int i = 0; i < CoinOperationInformations.Count; i++)
            {
                try
                {
                    CoinOperationInformations[i].current_balance = "0";
                    CoinOperationInformations[i].current_balance_in_try = 0;
                }
                catch (Exception ex)
                {
                    // LOG("Problem parsing the price");
                }
            }

            IReadOnlyCollection<IWebElement> coin_names_elements = driver.FindElements(By.XPath("//div[@class='p-table__data text-start']//span[@class='f-body-medium-bold t-text-tertiary']"));
            IReadOnlyCollection<IWebElement> coin_prices_elements = driver.FindElements(By.XPath("//div[@class='p-table__data text-end']//span[@class='t-text-primary f-body-medium-tnum-bold']"));
            IReadOnlyCollection<IWebElement> coin_amounts_elements = driver.FindElements(By.XPath("//div[@class='p-table__data text-end']//div[@class='place-items-end gap-1']//span[@class='t-text-primary f-body-medium-tnum-bold']"));
            IReadOnlyCollection<IWebElement> coin_total_try_elements = driver.FindElements(By.XPath("//div[@class='p-table__data text-end']//div[@class='t-text-tertiary f-body-small']//span[@class='t-text-primary f-body-small-tnum']"));
            for (int i = 0; i < coin_names_elements.Count; i++)
            {
                string coin_name = coin_names_elements.ElementAt(i).Text;
                string coin_price = coin_prices_elements.ElementAt(i).Text;
                string coin_amount = coin_amounts_elements.ElementAt(i).Text;
                int coin_amount_in_try = 0;
                try
                {
                    coin_amount_in_try = (int)(double.Parse(coin_total_try_elements.ElementAt(i).Text.Split(" ")[0]));
                }
                catch(Exception ex)
                {
                    // LOG("Cannot parse th coin values in try)
                }
                string coin_total_try = coin_total_try_elements.ElementAt(i).Text;

                for (int j = 0; j < CoinOperationInformations.Count; j++)
                {
                    try
                    {
                        if (coin_name == CoinOperationInformations[j].coin_name)
                        {
                            CoinOperationInformations[j].current_balance = coin_amount;
                            CoinOperationInformations[j].current_balance_in_try = coin_amount_in_try;
                        }
                        if (coin_name == "USDT")
                        {
                            total_usdt_amount = double.Parse(coin_amount);
                        }
                    }
                    catch (Exception ex)
                    {
                        // LOG("Problem parsing the price");
                    }
                }

                if (double.Parse(coin_total_try.Split(" ")[0]) < 5)
                {
                    break;
                }
            }
        }

        static void DeleteOpenOrders(OrderTypeEnm orderType)
        {
            IReadOnlyCollection<IWebElement> rowDivs = driver.FindElements(By.CssSelector(".p-table__body .p-table__row"));
            foreach (IWebElement rowDiv in rowDivs)
            {
                if (orderType == OrderTypeEnm.BUY_ORDER)
                {
                    IWebElement firstDiv = null;
                    try
                    {
                        firstDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-start.t-green-bright"));
                    }
                    catch (Exception ex)
                    {

                    }
                    if (firstDiv != null)
                    {
                        try
                        {
                            firstDiv.Click();
                        }
                        catch (Exception ex)
                        {
                            //LOG("Emir detayinin gorundugu sagdaki pencere acilamadi.")
                        }
                        try
                        {
                            IWebElement element = driver.FindElement(By.CssSelector(".order-detail__box .p-circle"));
                            element.Click();
                        }
                        catch(Exception ex)
                        {
                            //LOG("Emir silme tarafi acilmasina ragmen emir silme buyonuna tiklayamadi.");
                        }
                    }
                }
                if (orderType == OrderTypeEnm.SELL_ORDER)
                {
                    IWebElement firstDiv = null;
                    try
                    {
                        firstDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-start.t-red-bright"));
                    }
                    catch (Exception ex)
                    {
                    }
                    if (firstDiv != null)
                    {
                        try
                        {
                            firstDiv.Click();
                        }
                        catch(Exception ex)
                        {
                            //LOG("Emir detayinin gorundugu sagdaki pencere acilamadi.")
                        }
                        Actions actions = new Actions(driver);
                        try
                        {
                            IWebElement element = driver.FindElement(By.CssSelector(".order-detail__box .p-circle"));
                            element.Click();
                        }
                        catch (Exception ex)
                        {
                            //LOG("Emir silme tarafi acilmasina ragmen emir silme buyonuna tiklayamadi.");
                        }
                    }
                }
            }
        }
        public static void GetAllOpenOrders(int index)
        {
            IReadOnlyCollection<IWebElement> rowDivs = driver.FindElements(By.CssSelector(".p-table__body .p-table__row"));

            // Create a list to store the formatted strings for each row
            List<string> formattedStrings = new List<string>();

            foreach (IWebElement rowDiv in rowDivs)
            {
                OpenOrderClass order = new OpenOrderClass();

                IWebElement firstDiv = null;
                try
                {
                    firstDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-start.t-red-bright"));
                    order.order_type = OrderTypeEnm.SELL_ORDER;
                }
                catch (Exception ex)
                {

                }
                if (firstDiv == null)
                {
                    try
                    {
                        firstDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-start.t-green-bright"));
                        order.order_type = OrderTypeEnm.BUY_ORDER;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                try
                {
                    IWebElement secondDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-end:nth-child(2)"));
                    IWebElement thirdDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-end:nth-child(3)"));
                    IWebElement fourthDiv = rowDiv.FindElement(By.CssSelector(".p-table__data.text-end:nth-child(4) .f-body-x-small-tnum-bold"));

                    // Extract the text contents from the div elements
                    string firstValue = firstDiv.Text;
                    string secondValue = secondDiv.Text;
                    string thirdValue = thirdDiv.Text;
                    string fourthValue = fourthDiv.Text;

                    order.coin_name = firstValue;
                    order.price = (secondValue.Split("TL"))[0];
                    order.amount = (thirdValue.Split(" "))[0];
                    order.total_price = fourthValue;

                    CoinOperationInformationClass operation = CoinOperationInformations[index];
                    operation.openOrders.Add(order);

                    // Build the formatted string for the current row
                    StringBuilder result = new StringBuilder();
                    result.Append(firstValue);
                    result.Append(" - ");
                    result.Append(secondValue);
                    result.Append(" - ");
                    result.Append(thirdValue);
                    result.Append(" - ");
                    result.Append(fourthValue);

                    formattedStrings.Add(result.ToString());
                }
                catch(Exception ex)
                {
                    //LOG("Problem getting the wallet details.)
                }
            }
        }

        public static List<CoinPriceEntry> FilterCoinPricesRawTable(int coin_index, List<CoinPriceEntry> raw_table)
        {
            List<CoinPriceEntry> temp_filtered_table = new List<CoinPriceEntry>();
            List<CoinPriceEntry> filtered_table = new List<CoinPriceEntry>();

            double ignore_limit = Form1.coin_opeation_user_controls[coin_index].CoinConfigurationObj.IgnoreLimit;
            double ignore_percent_limit = Form1.coin_opeation_user_controls[coin_index].CoinConfigurationObj.IgnorePercent;

            for (int i = 0; i < raw_table.Count; i++)
            {
                if (double.Parse(raw_table[i].total_price) > ignore_limit)
                {
                    temp_filtered_table.Add(raw_table[i]);
                }
            }

            for (int i = 0; i < temp_filtered_table.Count - 2; i++)
            {
                double pfirst = double.Parse(temp_filtered_table[i].price);
                double psecond = double.Parse(temp_filtered_table[i + 1].price);
                double pthird = double.Parse(temp_filtered_table[i + 2].price);
                if (((((pfirst - psecond) / pfirst) * 100) < ignore_percent_limit) && ((((psecond - pthird) / pfirst) * 100) < ignore_percent_limit))
                {
                    for (int j = i; j < temp_filtered_table.Count; j++)
                    {
                        filtered_table.Add(temp_filtered_table[j]);
                    }
                    break;
                }
            }

            return filtered_table;
        }

        public static int CalculateDecimalDigitCount(List<CoinPriceEntry> prices)
        {
            int count = -1;

            if(prices.Count > 0)
            {
                string[] price_parts = prices[0].price.Split('.');
                count = price_parts[1].Length;
            }
            
            return count;
        }
        public static void StartCoinOperation()
        {
            while (true)
            {
                if (operation_exit_has_been_requested)
                    break;
                if (IsLoggedInPage())
                {
                    if (operation_exit_has_been_requested)
                        return;

                    AnalyzeWallet();

                    free_usdt_amount = total_usdt_amount - placed_orders_in_usdt;
                    placed_orders_in_usdt = 0;

                    foreach (CoinOperationInformationClass coin_info in CoinOperationInformations)
                    {
                        while (!robot_status_on)
                        {
                            Thread.Sleep(100);
                            if (operation_exit_has_been_requested)
                                return;
                        }

                        if (operation_exit_has_been_requested)
                            break;

                        CurrentAnalyzedCoinIndex = coin_info.coin_index;

                        if (operation_exit_has_been_requested)
                            return;
                        driver.Navigate().GoToUrl(coin_info.coin_price_url);

                        if (!Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].coin_binance_price_valid)
                        {
                            DeleteOpenOrders(OrderTypeEnm.BUY_ORDER);
                            DeleteOpenOrders(OrderTypeEnm.SELL_ORDER);
                            continue;
                        }
                        if (!Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.OperationOpen)
                        {
                            DeleteOpenOrders(OrderTypeEnm.BUY_ORDER);
                            DeleteOpenOrders(OrderTypeEnm.SELL_ORDER);
                            continue;
                        }

                        coin_info.coinBuyPrices.Clear();
                        coin_info.coinSellPrices.Clear();

                        if (operation_exit_has_been_requested)
                            break;
                        Random random = new Random();
                        int randomNumber = random.Next(-20, 21);
                        int first_wait_time = ( (Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.FirstWaitTime * 1000) * (randomNumber + 100)) / 100;
                        int last_wait_time = ((Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.LastWaitTime * 1000) * (randomNumber + 100)) / 100;
                        Thread.Sleep(first_wait_time);

                        if (operation_exit_has_been_requested)
                            return;
                        coin_info.coinBuyPrices = GetAllCoinPrices(OrderTypeEnm.BUY_ORDER);
                        if (coin_info.coinBuyPrices.Count != 15)
                        {
                            coin_info.coinBuyPrices.Clear();
                            if (operation_exit_has_been_requested)
                                return;
                            coin_info.coinBuyPrices = GetAllCoinPrices(OrderTypeEnm.BUY_ORDER);
                            if (coin_info.coinBuyPrices.Count != 15)
                            {
                                //Log("Cannot get 15 valid price entries")
                                continue;
                            }
                        }
                        if (operation_exit_has_been_requested)
                            return;
                        coin_info.coinSellPrices = GetAllCoinPrices(OrderTypeEnm.SELL_ORDER);
                        if (coin_info.coinSellPrices.Count != 15)
                        {
                            coin_info.coinSellPrices.Clear();
                            if (operation_exit_has_been_requested)
                                return;
                            coin_info.coinSellPrices = GetAllCoinPrices(OrderTypeEnm.SELL_ORDER);
                            if (coin_info.coinSellPrices.Count != 15)
                            {
                                //Log("Cannot get 15 valid price entries") Do not continue with the opeartion, it is nonsense. Wait for the next turn
                                continue;
                            }
                        }

                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.CalculatedTryDecimalPoint = CalculateDecimalDigitCount(coin_info.coinBuyPrices);

                        if (operation_exit_has_been_requested)
                            return;
                        coin_info.openOrders.Clear();
                        GetAllOpenOrders(coin_info.coin_index);

                        for (int i = 0; i < coin_info.openOrders.Count; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                string order_price = coin_info.openOrders.ElementAt(i).price;
                                string buy_price = coin_info.coinBuyPrices[j].price;
                                string sell_price = coin_info.coinSellPrices[j].price;

                                if (order_price == buy_price)
                                {
                                    double new_amount = double.Parse(coin_info.coinBuyPrices[j].amount) - double.Parse(coin_info.openOrders.ElementAt(i).amount);
                                    coin_info.coinBuyPrices[j].amount = new_amount.ToString();
                                    coin_info.coinBuyPrices[j].total_price = (new_amount * double.Parse(buy_price)).ToString();
                                }
                                
                                if (order_price == sell_price)
                                {
                                    double new_amount = double.Parse(coin_info.coinSellPrices[j].amount) - double.Parse(coin_info.openOrders.ElementAt(i).amount);
                                    coin_info.coinSellPrices[j].amount = new_amount.ToString();
                                    coin_info.coinSellPrices[j].total_price = (new_amount * double.Parse(sell_price)).ToString();
                                }
                            }
                        }

                        coin_info.coinFilteredBuyPrices = FilterCoinPricesRawTable(CurrentAnalyzedCoinIndex, coin_info.coinBuyPrices);
                        coin_info.coinFilteredSellPrices = FilterCoinPricesRawTable(CurrentAnalyzedCoinIndex, coin_info.coinSellPrices);

                        if ((coin_info.coinFilteredBuyPrices.Count == 0) || coin_info.coinFilteredSellPrices.Count == 0)
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteAvgPrice = 0;
                        else
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteAvgPrice = (double.Parse(coin_info.coinFilteredBuyPrices.ElementAt(0).price) + double.Parse(coin_info.coinFilteredSellPrices.ElementAt(0).price)) / 2;
                        
                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.raw_buy_prices.Clear();
                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.raw_sell_prices.Clear();
                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.filtered_buy_prices.Clear();
                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.filtered_sell_prices.Clear();

                        for (int i = 0; i < coin_info.coinBuyPrices.Count; i++)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.raw_buy_prices.Add(coin_info.coinBuyPrices[i].price, coin_info.coinBuyPrices[i].amount);
                        }
                        for (int i = 0; i < coin_info.coinSellPrices.Count; i++)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.raw_sell_prices.Add(coin_info.coinSellPrices[i].price, coin_info.coinSellPrices[i].amount);
                        }
                        for (int i = 0; i < coin_info.coinFilteredBuyPrices.Count; i++)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.filtered_buy_prices.Add(coin_info.coinFilteredBuyPrices[i].price, coin_info.coinFilteredBuyPrices[i].amount);
                        }
                        for (int i = 0; i < coin_info.coinFilteredSellPrices.Count; i++)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.filtered_sell_prices.Add(coin_info.coinFilteredSellPrices[i].price, coin_info.coinFilteredSellPrices[i].amount);
                        }

                        double temp_site_avg_price = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteAvgPrice;
                        if (temp_site_avg_price == 0) // coin price table has been filtered out. so use the raw price table for CoinAvailableInCurrency calculation
                        {
                            temp_site_avg_price = (double.Parse(coin_info.coinBuyPrices.ElementAt(0).price) + double.Parse(coin_info.coinBuyPrices.ElementAt(0).price)) / 2;
                        }
                        Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.CoinAvailableInCurrency = double.Parse(coin_info.current_balance) * temp_site_avg_price;

                        /******************** Calculate Site Binance Average Value ************************/
                        double siteweight = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.SiteWeight;
                        double siteavgprice = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteAvgPrice;
                        double binanceprice = 0;
                        if(robot_currency.Equals("USDT"))
                            binanceprice = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.CoinUsdtBinance;
                        else
                            binanceprice = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.CoinTryBinance;

                        if (siteavgprice == 0)
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg = 0;
                        else
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg = (siteavgprice * siteweight) + (binanceprice * (1 - siteweight));

                        /******************** Calculate Spred ************************/
                        double maxdiffpercent = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaxDiffPercent;
                        double maxspred = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaxSpred;
                        double minspred = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MinSpred;
                        double siteprice = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteAvgPrice;
                        double siteoverbinancepercent = Math.Abs((siteprice / binanceprice) - 1) * 100;
                        if(siteoverbinancepercent > maxdiffpercent)
                        {
                            siteoverbinancepercent = maxdiffpercent;
                        }
                        double spred = ((siteoverbinancepercent / maxdiffpercent) * (maxspred - minspred)) + minspred;
                        double asymmetry = ((siteoverbinancepercent / maxdiffpercent) * Form1.GlobalConfigObj.MaxAsymmetryValue);
                        if (siteprice != 0)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.Spred = spred;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.Asymmetry = asymmetry;
                        }
                        else
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.Spred = 0;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.Asymmetry = 0;
                        }

                        /******************** Calculate Min and Max Target Spreds ************************/
                            //double min_target_spred = (spred / 2) * 0.625;
                            //double max_target_spred = (spred / 2) * 1.375;
                        double min_target_spred = (spred / 2) * (1 - asymmetry);
                        double max_target_spred = (spred / 2) * (1 + asymmetry);
                        double buyspredvalue = 0;
                        double sellspredvalue = 0;
                        if(siteprice == 0)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredBuy = 0;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredSell = 0;
                        }
                        else if (siteprice < binanceprice)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredBuy = min_target_spred;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredSell = max_target_spred;
                            buyspredvalue = (Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg * min_target_spred) / 100;
                            sellspredvalue = (Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg * max_target_spred) / 100;
                        }
                        else
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredBuy = max_target_spred;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.TargetSpredSell = min_target_spred;
                            buyspredvalue = (Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg * max_target_spred) / 100;
                            sellspredvalue = (Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg * min_target_spred ) / 100;
                        }

                        /******************** Calculate Wors Buy and Sell Values ************************/
                        if (siteprice == 0)
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstBuyTarget = 0;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstSellTarget = 0;
                        }
                        else
                        {
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstBuyTarget = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg - buyspredvalue;
                            Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstSellTarget = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.SiteBinanceAvg + sellspredvalue;
                        }
                        
                        TriggerDataPopulation();

                        CurrentCoinPriceArrived = true;

                        /********************************************************************************************************/
                        /*************** Worst Buy ve Sell Target degerlerine bagli olarak alim ve satim emri verme *************/
                        /********************************************************************************************************/
                        if ((coin_info.coinFilteredBuyPrices.Count > 2) && (coin_info.coinFilteredSellPrices.Count > 2))
                        {
                            double worst_buy = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstBuyTarget;
                            double top_buy = double.Parse(coin_info.coinFilteredBuyPrices[0].price);
                            double worst_sell = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.WorstSellTarget;
                            double top_sell = double.Parse(coin_info.coinFilteredSellPrices[0].price);
                            //int decimal_point = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.TryDecimalPoint;
                            int decimal_point = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinIntermediateDataObj.CalculatedTryDecimalPoint;
                            int sit_to_top_percent = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.SitToTopPercent;
                            int sit_to_top_random_number = random.Next(1, 101);
                            bool buy_open = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.BuyOpen;
                            bool sell_open = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.SellOpen;
                            bool buy_order_has_been_placed = false;

                            if (buy_open && (top_buy <= worst_buy) )
                            {
                                double order_buy_price = 0;
                                if (sit_to_top_random_number < sit_to_top_percent)
                                {
                                    int mul_div_factor = (int)Math.Pow(10, decimal_point);
                                    top_buy *= mul_div_factor;
                                    top_buy = Math.Round(top_buy);
                                    order_buy_price = (top_buy + 1) / mul_div_factor;
                                    top_buy /= mul_div_factor;
                                    if (order_buy_price > worst_buy)
                                    {
                                        order_buy_price = top_buy;
                                    }
                                }
                                else
                                {
                                    order_buy_price = top_buy;
                                }

                                double current_coin_value_in_currency = double.Parse(CoinOperationInformations[CurrentAnalyzedCoinIndex].current_balance) * binanceprice;
                                double enterlimit = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.EnterLimit;

                                /************************************************************************************************************************************************/
                                /* Eger herhangi bir alim emri (normal kosullarda 1 tane olmasi gerekir), benim alim emri verecegimle ayni fiyattaysa, hic bir sey yapma. Zaten 
                                 * alim emri duruyor. Bosuna ekstra emir sil, emir gir olmasin.
                                 ************************************************************************************************************************************************/
                                bool buy_order_already_valid = false;
                                for (int i = 0; i < coin_info.openOrders.Count; i++)
                                {
                                    if(coin_info.openOrders.ElementAt(i).order_type == OrderTypeEnm.BUY_ORDER)
                                    {
                                        if (coin_info.openOrders.ElementAt(i).price == order_buy_price.ToString())
                                        {
                                            buy_order_already_valid = true;
                                            placed_orders_in_usdt += double.Parse(coin_info.openOrders.ElementAt(i).total_price);
                                        }
                                    }
                                }

                                if (!buy_order_already_valid)
                                {
                                    if (operation_exit_has_been_requested)
                                        return;
                                    DeleteOpenOrders(OrderTypeEnm.BUY_ORDER);
                                    Thread.Sleep(1000);
                                    if (current_coin_value_in_currency < enterlimit)
                                    {
                                        double free_amount_in_currency = 0;
                                        if (robot_currency.Equals("USDT"))
                                            free_amount_in_currency = free_usdt_amount;
                                        else
                                            free_amount_in_currency = free_try_amount;

                                        double buy_amount_in_currency = free_amount_in_currency * Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.EnterMultiplier;

                                        if ((buy_amount_in_currency) > Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MinimumOrderValue)
                                        {
                                            if (buy_amount_in_currency > Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaximumOrderValue)
                                            {
                                                buy_amount_in_currency = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaximumOrderValue;
                                            }

                                            if (operation_exit_has_been_requested)
                                                return;
                                            if(robot_currency.Equals("USDT"))
                                            {
                                                placed_orders_in_usdt += buy_amount_in_currency;
                                            }
                                            PlaceOrder(OrderTypeEnm.BUY_ORDER, order_buy_price.ToString(), ((int)buy_amount_in_currency).ToString(), last_wait_time);
                                            buy_order_has_been_placed = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (operation_exit_has_been_requested)
                                    return;
                                DeleteOpenOrders(OrderTypeEnm.BUY_ORDER);
                            }

                            if(sell_open && (top_sell >= worst_sell))
                            {
                                if(buy_order_has_been_placed)
                                {
                                    int middle_wait_time = ((Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MiddleWaitTime * 1000) * (randomNumber + 100)) / 100;
                                    Thread.Sleep(middle_wait_time);
                                }

                                double order_sell_price = 0;
                                if (sit_to_top_random_number < sit_to_top_percent)
                                {
                                    int mul_div_factor = (int)Math.Pow(10, decimal_point);
                                    top_sell *= mul_div_factor;
                                    top_sell = Math.Round(top_sell);
                                    order_sell_price = (top_sell - 1) / mul_div_factor;
                                    top_sell /= mul_div_factor;
                                    if (order_sell_price < worst_sell)
                                    {
                                        order_sell_price = top_sell;
                                    }
                                }
                                else
                                {
                                    order_sell_price = top_sell;
                                }

                                double coinsavailable = double.Parse(CoinOperationInformations[CurrentAnalyzedCoinIndex].current_balance);
                                double sell_amount_in_coin = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.ExitMultiplier * coinsavailable;
                                double sell_amount_in_currency = sell_amount_in_coin * binanceprice;

                                /************************************************************************************************************************************************/
                                /* Eger herhangi bir satim emri (normal kosullarda 1 tane olmasi gerekir), benim satim emri verecegimle ayni fiyattaysa, hic bir sey yapma. Zaten 
                                 * satim emri duruyor. Bosuna ekstra emir sil, emir gir olmasin.
                                 ************************************************************************************************************************************************/
                                bool sell_order_already_valid = false;
                                for (int i = 0; i < coin_info.openOrders.Count; i++)
                                {
                                    if (coin_info.openOrders.ElementAt(i).order_type == OrderTypeEnm.SELL_ORDER)
                                    {
                                        if (coin_info.openOrders.ElementAt(i).price == order_sell_price.ToString())
                                            sell_order_already_valid = true;
                                    }
                                }

                                if (!sell_order_already_valid)
                                {
                                    if (operation_exit_has_been_requested)
                                        return;
                                    DeleteOpenOrders(OrderTypeEnm.SELL_ORDER);
                                    Thread.Sleep(1000);

                                    if (sell_amount_in_currency > Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MinimumOrderValue)
                                    {
                                        if (sell_amount_in_currency > Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaximumOrderValue)
                                        {
                                            sell_amount_in_currency = Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.MaximumOrderValue;
                                        }

                                        if (operation_exit_has_been_requested)
                                            return;
                                        PlaceOrder(OrderTypeEnm.SELL_ORDER, order_sell_price.ToString(), ((int)sell_amount_in_currency).ToString(), last_wait_time);
                                    }
                                }
                            }
                            else
                            {
                                if (operation_exit_has_been_requested)
                                    return;
                                DeleteOpenOrders(OrderTypeEnm.SELL_ORDER);
                            }

                        }
                        else
                        {
                            if (operation_exit_has_been_requested)
                                return;
                            DeleteOpenOrders(OrderTypeEnm.BUY_ORDER);
                            DeleteOpenOrders(OrderTypeEnm.SELL_ORDER);
                        }

                        if (operation_exit_has_been_requested)
                            return;
                        last_wait_time = ((Form1.coin_opeation_user_controls[CurrentAnalyzedCoinIndex].CoinConfigurationObj.LastWaitTime * 1000) * (randomNumber + 100)) / 100;
                        Thread.Sleep(last_wait_time);
                    }
                }
            }
        }
    }
}
