using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownload
{
    class MangaFox: GetPageInfo
    {
        List<string> mangaName;
        List<string> mangaUrl;
        List<SingleManaFox> mangaList;
        public List<SingleManaFox> testOnly;
        private string url1 = "http://mangafox.me/manga/";
        public MangaFox()
        {
            mangaName = new List<string>();
            mangaUrl = new List<string>();
            testOnly = new List<SingleManaFox>();
            mangaList = new List<SingleManaFox>();
            string al = getMangaList(url1);
            testOnly =getLinks(al);
        }

        public void getMangaNameUrl()
        {

            
            
            
        }

        private List<SingleManaFox> getLinks(string s)
        {
            
            char[] dataArray = s.ToCharArray();
            StringBuilder name = new StringBuilder();
            StringBuilder url = new StringBuilder();
            bool scrapName = false;
            bool scrapUrl = false;
            bool scrapNameStart = false;
            bool scrapUrlStart = false;
            SingleManaFox smf;
            for (int i = 0; i < dataArray.Length; i++)
            {
                if ( dataArray[i] == '<' && dataArray[i +1] == 'l' && dataArray[i + 2] == 'i' ){

                    i += 5;
                    scrapUrl = true;
                }


                if (dataArray[i] == '"' && dataArray[i + 1] == '>')
                {
                    i += 2;
                    scrapName = true;
                }

                if (scrapName && dataArray[i] == '<' && dataArray[i + 1] == '/' && dataArray[i + 2] == 'a')
                {
                    scrapName = false;
                    i += 4;
                    //System.Windows.MessageBox.Show(url.ToString());
                    smf = new SingleManaFox();
                    smf.url = name.ToString();
                    smf.name = url.ToString();
                    mangaList.Add(smf);
                    url.Clear();
                    name.Clear();

                }

                if (scrapName)
                {
                    url.Append(dataArray[i]);
                    
                }
                if (scrapUrl)
                {
                    if (dataArray[i] == 'h' && dataArray[i + 1] == 'r' && dataArray[i + 2] == 'e' && dataArray[i + 3] == 'f' && dataArray[i + 4] == '=' && dataArray[i + 5] == '"')
                    {
                        i += 6;
                        scrapUrlStart = true;
                    }
                }

                
                    if (dataArray[i] == '"' && dataArray[i + 1] == ' ' && scrapUrlStart == true)
                    {
                        scrapUrlStart = false;
                        scrapUrl = false;
                        
                        //mangaUrl.Add(name.ToString());
                        //System.Windows.MessageBox.Show(name.ToString());
                        //name.Clear();
                    }
                

                if (scrapUrl && scrapUrlStart)
                {
                    name.Append(dataArray[i]);
                }
            }
                return mangaList;
        }

        private string getMangaList(string s)
        {
            string pageData = getPageContetn(s);
            char[] dataArray = pageData.ToCharArray();
            StringBuilder name = new StringBuilder();
            bool mangacon = false;
            for (int i = 0; i < dataArray.Length; i++)
            {
                if (dataArray[i] == 'm' && dataArray[i +1] == 'a' && dataArray[i + 2] == 'n' && dataArray[i + 3] == 'g' && dataArray[i +4] == 'a' && dataArray[i + 5] == '_'){
                    mangacon = true;
                    i += 10;
                }

                if (dataArray[i] == 'c' && dataArray[i + 1] == 'l' && dataArray[i + 2] == 'e' && dataArray[i + 3] == 'a' && dataArray[i + 4] == 'r' && dataArray[i + 5] == ' ' && dataArray[i + 6] == 'g')
                {
                    mangacon = false;
                    name.Append('?');
                    
                }
                if (mangacon)
                {
                    name.Append(dataArray[i]);
                }
            }
                return name.ToString();
        }

    }
}
