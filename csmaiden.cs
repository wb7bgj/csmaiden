// Class Calculations.cs
// This class contains methods used to perform calculations related to the
// the Maidenhead Grid Locator System.
// Written by: Kevin Hallquist, WB7BGJ

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaidenheadCalc
{
    public static class CSmaiden
    {
        // =========================================================
        // GridLocIDvalid - Validates correct format of a given grid ID string.
        // Input parameter: 2, 4, 6, 8 or 10 character grid locator character string
        // Returns: True or False
        public static bool GridLocIDvalid(string gridID)
        {
            if (gridID.Length % 2 == 0 && gridID.Length >= 2 && gridID.Length <= 10)
            {
                if (gridID.Length == 2)
                {
                    Regex reAR = new Regex(@"^[A-R]{2}$");
                    if (reAR.IsMatch(gridID))
                        return true;
                    else
                        return false;
                }
                else if (gridID.Length == 4)
                {
                    Regex reAR09 = new Regex(@"^[A-R]{2}[0-9]{2}$");
                    if (reAR09.IsMatch(gridID))
                        return true;
                    else
                        return false;
                }
                else if (gridID.Length == 6)
                {
                    Regex reAR09ax = new Regex(@"^[A-R]{2}[0-9]{2}[a-x]{2}$");
                    if (reAR09ax.IsMatch(gridID))
                        return true;
                    else
                        return false;
                }
                else if (gridID.Length == 8)
                {
                    Regex reAR09ax09 = new Regex(@"^[A-R]{2}[0-9]{2}[a-x]{2}[0-9]{2}$");
                    if (reAR09ax09.IsMatch(gridID))
                        return true;
                    else
                        return false;
                }
                else
                {
                    Regex reAR09ax09AX = new Regex(@"^[A-R]{2}[0-9]{2}[a-x]{2}[0-9]{2}[A-X]{2}$");
                    if (reAR09ax09AX.IsMatch(gridID))
                        return true;
                    else
                        return false;
                }

            }
            else
            {
                // Invalid string lentgh
                return false;
            }
        }

        // =========================================================
        // LatLonToGridLoc - Find cooresponding grid ID for a given lat/lon location
        // Input parameters: Longitude and Latitiude. (Use minus prefix for South and West)
        // Returns: A 10 character Grid Locator ID string
        // Implementation reference: The Maidenhead Grid System, Bruce Hall, W8BH
        public static string LatLonToGridLoc(double lat, double lon)
        { 
            if (!(-90 < lat && lat < 90))
            {
                return "Latitude out of range.";
            }
            if (!(-180 < lon && lon < 180))
            {
                return "Longitude out of range";
            }

            // Convert plus/minus 90 deg lat and 180 deg lon format to 180 deg Lat and 360 deg Lon
            lon += 180.0;
            lat += 90.0;

            // Empty string variable to hold grid ID values as they are calculated
            string gridID = "";

            // -----------------------------------------
            // Calculate First Pair (AA-RR)
            // Field, A-R, 18x18, 20 x 10 degrees

            // Add longitude Field character to gridID string
            var fieldLonIndex = (int)(lon / 20);
            gridID += MaidenheadCalc.GridTables.FieldIndex[fieldLonIndex];

            // Add latitude Field character to gridID string
            var fieldLatIndex = (int)(lat / 10);
            gridID += MaidenheadCalc.GridTables.FieldIndex[fieldLatIndex];

            // -----------------------------------------
            // Calculate Second Pair (00-99)
            // Square, 0-9, 10x10,  2 x 1 degrees
            var lonSquareIndexRmdr = (lon - fieldLonIndex * 20) / 2;
            gridID += (((int)lonSquareIndexRmdr).ToString());

            var latSquareIndexRmdr = (lat - fieldLatIndex * 10) / 1;
            gridID += (((int)latSquareIndexRmdr).ToString());

            // -----------------------------------------
            // Calculate Third Pair (aa-xx)
            // Subsquare, a-x, 24x24,  5 x 2.5 minutes
            var lonSubSquareRmdr = lonSquareIndexRmdr * 2 - (int)(lonSquareIndexRmdr) * 2;
            var lonSubSquareIndex = (int)(lonSubSquareRmdr * 12);
            gridID += MaidenheadCalc.GridTables.SubSquareIndex[lonSubSquareIndex];

            var latSubSquareRmdr = latSquareIndexRmdr - (int)(latSquareIndexRmdr);
            var latSubSquareIndex = (int)(latSubSquareRmdr * 24);
            gridID += MaidenheadCalc.GridTables.SubSquareIndex[latSubSquareIndex];

            //-----------------------------------------
            // Calculate Fourth Pair PAIR (00-99)
            // Extended square, 0-9, 10x10, 30 x 15 seconds
            var lonExtSubSquareRmdr = lonSubSquareRmdr - ((double)lonSubSquareIndex / 12);
            var lonExtSubSquareIndex = lonExtSubSquareRmdr * 120;
            gridID += ((int)(lonExtSubSquareIndex)).ToString();

            var latExtSubSquareRmdr = latSubSquareRmdr - ((double)latSubSquareIndex / 24);
            var latExtSubSquareIndexRmdr = latExtSubSquareRmdr * 240;
            gridID += ((int)(latExtSubSquareIndexRmdr)).ToString();

            // -----------------------------------------
            // Calculate Fifth Pair (AA-XX)
            // Super extended square, A-X, 24x24, 1.25 x 0.625 seconds
            var lonSupExtSquareRmdr = lonExtSubSquareRmdr - Math.Truncate(lonExtSubSquareIndex) / 120;
            var lonSupExtSquareIndex = (int)(lonSupExtSquareRmdr * 2880);
            gridID += MaidenheadCalc.GridTables.SupExtSubSquareIndex[lonSupExtSquareIndex];

            var latSupExtSquareRmdr = latExtSubSquareRmdr - Math.Truncate(latExtSubSquareIndexRmdr) / 240;
            var lat_supextsquare_index = (int)(latSupExtSquareRmdr * 5760);
            gridID += MaidenheadCalc.GridTables.SupExtSubSquareIndex[lat_supextsquare_index];

            return (gridID);
        }

        // =========================================================
        // GridLocIDbounds - Calculates and returns center and corner coordinates of a give 4 or 6 character grid ID
        // Input parameter: 2, 4, 6, 8 or 10 character grid locator character string
        // Returns: Structure containing lat/lon of the SW, NW, NE, SE corners and Center of the provide grid ID 
        // Implementation reference: Conversion Between Geodetic and Grid Locator Systems, QST magazine, January 1989
        public static Dictionary<string, Dictionary<string, double>> GridLocIDbounds(string gridID)
        {
            // Variables used to hold Lat/Lon information calculated
            // for a given grid square
            double swCoordLon = 0.0; double swCoordLat = 0.0;
            double nwCoordLon = 0.0; double nwCoordLat = 0.0;
            double neCoordLon = 0.0; double neCoordLat = 0.0;
            double seCoordLon = 0.0; double seCoordLat = 0.0;
            double cenCoordLon = 0.0; double cenCoordLat = 0.0;

            switch (gridID.Length)
            {
                case 10:
                    // Calculate the longitude and latitude of the square's SW corner
                    swCoordLon = GridTables.LonField[gridID[0]] + GridTables.LonSquare[gridID[2]] + (GridTables.LonSubsquare[gridID[4]] / 60) + (GridTables.LonExtendedSquare[gridID[6]] / 3600) + (GridTables.LonSupExtSquare[gridID[8]] / 3600);
                    swCoordLat = GridTables.LatField[gridID[1]] + GridTables.LatSquare[gridID[3]] + (GridTables.LatSubsquare[gridID[5]] / 60) + (GridTables.LatExtendedSquare[gridID[7]] / 3600) + (GridTables.LatSupExtSquare[gridID[9]] / 3600);

                    // Calculate lon/lat for the other three corners and center from the SW corner location
                    nwCoordLon = swCoordLon;
                    nwCoordLat = swCoordLat + 0.625 / 3600.0;

                    neCoordLon = swCoordLon + 1.25 / 3600.0;
                    neCoordLat = swCoordLat + 0.625 / 3600.0;

                    seCoordLon = swCoordLon + 1.25 / 3600.0;
                    seCoordLat = swCoordLat;

                    cenCoordLon = swCoordLon + 0.625 / 3600.0;
                    cenCoordLat = swCoordLat + 0.3125 / 3600.0;
                    break;

                case 8:
                    // Calculate the longitude and latitude of the square's SW corner
                    swCoordLon = GridTables.LonField[gridID[0]] + GridTables.LonSquare[gridID[2]] + (GridTables.LonSubsquare[gridID[4]] / 60) + (GridTables.LonExtendedSquare[gridID[6]] / 3600);
                    swCoordLat = GridTables.LatField[gridID[1]] + GridTables.LatSquare[gridID[3]] + (GridTables.LatSubsquare[gridID[5]] / 60) + (GridTables.LatExtendedSquare[gridID[7]] / 3600);

                    // Calculate lon/lat for the other three corners and center from the SW corner location
                    nwCoordLon = swCoordLon;
                    nwCoordLat = swCoordLat + 15.0 / 3600.0;

                    neCoordLon = swCoordLon + 30.0 / 3600.0;
                    neCoordLat = swCoordLat + 15.0 / 3600.0;

                    seCoordLon = swCoordLon + 30.0 / 3600.0;
                    seCoordLat = swCoordLat;

                    cenCoordLon = swCoordLon + 15.0 / 3600.0;
                    cenCoordLat = swCoordLat + 7.5 / 3600.0;
                    break;

                case 6:
                    // Calculate the longitude and latitude of the square's SW corner
                    swCoordLon = GridTables.LonField[gridID[0]] + GridTables.LonSquare[gridID[2]] + (GridTables.LonSubsquare[gridID[4]] / 60);
                    swCoordLat = GridTables.LatField[gridID[1]] + GridTables.LatSquare[gridID[3]] + (GridTables.LatSubsquare[gridID[5]] / 60);

                    // Calculate lon/lat for the other three corners and center from the SW corner location
                    nwCoordLon = swCoordLon;
                    nwCoordLat = swCoordLat + 2.5 / 60.0;

                    neCoordLon = swCoordLon + 5.0 / 60.0;
                    neCoordLat = swCoordLat + 2.5 / 60.0;

                    seCoordLon = swCoordLon + 5.0 / 60.0;
                    seCoordLat = swCoordLat;

                    cenCoordLon = swCoordLon + 2.5 / 60.0;
                    cenCoordLat = swCoordLat + 1.25 / 60.0;
                    break;

                case 4:
                    // Calculate the longitude and latitude of the square's SW corner
                    swCoordLon = GridTables.LonField[gridID[0]] + GridTables.LonSquare[gridID[2]];
                    swCoordLat = GridTables.LatField[gridID[1]] + GridTables.LatSquare[gridID[3]];

                    // Calculate lon/lat for the other three corners and center from the SW corner location
                    nwCoordLon = swCoordLon;
                    nwCoordLat = swCoordLat + 1.0;

                    neCoordLon = swCoordLon + 2.0;
                    neCoordLat = swCoordLat + 1.0;

                    seCoordLon = swCoordLon + 2.0;
                    seCoordLat = swCoordLat;

                    cenCoordLon = swCoordLon + 1.0;
                    cenCoordLat = swCoordLat + 0.5;
                    break;

                case 2:
                    // Calculate the longitude and latitude of the square's SW corner
                    swCoordLon = GridTables.LonField[gridID[0]];
                    swCoordLat = GridTables.LatField[gridID[1]];

                    // Calculate lon/lat for the other three corners and center from the SW corner location
                    nwCoordLon = swCoordLon;
                    nwCoordLat = swCoordLat + 10.0;

                    neCoordLon = swCoordLon + 20.0;
                    neCoordLat = swCoordLat + 10.0;

                    seCoordLon = swCoordLon + 20.0;
                    seCoordLat = swCoordLat;

                    cenCoordLon = swCoordLon + 10.0;
                    cenCoordLat = swCoordLat + 5.0;
                    break;
            }

            Dictionary<string, Dictionary<string, double>> gridBounds = new Dictionary<string, Dictionary<string, double>>()
                {
                    { "NE", new Dictionary<string, double>{{"lat", neCoordLat},{"lon", neCoordLon}}},
                    { "SE", new Dictionary<string, double>{{"lat", seCoordLat},{"lon", seCoordLon}}},
                    { "SW", new Dictionary<string, double>{{"lat", swCoordLat},{"lon", swCoordLon}}},
                    { "NW", new Dictionary<string, double>{{"lat", nwCoordLat},{"lon", nwCoordLon}}},
                    { "CEN", new Dictionary<string, double>{{"lat", cenCoordLat},{"lon", cenCoordLon}}}
            };

            return (gridBounds);
        }

        // =========================================================
        // Function: LatLonDistance - Calculates the distance between two lat/lon coordinates
        // Input parameters: lat/lon of a start point and end point
        // Returns: Structure containing the distance between the two point in kilometers, statue miles and nautical miles
        //
        public static Dictionary<string, double> LatLonDistance(double sLat, double sLon, double eLat, double eLon)
        {
            if (!(-90 < sLat && sLat < 90))
            {
                return new Dictionary<string, double>() { { "Start latitude out of range.", 0.0 } };
            }
            if (!(-180 < sLon && sLon < 180))
            {
                return new Dictionary<string, double>() { { "Start longitude out of range.", 0.0 } };
            }
            if (!(-90 < eLat && eLat < 90))
            {
                return new Dictionary<string, double>() { { "End latitude out of range.", 0.0 } };
            }
            if (!(-180 < eLon && eLon < 180))
            {
                return new Dictionary<string, double>() { { "End latitude out of range.", 0.0 } };
            }

            // Scale multipliers for different units of distance
            var kmMult = 6371.0210;
            var smMult = 3958.7613;
            var nmMult = 3437.8675;

            var sLatRad = sLat * (Math.PI / 180);
            var sLonRad = sLon * (Math.PI / 180);
            var eLatRad = eLat * (Math.PI / 180);
            var eLonRad = eLon * (Math.PI / 180);

            var kilometers = kmMult * Math.Acos(Math.Sin(sLatRad) * Math.Sin(eLatRad) + Math.Cos(sLatRad) * Math.Cos(eLatRad) * Math.Cos(sLonRad - eLonRad));
            var statuteMiles = smMult * Math.Acos(Math.Sin(sLatRad) * Math.Sin(eLatRad) + Math.Cos(sLatRad) * Math.Cos(eLatRad) * Math.Cos(sLonRad - eLonRad));
            var nauticalMiles = nmMult * Math.Acos(Math.Sin(sLatRad) * Math.Sin(eLatRad) + Math.Cos(sLatRad) * Math.Cos(eLatRad) * Math.Cos(sLonRad - eLonRad));

            Dictionary<string, double> distances = new Dictionary<string, double>()
            {
                {"km", kilometers },
                {"smi", statuteMiles },
                {"nmi", nauticalMiles }
            };

            return distances;
        }

        // =========================================================
        // GridLocDistance - Calculates distance between center points of two grid ID values
        // Input parameters: Grid ID of a start point and end point
        // Returns: Structure containing the distance between the two point in kilometer, statue miles and nautical miles
        //
        public static Dictionary<string, double> GridLocDistance(string gridID1, string gridID2)
        {
            // Get center lat and lon for start grid
            var startCoordinates = MaidenheadCalc.CSmaiden.GridLocIDbounds(gridID1);
            var slat = startCoordinates["CEN"]["lat"];
            var slon = startCoordinates["CEN"]["lon"];

            // Get center lat and lon for end grid
            var endCoordinates = MaidenheadCalc.CSmaiden.GridLocIDbounds(gridID2);
            var elat = endCoordinates["CEN"]["lat"];
            var elon = endCoordinates["CEN"]["lon"];

            return (MaidenheadCalc.CSmaiden.LatLonDistance(slat, slon, elat, elon));
        }

        // =========================================================
        // AngleFromCoordinates - Calculate bearing from start location to end location using lat/lon values
        // Input parameters: Lat/Lon of a start point and end point
        // Returns: Integer value between 0 and 360 degrees of initial bearing angle at start point
        //
        public static double AngleFromCoordinates(double sLat, double sLon, double eLat, double eLon)
        {
            if (!(-90 < sLat && sLat < 90))
            {
                return -1.0;
            }
            if (!(-180 < sLon && sLon < 180))
            {
                return -1.0;
            }
            if (!(-90 < eLat && eLat < 90))
            {
                return -1.0;
            }
            if (!(-180 < eLon && eLon < 180))
            {
                return -1.0;
            }

            var sLatRad = sLat * (Math.PI / 180);
            var sLonRad = sLon * (Math.PI / 180);
            var eLatRad = eLat * (Math.PI / 180);
            var eLonRad = eLon * (Math.PI / 180);

            // Calculate bearing with result in radians
            var dlonRad = eLonRad - sLonRad;
            var X = Math.Sin(dlonRad) * Math.Cos(eLatRad);
            var Y = Math.Cos(sLatRad) * Math.Sin(eLatRad) - Math.Sin(sLatRad) * Math.Cos(eLatRad) * Math.Cos(dlonRad);
            var bearingRad = Math.Atan2(X, Y);

            // Covert bearing into degrees and covert from 180/-180 deg format to 360 format 
            var startBearingDeg = Math.Round(((bearingRad + 180 * Math.PI) * (180 / Math.PI)) % 360);

            return startBearingDeg;
        }

        // =========================================================
        // Function: AngleFromGridLocIDs - Calculate bearing from start location to end location using either grid ID values
        // Input parameters: Grid IDs for the start point and end point 
        // Returns: Float value between 0 and 360 degrees of initial bearing angle at start point
        //
        public static double AngleFromGridLocIDs(string gridID1, string gridID2)
        {
            // Get center lat and lon for start grid
            var startCoordinates = MaidenheadCalc.CSmaiden.GridLocIDbounds(gridID1);
            var slat = startCoordinates["CEN"]["lat"];
            var slon = startCoordinates["CEN"]["lon"];

            // Get center lat and lon for end grid
            var endCoordinates = MaidenheadCalc.CSmaiden.GridLocIDbounds(gridID2);
            var elat = endCoordinates["CEN"]["lat"];
            var elon = endCoordinates["CEN"]["lon"];

            var retAngleFromCoordinates = MaidenheadCalc.CSmaiden.AngleFromCoordinates(slat, slon, elat, elon);

            return retAngleFromCoordinates;
        }

        // =========================================================
        // GridLocSize - Calculate the area and perimeter information for a given grid 
        // Input parameter: 2, 4, 6, 8 or 10 character grid locator character string
        // Returns: Structure containing grid area and perimeter in miles
        //
        public static Dictionary<string, double> GridLocSize(string gridID)
        {
            //Get corner coordinates of grid square
            var gridBounds = GridLocIDbounds(gridID);

            var northSide = MaidenheadCalc.CSmaiden.LatLonDistance(gridBounds["NW"]["lat"], gridBounds["NW"]["lon"], gridBounds["NE"]["lat"], gridBounds["NE"]["lon"]);
            var northSideMi = northSide["smi"];
            var northSideKm = northSide["km"];

            var southSide = MaidenheadCalc.CSmaiden.LatLonDistance(gridBounds["SW"]["lat"], gridBounds["SW"]["lon"], gridBounds["SE"]["lat"], gridBounds["SE"]["lon"]);
            var southSideMi = southSide["smi"];
            var southSideKm = southSide["km"];

            var eastwestSides = MaidenheadCalc.CSmaiden.LatLonDistance(gridBounds["NE"]["lat"], gridBounds["NE"]["lon"], gridBounds["SE"]["lat"], gridBounds["SE"]["lon"]);
            var eastwestSidesMi = eastwestSides["smi"];
            var eastwestSidesKm = eastwestSides["km"];

            var areaSqMi = ((northSideMi + southSideMi) / 2) * eastwestSidesMi;
            var areaSqKm = ((northSideKm + southSideKm) / 2) * eastwestSidesKm;

            var perimMi = northSideMi + southSideMi + (eastwestSidesMi * 2);
            var perimKm = northSideKm + southSideKm + (eastwestSidesKm * 2);

            Dictionary<string, double> distances = new Dictionary<string, double>()
            {
                {"sqmi", areaSqMi},
                {"sqkm", areaSqKm},
                {"permi", perimMi},
                {"perkm", perimKm},
                {"ndistmi", northSideMi},
                {"ndistkm", northSideKm},
                {"sdistmi", southSideMi},
                {"sdistkm", southSideKm},
                {"ewdistmi", eastwestSidesMi},
                {"ewdistkm", eastwestSidesKm}
            };

            return distances;
        }
    }
}
