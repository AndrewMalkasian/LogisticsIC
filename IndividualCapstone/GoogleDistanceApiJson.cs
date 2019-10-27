using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividualCapstone
{
    public class GoogleDistanceApi
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string[] origin_addresses { get; set; }
            public string[] destination_addresses { get; set; }
            public Row[] rows { get; set; }
        }

        public class Row
        {
            public Element[] elements { get; set; }
        }

        public class Element
        {
            public string status { get; set; }
            public Duration duration { get; set; }
            public Distance distance { get; set; }
        }

        public class Duration
        {
            public int value { get; set; }
            public string text { get; set; }
        }

        public class Distance
        {
            public int value { get; set; }
            public string text { get; set; }
        }

    }
}