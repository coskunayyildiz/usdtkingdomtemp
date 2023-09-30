using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V113.Debugger;
using static System.Net.Mime.MediaTypeNames;
using static TheKingdomProject.ParibuClass;

namespace TheKingdomProject
{
    public partial class Form1 : Form
    {
        ParibuClass ParibuObject = new ParibuClass();
        Thread ParibuAnalysisThread;
        bool program_initialized = false;
        int first_coin_operation_index = 0;
        Rectangle Balances_Gb_OriginalRect;
        Rectangle GlobalConfiguration_Gb_OriginalRect;
        int form1_system_seconds = 0;
        TelegramBotClass TelegramBotObj = new TelegramBotClass();
        string computer_name = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        /***************************************************************************************************/
        /* Burada listeden coin isimleri ve o coinlerin paribu url'leri okunuyor. Coin isimleri "operation_coin_names" 
        /* isimli string array e atiliyor ve coin url'leri ise operation_coin_urls isimli string array e atiliyor.
        /*  Tum islemler bu coinler uzerinden yuruyor. Bir cok initialization bu array ile yapildigi icin Form1_Load 
        /* icinde ilk bu okuma islemi yapilmali.
        /* todo: Restriction olarak coin sayisi konulmalidir. 6'dan fazla olmamasi gerekir diye.
        /***************************************************************************************************/
        public static string[] operation_coin_names;
        public static string[] operation_coin_urls;

        public class Operationcoin
        {
            public string coin_name { get; set; }
            public string url { get; set; }
        }

        public class AllOperationCoins
        {
            public List<Operationcoin> operationcoins { get; set; }
        }

        public class GlobalConfigClass
        {
            public double MaxAsymmetryValue;
            public string RobotCurrency;
        }
        public static GlobalConfigClass GlobalConfigObj = new GlobalConfigClass();
        public void GetCoinNames()
        {
            string text = File.ReadAllText(@"C:\thekingdom\parameters\coins_list.txt", Encoding.UTF8);
            AllOperationCoins coin_list = JsonConvert.DeserializeObject<AllOperationCoins>(text);
            operation_coin_names = new string[coin_list.operationcoins.Count];
            operation_coin_urls = new string[coin_list.operationcoins.Count];

            for (int i = 0; i < coin_list.operationcoins.Count; i++)
            {
                operation_coin_names[i] = coin_list.operationcoins[i].coin_name;
                operation_coin_urls[i] = coin_list.operationcoins[i].url;
            }
        }

        public void GetGlobalConfiguration()
        {
            string text = File.ReadAllText(@"C:\thekingdom\parameters\global_config.txt", Encoding.UTF8);
            GlobalConfigObj = JsonConvert.DeserializeObject<GlobalConfigClass>(text);
            MaxAsymmetryValue_Tb.Text = GlobalConfigObj.MaxAsymmetryValue.ToString();
            if (GlobalConfigObj.RobotCurrency.Equals("USDT"))
            {
                RobotCurrencyTry_Rb.Checked = false;
                RobotCurrencyUsdt_Rb.Checked = true;
                ParibuClass.SetRobotCurrency("USDT");
                CoinOperationUserControl.SetRobotCurrency("USDT");
            }
            else
            {
                GlobalConfigObj.RobotCurrency = "TRY";
                RobotCurrencyTry_Rb.Checked = true;
                RobotCurrencyUsdt_Rb.Checked = false;
                ParibuClass.SetRobotCurrency("TRY");
            }
        }
        /***************************************************************************************************/
        /* Buradaki kodlar tum operation_coin_prices coinleri icin Coin ismi ve CoinPricesWithTimetagsClass 
        /* bilgisini Dictionary@de tanimlamak icin vardir. Asagidaki satirlar henuz herhangi bir datayi cekmez
        /* sadece cekilecek olan datalar icin Dictionary'yi hazir hale getir. Sonrasinda timer icerisinde 
        /* ReadPricesTableFromFile fonksiyonu periyodik olarak cagrilarak tum coinlerin fiyatlari bu Dictionary'de
        /* hazir hale getirilir.
        /***************************************************************************************************/
        internal class CoinPricesWithTimetagsClass
        {
            public string coin_name;
            public string avg_price_in_usdt;
            public string avg_price_in_try;
            public string timetag;
            public bool is_valid;
        }

