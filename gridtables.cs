// Class GridTables.cs
// This class contains a series of lookup Lists and Dictionarys containing
// data for performing Maidenhead Grid Locator System calculations.
// Written by: Kevin Hallquist, WB7BGJ

using System.Collections.Generic;

namespace MaidenheadCalc
{
    public static class GridTables
    {
        // Field for index lookup
        public static readonly List<char> FieldIndex = new List<char>() // FieldIndex
        {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R'};

        // Subsquare for index lookup
        public static readonly List<char> SubSquareIndex = new List<char>()
        { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'};

        // Super extended subsquare for index lookup
        public static readonly List<char> SupExtSubSquareIndex = new List<char>()
        {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X'};

        // ============================== Latitude tables ==============================
        
        // Field boundary values in degrees
        public static readonly Dictionary<char, double> LatField = new Dictionary<char, double>()
        {
            { 'A', -90.0 },
            { 'B', -80.0 },
            { 'C', -70.0 },
            { 'D', -60.0 },
            { 'E', -50.0 },
            { 'F', -40.0 },
            { 'G', -30.0 },
            { 'H', -20.0 },
            { 'I', -10.0 },
            { 'J',   0.0 },
            { 'K',  10.0 },
            { 'L',  20.0 },
            { 'M',  30.0 },
            { 'N',  40.0 },
            { 'O',  50.0 },
            { 'P',  60.0 },
            { 'Q',  70.0 },
            { 'R',  80.0 }
        };

        // Square boundary values in degrees
        public static readonly Dictionary<char, double> LatSquare = new Dictionary<char, double>()
        {
            { '0', 0.0 },
            { '1', 1.0 },
            { '2', 2.0 },
            { '3', 3.0 },
            { '4', 4.0 },
            { '5', 5.0 },
            { '6', 6.0 },
            { '7', 7.0 },
            { '8', 8.0 },
            { '9', 9.0 }
         };

        // Subsquare boundary values in minutes
        public static readonly Dictionary<char, double> LatSubsquare = new Dictionary<char, double>()
        {
            { 'a',  0.0 },
            { 'b',  2.5 },
            { 'c',  5.0 },
            { 'd',  7.5 }, 
            { 'e', 10.0 },
            { 'f', 12.5 },
            { 'g', 15.0 },
            { 'h', 17.5 },
            { 'i', 20.0 },
            { 'j', 22.5 },
            { 'k', 25.0 },
            { 'l', 27.5 },
            { 'm', 30.0 },
            { 'n', 32.5 },
            { 'o', 35.0 },
            { 'p', 37.5 },
            { 'q', 40.0 },
            { 'r', 42.5 },
            { 's', 45.0 },
            { 't', 47.5 },
            { 'u', 50.0 },
            { 'v', 52.5 },
            { 'w', 55.0 },
            { 'x', 57.5 }
        };

        // Extended square boundary values in seconds
        public static readonly Dictionary<char, double> LatExtendedSquare = new Dictionary<char, double>()
         {
            { '0',   0.0 },
            { '1',  15.0 },
            { '2',  30.0 },
            { '3',  45.0 },
            { '4',  60.0 },
            { '5',  75.0 },
            { '6',  90.0 },
            { '7', 105.0 },
            { '8', 120.0 },
            { '9', 135.0 }
         };

        // Super extended square boundary values in seconds
        public static readonly Dictionary<char, double> LatSupExtSquare = new Dictionary<char, double>()
        {
            { 'A',    0.0 },
            { 'B',  0.625 },
            { 'C',   1.25 },
            { 'D',  1.875 },
            { 'E',    2.5 },
            { 'F',  3.125 },
            { 'G',   3.75 },
            { 'H',  4.375 },
            { 'I',    5.0 },
            { 'J',  5.625 },
            { 'K',   6.25 },
            { 'L',  6.875 },
            { 'M',    7.5 },
            { 'N',  8.125 },
            { 'O',   8.75 },
            { 'P',  9.375 },
            { 'Q',   10.0 },
            { 'R', 10.625 },
            { 'S',  11.25 },
            { 'T', 11.875 },
            { 'U',   12.5 },
            { 'V', 13.125 },
            { 'W',  13.75 },
            { 'X', 14.375 }
        };

        // ============================== Longitude tables =============================
        // Field boundary values in degrees
        public  static readonly Dictionary<char, double> LonField = new Dictionary<char, double>()
        {
            { 'A', -180.0 },
            { 'B', -160.0 },
            { 'C', -140.0 },
            { 'D', -120.0 },
            { 'E', -100.0 },
            { 'F',  -80.0 },
            { 'G',  -60.0 },
            { 'H',  -40.0 },
            { 'I',  -20.0 },
            { 'J',    0.0 },
            { 'K',   20.0 },
            { 'L',   40.0 },
            { 'M',   60.0 },
            { 'N',   80.0 },
            { 'O',  100.0 },
            { 'P',  120.0 },
            { 'Q',  140.0 },
            { 'R',  160.0 }
         };

        // Square boundary values in degrees
        public static readonly Dictionary<char, double> LonSquare = new Dictionary<char, double>()
        {
            { '0',  0.0 },
            { '1',  2.0 },
            { '2',  4.0 },
            { '3',  6.0 },
            { '4',  8.0 },
            { '5', 10.0 },
            { '6', 12.0 },
            { '7', 14.0 },
            { '8', 16.0 },
            { '9', 18.0 }
        };

        // Subsquare boundary values in minutes
        public static readonly Dictionary<char, double> LonSubsquare = new Dictionary<char, double>()
        {
            { 'a',   0.0 },
            { 'b',   5.0 },
            { 'c',  10.0 },
            { 'd',  15.0 },
            { 'e',  20.0 },
            { 'f',  25.0 },
            { 'g',  30.0 },
            { 'h',  35.0 },
            { 'i',  40.0 },
            { 'j',  45.0 },
            { 'k',  50.0 },
            { 'l',  55.0 },
            { 'm',  60.0 },
            { 'n',  65.0 },
            { 'o',  70.0 },
            { 'p',  75.0 },
            { 'q',  80.0 },
            { 'r',  85.0 },
            { 's',  90.0 },
            { 't',  95.0 },
            { 'u', 100.0 },
            { 'w', 110.0 },
            { 'x', 115.0 }
        };

        // Extended boundary values in seconds
        public static readonly Dictionary<char, double> LonExtendedSquare = new Dictionary<char, double>()
        {
            { '0',   0.0 },
            { '1',  30.0 },
            { '2',  60.0 },
            { '3',  90.0 },
            { '4', 120.0 },
            { '5', 150.0 },
            { '6', 180.0 },
            { '7', 210.0 },
            { '8', 240.0 },
            { '9', 270.0 }
        };

        // Super extended boundary values in seconds
        public static readonly Dictionary<char, double> LonSupExtSquare = new Dictionary<char, double>()
        {
            { 'A',   0.0 },
            { 'B',  1.25 },
            { 'C',   2.5 },
            { 'D',  3.75 },
            { 'E',   5.0 },
            { 'F',  6.25 },
            { 'G',   7.5 },
            { 'H',  8.75 },
            { 'I',  10.0 },
            { 'J', 11.25 },
            { 'K',  12.5 },
            { 'L', 13.75 },
            { 'M',  15.0 },
            { 'N', 16.25 },
            { 'O',  17.5 },
            { 'P', 18.75 },
            { 'Q',  20.0 },
            { 'R', 21.25 },
            { 'S',  22.5 },
            { 'T', 23.75 },
            { 'U',  25.0 },
            { 'V', 26.25 },
            { 'W',  27.5 },
            { 'X', 28.75 }
        };
    }
}
