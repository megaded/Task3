using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
   public class SettingService
    {
        private const string url = "http://mighttrust.me/testcase/api/list/getitems?searchfield=";
        public string GetSettings(string requestParams)
        {
            StringBuilder stringUrl = new StringBuilder(url + requestParams);
            Uri urlRequest = new Uri(stringUrl.ToString());
            var request = (HttpWebRequest)WebRequest.Create(urlRequest);
            var responce = request.GetResponse();
            using (StreamReader reader = new StreamReader(responce.GetResponseStream()))
            {
              var  json = reader.ReadToEnd();
                responce.Close();
                return json;
            }
        }
       
    }
}
