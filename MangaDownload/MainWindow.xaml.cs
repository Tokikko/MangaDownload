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

namespace MangaDownload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<SingleManaFox> mangaUrls;
        public MainWindow()
        {
            InitializeComponent();
            mangaUrls = new List<SingleManaFox>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MangaFox mf = new MangaFox();
            mangaUrls = mf.testOnly;
            foreach (SingleManaFox s in mangaUrls)
                listBoxMangaTest.Items.Add(s);

        }

        private void listBoxMangaTest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SingleManaFox sm = new SingleManaFox();
            sm = (SingleManaFox) listBoxMangaTest.SelectedItem;
            var mangaWindow = new SelectedManga(sm);
            mangaWindow.Show();
            
        }
    }
}
