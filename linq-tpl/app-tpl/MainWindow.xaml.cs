using data_lib;
using System.Threading.Tasks;
using System.Windows;

namespace app_tpl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public data_lib.linq_filters Cars { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Total.Content = 0;
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var path = this.path.Text;
            int total = 0;
            
            var parserTask = Task.Factory.StartNew(() =>
            {
                var cars = new data_lib.linq_filters(path);
                this.Cars = cars;
                total = cars.GetTotal();
            }).ContinueWith((a) =>
            {
                this.Total.Content = total;
            },context);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Total.Content = 0;
            var path = this.path.Text;
            int total = 0;
            var cars = new data_lib.linq_filters(path);
            total = cars.GetTotal();
            this.Total.Content = total;
        }

        private void Aggr_Click(object sender, RoutedEventArgs e)
        {
            FuelStatistics fuelStatistics;
            if (this.Cars != null)
            {
                fuelStatistics= this.Cars.GetFuelStatistics();
                this.Result.Text = string.Format("Calculated Performance Average:\n {0}, Min: {1}, Max: {2} :",
                    fuelStatistics.Avg, fuelStatistics.Min, fuelStatistics.Max);

            }
        }
    }
}
