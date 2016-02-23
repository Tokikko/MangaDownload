using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SelectedManga.xaml
    /// </summary>
    /// 
    
    public partial class SelectedManga : Window
    {
        private string folderPath = "";
        SingleManaFox smf;
        List<SingleManaFox> listSmf;
        
        int testCounter = 0;
        public SelectedManga()
        {
            InitializeComponent();
        }

        public SelectedManga(SingleManaFox smf)
        {
            InitializeComponent();
            this.smf = smf;
            
            listSmf = new List<SingleManaFox>();
            MangaFoxSelected mfs = new MangaFoxSelected(smf.url);
            listSmf = mfs.getMangaChapters();
            foreach (SingleManaFox ss in listSmf)
            {
                listBoxMangaChapters.Items.Add(ss);
            }
            //MessageBox.Show(smf.name);
        }

        private void listBoxMangaChapters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SingleManaFox readsmf = new SingleManaFox();
            readsmf = (SingleManaFox)listBoxMangaChapters.SelectedItem;
            //MessageBox.Show(readsmf.name);
            List<string> mangaReadUrl = new List<string>();
            MangaFoxDownload mfdRead = new MangaFoxDownload(readsmf);
            mangaReadUrl = mfdRead.readManga();
            readChapter rc = new readChapter(mangaReadUrl);
            rc.Show();
        }

        private void btnFolderPath_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = dlg.SelectedPath;
                MessageBox.Show(folderPath);
                
            }

        }

        private void btnSelectChapter_Click(object sender, RoutedEventArgs e)
        {
            //ProgressBar pg = new ProgressBar();
            //Queue<string> downloadLinks = new Queue<string>();
            //BackgroundWorker bg = new BackgroundWorker();
            if (folderPath != "")
                
            {
                progressBarDownload.Value = 0;
                progressBarDownload.Maximum = listBoxMangaChapters.Items.Count - 1;
                Thread thread = new Thread(new ThreadStart(downloadChapterOnebyOne));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                
                //thread.Join();
                
                
               
            }
            else
            {
                MessageBox.Show("Select save folder first");
            }
        }

        delegate void ProgressDelegate(int Progress);
        private void UpdateProgress(int Progress)
        {
            if (progressBarDownload.Dispatcher.CheckAccess())
            {
                progressBarDownload.Value = Progress;
            }
            else
            {
                ProgressDelegate progressD = new ProgressDelegate(UpdateProgress);
                progressBarDownload.Dispatcher.Invoke(progressD, new object[] { Progress });
            }
        }

        private void downloadChapterOnebyOne()
        {
            
         
            int numOfChapters = listBoxMangaChapters.Items.Count - 1;
           
            int xyy = 0;
            for (int i = 0; i <= numOfChapters; i++)
            {
                UpdateProgress(xyy);
                xyy++;
                SingleManaFox newSmf = new SingleManaFox();
                newSmf = (SingleManaFox)listBoxMangaChapters.Items.GetItemAt(i);
                MangaFoxDownload mfd = new MangaFoxDownload(newSmf, folderPath);
                countUp();

            }

            MessageBox.Show("Download completed");
            
        }


        private void downloadChapterOnebyOne(int i1, int i2)
        {
            int count = i2 - i1;
          
            int numOfChapters = listBoxMangaChapters.Items.Count - 1;
            int xyy = 0;

            for (int i = i1; i <= i2; i++)
            {
                
                
                UpdateProgress(xyy);
                xyy += 1;
                SingleManaFox newSmf = new SingleManaFox();
                newSmf = (SingleManaFox)listBoxMangaChapters.Items.GetItemAt(i);
                MangaFoxDownload mfd = new MangaFoxDownload(newSmf, folderPath);
                countUp();

            }

            MessageBox.Show("Download completed");

        }

        private void countUp()
        {
            testCounter++;
            //MessageBox.Show(testCounter.ToString());
        }

        private void btnDownloadSelected_Click(object sender, RoutedEventArgs e)
        {
         
            int cFrom, cTo;
            
            if (folderPath != "")
            {
                try
                {
                    cFrom = Int32.Parse(txtBoxChapterFrom.Text) - 1;
                    cTo = Int32.Parse(txtBoxChapterTo.Text) - 1;
                    progressBarDownload.Value = 0;
                    progressBarDownload.Maximum = cTo - cFrom + 1;
                    Thread thread = new Thread(() => downloadChapterOnebyOne(cFrom, cTo));
                    //downloadChapterOnebyOne(cFrom, cTo);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    
                }

                catch
                {
                    MessageBox.Show("Only number allowed");
                }
            }

            else
            {
                MessageBox.Show("Select save folder first!");
            }
        }


       
    }
}
