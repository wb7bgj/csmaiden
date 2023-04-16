using System;
using System.Collections.Generic;
using MaidenheadCalc;

// Console examples of calling functions in CSmaiden

namespace MaidenheadConsole
{
    class ConsoleDemo
    {
        static void Main(string[] args)
        {
            // Nairobi, Kenya
            // Wikipedia city center: lat/lon and Grid Square for city center
            var slat = -1.286389;
            var slon = 36.817222;
            var gridID1 = "KI88jr";

            // Seattle, WA
            // Wikipedia city center: lat/lon and Grid Square for city center
            var eLat = 47.609722;
            var elon = -122.333056;
            var gridID2 = "CN87uo";

            // ***********************************
            CallGridLocIDvalid(gridID1);

            // ***********************************
            CallLatLonToGridLoc(slat, slon);

            // ***********************************
            CallGridLocIDbounds(gridID1);

            // ***********************************
            CallLatLonDistance(slat, slon, eLat, elon);

            // ***********************************
            CallGridLocDistance(gridID1, gridID2);

            // ***********************************
            CallAngleFromCoordinates(slat, slon, eLat, elon);

            // ***********************************
            CallAngleFromGridLocIDs(gridID1, gridID2);

            // ***********************************
            CallGridLocSize(gridID1);
        }

        // -------------------------------------------------------------------
        static void CallGridLocIDvalid(string gridID)
        {
            // Call GridLocIDvalid method in Calculations class
            var retGridLocIDvalid = MaidenheadCalc.CSmaiden.GridLocIDvalid(gridID);

            // Display call results
            Console.WriteLine($"GridLocIDvalid({gridID})");
            Console.WriteLine($"{retGridLocIDvalid}\n");
        }

        // -------------------------------------------------------------------
        static void CallLatLonToGridLoc(double lat, double lon)
        {
            // Call LatLonToGridLoc method in Calculations class
            var retLatLonToGridLoc = MaidenheadCalc.CSmaiden.LatLonToGridLoc(lat, lon);

            // Display call results
            Console.WriteLine($"LatLonToGridLoc({lat},{lon})");
            Console.WriteLine($"{retLatLonToGridLoc}\n");
        }

        // -------------------------------------------------------------------
        static void CallGridLocIDbounds(string gridID)
        {
            // Call GridLocIDbounds method in Calculations class 
            var retGridLocIDbounds = MaidenheadCalc.CSmaiden.GridLocIDbounds(gridID);

            // Display call results
            Console.WriteLine($"GridLocIDbounds({gridID})");
            Console.WriteLine($"NE  - Lat: {retGridLocIDbounds["NE"]["lat"].ToString("F6")} Lon: {retGridLocIDbounds["NE"]["lon"].ToString("F6")}");
            Console.WriteLine($"SE  - Lat: {retGridLocIDbounds["SE"]["lat"].ToString("F6")} Lon: {retGridLocIDbounds["SE"]["lon"].ToString("F6")}");
            Console.WriteLine($"SW  - Lat: {retGridLocIDbounds["SW"]["lat"].ToString("F6")} Lon: {retGridLocIDbounds["SW"]["lon"].ToString("F6")}");
            Console.WriteLine($"NW  - Lat: {retGridLocIDbounds["NW"]["lat"].ToString("F6")} Lon: {retGridLocIDbounds["NW"]["lon"].ToString("F6")}");
            Console.WriteLine($"CEN - Lat: {retGridLocIDbounds["CEN"]["lat"].ToString("F6")} Lon: {retGridLocIDbounds["CEN"]["lon"].ToString("F6")}\n");
        }

        // -------------------------------------------------------------------
        static void CallLatLonDistance(double slat, double slon, double eLat, double elon)
        {
            // Call LatLonDistance method in Calculations class
            var retLatLonDistances = MaidenheadCalc.CSmaiden.LatLonDistance(slat, slon, eLat, elon);

            // Display call results
            Console.WriteLine($"LatLonDistance({slat},{slon},{eLat},{elon})");
            Console.WriteLine($"Km: {retLatLonDistances["km"].ToString("F6")}, Stat Mi: {retLatLonDistances["smi"].ToString("F6")} , Naut Mi: {retLatLonDistances["nmi"].ToString("F6")}\n");
        }

        // -------------------------------------------------------------------
        static void CallGridLocDistance(string gridID1, string gridID2)
        {
            // Call GridLocDistance method in Calculations class
            var retGridLocDistance = MaidenheadCalc.CSmaiden.GridLocDistance(gridID1, gridID2);

            // Display call results
            Console.WriteLine($"GridLocDistance({gridID1},{gridID2})");
            Console.WriteLine($"Km: {retGridLocDistance["km"].ToString("F6")}, Stat Mi: {retGridLocDistance["smi"].ToString("F6")} , Naut Mi: {retGridLocDistance["nmi"].ToString("F6")}\n");
        }

        // -------------------------------------------------------------------
        static void CallAngleFromCoordinates(double slat, double slon, double eLat, double elon)
        {
            // Call AngleFromCoordinates method in Calculations class
            var retAngleFromCoordinates = MaidenheadCalc.CSmaiden.AngleFromCoordinates(slat, slon, eLat, elon);

            // Display call results
            Console.WriteLine($"AngleFromCoordinates({slat},{slon},{eLat},{elon})");
            Console.WriteLine($"Azimuth from start location: {retAngleFromCoordinates}\n");
        }

        // -------------------------------------------------------------------
        static void CallAngleFromGridLocIDs(string gridID1, string gridID2)
        {
            // Call AngleFromGridLocIDs method in Calculations class
            var retAngleFromGridLocIDs = MaidenheadCalc.CSmaiden.AngleFromGridLocIDs(gridID1, gridID2);

            // Display call results
            Console.WriteLine($"AngleFromGridLocIDs({gridID1},{gridID2})");
            Console.WriteLine($"Azimuth from start location: {retAngleFromGridLocIDs}\n");
        }

        // -------------------------------------------------------------------
        static void CallGridLocSize(string gridID)
        {
            // Call GridLocSize method in Calculations class
            var retGridLocSize = MaidenheadCalc.CSmaiden.GridLocSize(gridID);

            // Display call results
            Console.WriteLine($"GridLocSize({gridID})");
            foreach (KeyValuePair<string, double> item in retGridLocSize)
            {
                Console.WriteLine($"Key: {item.Key}\t Value: {item.Value:F6}");
            }
        }

    }
}