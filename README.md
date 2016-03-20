# RPiDht22CSharpUtilities
Utilities in C# to obtain and save data to a SQLite database from a DHT22 sensor on a RaspberryPi B+ 

Repository external content:
 - C# wrapper to the WiringPi library (https://github.com/PEEU/WiringPi.Net/)
 - Sensor reading logic ./lib/libdht22reader.c (adaptation of https://github.com/technion/lol_dht22)
 - SQLite database access library (https://github.com/praeclarum/sqlite-net)

Dependencies:
 - mono (tested on version 3.2.8)
 - WiringPi 
 - gcc
 - SQLite3
 - locale must be set to en_US (or sqlite-net won't compile)
 
Building:
Check the settings in the default configuration file ConfigurationFile.config.
Then just execute ./build.sh and ./run.sh .