        Dictionary<string, CoinPricesWithTimetagsClass> BinanceCoinPricesFromFile = new Dictionary<string, CoinPricesWithTimetagsClass>();

        void InitializeCoinPriceDictionary()
        {
            for (int i = 0; i < operation_coin_names.Length; i++)
            {
                CoinPricesWithTimetagsClass info = new CoinPricesWithTimetagsClass();
                BinanceCoinPricesFromFile.Add(operation_coin_names[i], info);
            }
        }
        /***************************************************************************************************/
        /* Yukarida Dictionary'si hazirlanmis olan coin price bilgilerini dosyadan ceken fonksiyondur. Coin 
        /* ismi ile bu fonksiyon cagrildiginda o coin in fiyati Dictionary'de guncellenir. Bu fonksiyon timer
        /* ile periyodik olarak cagrilmalidir.
        /***************************************************************************************************/
        /* testpurposes*/

        void ReadPricesTableFromFile(string coin_name)
        {
            Mutex mutex = new Mutex();
            string text = "";
            try
            {
                mutex = Mutex.OpenExisting("MyPriceFileMutex");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Coin Presenter is not working!!!");
                //return;
            }

            mutex.WaitOne();
            text = File.ReadAllText(@"C:\thekingdom\parameters\coin_prices\" + coin_name + "\\" + coin_name + ".txt", Encoding.UTF8);
            mutex.ReleaseMutex();
            CoinPricesWithTimetagsClass filesobj = JsonConvert.DeserializeObject<CoinPricesWithTimetagsClass>(text);

            if (filesobj != null)
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                string timetag = now.ToUnixTimeSeconds().ToString();
                BinanceCoinPricesFromFile[coin_name] = filesobj;

                long diff = (long)(double.Parse(timetag) - double.Parse(filesobj.timetag));

                for (int i = 0; i < operation_coin_names.Length; i++)
                {
                    if (operation_coin_names[i].Equals(coin_name))
                    {
                        if ((filesobj.is_valid == false) || (diff > 20))
                        {
                            coin_opeation_user_controls[i].SetCoinPresenterPriceTimetagColors(true);
                        }
                        else
                        {
                            coin_opeation_user_controls[i].SetCoinPresenterPriceTimetagColors(false);
                        }

                        break;
                    }
                }
            }
        }

        /***************************************************************************************************/
        /* Asagida operation_coin_names'deki tum coinler icin CoinOperationUserControl guisini olusturan kodlar
        /* bulunuyor. Bu fonksiyon main gui'de coin operation sub guisini koyarken ayni zamanda CoinOperation
        /* guisindeki configuration datayi da dosyadan okuma isini yapiyor.
        /***************************************************************************************************/
        public static CoinOperationUserControl[] coin_opeation_user_controls;
        private void CreateCoinOperationUserControl(string name, int index, int x, int y)
        {
            coin_opeation_user_controls[index] = new CoinOperationUserControl();
            coin_opeation_user_controls[index].CoinOperation_Gb_Uc.Text = name;
            this.Controls.Add(coin_opeation_user_controls[index]);
            coin_opeation_user_controls[index].Location = new Point(x, y);
        }
        void InitializeCoinOperationUserControls()
        {
            coin_opeation_user_controls = new CoinOperationUserControl[operation_coin_names.Length];

            for (int i = 0; i < operation_coin_names.Length; i++)
            {
                //CreateCoinOperationUserControl(operation_coin_names[i], i, 0 + (i % 3) * 635, 10 + (i / 3) * 433);
                CreateCoinOperationUserControl(operation_coin_names[i], i, 0 + (i % 3) * 635, 90 + (i / 3) * 455);
                coin_opeation_user_controls[i].GetConfigurationDataFromFile(operation_coin_names[i]);
                coin_opeation_user_controls[i].InitializeSizesOfElements();
            }
        }

