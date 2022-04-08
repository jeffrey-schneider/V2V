using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Point
    {
        private string descr;
        private double x;
        private double y;

        public Point()
        {
        }

        public Point(double longitude, double latitude)
        {
            X = longitude;
            Y = latitude;
        }

        public Point(string descr, double longitude, double latitude)
        {
            this.descr = descr;
            X = longitude;
            Y = latitude;
        }

        public double X { get => x; set => x = value; }
        public double Longitude { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Latitude { get => y; set => y = value; }
        public string Description { get => descr; set => descr = value; }

        public override string ToString()
        {
            return base.ToString() + Description +    " Latitude: " + Latitude + " Longitude "+ Longitude;
        }
    }
}
