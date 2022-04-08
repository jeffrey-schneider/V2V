using System;
using System.Collections.Generic;
using System.Windows;

namespace ConsoleApp1
{
    class Program
    {

        //const double R = 6378100;  //Meters
        const double R = 6371000; //Meters

        static List<Point> northBoundCheckpoints = new List<Point>();
        static List<Point> southBoundCheckpoints = new List<Point>();

        static void Main(string[] args)
        {
            /** TODO:  
             * 1) Make the following a method.
             * 2) Determine what is the entrance and exit for the vehicles
             * 3) Change speed
             * 4) North to South or South to North. Determine from the entrance and exits
             **/

            //This program is Northbound only.
            northBoundCheckpoints = LoadTheList();
            southBoundCheckpoints = LoadTheList();
            southBoundCheckpoints.Reverse();

            double bearing;
            double timeToNext = 0.0;
            double distanceTo = 0.0;           

            int step = 0;
            Point starting = northBoundCheckpoints[0];
            Point exit = northBoundCheckpoints[northBoundCheckpoints.Count-1];
            Point nextStep = northBoundCheckpoints[step + 1];
            Point vehicleMovedTo;
            double timeUnit = 1; //seconds
            double velocity = 96.56 * 1000 /60 /60 ;  // meters per second  //60 mph = 96.56 kph
            double stepDistance = 0.0;  // how far apart are the steps in meters

            starting = northBoundCheckpoints[step];
            nextStep = northBoundCheckpoints[step + 1];

            stepDistance = distanceOff(starting, nextStep); //in kilometers

            vehicleMovedTo = starting;
            bearing = bearingTo(starting, nextStep);
            distanceTo = distanceOff(starting, nextStep);
            
            for (int i = 0; i < northBoundCheckpoints.Count - 1; i++)
            {
                int counter = 0;
                distanceTo = 0.0;
                starting = northBoundCheckpoints[i];
                nextStep = northBoundCheckpoints[i + 1];

                bearing = bearingTo(starting, nextStep);
                stepDistance = distanceOff(starting, nextStep);
                timeToNext = stepDistance / velocity;

                Console.WriteLine("Step {0} Distance is {1}", i, stepDistance);
                Console.WriteLine("Seconds from {0} to {1} = {2} ", starting.Description, nextStep.Description, timeToNext);
                do
                    //The actual movement of the vehicle is resolved here.
                {
                    vehicleMovedTo = newLocation(vehicleMovedTo, bearing, velocity, timeUnit);
                    distanceTo = distanceOff(vehicleMovedTo, nextStep);
                    
                    Console.WriteLine("{0} Lon {1} Lat {2} DistanceTo: {3}", counter, vehicleMovedTo.Longitude, vehicleMovedTo.Latitude, distanceTo);
                    counter++;

                } while (counter < Math.Truncate(timeToNext));

                vehicleMovedTo = nextStep;

                Console.WriteLine();
                
            }            
            
            Console.ReadKey();
        }


        private static List<Point> LoadTheList()
        {
            List<Point> returnList = new List<Point>();
            returnList.Add(new Point("Southern Terminus", -85.179941030649, 35.029561388082));
            returnList.Add(new Point("US64", -85.1824657881087, 35.0323112238958));
            returnList.Add(new Point("Shepherd Road", -85.1863367738004, 35.0424448090027));
            returnList.Add(new Point("Shallowford Road", -85.190834978176, 35.0542741363785));
            returnList.Add(new Point("Jersey Pike", -85.1950040605971, 35.0652356445995));
            returnList.Add(new Point("Railroad", -85.1966313156479, 35.0694505789362));
            returnList.Add(new Point("Bonny Oaks South", -85.1991274092318, 35.0751506799942));
            returnList.Add(new Point("US58", -85.2065585304881, 35.0824070017701));
            returnList.Add(new Point("Amnicola", -85.2186535312278, 35.0901685033162));
            returnList.Add(new Point("Bridge Southside", -85.2253756280281, 35.0948722198887));
            returnList.Add(new Point("Dam Southern Terminus", -85.2290383667825, 35.1023748770635));
            returnList.Add(new Point("Dam Northern", -85.2292106266786, 35.1064924043414));
            returnList.Add(new Point("Access Road", -85.2295083983012, 35.1118347249231));
            returnList.Add(new Point("North Railroad", -85.2340222535588, 35.1175001460281));
            returnList.Add(new Point("DuPont Pkwy", -85.238046984725, 35.1212201712875));
            returnList.Add(new Point("Hixson Pike", -85.2445573615181, 35.1311145657253));
            returnList.Add(new Point("Gadd Road", -85.2487435693678, 35.1418486532827));
            returnList.Add(new Point("Towne Hills", -85.2492085167847, 35.1488813417943));
            returnList.Add(new Point("Grubb Road", -85.248513824301, 35.1581845762589));
            returnList.Add(new Point("Dayton Blvd", -85.2467790433745, 35.179335749694));
            returnList.Add(new Point("Northern Terminus", -85.246276499545, 35.1881679452401));
            return returnList;
        }

        
        private static List<Point> LoadTheListOrig()
        {
            List<Point> returnList = new List<Point>();
            returnList.Add(new Point("Southern Terminus", -85.18031, 35.02987));
            returnList.Add(new Point("US64", -85.18249, 35.03226));
            returnList.Add(new Point("Shepherd Road", -85.18635, 35.0424));
            returnList.Add(new Point("Shallowford Road", -85.19087, 35.05427));
            returnList.Add(new Point("Jersey Pike", -85.19503, 35.06532));
            returnList.Add(new Point("Bonny Oaks South", -85.19915, 35.07518));
            returnList.Add(new Point("Bonny Oaks North", -85.20668, 35.08253));
            returnList.Add(new Point("Amnicola Highway", -85.2186, 35.09026));
            returnList.Add(new Point("South Dam", -85.22904, 35.1026));
            returnList.Add(new Point("North Dam", -85.22913, 35.10622));
            returnList.Add(new Point("Access Road", -85.22944, 35.11141));
            returnList.Add(new Point("Dupont Pkwy", -85.23763, 35.12064));
            returnList.Add(new Point("Hixson Pike", -85.24452, 35.13109));
            returnList.Add(new Point("Gadd Road", -85.24871, 35.14181));
            returnList.Add(new Point("Towne Hills", -85.24921, 35.14888));
            returnList.Add(new Point("Dayton Blvd", -85.24696, 35.17888));
            returnList.Add(new Point("Northern Terminus", -85.24627, 35.18818));

            return returnList;
        }