        /***************************************************************************************************/
        /* Form1_Load sistemde cagrilan ilk fonksiyondur. Ilk is olarak GetCoinNames() fonksiyonunu cagirip
        /* operation yapilacak coinler olusturulur. Sonrasinda coin fiyatlarinin dosyadan okunup store edilecegi
        /* Dictionary olusturulur. Daha sonra bu coinler icin CoinOperation sub-guisi olusturulur.
        /* Daha sonra 
        /***************************************************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            GetCoinNames();
            GetGlobalConfiguration();
            InitializeCoinPriceDictionary();
            InitializeCoinOperationUserControls();
            Balances_Gb_OriginalRect = new Rectangle(Balances_Gb.Location.X, Balances_Gb.Location.Y, Balances_Gb.Width, Balances_Gb.Height);
            GlobalConfiguration_Gb_OriginalRect = new Rectangle(GlobalConfiguration_Gb.Location.X, GlobalConfiguration_Gb.Location.Y, GlobalConfiguration_Gb.Width, GlobalConfiguration_Gb.Height);
            computer_name = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[0];

            for (int i = 0; i < operation_coin_names.Length; i++)
            {
                ParibuObject.InitializeCoinOperationParameters(i, operation_coin_names[i], operation_coin_urls[i]);
            }

            UpdateGuiInfo_Tm.Enabled = true;

            UpdateGuiInfo_Tm.Start();

            ParibuAnalysisThread = new Thread(new ThreadStart(ParibuClass.StartCoinOperation));

            ParibuClass.OpenParibuLoginPage();

            if (ParibuClass.IsLoginPage())
            {
                ParibuClass.LoginToParibu("5325532869", "5bak+Yuz");
            }
            else
            {
                ParibuAnalysisThread.Start();
            }

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            UserControlGuiPopulatin_Tm.Start();

            program_initialized = true;

            coin_opeation_user_controls[first_coin_operation_index].SetBackgroundColorToActiveOrDeactive(true);
        }


        private void UpdateGuiInfo_Tm_Tick(object sender, EventArgs e)
        {
            form1_system_seconds++;
            total_try_values_Lb.Text = ((int)(ParibuClass.total_try_amount)).ToString();
            free_try_values_Lb.Text = ((int)(ParibuClass.free_try_amount)).ToString();
            total_usdt_values_Lb.Text = ((int)(ParibuClass.total_usdt_amount)).ToString();
            total_assets_in_try_Lb.Text = ((int)total_assets_in_try).ToString();

            string coins_infos = "\r\n";
            for (int i = 0; i < ParibuClass.CoinOperationInformations.Count; i++)
            {
                coins_infos += (CoinOperationInformations[i].coin_name + ": " + CoinOperationInformations[i].current_balance_in_try + " TL \r\n");
            }

            if (form1_system_seconds % 600 == 0) // Send message once in 10 minutes
            {
                string msg = computer_name + " message: \r\n\r\nTop. Varlik (TL): " + total_assets_in_try.ToString() + "\r\n" + "Top. TRY: " + total_try_values_Lb.Text + "\r\n" + "Free TRY: " + free_try_values_Lb.Text + "\r\n" + "Top. USDT: " + total_usdt_values_Lb.Text + "\r\n" + coins_infos;
                TelegramBotObj.SendMessage(msg);
            }
            for (int i = 0; i < operation_coin_names.Length; i++)
            {
                ReadPricesTableFromFile(operation_coin_names[i]);
                double coin_try_price = double.Parse(BinanceCoinPricesFromFile[operation_coin_names[i]].avg_price_in_try);
                double coin_usdt_price = double.Parse(BinanceCoinPricesFromFile[operation_coin_names[i]].avg_price_in_usdt);

                coin_opeation_user_controls[i].SetCoinBinancePrices(coin_try_price, coin_usdt_price);
            }

            if (MaxAsymmetryValue_Tb.Text != GlobalConfigObj.MaxAsymmetryValue.ToString())
            {
                MaxAsymmetry_Lb.BackColor = Color.Pink;
            }
            else
            {
                MaxAsymmetry_Lb.BackColor = Color.PaleGreen;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            double x_mul = (double)this.Width / 1920.0;
            double y_mul = (double)this.Height / 1080.0;
            Balances_Gb.Location = new Point((int)((double)Balances_Gb_OriginalRect.X * x_mul), (int)((double)Balances_Gb_OriginalRect.Y * y_mul));
            Balances_Gb.Width = (int)((double)Balances_Gb_OriginalRect.Width * x_mul);
            Balances_Gb.Height = (int)((double)Balances_Gb_OriginalRect.Height * y_mul);

            GlobalConfiguration_Gb.Location = new Point((int)((double)GlobalConfiguration_Gb_OriginalRect.X * x_mul), (int)((double)GlobalConfiguration_Gb_OriginalRect.Y * y_mul));
            GlobalConfiguration_Gb.Width = (int)((double)GlobalConfiguration_Gb_OriginalRect.Width * x_mul);
            GlobalConfiguration_Gb.Height = (int)((double)GlobalConfiguration_Gb_OriginalRect.Height * y_mul);

            if (operation_coin_names != null)
            {
                for (int i = 0; i < operation_coin_names.Length; i++)
                {
                    if (coin_opeation_user_controls[i] != null)
                    {
                        coin_opeation_user_controls[i].ResizeUserControlElements(this.Width, this.Height);
                    }
                }
            }
        }

        void CaptureGuiImage()
        {
            Bitmap screenshot = new Bitmap(this.Width, this.Height);
            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                // Capture the form's contents into the bitmap
                graphics.CopyFromScreen(this.Location, Point.Empty, this.Size);
            }
            string filePath = "screenshot.jpg";
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        private void UserControlGuiPopulatin_Tm_Tick(object sender, EventArgs e)
        {
            if (ParibuClass.CurrentAnalyzedCoinIndex != first_coin_operation_index)
            {
                coin_opeation_user_controls[first_coin_operation_index].SetBackgroundColorToActiveOrDeactive(false);
                coin_opeation_user_controls[ParibuClass.CurrentAnalyzedCoinIndex].SetBackgroundColorToActiveOrDeactive(true);
                first_coin_operation_index = ParibuClass.CurrentAnalyzedCoinIndex;
            }

            if (CoinOperationUserControl.required_parameters_arrived)
            {
                CoinOperationUserControl.required_parameters_arrived = false;
                coin_opeation_user_controls[first_coin_operation_index].SetCurrencyAmounts(ParibuClass.total_try_amount, ParibuClass.free_try_amount, ParibuClass.total_usdt_amount, ParibuClass.free_usdt_amount);
                coin_opeation_user_controls[ParibuClass.CurrentAnalyzedCoinIndex].PopulateAllIntermediateData();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParibuClass.operation_exit_has_been_requested = true;
            ParibuAnalysisThread.Join();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            CaptureGuiImage();
        }

        private void RobotStatusOn_Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (RobotStatusOn_Rb.Checked)
                ParibuClass.robot_status_on = true;
        }

        private void RobotStatusOff_Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (RobotStatusOff_Rb.Checked)
                ParibuClass.robot_status_on = false;
        }

        private void MaxAsymmetry_Lb_Click(object sender, EventArgs e)
        {
            GlobalConfigObj.MaxAsymmetryValue = double.Parse(MaxAsymmetryValue_Tb.Text);
            string json = JsonConvert.SerializeObject(GlobalConfigObj, Formatting.Indented);
            File.WriteAllText(@"C:\thekingdom\parameters\global_config.txt", json);
        }
    }
}