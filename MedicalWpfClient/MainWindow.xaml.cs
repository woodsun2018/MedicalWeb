using ShareLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalWpfClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Timer createDataTimer = new Timer(1000);
        private CreateDemoBioValues createDemoBioValues = new CreateDemoBioValues();
        private MedicalWebClient medicalWebClient = new MedicalWebClient();
        private bool isSampleData = false;
        private BioValues bioValuesVM = new BioValues();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            createDataTimer.AutoReset = true;
            createDataTimer.Elapsed += CreateDataTimer_Elapsed;

            labHost.Content = $"服务器: {medicalWebClient.Host}";

            btnStartSample.Click += BtnStartSample_Click;

            //绑定生理参数VM
            boxBioValues.DataContext = bioValuesVM;
        }

        private void CreateDataTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BioValues bioValues = createDemoBioValues.GetNewValue();

            this.Dispatcher.Invoke(() =>
            {
                //绑定生理参数VM
                boxBioValues.DataContext = bioValues;
            });

            medicalWebClient.PostBioValuesAsync(bioValues);
        }

        private void BtnStartSample_Click(object sender, RoutedEventArgs e)
        {
            if (!isSampleData)
            {
                //如果当前没有在采样生理参数，启动定时器，开始采样生理参数
                createDataTimer.Start();

                btnStartSample.Content = "停止采样";

                isSampleData = true;
            }
            else
            {
                //如果当前正在采样生理参数，暂停定时器，暂停采样生理参数
                createDataTimer.Stop();

                btnStartSample.Content = "开始采样";

                isSampleData = false;
            }
        }


    }
}
