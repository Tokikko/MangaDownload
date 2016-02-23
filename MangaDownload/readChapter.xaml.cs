using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MangaDownload
{
    /// <summary>
    /// Interaction logic for readChapter.xaml
    /// </summary>
    public partial class readChapter : Window
    {
        private int currentPage = 0;
        private int maxPage;
        private List<string> pagesToDisplay;
        public readChapter()
        {
            InitializeComponent();
        }


        public readChapter(List<string> chapterpages)
        {
            InitializeComponent();
            pagesToDisplay = new List<string>();
            pagesToDisplay = chapterpages;
            maxPage = pagesToDisplay.Count - 1;
            txtBoxCurrentPage.Text = currentPage.ToString();
            txtBoxNumberOfPages.Text = maxPage.ToString() ;
           // byte[] array = new byte[4096];
            displayFirstPage(pagesToDisplay[currentPage]);
            //loadImage(array);
            //imgChapter.Source = loadImage(array);
        }



        private void displayFirstPage(string s)
        {
            HttpWebRequest reques = (HttpWebRequest)WebRequest.Create(s);
            HttpWebResponse response = (HttpWebResponse)reques.GetResponse();
            byte[] buffer = new byte[4096];
            if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect))
                {
                    imgChapter.Source = new BitmapImage(new Uri(s));
               
                   /* using (Stream inputStream = response.GetResponseStream())
                    {
                       
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            
                        } while (bytesRead != 0);

                        //imgChapter.Source = BitmapFrame.Create(inputStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                    }*/
                }

            else
            {
                MessageBox.Show("Couldn't find the requested page");
            }
                
            response.Close();
            reques.Abort();
            //return buffer;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < maxPage - 1)
            {
                currentPage++;
                txtBoxCurrentPage.Text = currentPage.ToString();
                
                displayFirstPage(pagesToDisplay[currentPage]);
            }
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                txtBoxCurrentPage.Text = currentPage.ToString();
                
                displayFirstPage(pagesToDisplay[currentPage]);
            }
        }

        private void txtBoxCurrentPage_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                int userEnterPage = Int32.Parse(txtBoxCurrentPage.Text);
                if (currentPage != userEnterPage)
                {
                    currentPage = userEnterPage;
                    txtBoxCurrentPage.Text = userEnterPage.ToString();
                    displayFirstPage(pagesToDisplay[userEnterPage]);
                }
            }

            catch
            {
                MessageBox.Show("Only numbers allowed");
            }
        }


        /*private void displayFirstPage(){
             HttpWebRequest reques = (HttpWebRequest)WebRequest.Create(s);
             HttpWebResponse response = (HttpWebResponse)reques.GetResponse();
             if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect))
             {
                 imgChapter.U
             }

        }*/
        /*private static BitmapImage loadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;

            return null;
        }*/
    }
}
