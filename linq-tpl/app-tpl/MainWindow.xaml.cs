using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using data_lib;

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
