using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileSort.Domain.DataSorter;
using FileSort.Domain.FileGeneration;

namespace FileSort
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

        private async void GenerateFileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataSorter = new DataSorter() as IDataSorter;
            //var files = await dataSorter.CreateBatchFilesAsync();
            //await dataSorter.CreateSortedFile(files);
            await dataSorter.CreateFileWithSortedData();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            var dataSorter = new DataSorter() as IDataSorter;
            await dataSorter.CreateTempChunks();
            await dataSorter.CreateFileWithSortedData();
            watch.Stop();
            var time = watch.Elapsed;
        }
    }
}