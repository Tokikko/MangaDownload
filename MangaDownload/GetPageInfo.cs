using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownload
{
     abstract class GetPageInfo
    {

         
        protected virtual   string getPageContetn(string s)
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(s);
            webRequest.UserAgent = "Test";
            WebResponse response = webRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string responseText = sr.ReadToEnd();
            string splitedBody = splitBody(responseText);
            return splitedBody;
        }

        protected string splitBody(string s)
        {
            int index = s.IndexOf("<body") + 5;
            string body = s.Substring(index, s.IndexOf("</body>") - index);
            return body;


        }
    }
}
