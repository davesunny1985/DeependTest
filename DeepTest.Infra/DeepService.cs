using DeepTest.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DeepTest.Infra
{
    public class DeepService
    {

        public JsonContainer LoadJson()
        {
            using (StreamReader r = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"\JsonFiles" + @"\data_small.json"))
            {
                string json = r.ReadToEnd();
                JsonContainer items = JsonConvert.DeserializeObject<JsonContainer>(json);
                return items;
            }
        }

        public JsonContainer LoadFullJson()
        {
            using (StreamReader r = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"\JsonFiles" + @"\data_large.json"))
            {
                string json = r.ReadToEnd();
                JsonContainer items = JsonConvert.DeserializeObject<JsonContainer>(json);
                return items;
            }
        }
    }
}
