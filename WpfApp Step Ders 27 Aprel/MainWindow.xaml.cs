using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

namespace WpfApp_Step_Ders_27_Aprel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string Key { get; set; } = "JfsZRDltXOsusWxYblw0~cTBJozBtWcHtRsa5W2vDjg~AoaabfvaywN4L5Eln_Pv1M72JOAKY9lCu2xWPvz6UQzamZ7lFKd-sHLJmllipEKt";
        public ApplicationIdCredentialsProvider Provider { get; set; }

        private string _location;
        public string Location { get { return _location; } set { _location = value; OnPropertyChanged(); } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Provider = new ApplicationIdCredentialsProvider(Key);

            GetBuses();

            Timer.Interval = 1;

            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Dispatcher.Invoke(() => { LblTime.Content = DateTime.Now.ToLongTimeString(); });
        }

        public Timer Timer { get; set; } = new Timer();

        public void GetBuses()
        {
            HttpClient client = new HttpClient();
            string link = @"https://www.bakubus.az/az/ajax/apiNew1";

            dynamic busses = JsonConvert.DeserializeObject(client.GetAsync(link).Result.Content.ReadAsStringAsync().Result);

                            

            foreach (var bus in busses.BUS)
            {
                dynamic data = bus["@attributes"];
                string name = data["DRIVER_NAME"];


                Location = $"{data["LATITUDE"]}, {data["LONGITUDE"]}";                
                break;
            }


        }

    }
}