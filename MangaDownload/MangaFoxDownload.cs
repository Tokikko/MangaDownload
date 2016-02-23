using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownload
{
    class MangaFoxDownload: GetPageInfo
    {
        public SingleManaFox smf;
        public string nextUrl = "";
        private string lastDownloadedUrl = "";
        private string saveFolderPath = "";
        List<string> mangaReadUrls;
        int x = 1;
        public MangaFoxDownload(SingleManaFox smf, string path)
        {
            this.smf = smf;
            //string test = fixUrl(smf.url);
            this.saveFolderPath = path;
            
            //nextUrl = link;
            bool loop = true;
            
            try
            {
                while (loop)
                {
                    string editUrl = fixUrl(smf.url);
                    string editedUrl = editUrl + x + ".html";
                    string body = getPageContetn(editedUrl);
                    string link = getImageLink(body);
                    bool testThis = testSaveImage(link);
                   // break;
                    x++;

                    if (!testThis)
                        break;
                }
            }

            catch
            {
                System.Windows.MessageBox.Show("End:");
            }
        }


        public MangaFoxDownload(SingleManaFox smf)
        {
            this.smf = smf;
            //string test = fixUrl(smf.url);
            

            //nextUrl = link;
            bool loop = true;
            mangaReadUrls = new List<string>();
            try
            {
                while (loop)
                {
                    string editUrl = fixUrl(smf.url);
                    string editedUrl = editUrl + x + ".html";
                    string body = getPageContetn(editedUrl);
                    string link = getImageLink(body);
                    bool testThis = linkFoundForReading(link);
                    
                    x++;

                    if (!testThis)
                        break;
                }

                
            }

            catch
            {
                System.Windows.MessageBox.Show("End");
            }
        }


        public string getImageLink(string s){
            char[] array = s.ToCharArray();
            StringBuilder sb = new StringBuilder();
            bool found = false;
            bool foundStart = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 'i' && array[i + 1] == 'd' && array[i + 2] == '=' && array[i + 3] == '"' && array[i + 4] == 'v' && array[i + 5] == 'i' && array[i + 6] == 'e' && array[i + 7] == 'w')
                {
                    i += 12;
                    found = true;
                }

                if (found && array[i] == 's' && array[i + 1] == 'r' && array[i + 2] == 'c' && array[i + 3] == '=' && array[i + 4] == '"')
                {
                    i += 5;
                    foundStart = true;
                }

                if (foundStart && found && array[i] == '"')
                {
                    found = false;
                    foundStart = false;
                }

                if (foundStart && found) 
                {
                    sb.Append(array[i]);
                }
            }
            
            return sb.ToString() ;

        }


        private string fixUrl(string a)
        {
            int aa = a.LastIndexOf('/');
            string ax = a.Substring(0, aa + 1);
            //System.Windows.MessageBox.Show(ax);
            return ax;
        }


        private bool testSaveImage(string link)
        {
            if (lastDownloadedUrl != link)
            {
                HttpWebRequest reques = (HttpWebRequest)WebRequest.Create(link);
                HttpWebResponse response = (HttpWebResponse)reques.GetResponse();
                if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect))
                {
                    Directory.CreateDirectory(saveFolderPath + "\\" + smf.name);
                    using (Stream inputStream = response.GetResponseStream())
                    using (Stream outputStream = File.OpenWrite(saveFolderPath +"\\" +smf.name + "\\" + smf.name + x + ".jpg"))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }
                lastDownloadedUrl = link;
                response.Close();
                reques.Abort();
                return true;
            }

            else
            {
                return false;
            }
        }


        private bool linkFoundForReading(string link)
        {
            if (lastDownloadedUrl != link)
            {
                HttpWebRequest reques = (HttpWebRequest)WebRequest.Create(link);
                HttpWebResponse response = (HttpWebResponse)reques.GetResponse();
                if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect))
                {
                    mangaReadUrls.Add(link);
                    lastDownloadedUrl = link;
                    response.Close();
                    reques.Abort();
                    return true;
                }


            }

            return false;

        }

        public List<String> readManga()
        {

            return mangaReadUrls;
        }
    }
}