        private static void TestingQueue()
        {
            Console.WriteLine("Testing");
            double lat1 = 35.02987;
            double lon1 = -85.1803;
            double lat2 = 35.18818;
            double lon2 = -85.2463;

            Point point1 = new Point(lon1, lat1);
            Point point2 = new Point(lon2, lat2);

            Queue<Point> pointQueue = new Queue<Point>();
            foreach (var item in northBoundCheckpoints)
            {
                pointQueue.Enqueue(item);
            }

            Point source = new Point();
            Point dest = new Point();
            while (pointQueue.Count > 1)
            {
                source = pointQueue.Dequeue();
                dest = pointQueue.Peek();
                Console.Write(bearingTo(source, dest) + " ");
                Console.WriteLine(distanceOff(source, dest));
            }
        }

        static double distanceOff(Point point1, Point point2)
        {
            return distanceOff(point1.Y, point1.X, point2.Y, point2.X);
        }

        static double bearingTo(Point point1, Point point2)
        {
            return bearingTo(point1.Y, point1.X, point2.Y, point2.X);
        }

        static double bearingTo(double lat1, double lon1, double lat2, double lon2)
        {
            //https://gis.stackexchange.com/questions/252672/calculate-bearing-between-two-decimal-gps-coordinates-arduino-c
            double theta1 = deg2rad(lat1);
            double theta2 = deg2rad(lat2);
            double delta1 = deg2rad(lat2 - lat1);
            double delta2 = deg2rad(lon2 - lon1);

            double y = Math.Sin(delta2) * Math.Cos(theta2);
            double x = Math.Cos(theta1) * Math.Sin(theta2) - Math.Sin(theta1) * Math.Cos(theta2) * Math.Cos(delta2);
            double brng = Math.Atan2(y, x);
            brng = rad2deg(brng);// radians to degrees
            brng = (((int)brng + 360) % 360);
            return brng;
        }


        static double distanceOff(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = deg2rad(lat2 - lat1);
            double dLon = deg2rad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }

        static double timeToNextStep(Point presentLocation, Point destinationLocation, double velocity)
        {
            double distanceToTravel = distanceOff(presentLocation, destinationLocation);
            double timeToNextStep = distanceToTravel / velocity;
            return timeToNextStep;
        }

        //https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula
        static double deg2rad(double x)
        {
            return x * (Math.PI / 180);
        }

        static double toRadians(double x)
        {
            return deg2rad(x);
        }
        static double rad2deg(double brng)
        {
            return brng * (180 / Math.PI);
        }

        static double toDegrees(double x)
        {
            return rad2deg(x);
        }

        static double distanceTravelled(double velocity, double time)
        {
            return velocity  * time;
        }

        static Point newLocation(Point startPoint, double bearing, double velocity, double time)
        {
            double distance = distanceTravelled(velocity, time);
            double lat2 = Math.Asin(Math.Sin(toRadians(startPoint.Latitude)) * Math.Cos(distance / R)
            + Math.Cos(toRadians(startPoint.Latitude)) * Math.Sin(distance / R) * Math.Cos(toRadians(bearing)));

            double lon2 = toRadians(startPoint.Longitude)
                    + Math.Atan2(Math.Sin(toRadians(bearing)) * Math.Sin(distance / R) * Math.Cos(toRadians(startPoint.Latitude)), Math.Cos(distance / R)
                    - Math.Sin(toRadians(startPoint.Latitude)) * Math.Sin(lat2));
            lat2 = toDegrees(lat2);
            lon2 = toDegrees(lon2);
            return new Point(lon2, lat2);
        }
    }
}
