# RPiDht22CSharpUtilities
Utilities in C# to obtain and save data from a DHT22 sensor from a RaspberryPi B+ 

This repository contains:
 - a C# wrapper to the WiringPi library (https://github.com/danriches/WiringPi.Net/)
 - the actual sensor reading logic ./lib/libdht22reader.c (adaptation from https://github.com/technion/lol_dht22)
 - SQLite database access library (https://github.com/praeclarum/sqlite-net)
 - a build and run script
  
Dependencies:
 - mono (tested on version 3.2.8)
 - WiringPi 
 - gcc
 - SQLite3
 - locale must be set to en_US (otherwise sqlite-net would not compile in my case)
	
To build you have to first create a suitable .config file in the root directory of the project. 
Then just execute ./build.sh and ./run.sh .
