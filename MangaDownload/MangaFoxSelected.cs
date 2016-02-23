using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownload
{
    class MangaFoxSelected: GetPageInfo
    {
        string url;
        List<SingleManaFox> smf;
        public MangaFoxSelected(string s)
        {
            this.url = s;
            smf = new List<SingleManaFox>();
        }


        public List<SingleManaFox> getMangaChapters()
        {
            string body = getPageContetn(url);
            string body1 = splitSummary(body);
            char[] getManaList = body1.ToCharArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbName = new StringBuilder();
            bool mName = false;
            bool mUrl = false;
            bool mStartUrl = false;
            bool mNameStart = false;
            for (int i = 0; i < getManaList.Length; i++)
            {
                if (getManaList[i] == 'm' && getManaList[i + 1] == 'i' && getManaList[i + 2] == 'n' && getManaList[i + 3] == 'i' && getManaList[i + 4] == 'e' && getManaList[i + 5] == 'd' && getManaList[i + 6] == 'i' && getManaList[i + 7] == 't')
                {
                    i += 10;
                    mUrl = true;
                }

                if (mUrl)
                {
                    if (getManaList[i] == 'h' && getManaList[i + 1] == 'r' && getManaList[i + 2] == 'e' && getManaList[i + 3] == 'f' && getManaList[i + 4] == '=')
                    {
                        i += 6;
                        mStartUrl = true;
                    }
                }

                if (mStartUrl && getManaList[i] == '"' && getManaList[i + 1] == ' ')
                {
                    mUrl = false;
                    mStartUrl = false;
                    mName = true;
                }

                if (mStartUrl)
                {
                    sb.Append(getManaList[i]);
                }

                if (mName && getManaList[i] == '"' && getManaList[i +1] == '>')
                {
                    mNameStart = true;
                    i += 2;
                }

                if (mNameStart && getManaList[i] == '<' && getManaList[i + 1] == '/' && getManaList[i + 2] == 'a')
                {
                    mNameStart = false;
                    mName = false;
                    SingleManaFox ssmf = new SingleManaFox();
                    ssmf.name = sbName.ToString();
                    ssmf.url = sb.ToString();
                    sbName.Clear();
                    sb.Clear();
                    smf.Add(ssmf);
                }

                if (mNameStart)
                {
                    sbName.Append(getManaList[i]);
                }
            }

                return smf;
        }


        private string splitSummary(string sa){
            char[] summary = sa.ToCharArray();
            StringBuilder sb = new StringBuilder();
            bool scrap = false;
            for (int i = 0; i < summary.Length; i++)
            {
                if (summary[i] == '=' && summary[i + 1] == '"' && summary[i + 2] == 's' && summary[i + 3] == 'u' && summary[i + 4] == 'm' && summary[i + 5] == 'm')
                {
                    i += 10;
                    scrap = true;
                }

                if (summary[i] == 'i' && summary[i + 1] == 'd' && summary[i + 2] == '=' && summary[i + 3] == 'd' && summary[i + 4] == 'i' && summary[i + 5] == 's')
                {
                    scrap = false;
                    break;
                }

                if (scrap)
                {
                    sb.Append(summary[i]);
                }
            }


                return sb.ToString();
        }



    }
}
