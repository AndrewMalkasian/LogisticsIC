using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace IndividualCapstone
{
    public class GoogleMapsGeoCodingApiJson
    {
        
        public class RootObject
        {

            public Result[] results { get; set; }
            public string status { get; set; }
        }
        //public static RootObject GetLatLongByAddress(string address)
        //{
        //    RootObject root = new RootObject();

        //    string url = string.Format( "http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true_or_false", address);
        //    var req = (HttpWebRequest)WebRequest.Create(url);

        //    var res = (HttpWebResponse)req.GetResponse();

        //    using (var streamreader = new StreamReader(res.GetResponseStream()))
        //    {
        //        var result = streamreader.ReadToEnd();

        //        if (!string.IsNullOrWhiteSpace(result))
        //        {
        //            root = JsonConvert.DeserializeObject<RootObject>(result);
        //        }
        //    }
        //    return root;


        //}
    public class Result
        {
            public Address_Components[] address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            //public Plus_Code plus_code { get; set; }
            public string[] types { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
            public string location_type { get; set; }
            //public Viewport viewport { get; set; }
        }

        public class Location
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Northeast
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Southwest
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Plus_Code
        {
            public string compound_code { get; set; }
            public string global_code { get; set; }
        }

        public class Address_Components
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public string[] types { get; set; }
        }
       

    }
}