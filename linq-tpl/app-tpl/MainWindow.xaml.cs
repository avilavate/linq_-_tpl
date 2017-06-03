using System.Threading.Tasks;
using System.Windows;

namespace app_tpl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}
