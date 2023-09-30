using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static TheKingdomProject.Form1;
using static TheKingdomProject.ParibuClass;

namespace TheKingdomProject
{
    public partial class CoinOperationUserControl : UserControl
    {
        public static string robot_currency = "TRY"; // default is TRY, it can be set to USDT in Form1

        public static bool required_parameters_arrived = false;
        public int system_seconds = 0;

        public int last_price_update_second = 0;
        public int coin_operation_start_second = 0;
        public int coin_operation_finish_second = 0;

        public bool coin_binance_price_valid = true;

        private double total_try_val = 0;
        private double free_try_val = 0;
        private double total_usdt_val = 0;
        private double free_usdt_val = 0;

        public CoinConfigurationClass CoinConfigurationObj = new CoinConfigurationClass();

        public Rectangle OriginalFormSize;
        public Rectangle UserControlFormSize_OriginalRect;
        public Rectangle CoinOperation_Gb_OriginalRect;
        public Rectangle CoinTryBinance_Lb_OriginalRect;
        public Rectangle CoinTryBinanceValue_Lb_OriginalRect;
        public Rectangle CoinUsdtBinance_Lb_OriginalRect;
        public Rectangle CoinUsdtBinanceValue_Lb_OriginalRect;
        public Rectangle Spred_Lb_OriginalRect;
        public Rectangle SpredValue_Lb_OriginalRect;
        public Rectangle SiteAvgPrice_Lb_OriginalRect;
        public Rectangle SiteAvgPriceValue_Lb_OriginalRect;
        public Rectangle SiteOverBinance_Lb_OriginalRect;
        public Rectangle SiteOverBinanceValue_Lb_OriginalRect;
        public Rectangle SiteBinanceAvg_Lb_OriginalRect;
        public Rectangle SiteBinanceAvgValue_Lb_OriginalRect;
        public Rectangle TargetSpredBuy_Lb_OriginalRect;
        public Rectangle TargetSpredBuyValue_Lb_OriginalRect;
        public Rectangle TargetSpredSell_Lb_OriginalRect;
        public Rectangle TargetSpredSellPrice_Lb_OriginalRect;
        public Rectangle WorstBuyTarget_Lb_OriginalRect;
        public Rectangle WorstBuyTargetValue_Lb_OriginalRect;
        public Rectangle WorstSellTarget_Lb_OriginalRect;
        public Rectangle WorstSellTargetValue_Lb_OriginalRect;
        public Rectangle CoinTryCurrentBinance_Lb_OriginalRect;
        public Rectangle CoinTryCurrentBinanceValue_Lb_OriginalRect;
        public Rectangle IgnoreLimit_Lb_OriginalRect;
        public Rectangle IgnoreLimitValue_Tb_OriginalRect;
        public Rectangle IgnorePercentLimit_Lb_OriginalRect;
        public Rectangle IgnorePercentLimitValue_Tb_OriginalRect;
        public Rectangle MinSpred_Lb_OriginalRect;
        public Rectangle MinSpredValue_Tb_OriginalRect;
        public Rectangle MaxSpred_Lb_OriginalRect;
        public Rectangle MaxSpredValue_Tb_OriginalRect;
        public Rectangle MaxDiffPercent_Lb_OriginalRect;
        public Rectangle MaxDiffPercentValue_Tb_OriginalRect;
        public Rectangle FirstWait_Lb_OriginalRect;
        public Rectangle FirstWaitValue_Tb_OriginalRect;
        public Rectangle LastWait_Lb_OriginalRect;
        public Rectangle TryDecimalPoint_Lb_OriginalRect;
        public Rectangle TryDecimalPointValue_Tb_OriginalRect;
        public Rectangle LastWaitValue_Tb_OriginalRect;
        public Rectangle EnterLimit_Lb_OriginalRect;
        public Rectangle EnterLimitValue_Tb_OriginalRect;
        public Rectangle EnterMultiplier_Lb_OriginalRect;
        public Rectangle EnterMultiplierValue_Tb_OriginalRect;
        public Rectangle ExitMultiplier_Lb_OriginalRect;
        public Rectangle ExitMultiplierValue_Tb_OriginalRect;
        public Rectangle SitToTopPercent_Lb_OriginalRect;
        public Rectangle SitToTopPercentValue_Tb_OriginalRect;
        public Rectangle SiteWeight_Lb_OriginalRect;
        public Rectangle SiteWeightValue_Tb_OriginalRect;
        public Rectangle MiddleWait_Lb_OriginalRect;
        public Rectangle MiddleWaitValue_Tb_OriginalRect;
        public Rectangle UpToDate_Lb_OriginalRect;
        public Rectangle UpToDateValue_Lb_OriginalRect;
        public Rectangle WorkingTime_Lb_OriginalRect;
        public Rectangle WorkingTimeValue_Lb_OriginalRect;
        public Rectangle WorkCount_Lb_OriginalRect;
        public Rectangle WorkCountValue_Lb_OriginalRect;
        public Rectangle CoinAvailableInTry_Lb_OriginalRect;
        public Rectangle CoinAvailableInTryValue_Lb_OriginalRect;
        public Rectangle BidsRawPriceTable_Dgv_OriginalRect;
        public Rectangle AsksRawPriceTable_Dgv_OriginalRect;
        public Rectangle BidsFilteredPriceTable_Dgv_OriginalRect;
        public Rectangle AsksFilteredPriceTable_Dgv_OriginalRect;
        public Rectangle RawPriceTableBidAmount_DgvC_OriginalRect;
        public Rectangle RawPriceTableBids_DgvC_OriginalRect;
        public Rectangle RawPriceTableAsks_DgvC_OriginalRect;
        public Rectangle RawPriceTableAskAmount_DgvC_OriginalRect;
        public Rectangle FilteredPriceTableBidAmount_DgvC_OriginalRect;
        public Rectangle FilteredPriceTableBids_DgvC_OriginalRect;
        public Rectangle FilteredPriceTableAskAmount_DgvC_OriginalRect;
        public Rectangle FilteredPriceTableAsks_DgvC_OriginalRect;
        public Rectangle MinOrder_Lb_OriginalRect;
        public Rectangle MinOrderValue_Lb_OriginalRect;
        public Rectangle MaxOrder_Lb_OriginalRect;
        public Rectangle MaxOrderValue_Lb_OriginalRect;

        public class CoinConfigurationClass
        {
            public string TryUrl;
            public string UsdtUrl;
            public int TryDecimalPoint;
            public int UsdtDecimalPoint;
            public double IgnoreLimit;
            public double IgnorePercent;
            public double MinSpred;
            public double MaxSpred;
            public double MaxDiffPercent;
            public double SiteWeight;
            public double EnterLimit;
            public double EnterMultiplier;
            public double ExitMultiplier;
            public int SitToTopPercent;
            public int FirstWaitTime;
            public int LastWaitTime;
            public int MiddleWaitTime;
            public int CoinTryPrecision;
            public int CoinUsdtPrecision;
            public int MinimumOrderValue;
            public int MaximumOrderValue;
            public bool OperationOpen;
            public bool BuyOpen;
            public bool SellOpen;
        }
        public class CoinIntermediateDataClass
        {
            public double CoinTryBinance;
            public double CoinUsdtBinance;
            public double Spred;
            public double SiteAvgPrice;
            public double SiteOverBinance;
            public double SiteBinanceAvg;
            public double TargetSpredBuy;
            public double TargetSpredSell;
            public double WorstBuyTarget;
            public double WorstSellTarget;
            public double UpToDate;
            public double WorkCount;
            public double WorkingTime;
            public double CoinAvailableInCurrency;
            public Dictionary<string, string> raw_buy_prices;
            public Dictionary<string, string> raw_sell_prices;
            public Dictionary<string, string> filtered_buy_prices;
            public Dictionary<string, string> filtered_sell_prices;
            public int CalculatedTryDecimalPoint;
            public double Asymmetry;
            public CoinIntermediateDataClass()
            {
                raw_buy_prices = new Dictionary<string, string>();
                raw_sell_prices = new Dictionary<string, string>();
                filtered_buy_prices = new Dictionary<string, string>();
                filtered_sell_prices = new Dictionary<string, string>();
                WorkCount = 0;
            }
        }

        public CoinIntermediateDataClass CoinIntermediateDataObj = new CoinIntermediateDataClass();

        public static void SetRobotCurrency(string currency)
        {
            if (currency.Equals("USDT"))
                robot_currency = currency;
            else
                robot_currency = "TRY";
        }
        public void SetCoinBinancePrices(double coin_try_price, double coin_usdt_price)
        {
            CoinIntermediateDataObj.CoinTryBinance = coin_try_price;
            CoinIntermediateDataObj.CoinUsdtBinance = coin_usdt_price;

            int try_decimal = (int)CoinConfigurationObj.TryDecimalPoint;
            int usdt_decimal = (int)CoinConfigurationObj.UsdtDecimalPoint;
            if (robot_currency.Equals("USDT"))
                CoinCurrentPriceBinanceValue_Lb.Text = CoinIntermediateDataObj.CoinTryBinance.ToString($"F{usdt_decimal}");
            else
                CoinCurrentPriceBinanceValue_Lb.Text = CoinIntermediateDataObj.CoinTryBinance.ToString($"F{try_decimal}");
        }
        void CreateCoinIntermediateData(double coin_try_price)
        {

        }

