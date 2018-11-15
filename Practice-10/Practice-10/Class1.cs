using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_10
{
    public class Rootobject
    {
        public Restresponse RestResponse { get; set; }
    }

    public class Restresponse
    {
        public string[] messages { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        [JsonProperty("alpha2_code")]
        public string Alpha2Code { get; set; }
        public string alpha3_code { get; set; }
    }

    class Class1
    {
    }
}
