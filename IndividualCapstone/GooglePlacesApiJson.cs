using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividualCapstone
{
    public class GooglePlacesApiJson
    {

        public class Rootobject
        {
            public object[] html_attributions { get; set; }
            public Result[] results { get; set; }
            public string status { get; set; }
        }

        public class Result
        {
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string icon { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string place_id { get; set; }
            public string reference { get; set; }
            public string[] types { get; set; }
            public Plus_Code plus_code { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
            public Viewport viewport { get; set; }
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

    }
}