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
            public string Status { get; set; }
            public string[] Origin_addresses { get; set; }
            public string[] Destination_addresses { get; set; }
            public Row[] Rows { get; set; }
        }

        public class Row
        {
            public Element[] Elements { get; set; }
        }

        public class Element
        {
            public string status { get; set; }
            public Duration Duration { get; set; }
            public Distance Distance { get; set; }
        }

        public class Duration
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public class Distance
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

    }

    public class Rootobject
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

}