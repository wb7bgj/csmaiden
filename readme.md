# csmaiden #
This module contains a set of eight functions which provide calculations related to the Maidenhead Grid Locator System used world wide y the Amateur Radio community. See "Maidenhead Grid ID pair descriptions" and "Resources" below for more information on the Maidenhead rid Locator System.

Written by: Kevin Hallquist, WB7BGJ

## Install Visual Studio
To use the provided Visual Studio solution you will need to install Microsoft's Free Intergrated Development Environment (IDE) called Visual Studio Community.\
https://visualstudio.microsoft.com/

## Using csmaiden
To get csmaiden, clone the repository with GitHub CLI or download a ZIP file of the csmaiden project and unzip the these files into the root of your development folder.
    
Open the csmaiden.sln file with Visual Studio Community version.

Once Visaul Studio has opened the solution, Press F5 to run the Program.cs file to see a demonstration of how to use csmaiden within a simple CLI project. 

## Using csmaiden in your own project
To use csmaiden within your own project, you will need to place the csmaiden.cs and gridtables.cs files into the root project folder and ensure they are included as dependecies in your Visual Studio project.

## Functions in csmaiden

### GridLocIDvalid
Validates correct format of a given grid ID. Characters are case sensitive . The required case format is AR00ax00AX\
Input parameter: 2, 4, 6, 8 or 10 character grid locator character string\
Returns: True or False

### LatLonToGridLoc
Find cooresponding grid ID for a given lat/lon location\
Input parameters: Longitude and Latitiude. (Use minus prefix for South and West)\
Returns: A 10 character Grid Locator ID string

### GridLocIDbounds
Calculates and returns center and corner coordinates of a given grid ID\
Input parameter: 2, 4, 6, 8 or 10 character grid locator character string\
Returns: Structure containing lat/lon of the SW, NW, NE, SE corners and Center of the provide grid ID

### LatLonDistance
Calculates the distance between two lat/lon coordinates\
Input parameters: lat/lon of a start point and end point\
Returns: Structure containing the distance between the two point in kilometers, statue miles and nautical miles

### GridLocDistance
Calculates distance between center points of two grid ID values\
Input parameters: Grid ID of a start point and end point\
Returns: Structure containing the distance between the two point in kilometer, statue miles ;and nautical miles

### AngleFromCoordinates
Calculate bearing from start location to end location using lat/lon values\
Input parameters: Lat/Lon of a start point and end point\
Returns: Integer value between 0 and 360 degrees of initial bearing angle at start point

### AngleFromGridLocIDs
Calculate bearing from start location to end location using either grid ID values\
Input parameters: Grid IDs for the start point and end point\
Returns: Float value between 0 and 360 degrees of initial bearing angle at start point

### GridLocSize
Calculate the area and perimeter information for a given grid\
Input parameter: 2, 4, 6, 8 or 10 character grid locator character string\
Returns: Structure containing grid area and perimeter in miles

---
## Maidenhead Grid Locator System description

The Maidenhead Grid Locator System provides a reasonably accurate global
location through the use of a concise identifier that can be
2, 4, 6, 8 or 10 characters in length depending on the accuracy desidered.
Example ID: "IO91dm35VE". This ID is broken down as IO, 91, dm, 35 and VE.
The first character of each pair indicates the Longitude and the second
character indicates the Latitude of the grid box. I, 9, d, 3,
and V provide Longitude data and O, 1, m, 5, and E provide Longitude data.
Each successive pair increases the accuracy of the grid location.  

The first Field level grid ID is AA. This is the most SW location this system
defines. The SW corner of AA is at -180 Lon and -90 Lat or S90,W180. Hence, in
all operations performed by this module, the SW corner is the starting for grid
location calculations.

More info at: https://en.wikipedia.org/wiki/Maidenhead_Locator_System

---
## Maidenhead Grid ID pair descriptions

### First character pair (Field): [AR]
Character range: A through R (18)\
The globe is gridded into 324 Fields of size 20 degrees longitude by 10
degrees latitude. The first Field level grid ID is AA. The SW corner of
AA is the starting point of the grid system.\
Total global Fields: 324

### Second character pair (Square): AR[09]
Character range: 0 through 9 (10)\
Each Field is gridded into 100 Squares of the size 2 degrees longitude
by 1 degree latitude.\
Total global Squares: 32,400

### Third character pair (Subsquare): AR09[ax]
Character range: a through x (24)\
Each Square is broken down into 576 Subsquares of
the size 5 minutes longitude and 2.5 minutes latitude.\
Total global Subsquares: 18,662,400

### Fourth character pair (Extended squares): AR09ax[09]
Character range: 0 through 9 (10)\
Each Subsquare is broken down into 100 Extended squares of
the size 30 seconds longitude and 15 seconds latitude.\
Total global Extended squares: 1,866,240,000

### Fifth character pair (Super extended squares): AR09ax09[AX]
Character range: A through X (24)\
Each Extended square is broken down into 576 Super extended squares of
the size 1.25 seconds longitude and 0.625 seconds latitude.\
Total global Super extended squares: 1,074,954,240,000

---
## Maidenhead Grid ID format breakdown
```
Grid ID: XX00xx00XX
          | | | | |
          | | | | +- Super extended square, A-X, 24x24, 1.25 x 0.625 seconds
          | | | |
          | | | +- Extended square, 0-9, 30 x 15 seconds
          | | |
          | | +--- Subsquare, a-x, 24x24,  5 x 2.5 minutes
          | |
          | +----- Square, 0-9, 10x10,  2 x 1 degrees
          |
          +------- Field, A-R, 18x18, 20 x 10 degrees
```

---
## Resources used
The DX Zone - Grid Square Locator System\
https://www.dxzone.com/grid-square-locator-system-explained/

QST January 1989\
Conversion Between Geodetic and Grid Locator Systems, Edmund Tyson, N5JTY

How to calculate the grid ID for a given lat/lon location, "The Maidenhead Grid System" by Bruce E. Hall, W8BH\
http://w8bh.net/grid_squares.pdf

Calculating the bearing between two geospatial coordinates, Daniel Ellis\
https://towardsdatascience.com/calculating-the-bearing-between-two-geospatial-coordinates-66203f57e4b4

Calculate distance, bearing and more between Latitude/Longitude points\
https://www.movable-type.co.uk/scripts/latlong.html

GPS Visualizer's coordinate calculators & distance tools\
https://www.gpsvisualizer.com/calculators

Scale multipliers for different unit of distance used in LatLonDistance\
See: https://en.wikipedia.org/wiki/Earth_radius

NOAA Latitude/Longitude Distance Calculator\
https://www.nhc.noaa.gov/gccalc.shtml

Request an Azimuthal Map\
https://ns6t.net/azimuth/azimuth.html

---
## Validtion:
Interactive grid ID map\
https://k7fry.com/grid/

https://www.karhukoti.com/Maidenhead-Grid-Square-Locator

https://www.chris.org/cgi-bin/finddis

https://dxcluster.ha8tks.hu/hamgeocoding/

Calculator that provides 8 character resolution\
https://www.hamradio.me/charts/maidenhead-calculator.php