        public void SetBackgroundColorToActiveOrDeactive(bool is_active)
        {
            try
            {
                if (is_active)
                {
                    CoinOperation_Gb.BackColor = Color.Pink;
                    coin_operation_start_second = system_seconds;
                }
                else
                {
                    CoinOperation_Gb.BackColor = SystemColors.Control;
                    coin_operation_finish_second = system_seconds;
                    WorkingTimeValue_Lb.Text = (coin_operation_finish_second - coin_operation_start_second).ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void GetConfigurationDataFromFile(string coin_name)
        {
            string text = File.ReadAllText(@"C:\thekingdom\parameters\coin_parameters\" + coin_name + ".txt", Encoding.UTF8);
            CoinConfigurationObj = JsonConvert.DeserializeObject<CoinConfigurationClass>(text);
            if (CoinConfigurationObj == null)
            {
                MessageBox.Show("Config File Not Found");
            }
            else
            {
                IgnoreLimitValue_Tb.Text = CoinConfigurationObj.IgnoreLimit.ToString();
                IgnorePercentLimitValue_Tb.Text = CoinConfigurationObj.IgnorePercent.ToString();
                MinSpredValue_Tb.Text = CoinConfigurationObj.MinSpred.ToString();
                MaxSpredValue_Tb.Text = CoinConfigurationObj.MaxSpred.ToString();
                MaxDiffPercentValue_Tb.Text = CoinConfigurationObj.MaxDiffPercent.ToString();
                FirstWaitValue_Tb.Text = CoinConfigurationObj.FirstWaitTime.ToString();
                LastWaitValue_Tb.Text = CoinConfigurationObj.LastWaitTime.ToString();
                TryDecimalPointValue_Tb.Text = CoinConfigurationObj.TryDecimalPoint.ToString();
                EnterLimitValue_Tb.Text = CoinConfigurationObj.EnterLimit.ToString();
                EnterMultiplierValue_Tb.Text = CoinConfigurationObj.EnterMultiplier.ToString();
                ExitMultiplierValue_Tb.Text = CoinConfigurationObj.ExitMultiplier.ToString();
                SitToTopPercentValue_Tb.Text = CoinConfigurationObj.SitToTopPercent.ToString();
                SiteWeightValue_Tb.Text = CoinConfigurationObj.SiteWeight.ToString();
                MiddleWaitValue_Tb.Text = CoinConfigurationObj.MiddleWaitTime.ToString();
                MinOrderValue_Tb.Text = CoinConfigurationObj.MinimumOrderValue.ToString();
                MaxOrderValue_Tb.Text = CoinConfigurationObj.MaximumOrderValue.ToString();
                OperationOpen_Cb.Checked = CoinConfigurationObj.OperationOpen;
                BuyOperationOpen_Cb.Checked = CoinConfigurationObj.BuyOpen;
                SellOperationOpen_Cb.Checked = CoinConfigurationObj.SellOpen;
            }
        }

        public void InitializeAllPriceTables()
        {
            for (int i = 0; i < 15; i++)
            {
                BidsRawPriceTable_Dgv.Rows.Add();
                BidsRawPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                BidsRawPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                AsksRawPriceTable_Dgv.Rows.Add();
                AsksRawPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                AsksRawPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                BidsFilteredPriceTable_Dgv.Rows.Add();
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                AsksFilteredPriceTable_Dgv.Rows.Add();
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                BidsRawPriceTable_Dgv.Rows[i].Cells[0].Selected = false;
                AsksRawPriceTable_Dgv.Rows[i].Cells[0].Selected = false;
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[0].Selected = false;
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[0].Selected = false;

                if ((i % 2) == 1)
                {
                    BidsRawPriceTable_Dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    AsksRawPriceTable_Dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    BidsFilteredPriceTable_Dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    AsksFilteredPriceTable_Dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        public static void TriggerDataPopulation()
        {
            required_parameters_arrived = true;
        }

        public void SetCurrencyAmounts(double total_try, double free_try, double total_usdt, double free_usdt)
        {
            total_try_val = total_try;
            free_try_val = free_try;
            total_usdt_val = total_usdt;
            free_usdt_val = free_usdt;
        }
        private void ZeroizeAllPriceTables()
        {
            for (int i = 0; i < 15; i++)
            {
                BidsRawPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                BidsRawPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                AsksRawPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                AsksRawPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                BidsFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";

                AsksFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = "0.000";
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = "0.000";
            }
        }
        public void PopulateAllIntermediateData()
        {
            ZeroizeAllPriceTables();

            for (int i = 0; i < CoinIntermediateDataObj.raw_buy_prices.Count; i++)
            {
                BidsRawPriceTable_Dgv.Rows[i].Cells[0].Value = CoinIntermediateDataObj.raw_buy_prices.ElementAt(i).Value.ToString();
                BidsRawPriceTable_Dgv.Rows[i].Cells[1].Value = CoinIntermediateDataObj.raw_buy_prices.ElementAt(i).Key.ToString();
            }
            for (int i = 0; i < CoinIntermediateDataObj.raw_sell_prices.Count; i++)
            {
                AsksRawPriceTable_Dgv.Rows[i].Cells[0].Value = CoinIntermediateDataObj.raw_sell_prices.ElementAt(i).Key.ToString();
                AsksRawPriceTable_Dgv.Rows[i].Cells[1].Value = CoinIntermediateDataObj.raw_sell_prices.ElementAt(i).Value.ToString();
            }
            for (int i = 0; i < CoinIntermediateDataObj.filtered_buy_prices.Count; i++)
            {
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = CoinIntermediateDataObj.filtered_buy_prices.ElementAt(i).Value.ToString();
                BidsFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = CoinIntermediateDataObj.filtered_buy_prices.ElementAt(i).Key.ToString();
            }
            for (int i = 0; i < CoinIntermediateDataObj.filtered_sell_prices.Count; i++)
            {
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[0].Value = CoinIntermediateDataObj.filtered_sell_prices.ElementAt(i).Key.ToString();
                AsksFilteredPriceTable_Dgv.Rows[i].Cells[1].Value = CoinIntermediateDataObj.filtered_sell_prices.ElementAt(i).Value.ToString();
            }

            int try_decimal = (int)CoinConfigurationObj.TryDecimalPoint;
            int usdt_decimal = (int)CoinConfigurationObj.UsdtDecimalPoint;
            CoinIntermediateDataObj.WorkCount++;
            WorkCountValue_Lb.Text = CoinIntermediateDataObj.WorkCount.ToString();
            last_price_update_second = system_seconds;
            CoinAvailableInCurrencyValue_Lb.Text = ((int)CoinIntermediateDataObj.CoinAvailableInCurrency).ToString();
            SiteAvgPriceValue_Lb.Text = CoinIntermediateDataObj.SiteAvgPrice.ToString($"F{try_decimal}");
            SiteOverBinanceValue_Lb.Text = (CoinIntermediateDataObj.SiteAvgPrice / CoinIntermediateDataObj.CoinTryBinance).ToString("0.0000");
            SiteBinanceAvgValue_Lb.Text = CoinIntermediateDataObj.SiteBinanceAvg.ToString($"F{try_decimal}");
            SpredValue_Lb.Text = CoinIntermediateDataObj.Spred.ToString("0.0000");
            TargetSpredBuyValue_Lb.Text = CoinIntermediateDataObj.TargetSpredBuy.ToString("0.0000");
            TargetSpredSellValue_Lb.Text = CoinIntermediateDataObj.TargetSpredSell.ToString("0.0000");
            WorstBuyTargetValue_Lb.Text = CoinIntermediateDataObj.WorstBuyTarget.ToString($"F{try_decimal}");
            WorstSellTargetValue_Lb.Text = CoinIntermediateDataObj.WorstSellTarget.ToString($"F{try_decimal}");
            CoinTryBinanceValue_Lb.Text = CoinIntermediateDataObj.CoinTryBinance.ToString($"F{try_decimal}");
            CoinUsdtBinanceValue_Lb.Text = CoinIntermediateDataObj.CoinUsdtBinance.ToString($"F{usdt_decimal}");
            AsymmetryTemp_Lb.Text = "Asymmetry: " + CoinIntermediateDataObj.Asymmetry.ToString("0.000");
            if ((int)CoinIntermediateDataObj.CoinAvailableInCurrency > CoinConfigurationObj.EnterLimit)
            {
                SetEnterLimitExceedColor(true);
            }
            else
            {
                SetEnterLimitExceedColor(false);
            }

            if (CoinIntermediateDataObj.CalculatedTryDecimalPoint != CoinConfigurationObj.TryDecimalPoint)
            {
                TryDecimalPointValue_Tb.BackColor = Color.Red;
            }
            else
            {
                TryDecimalPointValue_Tb.BackColor = SystemColors.Window;
            }

            /*******************************************************************************************
             * Alim emri verilebilecek bir ortam olustuysa En Kotu Emir kismi Mavi yapilmali. Eger alim
             * emri verilemiyorsa kahverengi olmali.
             * *****************************************************************************************/
            if ((CoinIntermediateDataObj.filtered_buy_prices.Count >= 3) && (CoinIntermediateDataObj.filtered_sell_prices.Count >= 3))
            {
                double prc = double.Parse(CoinIntermediateDataObj.filtered_buy_prices.ElementAt(0).Key);
                double worst = CoinIntermediateDataObj.WorstBuyTarget;
                if (prc <= worst)
                {
                    WorstBuyTargetValue_Lb.BackColor = Color.Cyan;
                    /*******************************************************************************************
                    * Alis emri ortami olustu fakat giris carptani ve minimum emir buyuklugunden dolayi emir 
                    * girilemiyor. Bu durumda Giris Limiti Textbox'i da yesil yakilmali.
                    *******************************************************************************************/
                    double buy_amount_in_currency = 0;
                    if (robot_currency.Equals("USDT"))
                        buy_amount_in_currency = free_usdt_val * CoinConfigurationObj.EnterMultiplier;
                    else
                        buy_amount_in_currency = free_try_val * CoinConfigurationObj.EnterMultiplier;

                    if (buy_amount_in_currency < CoinConfigurationObj.MinimumOrderValue)
                    {
                        EnterMultiplierValue_Tb.BackColor = Color.Cyan;
                    }
                    else
                    {
                        EnterMultiplierValue_Tb.BackColor = Color.White;
                    }
                }
                else
                {
                    EnterMultiplierValue_Tb.BackColor = Color.White;
                    WorstBuyTargetValue_Lb.BackColor = Color.FromArgb(225, 128, 0);
                }
            }
            else
            {
                WorstBuyTargetValue_Lb.BackColor = Color.FromArgb(225, 128, 0);
            }
            /*******************************************************************************************
             * Satis emri verilebilecek bir ortam olustuysa En Kotu Emir kismi Mavi yapilmali. Eger satis
             * emri verilemiyorsa kahverengi olmali.
             * *****************************************************************************************/
            if ((CoinIntermediateDataObj.filtered_sell_prices.Count >= 3) && (CoinIntermediateDataObj.filtered_buy_prices.Count >= 3))
            {
                double prc = double.Parse(CoinIntermediateDataObj.filtered_sell_prices.ElementAt(0).Key);
                double worst = CoinIntermediateDataObj.WorstSellTarget;
                if (prc >= worst)
                {
                    WorstSellTargetValue_Lb.BackColor = Color.Cyan;
                    /*******************************************************************************************
                    * Satis emri ortami olustu fakat cikis carpani ve minimum emir buyuklugunden dolayi emir 
                    * girilemiyor. Bu durumda Giris Limiti Textbox'i da yesil yakilmali.
                    *******************************************************************************************/
                    double sell_amount_in_currency = double.Parse(CoinAvailableInCurrencyValue_Lb.Text) * CoinConfigurationObj.ExitMultiplier;
                    if (sell_amount_in_currency < CoinConfigurationObj.MinimumOrderValue)
                    {
                        ExitMultiplierValue_Tb.BackColor = Color.Cyan;
                    }
                    else
                    {
                        ExitMultiplierValue_Tb.BackColor = Color.White;
                    }
                }
                else
                {
                    ExitMultiplierValue_Tb.BackColor = Color.White;
                    WorstSellTargetValue_Lb.BackColor = Color.FromArgb(225, 128, 0);
                }
            }
            else
            {
                WorstSellTargetValue_Lb.BackColor = Color.FromArgb(225, 128, 0);
            }
        }

        public void SetCoinPresenterPriceTimetagColors(bool time_expired)
        {
            if (time_expired)
            {
                CoinTryBinance_Lb.BackColor = Color.OrangeRed;
                CoinTryBinanceValue_Lb.BackColor = Color.OrangeRed;
                CoinUsdtBinance_Lb.BackColor = Color.OrangeRed;
                CoinUsdtBinanceValue_Lb.BackColor = Color.OrangeRed;
                coin_binance_price_valid = false;
            }
            else
            {
                CoinTryBinance_Lb.BackColor = Color.FromArgb(224, 224, 224);
                CoinTryBinanceValue_Lb.BackColor = Color.FromArgb(224, 224, 224);
                CoinUsdtBinance_Lb.BackColor = Color.FromArgb(224, 224, 224);
                CoinUsdtBinanceValue_Lb.BackColor = Color.FromArgb(224, 224, 224);
                coin_binance_price_valid = true;
            }
        }

        public Label CoinUsdtBinanceValue_Lb_Uc => CoinUsdtBinanceValue_Lb;
        public Label CoinTryBinanceValue_Lb_Uc => CoinTryBinanceValue_Lb;
        public DataGridViewRowCollection AsksRawPriceTable_Dgv_Rows_Uc => BidsRawPriceTable_Dgv.Rows;
        public DataGridViewRowCollection BidsRawPriceTable_Dgv_Rows_Uc => AsksRawPriceTable_Dgv.Rows;
        public DataGridViewRowCollection AsksFilteredPriceTable_Dgv_Rows_Uc => BidsFilteredPriceTable_Dgv.Rows;
        public DataGridViewRowCollection BidsFilteredPriceTable_Dgv_Rows_Uc => AsksFilteredPriceTable_Dgv.Rows;
        public GroupBox CoinOperation_Gb_Uc => CoinOperation_Gb;

        public CoinOperationUserControl()
        {
            InitializeComponent();
            InitializeAllPriceTables();
            SystemSecondsCreator_Tm.Start();
        }
















        public void InitializeSizesOfElements()
        {
            BidsRawPriceTable_Dgv.Width = 150;
            BidsRawPriceTable_Dgv.Height = 150;

            OriginalFormSize = new Rectangle(0, 0, 1920, 1080);

            UserControlFormSize_OriginalRect = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            CoinOperation_Gb_OriginalRect = new Rectangle(CoinOperation_Gb.Location.X, CoinOperation_Gb.Location.Y, CoinOperation_Gb.Width, CoinOperation_Gb.Height);
            CoinTryBinance_Lb_OriginalRect = new Rectangle(CoinTryBinance_Lb.Location.X, CoinTryBinance_Lb.Location.Y, CoinTryBinance_Lb.Width, CoinTryBinance_Lb.Height);
            CoinTryBinanceValue_Lb_OriginalRect = new Rectangle(CoinTryBinanceValue_Lb.Location.X, CoinTryBinanceValue_Lb.Location.Y, CoinTryBinanceValue_Lb.Width, CoinTryBinanceValue_Lb.Height);
            CoinUsdtBinance_Lb_OriginalRect = new Rectangle(CoinUsdtBinance_Lb.Location.X, CoinUsdtBinance_Lb.Location.Y, CoinUsdtBinance_Lb.Width, CoinUsdtBinance_Lb.Height);
            CoinUsdtBinanceValue_Lb_OriginalRect = new Rectangle(CoinUsdtBinanceValue_Lb.Location.X, CoinUsdtBinanceValue_Lb.Location.Y, CoinUsdtBinanceValue_Lb.Width, CoinUsdtBinanceValue_Lb.Height);
            Spred_Lb_OriginalRect = new Rectangle(Spred_Lb.Location.X, Spred_Lb.Location.Y, Spred_Lb.Width, Spred_Lb.Height);
            SpredValue_Lb_OriginalRect = new Rectangle(SpredValue_Lb.Location.X, SpredValue_Lb.Location.Y, SpredValue_Lb.Width, SpredValue_Lb.Height);
            SiteAvgPrice_Lb_OriginalRect = new Rectangle(SiteAvgPrice_Lb.Location.X, SiteAvgPrice_Lb.Location.Y, SiteAvgPrice_Lb.Width, SiteAvgPrice_Lb.Height);
            SiteAvgPriceValue_Lb_OriginalRect = new Rectangle(SiteAvgPriceValue_Lb.Location.X, SiteAvgPriceValue_Lb.Location.Y, SiteAvgPriceValue_Lb.Width, SiteAvgPriceValue_Lb.Height);
            SiteOverBinance_Lb_OriginalRect = new Rectangle(SiteOverBinance_Lb.Location.X, SiteOverBinance_Lb.Location.Y, SiteOverBinance_Lb.Width, SiteOverBinance_Lb.Height);
            SiteOverBinanceValue_Lb_OriginalRect = new Rectangle(SiteOverBinanceValue_Lb.Location.X, SiteOverBinanceValue_Lb.Location.Y, SiteOverBinanceValue_Lb.Width, SiteOverBinanceValue_Lb.Height);
            SiteBinanceAvg_Lb_OriginalRect = new Rectangle(SiteBinanceAvg_Lb.Location.X, SiteBinanceAvg_Lb.Location.Y, SiteBinanceAvg_Lb.Width, SiteBinanceAvg_Lb.Height);
            SiteBinanceAvgValue_Lb_OriginalRect = new Rectangle(SiteBinanceAvgValue_Lb.Location.X, SiteBinanceAvgValue_Lb.Location.Y, SiteBinanceAvgValue_Lb.Width, SiteBinanceAvgValue_Lb.Height);
            TargetSpredBuy_Lb_OriginalRect = new Rectangle(TargetSpredBuy_Lb.Location.X, TargetSpredBuy_Lb.Location.Y, TargetSpredBuy_Lb.Width, TargetSpredBuy_Lb.Height);
            TargetSpredBuyValue_Lb_OriginalRect = new Rectangle(TargetSpredBuyValue_Lb.Location.X, TargetSpredBuyValue_Lb.Location.Y, TargetSpredBuyValue_Lb.Width, TargetSpredBuyValue_Lb.Height);
            TargetSpredSell_Lb_OriginalRect = new Rectangle(TargetSpredSell_Lb.Location.X, TargetSpredSell_Lb.Location.Y, TargetSpredSell_Lb.Width, TargetSpredSell_Lb.Height);
            TargetSpredSellPrice_Lb_OriginalRect = new Rectangle(TargetSpredSellValue_Lb.Location.X, TargetSpredSellValue_Lb.Location.Y, TargetSpredSellValue_Lb.Width, TargetSpredSellValue_Lb.Height);
            WorstBuyTarget_Lb_OriginalRect = new Rectangle(WorstBuyTarget_Lb.Location.X, WorstBuyTarget_Lb.Location.Y, WorstBuyTarget_Lb.Width, WorstBuyTarget_Lb.Height);
            WorstBuyTargetValue_Lb_OriginalRect = new Rectangle(WorstBuyTargetValue_Lb.Location.X, WorstBuyTargetValue_Lb.Location.Y, WorstBuyTargetValue_Lb.Width, WorstBuyTargetValue_Lb.Height);
            WorstSellTarget_Lb_OriginalRect = new Rectangle(WorstSellTarget_Lb.Location.X, WorstSellTarget_Lb.Location.Y, WorstSellTarget_Lb.Width, WorstSellTarget_Lb.Height);
            WorstSellTargetValue_Lb_OriginalRect = new Rectangle(WorstSellTargetValue_Lb.Location.X, WorstSellTargetValue_Lb.Location.Y, WorstSellTargetValue_Lb.Width, WorstSellTargetValue_Lb.Height);
            CoinTryCurrentBinance_Lb_OriginalRect = new Rectangle(CoinCurrentPriceBinance_Lb.Location.X, CoinCurrentPriceBinance_Lb.Location.Y, CoinCurrentPriceBinance_Lb.Width, CoinCurrentPriceBinance_Lb.Height);
            CoinTryCurrentBinanceValue_Lb_OriginalRect = new Rectangle(CoinCurrentPriceBinanceValue_Lb.Location.X, CoinCurrentPriceBinanceValue_Lb.Location.Y, CoinCurrentPriceBinanceValue_Lb.Width, CoinCurrentPriceBinanceValue_Lb.Height);
            IgnoreLimit_Lb_OriginalRect = new Rectangle(IgnoreLimit_Lb.Location.X, IgnoreLimit_Lb.Location.Y, IgnoreLimit_Lb.Width, IgnoreLimit_Lb.Height);
            IgnoreLimitValue_Tb_OriginalRect = new Rectangle(IgnoreLimitValue_Tb.Location.X, IgnoreLimitValue_Tb.Location.Y, IgnoreLimitValue_Tb.Width, IgnoreLimitValue_Tb.Height);
            IgnorePercentLimit_Lb_OriginalRect = new Rectangle(IgnorePercentLimit_Lb.Location.X, IgnorePercentLimit_Lb.Location.Y, IgnorePercentLimit_Lb.Width, IgnorePercentLimit_Lb.Height);
            IgnorePercentLimitValue_Tb_OriginalRect = new Rectangle(IgnorePercentLimitValue_Tb.Location.X, IgnorePercentLimitValue_Tb.Location.Y, IgnorePercentLimitValue_Tb.Width, IgnorePercentLimitValue_Tb.Height);
            MinSpred_Lb_OriginalRect = new Rectangle(MinSpred_Lb.Location.X, MinSpred_Lb.Location.Y, MinSpred_Lb.Width, MinSpred_Lb.Height);
            MinSpredValue_Tb_OriginalRect = new Rectangle(MinSpredValue_Tb.Location.X, MinSpredValue_Tb.Location.Y, MinSpredValue_Tb.Width, MinSpredValue_Tb.Height);
            MaxSpred_Lb_OriginalRect = new Rectangle(MaxSpred_Lb.Location.X, MaxSpred_Lb.Location.Y, MaxSpred_Lb.Width, MaxSpred_Lb.Height);
            MaxSpredValue_Tb_OriginalRect = new Rectangle(MaxSpredValue_Tb.Location.X, MaxSpredValue_Tb.Location.Y, MaxSpredValue_Tb.Width, MaxSpredValue_Tb.Height);
            MaxDiffPercent_Lb_OriginalRect = new Rectangle(MaxDiffPercent_Lb.Location.X, MaxDiffPercent_Lb.Location.Y, MaxDiffPercent_Lb.Width, MaxDiffPercent_Lb.Height);
            MaxDiffPercentValue_Tb_OriginalRect = new Rectangle(MaxDiffPercentValue_Tb.Location.X, MaxDiffPercentValue_Tb.Location.Y, MaxDiffPercentValue_Tb.Width, MaxDiffPercentValue_Tb.Height);
            FirstWait_Lb_OriginalRect = new Rectangle(FirstWait_Lb.Location.X, FirstWait_Lb.Location.Y, FirstWait_Lb.Width, FirstWait_Lb.Height);
            FirstWaitValue_Tb_OriginalRect = new Rectangle(FirstWaitValue_Tb.Location.X, FirstWaitValue_Tb.Location.Y, FirstWaitValue_Tb.Width, FirstWaitValue_Tb.Height);
            LastWait_Lb_OriginalRect = new Rectangle(LastWait_Lb.Location.X, LastWait_Lb.Location.Y, LastWait_Lb.Width, LastWait_Lb.Height);
            TryDecimalPoint_Lb_OriginalRect = new Rectangle(TryDecimalPoint_Lb.Location.X, TryDecimalPoint_Lb.Location.Y, TryDecimalPoint_Lb.Width, TryDecimalPoint_Lb.Height);
            TryDecimalPointValue_Tb_OriginalRect = new Rectangle(TryDecimalPointValue_Tb.Location.X, TryDecimalPointValue_Tb.Location.Y, TryDecimalPointValue_Tb.Width, TryDecimalPointValue_Tb.Height);
            LastWaitValue_Tb_OriginalRect = new Rectangle(LastWaitValue_Tb.Location.X, LastWaitValue_Tb.Location.Y, LastWaitValue_Tb.Width, LastWaitValue_Tb.Height);
            EnterLimit_Lb_OriginalRect = new Rectangle(EnterLimit_Lb.Location.X, EnterLimit_Lb.Location.Y, EnterLimit_Lb.Width, EnterLimit_Lb.Height);
            EnterLimitValue_Tb_OriginalRect = new Rectangle(EnterLimitValue_Tb.Location.X, EnterLimitValue_Tb.Location.Y, EnterLimitValue_Tb.Width, EnterLimitValue_Tb.Height);
            EnterMultiplier_Lb_OriginalRect = new Rectangle(EnterMultiplier_Lb.Location.X, EnterMultiplier_Lb.Location.Y, EnterMultiplier_Lb.Width, EnterMultiplier_Lb.Height);
            EnterMultiplierValue_Tb_OriginalRect = new Rectangle(EnterMultiplierValue_Tb.Location.X, EnterMultiplierValue_Tb.Location.Y, EnterMultiplierValue_Tb.Width, EnterMultiplierValue_Tb.Height);
            ExitMultiplier_Lb_OriginalRect = new Rectangle(ExitMultiplier_Lb.Location.X, ExitMultiplier_Lb.Location.Y, ExitMultiplier_Lb.Width, ExitMultiplier_Lb.Height);
            ExitMultiplierValue_Tb_OriginalRect = new Rectangle(ExitMultiplierValue_Tb.Location.X, ExitMultiplierValue_Tb.Location.Y, ExitMultiplierValue_Tb.Width, ExitMultiplierValue_Tb.Height);
            SitToTopPercent_Lb_OriginalRect = new Rectangle(SitToTopPercent_Lb.Location.X, SitToTopPercent_Lb.Location.Y, SitToTopPercent_Lb.Width, SitToTopPercent_Lb.Height);
            SitToTopPercentValue_Tb_OriginalRect = new Rectangle(SitToTopPercentValue_Tb.Location.X, SitToTopPercentValue_Tb.Location.Y, SitToTopPercentValue_Tb.Width, SitToTopPercentValue_Tb.Height);
            SiteWeight_Lb_OriginalRect = new Rectangle(SiteWeight_Lb.Location.X, SiteWeight_Lb.Location.Y, SiteWeight_Lb.Width, SiteWeight_Lb.Height);
            SiteWeightValue_Tb_OriginalRect = new Rectangle(SiteWeightValue_Tb.Location.X, SiteWeightValue_Tb.Location.Y, SiteWeightValue_Tb.Width, SiteWeightValue_Tb.Height);
            MiddleWait_Lb_OriginalRect = new Rectangle(MiddleWait_Lb.Location.X, MiddleWait_Lb.Location.Y, MiddleWait_Lb.Width, MiddleWait_Lb.Height);
            MiddleWaitValue_Tb_OriginalRect = new Rectangle(MiddleWaitValue_Tb.Location.X, MiddleWaitValue_Tb.Location.Y, MiddleWaitValue_Tb.Width, MiddleWaitValue_Tb.Height);
            UpToDate_Lb_OriginalRect = new Rectangle(UpToDate_Lb.Location.X, UpToDate_Lb.Location.Y, UpToDate_Lb.Width, UpToDate_Lb.Height);
            UpToDateValue_Lb_OriginalRect = new Rectangle(UpToDateValue_Lb.Location.X, UpToDateValue_Lb.Location.Y, UpToDateValue_Lb.Width, UpToDateValue_Lb.Height);
            WorkingTime_Lb_OriginalRect = new Rectangle(WorkingTime_Lb.Location.X, WorkingTime_Lb.Location.Y, WorkingTime_Lb.Width, WorkingTime_Lb.Height);
            WorkingTimeValue_Lb_OriginalRect = new Rectangle(WorkingTimeValue_Lb.Location.X, WorkingTimeValue_Lb.Location.Y, WorkingTimeValue_Lb.Width, WorkingTimeValue_Lb.Height);
            WorkCount_Lb_OriginalRect = new Rectangle(WorkCount_Lb.Location.X, WorkCount_Lb.Location.Y, WorkCount_Lb.Width, WorkCount_Lb.Height);
            WorkCountValue_Lb_OriginalRect = new Rectangle(WorkCountValue_Lb.Location.X, WorkCountValue_Lb.Location.Y, WorkCountValue_Lb.Width, WorkCountValue_Lb.Height);
            CoinAvailableInTry_Lb_OriginalRect = new Rectangle(CoinAvailableInCurrency_Lb.Location.X, CoinAvailableInCurrency_Lb.Location.Y, CoinAvailableInCurrency_Lb.Width, CoinAvailableInCurrency_Lb.Height);
            CoinAvailableInTryValue_Lb_OriginalRect = new Rectangle(CoinAvailableInCurrencyValue_Lb.Location.X, CoinAvailableInCurrencyValue_Lb.Location.Y, CoinAvailableInCurrencyValue_Lb.Width, CoinAvailableInCurrencyValue_Lb.Height);

            BidsRawPriceTable_Dgv_OriginalRect = new Rectangle(BidsRawPriceTable_Dgv.Location.X, BidsRawPriceTable_Dgv.Location.Y, BidsRawPriceTable_Dgv.Width, BidsRawPriceTable_Dgv.Height);
            AsksRawPriceTable_Dgv_OriginalRect = new Rectangle(AsksRawPriceTable_Dgv.Location.X, AsksRawPriceTable_Dgv.Location.Y, AsksRawPriceTable_Dgv.Width, AsksRawPriceTable_Dgv.Height);
            BidsFilteredPriceTable_Dgv_OriginalRect = new Rectangle(BidsFilteredPriceTable_Dgv.Location.X, BidsFilteredPriceTable_Dgv.Location.Y, BidsFilteredPriceTable_Dgv.Width, BidsFilteredPriceTable_Dgv.Height);
            AsksFilteredPriceTable_Dgv_OriginalRect = new Rectangle(AsksFilteredPriceTable_Dgv.Location.X, AsksFilteredPriceTable_Dgv.Location.Y, AsksFilteredPriceTable_Dgv.Width, AsksFilteredPriceTable_Dgv.Height);
            RawPriceTableBidAmount_DgvC_OriginalRect = new Rectangle(0, 0, RawPriceTableBidAmount_DgvC.Width, 0);
            RawPriceTableBids_DgvC_OriginalRect = new Rectangle(0, 0, RawPriceTableBids_DgvC.Width, 0);
            RawPriceTableAsks_DgvC_OriginalRect = new Rectangle(0, 0, RawPriceTableAsks_DgvC.Width, 0);
            RawPriceTableAskAmount_DgvC_OriginalRect = new Rectangle(0, 0, RawPriceTableAskAmount_DgvC.Width, 0);
            FilteredPriceTableBidAmount_DgvC_OriginalRect = new Rectangle(0, 0, FilteredPriceTableBidAmount_DgvC.Width, 0);
            FilteredPriceTableBids_DgvC_OriginalRect = new Rectangle(0, 0, FilteredPriceTableBids_DgvC.Width, 0);
            FilteredPriceTableAskAmount_DgvC_OriginalRect = new Rectangle(0, 0, FilteredPriceTableAskAmount_DgvC.Width, 0);
            FilteredPriceTableAsks_DgvC_OriginalRect = new Rectangle(0, 0, FilteredPriceTableAsks_DgvC.Width, 0);

            MinOrder_Lb_OriginalRect = new Rectangle(MinOrder_Lb.Location.X, MinOrder_Lb.Location.Y, MinOrder_Lb.Width, MinOrder_Lb.Height);
            MinOrderValue_Lb_OriginalRect = new Rectangle(MinOrderValue_Tb.Location.X, MinOrderValue_Tb.Location.Y, MinOrderValue_Tb.Width, MinOrderValue_Tb.Height);
            MaxOrder_Lb_OriginalRect = new Rectangle(MaxOrder_Lb.Location.X, MaxOrder_Lb.Location.Y, MaxOrder_Lb.Width, MaxOrder_Lb.Height);
            MaxOrderValue_Lb_OriginalRect = new Rectangle(MaxOrderValue_Tb.Location.X, MaxOrderValue_Tb.Location.Y, MaxOrderValue_Tb.Width, MaxOrderValue_Tb.Height);
        }
        public void ResizeUserControlElements(int width, int height)
        {
            double x_mul = (double)width / (double)OriginalFormSize.Width;
            double y_mul = (double)height / (double)OriginalFormSize.Height;

            this.Location = new Point((int)((double)UserControlFormSize_OriginalRect.X * x_mul), (int)((double)UserControlFormSize_OriginalRect.Y * y_mul));
            this.Width = (int)((double)UserControlFormSize_OriginalRect.Width * x_mul);
            this.Height = (int)((double)UserControlFormSize_OriginalRect.Height * y_mul);

            CoinOperation_Gb.Location = new Point((int)((double)CoinOperation_Gb_OriginalRect.X * x_mul), (int)((double)CoinOperation_Gb_OriginalRect.Y * y_mul));
            CoinOperation_Gb.Width = (int)((double)CoinOperation_Gb_OriginalRect.Width * x_mul);
            CoinOperation_Gb.Height = (int)((double)CoinOperation_Gb_OriginalRect.Height * y_mul);

            CoinTryBinance_Lb.Location = new Point((int)((double)CoinTryBinance_Lb_OriginalRect.X * x_mul), (int)((double)CoinTryBinance_Lb_OriginalRect.Y * y_mul));
            CoinTryBinance_Lb.Width = (int)((double)CoinTryBinance_Lb_OriginalRect.Width * x_mul);
            CoinTryBinance_Lb.Height = (int)((double)CoinTryBinance_Lb_OriginalRect.Height * y_mul);

            CoinTryBinanceValue_Lb.Location = new Point((int)((double)CoinTryBinanceValue_Lb_OriginalRect.X * x_mul), (int)((double)CoinTryBinanceValue_Lb_OriginalRect.Y * y_mul));
            CoinTryBinanceValue_Lb.Width = (int)((double)CoinTryBinanceValue_Lb_OriginalRect.Width * x_mul);
            CoinTryBinanceValue_Lb.Height = (int)((double)CoinTryBinanceValue_Lb_OriginalRect.Height * y_mul);

            CoinUsdtBinance_Lb.Location = new Point((int)((double)CoinUsdtBinance_Lb_OriginalRect.X * x_mul), (int)((double)CoinUsdtBinance_Lb_OriginalRect.Y * y_mul));
            CoinUsdtBinance_Lb.Width = (int)((double)CoinUsdtBinance_Lb_OriginalRect.Width * x_mul);
            CoinUsdtBinance_Lb.Height = (int)((double)CoinUsdtBinance_Lb_OriginalRect.Height * y_mul);

            CoinUsdtBinanceValue_Lb.Location = new Point((int)((double)CoinUsdtBinanceValue_Lb_OriginalRect.X * x_mul), (int)((double)CoinUsdtBinanceValue_Lb_OriginalRect.Y * y_mul));
            CoinUsdtBinanceValue_Lb.Width = (int)((double)CoinUsdtBinanceValue_Lb_OriginalRect.Width * x_mul);
            CoinUsdtBinanceValue_Lb.Height = (int)((double)CoinUsdtBinanceValue_Lb_OriginalRect.Height * y_mul);

            Spred_Lb.Location = new Point((int)((double)Spred_Lb_OriginalRect.X * x_mul), (int)((double)Spred_Lb_OriginalRect.Y * y_mul));
            Spred_Lb.Width = (int)((double)Spred_Lb_OriginalRect.Width * x_mul);
            Spred_Lb.Height = (int)((double)Spred_Lb_OriginalRect.Height * y_mul);

            SpredValue_Lb.Location = new Point((int)((double)SpredValue_Lb_OriginalRect.X * x_mul), (int)((double)SpredValue_Lb_OriginalRect.Y * y_mul));
            SpredValue_Lb.Width = (int)((double)SpredValue_Lb_OriginalRect.Width * x_mul);
            SpredValue_Lb.Height = (int)((double)SpredValue_Lb_OriginalRect.Height * y_mul);

            SiteAvgPrice_Lb.Location = new Point((int)((double)SiteAvgPrice_Lb_OriginalRect.X * x_mul), (int)((double)SiteAvgPrice_Lb_OriginalRect.Y * y_mul));
            SiteAvgPrice_Lb.Width = (int)((double)SiteAvgPrice_Lb_OriginalRect.Width * x_mul);
            SiteAvgPrice_Lb.Height = (int)((double)SiteAvgPrice_Lb_OriginalRect.Height * y_mul);

            SiteAvgPriceValue_Lb.Location = new Point((int)((double)SiteAvgPriceValue_Lb_OriginalRect.X * x_mul), (int)((double)SiteAvgPriceValue_Lb_OriginalRect.Y * y_mul));
            SiteAvgPriceValue_Lb.Width = (int)((double)SiteAvgPriceValue_Lb_OriginalRect.Width * x_mul);
            SiteAvgPriceValue_Lb.Height = (int)((double)SiteAvgPriceValue_Lb_OriginalRect.Height * y_mul);

            SiteOverBinance_Lb.Location = new Point((int)((double)SiteOverBinance_Lb_OriginalRect.X * x_mul), (int)((double)SiteOverBinance_Lb_OriginalRect.Y * y_mul));
            SiteOverBinance_Lb.Width = (int)((double)SiteOverBinance_Lb_OriginalRect.Width * x_mul);
            SiteOverBinance_Lb.Height = (int)((double)SiteOverBinance_Lb_OriginalRect.Height * y_mul);

            SiteOverBinanceValue_Lb.Location = new Point((int)((double)SiteOverBinanceValue_Lb_OriginalRect.X * x_mul), (int)((double)SiteOverBinanceValue_Lb_OriginalRect.Y * y_mul));
            SiteOverBinanceValue_Lb.Width = (int)((double)SiteOverBinanceValue_Lb_OriginalRect.Width * x_mul);
            SiteOverBinanceValue_Lb.Height = (int)((double)SiteOverBinanceValue_Lb_OriginalRect.Height * y_mul);

            SiteBinanceAvg_Lb.Location = new Point((int)((double)SiteBinanceAvg_Lb_OriginalRect.X * x_mul), (int)((double)SiteBinanceAvg_Lb_OriginalRect.Y * y_mul));
            SiteBinanceAvg_Lb.Width = (int)((double)SiteBinanceAvg_Lb_OriginalRect.Width * x_mul);
            SiteBinanceAvg_Lb.Height = (int)((double)SiteBinanceAvg_Lb_OriginalRect.Height * y_mul);

            SiteBinanceAvgValue_Lb.Location = new Point((int)((double)SiteBinanceAvgValue_Lb_OriginalRect.X * x_mul), (int)((double)SiteBinanceAvgValue_Lb_OriginalRect.Y * y_mul));
            SiteBinanceAvgValue_Lb.Width = (int)((double)SiteBinanceAvgValue_Lb_OriginalRect.Width * x_mul);
            SiteBinanceAvgValue_Lb.Height = (int)((double)SiteBinanceAvgValue_Lb_OriginalRect.Height * y_mul);

            TargetSpredBuy_Lb.Location = new Point((int)((double)TargetSpredBuy_Lb_OriginalRect.X * x_mul), (int)((double)TargetSpredBuy_Lb_OriginalRect.Y * y_mul));
            TargetSpredBuy_Lb.Width = (int)((double)TargetSpredBuy_Lb_OriginalRect.Width * x_mul);
            TargetSpredBuy_Lb.Height = (int)((double)TargetSpredBuy_Lb_OriginalRect.Height * y_mul);

            TargetSpredBuyValue_Lb.Location = new Point((int)((double)TargetSpredBuyValue_Lb_OriginalRect.X * x_mul), (int)((double)TargetSpredBuyValue_Lb_OriginalRect.Y * y_mul));
            TargetSpredBuyValue_Lb.Width = (int)((double)TargetSpredBuyValue_Lb_OriginalRect.Width * x_mul);
            TargetSpredBuyValue_Lb.Height = (int)((double)TargetSpredBuyValue_Lb_OriginalRect.Height * y_mul);

            TargetSpredSell_Lb.Location = new Point((int)((double)TargetSpredSell_Lb_OriginalRect.X * x_mul), (int)((double)TargetSpredSell_Lb_OriginalRect.Y * y_mul));
            TargetSpredSell_Lb.Width = (int)((double)TargetSpredSell_Lb_OriginalRect.Width * x_mul);
            TargetSpredSell_Lb.Height = (int)((double)TargetSpredSell_Lb_OriginalRect.Height * y_mul);

            TargetSpredSellValue_Lb.Location = new Point((int)((double)TargetSpredSellPrice_Lb_OriginalRect.X * x_mul), (int)((double)TargetSpredSellPrice_Lb_OriginalRect.Y * y_mul));
            TargetSpredSellValue_Lb.Width = (int)((double)TargetSpredSellPrice_Lb_OriginalRect.Width * x_mul);
            TargetSpredSellValue_Lb.Height = (int)((double)TargetSpredSellPrice_Lb_OriginalRect.Height * y_mul);

            WorstBuyTarget_Lb.Location = new Point((int)((double)WorstBuyTarget_Lb_OriginalRect.X * x_mul), (int)((double)WorstBuyTarget_Lb_OriginalRect.Y * y_mul));
            WorstBuyTarget_Lb.Width = (int)((double)WorstBuyTarget_Lb_OriginalRect.Width * x_mul);
            WorstBuyTarget_Lb.Height = (int)((double)WorstBuyTarget_Lb_OriginalRect.Height * y_mul);

            WorstBuyTargetValue_Lb.Location = new Point((int)((double)WorstBuyTargetValue_Lb_OriginalRect.X * x_mul), (int)((double)WorstBuyTargetValue_Lb_OriginalRect.Y * y_mul));
            WorstBuyTargetValue_Lb.Width = (int)((double)WorstBuyTargetValue_Lb_OriginalRect.Width * x_mul);
            WorstBuyTargetValue_Lb.Height = (int)((double)WorstBuyTargetValue_Lb_OriginalRect.Height * y_mul);

            WorstSellTarget_Lb.Location = new Point((int)((double)WorstSellTarget_Lb_OriginalRect.X * x_mul), (int)((double)WorstSellTarget_Lb_OriginalRect.Y * y_mul));
            WorstSellTarget_Lb.Width = (int)((double)WorstSellTarget_Lb_OriginalRect.Width * x_mul);
            WorstSellTarget_Lb.Height = (int)((double)WorstSellTarget_Lb_OriginalRect.Height * y_mul);

            CoinCurrentPriceBinance_Lb.Location = new Point((int)((double)CoinTryCurrentBinance_Lb_OriginalRect.X * x_mul), (int)((double)CoinTryCurrentBinance_Lb_OriginalRect.Y * y_mul));
            CoinCurrentPriceBinance_Lb.Width = (int)((double)CoinTryCurrentBinance_Lb_OriginalRect.Width * x_mul);
            CoinCurrentPriceBinance_Lb.Height = (int)((double)CoinTryCurrentBinance_Lb_OriginalRect.Height * y_mul);

            CoinCurrentPriceBinanceValue_Lb.Location = new Point((int)((double)CoinTryCurrentBinanceValue_Lb_OriginalRect.X * x_mul), (int)((double)CoinTryCurrentBinanceValue_Lb_OriginalRect.Y * y_mul));
            CoinCurrentPriceBinanceValue_Lb.Width = (int)((double)CoinTryCurrentBinanceValue_Lb_OriginalRect.Width * x_mul);
            CoinCurrentPriceBinanceValue_Lb.Height = (int)((double)CoinTryCurrentBinanceValue_Lb_OriginalRect.Height * y_mul);

            WorstSellTargetValue_Lb.Location = new Point((int)((double)WorstSellTargetValue_Lb_OriginalRect.X * x_mul), (int)((double)WorstSellTargetValue_Lb_OriginalRect.Y * y_mul));
            WorstSellTargetValue_Lb.Width = (int)((double)WorstSellTargetValue_Lb_OriginalRect.Width * x_mul);
            WorstSellTargetValue_Lb.Height = (int)((double)WorstSellTargetValue_Lb_OriginalRect.Height * y_mul);

            IgnoreLimit_Lb.Location = new Point((int)((double)IgnoreLimit_Lb_OriginalRect.X * x_mul), (int)((double)IgnoreLimit_Lb_OriginalRect.Y * y_mul));
            IgnoreLimit_Lb.Width = (int)((double)IgnoreLimit_Lb_OriginalRect.Width * x_mul);
            IgnoreLimit_Lb.Height = (int)((double)IgnoreLimit_Lb_OriginalRect.Height * y_mul);

            IgnoreLimitValue_Tb.Location = new Point((int)((double)IgnoreLimitValue_Tb_OriginalRect.X * x_mul), (int)((double)IgnoreLimitValue_Tb_OriginalRect.Y * y_mul));
            IgnoreLimitValue_Tb.Width = (int)((double)IgnoreLimitValue_Tb_OriginalRect.Width * x_mul);
            IgnoreLimitValue_Tb.Height = (int)((double)IgnoreLimitValue_Tb_OriginalRect.Height * y_mul);

            IgnorePercentLimit_Lb.Location = new Point((int)((double)IgnorePercentLimit_Lb_OriginalRect.X * x_mul), (int)((double)IgnorePercentLimit_Lb_OriginalRect.Y * y_mul));
            IgnorePercentLimit_Lb.Width = (int)((double)IgnorePercentLimit_Lb_OriginalRect.Width * x_mul);
            IgnorePercentLimit_Lb.Height = (int)((double)IgnorePercentLimit_Lb_OriginalRect.Height * y_mul);

            IgnorePercentLimitValue_Tb.Location = new Point((int)((double)IgnorePercentLimitValue_Tb_OriginalRect.X * x_mul), (int)((double)IgnorePercentLimitValue_Tb_OriginalRect.Y * y_mul));
            IgnorePercentLimitValue_Tb.Width = (int)((double)IgnorePercentLimitValue_Tb_OriginalRect.Width * x_mul);
            IgnorePercentLimitValue_Tb.Height = (int)((double)IgnorePercentLimitValue_Tb_OriginalRect.Height * y_mul);

            MinSpred_Lb.Location = new Point((int)((double)MinSpred_Lb_OriginalRect.X * x_mul), (int)((double)MinSpred_Lb_OriginalRect.Y * y_mul));
            MinSpred_Lb.Width = (int)((double)MinSpred_Lb_OriginalRect.Width * x_mul);
            MinSpred_Lb.Height = (int)((double)MinSpred_Lb_OriginalRect.Height * y_mul);

            MinSpredValue_Tb.Location = new Point((int)((double)MinSpredValue_Tb_OriginalRect.X * x_mul), (int)((double)MinSpredValue_Tb_OriginalRect.Y * y_mul));
            MinSpredValue_Tb.Width = (int)((double)MinSpredValue_Tb_OriginalRect.Width * x_mul);
            MinSpredValue_Tb.Height = (int)((double)MinSpredValue_Tb_OriginalRect.Height * y_mul);

            MaxSpred_Lb.Location = new Point((int)((double)MaxSpred_Lb_OriginalRect.X * x_mul), (int)((double)MaxSpred_Lb_OriginalRect.Y * y_mul));
            MaxSpred_Lb.Width = (int)((double)MaxSpred_Lb_OriginalRect.Width * x_mul);
            MaxSpred_Lb.Height = (int)((double)MaxSpred_Lb_OriginalRect.Height * y_mul);

            MaxSpredValue_Tb.Location = new Point((int)((double)MaxSpredValue_Tb_OriginalRect.X * x_mul), (int)((double)MaxSpredValue_Tb_OriginalRect.Y * y_mul));
            MaxSpredValue_Tb.Width = (int)((double)MaxSpredValue_Tb_OriginalRect.Width * x_mul);
            MaxSpredValue_Tb.Height = (int)((double)MaxSpredValue_Tb_OriginalRect.Height * y_mul);

            MaxDiffPercent_Lb.Location = new Point((int)((double)MaxDiffPercent_Lb_OriginalRect.X * x_mul), (int)((double)MaxDiffPercent_Lb_OriginalRect.Y * y_mul));
            MaxDiffPercent_Lb.Width = (int)((double)MaxDiffPercent_Lb_OriginalRect.Width * x_mul);
            MaxDiffPercent_Lb.Height = (int)((double)MaxDiffPercent_Lb_OriginalRect.Height * y_mul);

            MaxDiffPercentValue_Tb.Location = new Point((int)((double)MaxDiffPercentValue_Tb_OriginalRect.X * x_mul), (int)((double)MaxDiffPercentValue_Tb_OriginalRect.Y * y_mul));
            MaxDiffPercentValue_Tb.Width = (int)((double)MaxDiffPercentValue_Tb_OriginalRect.Width * x_mul);
            MaxDiffPercentValue_Tb.Height = (int)((double)MaxDiffPercentValue_Tb_OriginalRect.Height * y_mul);

            FirstWait_Lb.Location = new Point((int)((double)FirstWait_Lb_OriginalRect.X * x_mul), (int)((double)FirstWait_Lb_OriginalRect.Y * y_mul));
            FirstWait_Lb.Width = (int)((double)FirstWait_Lb_OriginalRect.Width * x_mul);
            FirstWait_Lb.Height = (int)((double)FirstWait_Lb_OriginalRect.Height * y_mul);

            FirstWaitValue_Tb.Location = new Point((int)((double)FirstWaitValue_Tb_OriginalRect.X * x_mul), (int)((double)FirstWaitValue_Tb_OriginalRect.Y * y_mul));
            FirstWaitValue_Tb.Width = (int)((double)FirstWaitValue_Tb_OriginalRect.Width * x_mul);
            FirstWaitValue_Tb.Height = (int)((double)FirstWaitValue_Tb_OriginalRect.Height * y_mul);

            LastWait_Lb.Location = new Point((int)((double)LastWait_Lb_OriginalRect.X * x_mul), (int)((double)LastWait_Lb_OriginalRect.Y * y_mul));
            LastWait_Lb.Width = (int)((double)LastWait_Lb_OriginalRect.Width * x_mul);
            LastWait_Lb.Height = (int)((double)LastWait_Lb_OriginalRect.Height * y_mul);

            LastWaitValue_Tb.Location = new Point((int)((double)LastWaitValue_Tb_OriginalRect.X * x_mul), (int)((double)LastWaitValue_Tb_OriginalRect.Y * y_mul));
            LastWaitValue_Tb.Width = (int)((double)LastWaitValue_Tb_OriginalRect.Width * x_mul);
            LastWaitValue_Tb.Height = (int)((double)LastWaitValue_Tb_OriginalRect.Height * y_mul);

            TryDecimalPoint_Lb.Location = new Point((int)((double)TryDecimalPoint_Lb_OriginalRect.X * x_mul), (int)((double)TryDecimalPoint_Lb_OriginalRect.Y * y_mul));
            TryDecimalPoint_Lb.Width = (int)((double)TryDecimalPoint_Lb_OriginalRect.Width * x_mul);
            TryDecimalPoint_Lb.Height = (int)((double)TryDecimalPoint_Lb_OriginalRect.Height * y_mul);

            TryDecimalPointValue_Tb.Location = new Point((int)((double)TryDecimalPointValue_Tb_OriginalRect.X * x_mul), (int)((double)TryDecimalPointValue_Tb_OriginalRect.Y * y_mul));
            TryDecimalPointValue_Tb.Width = (int)((double)TryDecimalPointValue_Tb_OriginalRect.Width * x_mul);
            TryDecimalPointValue_Tb.Height = (int)((double)TryDecimalPointValue_Tb_OriginalRect.Height * y_mul);

            EnterLimit_Lb.Location = new Point((int)((double)EnterLimit_Lb_OriginalRect.X * x_mul), (int)((double)EnterLimit_Lb_OriginalRect.Y * y_mul));
            EnterLimit_Lb.Width = (int)((double)EnterLimit_Lb_OriginalRect.Width * x_mul);
            EnterLimit_Lb.Height = (int)((double)EnterLimit_Lb_OriginalRect.Height * y_mul);

            EnterLimitValue_Tb.Location = new Point((int)((double)EnterLimitValue_Tb_OriginalRect.X * x_mul), (int)((double)EnterLimitValue_Tb_OriginalRect.Y * y_mul));
            EnterLimitValue_Tb.Width = (int)((double)EnterLimitValue_Tb_OriginalRect.Width * x_mul);
            EnterLimitValue_Tb.Height = (int)((double)EnterLimitValue_Tb_OriginalRect.Height * y_mul);

            EnterMultiplier_Lb.Location = new Point((int)((double)EnterMultiplier_Lb_OriginalRect.X * x_mul), (int)((double)EnterMultiplier_Lb_OriginalRect.Y * y_mul));
            EnterMultiplier_Lb.Width = (int)((double)EnterMultiplier_Lb_OriginalRect.Width * x_mul);
            EnterMultiplier_Lb.Height = (int)((double)EnterMultiplier_Lb_OriginalRect.Height * y_mul);

            EnterMultiplierValue_Tb.Location = new Point((int)((double)EnterMultiplierValue_Tb_OriginalRect.X * x_mul), (int)((double)EnterMultiplierValue_Tb_OriginalRect.Y * y_mul));
            EnterMultiplierValue_Tb.Width = (int)((double)EnterMultiplierValue_Tb_OriginalRect.Width * x_mul);
            EnterMultiplierValue_Tb.Height = (int)((double)EnterMultiplierValue_Tb_OriginalRect.Height * y_mul);

            ExitMultiplier_Lb.Location = new Point((int)((double)ExitMultiplier_Lb_OriginalRect.X * x_mul), (int)((double)ExitMultiplier_Lb_OriginalRect.Y * y_mul));
            ExitMultiplier_Lb.Width = (int)((double)ExitMultiplier_Lb_OriginalRect.Width * x_mul);
            ExitMultiplier_Lb.Height = (int)((double)ExitMultiplier_Lb_OriginalRect.Height * y_mul);

            ExitMultiplierValue_Tb.Location = new Point((int)((double)ExitMultiplierValue_Tb_OriginalRect.X * x_mul), (int)((double)ExitMultiplierValue_Tb_OriginalRect.Y * y_mul));
            ExitMultiplierValue_Tb.Width = (int)((double)ExitMultiplierValue_Tb_OriginalRect.Width * x_mul);
            ExitMultiplierValue_Tb.Height = (int)((double)ExitMultiplierValue_Tb_OriginalRect.Height * y_mul);

            SitToTopPercent_Lb.Location = new Point((int)((double)SitToTopPercent_Lb_OriginalRect.X * x_mul), (int)((double)SitToTopPercent_Lb_OriginalRect.Y * y_mul));
            SitToTopPercent_Lb.Width = (int)((double)SitToTopPercent_Lb_OriginalRect.Width * x_mul);
            SitToTopPercent_Lb.Height = (int)((double)SitToTopPercent_Lb_OriginalRect.Height * y_mul);

            SitToTopPercentValue_Tb.Location = new Point((int)((double)SitToTopPercentValue_Tb_OriginalRect.X * x_mul), (int)((double)SitToTopPercentValue_Tb_OriginalRect.Y * y_mul));
            SitToTopPercentValue_Tb.Width = (int)((double)SitToTopPercentValue_Tb_OriginalRect.Width * x_mul);
            SitToTopPercentValue_Tb.Height = (int)((double)SitToTopPercentValue_Tb_OriginalRect.Height * y_mul);

            SiteWeight_Lb.Location = new Point((int)((double)SiteWeight_Lb_OriginalRect.X * x_mul), (int)((double)SiteWeight_Lb_OriginalRect.Y * y_mul));
            SiteWeight_Lb.Width = (int)((double)SiteWeight_Lb_OriginalRect.Width * x_mul);
            SiteWeight_Lb.Height = (int)((double)SiteWeight_Lb_OriginalRect.Height * y_mul);

            SiteWeightValue_Tb.Location = new Point((int)((double)SiteWeightValue_Tb_OriginalRect.X * x_mul), (int)((double)SiteWeightValue_Tb_OriginalRect.Y * y_mul));
            SiteWeightValue_Tb.Width = (int)((double)SiteWeightValue_Tb_OriginalRect.Width * x_mul);
            SiteWeightValue_Tb.Height = (int)((double)SiteWeightValue_Tb_OriginalRect.Height * y_mul);

            MiddleWait_Lb.Location = new Point((int)((double)MiddleWait_Lb_OriginalRect.X * x_mul), (int)((double)MiddleWait_Lb_OriginalRect.Y * y_mul));
            MiddleWait_Lb.Width = (int)((double)MiddleWait_Lb_OriginalRect.Width * x_mul);
            MiddleWait_Lb.Height = (int)((double)MiddleWait_Lb_OriginalRect.Height * y_mul);

            MiddleWaitValue_Tb.Location = new Point((int)((double)MiddleWaitValue_Tb_OriginalRect.X * x_mul), (int)((double)MiddleWaitValue_Tb_OriginalRect.Y * y_mul));
            MiddleWaitValue_Tb.Width = (int)((double)MiddleWaitValue_Tb_OriginalRect.Width * x_mul);
            MiddleWaitValue_Tb.Height = (int)((double)MiddleWaitValue_Tb_OriginalRect.Height * y_mul);

            UpToDate_Lb.Location = new Point((int)((double)UpToDate_Lb_OriginalRect.X * x_mul), (int)((double)UpToDate_Lb_OriginalRect.Y * y_mul));
            UpToDate_Lb.Width = (int)((double)UpToDate_Lb_OriginalRect.Width * x_mul);
            UpToDate_Lb.Height = (int)((double)UpToDate_Lb_OriginalRect.Height * y_mul);

            UpToDateValue_Lb.Location = new Point((int)((double)UpToDateValue_Lb_OriginalRect.X * x_mul), (int)((double)UpToDateValue_Lb_OriginalRect.Y * y_mul));
            UpToDateValue_Lb.Width = (int)((double)UpToDateValue_Lb_OriginalRect.Width * x_mul);
            UpToDateValue_Lb.Height = (int)((double)UpToDateValue_Lb_OriginalRect.Height * y_mul);

            WorkingTime_Lb.Location = new Point((int)((double)WorkingTime_Lb_OriginalRect.X * x_mul), (int)((double)WorkingTime_Lb_OriginalRect.Y * y_mul));
            WorkingTime_Lb.Width = (int)((double)WorkingTime_Lb_OriginalRect.Width * x_mul);
            WorkingTime_Lb.Height = (int)((double)WorkingTime_Lb_OriginalRect.Height * y_mul);

            WorkingTimeValue_Lb.Location = new Point((int)((double)WorkingTimeValue_Lb_OriginalRect.X * x_mul), (int)((double)WorkingTimeValue_Lb_OriginalRect.Y * y_mul));
            WorkingTimeValue_Lb.Width = (int)((double)WorkingTimeValue_Lb_OriginalRect.Width * x_mul);
            WorkingTimeValue_Lb.Height = (int)((double)WorkingTimeValue_Lb_OriginalRect.Height * y_mul);

            WorkCount_Lb.Location = new Point((int)((double)WorkCount_Lb_OriginalRect.X * x_mul), (int)((double)WorkCount_Lb_OriginalRect.Y * y_mul));
            WorkCount_Lb.Width = (int)((double)WorkCount_Lb_OriginalRect.Width * x_mul);
            WorkCount_Lb.Height = (int)((double)WorkCount_Lb_OriginalRect.Height * y_mul);

            WorkCountValue_Lb.Location = new Point((int)((double)WorkCountValue_Lb_OriginalRect.X * x_mul), (int)((double)WorkCountValue_Lb_OriginalRect.Y * y_mul));
            WorkCountValue_Lb.Width = (int)((double)WorkCountValue_Lb_OriginalRect.Width * x_mul);
            WorkCountValue_Lb.Height = (int)((double)WorkCountValue_Lb_OriginalRect.Height * y_mul);

            CoinAvailableInCurrency_Lb.Location = new Point((int)((double)CoinAvailableInTry_Lb_OriginalRect.X * x_mul), (int)((double)CoinAvailableInTry_Lb_OriginalRect.Y * y_mul));
            CoinAvailableInCurrency_Lb.Width = (int)((double)CoinAvailableInTry_Lb_OriginalRect.Width * x_mul);
            CoinAvailableInCurrency_Lb.Height = (int)((double)CoinAvailableInTry_Lb_OriginalRect.Height * y_mul);

            CoinAvailableInCurrencyValue_Lb.Location = new Point((int)((double)CoinAvailableInTryValue_Lb_OriginalRect.X * x_mul), (int)((double)CoinAvailableInTryValue_Lb_OriginalRect.Y * y_mul));
            CoinAvailableInCurrencyValue_Lb.Width = (int)((double)CoinAvailableInTryValue_Lb_OriginalRect.Width * x_mul);
            CoinAvailableInCurrencyValue_Lb.Height = (int)((double)CoinAvailableInTryValue_Lb_OriginalRect.Height * y_mul);

            BidsRawPriceTable_Dgv.Location = new Point((int)((double)BidsRawPriceTable_Dgv_OriginalRect.X * x_mul), (int)((double)BidsRawPriceTable_Dgv_OriginalRect.Y * y_mul));
            BidsRawPriceTable_Dgv.Width = (int)((double)BidsRawPriceTable_Dgv_OriginalRect.Width * x_mul);
            BidsRawPriceTable_Dgv.Height = (int)((double)BidsRawPriceTable_Dgv_OriginalRect.Height * y_mul);

            AsksRawPriceTable_Dgv.Location = new Point((int)((double)AsksRawPriceTable_Dgv_OriginalRect.X * x_mul), (int)((double)AsksRawPriceTable_Dgv_OriginalRect.Y * y_mul));
            AsksRawPriceTable_Dgv.Width = (int)((double)AsksRawPriceTable_Dgv_OriginalRect.Width * x_mul);
            AsksRawPriceTable_Dgv.Height = (int)((double)AsksRawPriceTable_Dgv_OriginalRect.Height * y_mul);

            BidsFilteredPriceTable_Dgv.Location = new Point((int)((double)BidsFilteredPriceTable_Dgv_OriginalRect.X * x_mul), (int)((double)BidsFilteredPriceTable_Dgv_OriginalRect.Y * y_mul));
            BidsFilteredPriceTable_Dgv.Width = (int)((double)BidsFilteredPriceTable_Dgv_OriginalRect.Width * x_mul);
            BidsFilteredPriceTable_Dgv.Height = (int)((double)BidsFilteredPriceTable_Dgv_OriginalRect.Height * y_mul);

            AsksFilteredPriceTable_Dgv.Location = new Point((int)((double)AsksFilteredPriceTable_Dgv_OriginalRect.X * x_mul), (int)((double)AsksFilteredPriceTable_Dgv_OriginalRect.Y * y_mul));
            AsksFilteredPriceTable_Dgv.Width = (int)((double)AsksFilteredPriceTable_Dgv_OriginalRect.Width * x_mul);
            AsksFilteredPriceTable_Dgv.Height = (int)((double)AsksFilteredPriceTable_Dgv_OriginalRect.Height * y_mul);

            RawPriceTableBidAmount_DgvC.Width = (int)((double)RawPriceTableBidAmount_DgvC_OriginalRect.Width * x_mul);
            RawPriceTableBids_DgvC.Width = (int)((double)RawPriceTableBids_DgvC_OriginalRect.Width * x_mul);
            RawPriceTableAsks_DgvC.Width = (int)((double)RawPriceTableAsks_DgvC_OriginalRect.Width * x_mul);
            RawPriceTableAskAmount_DgvC.Width = (int)((double)RawPriceTableAskAmount_DgvC_OriginalRect.Width * x_mul);

            FilteredPriceTableBidAmount_DgvC.Width = (int)((double)FilteredPriceTableBidAmount_DgvC_OriginalRect.Width * x_mul);
            FilteredPriceTableBids_DgvC.Width = (int)((double)FilteredPriceTableBids_DgvC_OriginalRect.Width * x_mul);
            FilteredPriceTableAskAmount_DgvC.Width = (int)((double)FilteredPriceTableAskAmount_DgvC_OriginalRect.Width * x_mul);
            FilteredPriceTableAsks_DgvC.Width = (int)((double)FilteredPriceTableAsks_DgvC_OriginalRect.Width * x_mul);

            MinOrder_Lb.Location = new Point((int)((double)MinOrder_Lb_OriginalRect.X * x_mul), (int)((double)MinOrder_Lb_OriginalRect.Y * y_mul));
            MinOrder_Lb.Width = (int)((double)MinOrder_Lb_OriginalRect.Width * x_mul);
            MinOrder_Lb.Height = (int)((double)MinOrder_Lb_OriginalRect.Height * y_mul);

            MinOrderValue_Tb.Location = new Point((int)((double)MinOrderValue_Lb_OriginalRect.X * x_mul), (int)((double)MinOrderValue_Lb_OriginalRect.Y * y_mul));
            MinOrderValue_Tb.Width = (int)((double)MinOrderValue_Lb_OriginalRect.Width * x_mul);
            MinOrderValue_Tb.Height = (int)((double)MinOrderValue_Lb_OriginalRect.Height * y_mul);

            MaxOrder_Lb.Location = new Point((int)((double)MaxOrder_Lb_OriginalRect.X * x_mul), (int)((double)MaxOrder_Lb_OriginalRect.Y * y_mul));
            MaxOrder_Lb.Width = (int)((double)MaxOrder_Lb_OriginalRect.Width * x_mul);
            MaxOrder_Lb.Height = (int)((double)MaxOrder_Lb_OriginalRect.Height * y_mul);

            MaxOrderValue_Tb.Location = new Point((int)((double)MaxOrderValue_Lb_OriginalRect.X * x_mul), (int)((double)MaxOrderValue_Lb_OriginalRect.Y * y_mul));
            MaxOrderValue_Tb.Width = (int)((double)MaxOrderValue_Lb_OriginalRect.Width * x_mul);
            MaxOrderValue_Tb.Height = (int)((double)MaxOrderValue_Lb_OriginalRect.Height * y_mul);
        }

        private void CoinOperationUserControl_Load(object sender, EventArgs e)
        {

        }

        private void SystemSecondsCreator_Tm_Tick(object sender, EventArgs e)
        {
            system_seconds++;
            if (last_price_update_second > 0)
            {
                CoinIntermediateDataObj.UpToDate = (system_seconds - last_price_update_second);
                UpToDateValue_Lb.Text = CoinIntermediateDataObj.UpToDate.ToString();
            }
            if (IgnoreLimitValue_Tb.Text != CoinConfigurationObj.IgnoreLimit.ToString())
            {
                IgnoreLimit_Lb.BackColor = Color.Pink;
            }
            else
            {
                IgnoreLimit_Lb.BackColor = Color.PaleGreen;
            }
            if (IgnorePercentLimitValue_Tb.Text != CoinConfigurationObj.IgnorePercent.ToString())
            {
                IgnorePercentLimit_Lb.BackColor = Color.Pink;
            }
            else
            {
                IgnorePercentLimit_Lb.BackColor = Color.PaleGreen;
            }
            if (MinSpredValue_Tb.Text != CoinConfigurationObj.MinSpred.ToString())
            {
                MinSpred_Lb.BackColor = Color.Pink;
            }
            else
            {
                MinSpred_Lb.BackColor = Color.PaleGreen;
            }
            if (MaxSpredValue_Tb.Text != CoinConfigurationObj.MaxSpred.ToString())
            {
                MaxSpred_Lb.BackColor = Color.Pink;
            }
            else
            {
                MaxSpred_Lb.BackColor = Color.PaleGreen;
            }
            if (MaxDiffPercentValue_Tb.Text != CoinConfigurationObj.MaxDiffPercent.ToString())
            {
                MaxDiffPercent_Lb.BackColor = Color.Pink;
            }
            else
            {
                MaxDiffPercent_Lb.BackColor = Color.PaleGreen;
            }
            if (FirstWaitValue_Tb.Text != CoinConfigurationObj.FirstWaitTime.ToString())
            {
                FirstWait_Lb.BackColor = Color.Pink;
            }
            else
            {
                FirstWait_Lb.BackColor = Color.PaleGreen;
            }
            if (LastWaitValue_Tb.Text != CoinConfigurationObj.LastWaitTime.ToString())
            {
                LastWait_Lb.BackColor = Color.Pink;
            }
            else
            {
                LastWait_Lb.BackColor = Color.PaleGreen;
            }
            if (TryDecimalPointValue_Tb.Text != CoinConfigurationObj.TryDecimalPoint.ToString())
            {
                TryDecimalPoint_Lb.BackColor = Color.Pink;
            }
            else
            {
                TryDecimalPoint_Lb.BackColor = Color.PaleGreen;
            }

            if (MinOrderValue_Tb.Text != CoinConfigurationObj.MinimumOrderValue.ToString())
            {
                MinOrder_Lb.BackColor = Color.Pink;
            }
            else
            {
                MinOrder_Lb.BackColor = Color.PaleGreen;
            }
            if (EnterLimitValue_Tb.Text != CoinConfigurationObj.EnterLimit.ToString())
            {
                EnterLimit_Lb.BackColor = Color.Pink;
            }
            else
            {
                EnterLimit_Lb.BackColor = Color.PaleGreen;
            }
            if (EnterMultiplierValue_Tb.Text != CoinConfigurationObj.EnterMultiplier.ToString())
            {
                EnterMultiplier_Lb.BackColor = Color.Pink;
            }
            else
            {
                EnterMultiplier_Lb.BackColor = Color.PaleGreen;
            }
            if (ExitMultiplierValue_Tb.Text != CoinConfigurationObj.ExitMultiplier.ToString())
            {
                ExitMultiplier_Lb.BackColor = Color.Pink;
            }
            else
            {
                ExitMultiplier_Lb.BackColor = Color.PaleGreen;
            }
            if (SitToTopPercentValue_Tb.Text != CoinConfigurationObj.SitToTopPercent.ToString())
            {
                SitToTopPercent_Lb.BackColor = Color.Pink;
            }
            else
            {
                SitToTopPercent_Lb.BackColor = Color.PaleGreen;
            }
            if (SiteWeightValue_Tb.Text != CoinConfigurationObj.SiteWeight.ToString())
            {
                SiteWeight_Lb.BackColor = Color.Pink;
            }
            else
            {
                SiteWeight_Lb.BackColor = Color.PaleGreen;
            }
            if (MiddleWaitValue_Tb.Text != CoinConfigurationObj.MiddleWaitTime.ToString())
            {
                MiddleWait_Lb.BackColor = Color.Pink;
            }
            else
            {
                MiddleWait_Lb.BackColor = Color.PaleGreen;
            }
            if (MaxOrderValue_Tb.Text != CoinConfigurationObj.MaximumOrderValue.ToString())
            {
                MaxOrder_Lb.BackColor = Color.Pink;
            }
            else
            {
                MaxOrder_Lb.BackColor = Color.PaleGreen;
            }
        }

        public void SetEnterLimitExceedColor(bool limit_exceeded)
        {
            if (limit_exceeded)
                EnterLimitValue_Tb.BackColor = Color.PaleGreen;
            else
                EnterLimitValue_Tb.BackColor = SystemColors.Control;
        }

        private void EnterLimitValue_Tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void OperationOpen_Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (!OperationOpen_Cb.Checked)
            {
                BuyOperationOpen_Cb.Enabled = false;
                SellOperationOpen_Cb.Enabled = false;
            }
            else
            {
                BuyOperationOpen_Cb.Enabled = true;
                SellOperationOpen_Cb.Enabled = true;
            }
            CoinConfigurationObj.OperationOpen = OperationOpen_Cb.Checked;

            string json = JsonConvert.SerializeObject(CoinConfigurationObj, Formatting.Indented);
            File.WriteAllText(@"C:\thekingdom\parameters\coin_parameters\" + this.CoinOperation_Gb_Uc.Text + ".txt", json);
        }

        private void BuyOperationOpen_Cb_CheckedChanged(object sender, EventArgs e)
        {
            CoinConfigurationObj.BuyOpen = BuyOperationOpen_Cb.Checked;
            string json = JsonConvert.SerializeObject(CoinConfigurationObj, Formatting.Indented);
            File.WriteAllText(@"C:\thekingdom\parameters\coin_parameters\" + this.CoinOperation_Gb_Uc.Text + ".txt", json);
        }

        private void SellOperationOpen_Cb_CheckedChanged(object sender, EventArgs e)
        {
            CoinConfigurationObj.SellOpen = SellOperationOpen_Cb.Checked;
            string json = JsonConvert.SerializeObject(CoinConfigurationObj, Formatting.Indented);
            File.WriteAllText(@"C:\thekingdom\parameters\coin_parameters\" + this.CoinOperation_Gb_Uc.Text + ".txt", json);
        }

        private void AllConfigurationButtons_Lb_Click(object sender, EventArgs e)
        {
            if ((string)((Label)sender).Tag == "1")
            {
                CoinConfigurationObj.IgnoreLimit = double.Parse(IgnoreLimitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "2")
            {
                CoinConfigurationObj.IgnorePercent = double.Parse(IgnorePercentLimitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "3")
            {
                CoinConfigurationObj.MinSpred = double.Parse(MinSpredValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "4")
            {
                CoinConfigurationObj.MaxSpred = double.Parse(MaxSpredValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "5")
            {
                CoinConfigurationObj.MaxDiffPercent = double.Parse(MaxDiffPercentValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "6")
            {
                CoinConfigurationObj.FirstWaitTime = int.Parse(FirstWaitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "7")
            {
                CoinConfigurationObj.LastWaitTime = int.Parse(LastWaitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "8")
            {
                CoinConfigurationObj.MinimumOrderValue = int.Parse(MinOrderValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "9")
            {
                CoinConfigurationObj.EnterLimit = double.Parse(EnterLimitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "10")
            {
                CoinConfigurationObj.EnterMultiplier = double.Parse(EnterMultiplierValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "11")
            {
                CoinConfigurationObj.ExitMultiplier = double.Parse(ExitMultiplierValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "12")
            {
                CoinConfigurationObj.SitToTopPercent = int.Parse(SitToTopPercentValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "13")
            {
                CoinConfigurationObj.SiteWeight = double.Parse(SiteWeightValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "14")
            {
                CoinConfigurationObj.MiddleWaitTime = int.Parse(MiddleWaitValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "15")
            {
                CoinConfigurationObj.TryDecimalPoint = int.Parse(TryDecimalPointValue_Tb.Text);
            }
            if ((string)((Label)sender).Tag == "16")
            {
                CoinConfigurationObj.MaximumOrderValue = int.Parse(MaxOrderValue_Tb.Text);
            }

            string json = JsonConvert.SerializeObject(CoinConfigurationObj, Formatting.Indented);
            File.WriteAllText(@"C:\thekingdom\parameters\coin_parameters\" + this.CoinOperation_Gb_Uc.Text + ".txt", json);
        }
    }
}
