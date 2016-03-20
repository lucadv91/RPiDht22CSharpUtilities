#!/bin/bash

mkdir ./bin
rm ./bin/*

gcc -lwiringPi -shared -Wall -I./lib -o ./bin/libdht22reader.so ./lib/libdht22reader.c

#cp ./lib/*.so ./bin
#cp ./lib/*.dll ./bin
cp ConfigurationFile.config ./bin/Dht22DataReader.exe.config

mcs -unsafe -r:System.Data.dll,System.Configuration.dll -out:./bin/Dht22DataReader.exe ConsoleProgram.cs Dht22Reader.cs ./lib/WiringPi.cs Dht22SQLiteSaver.cs Dht22Data.cs ./lib/sqlite-net/src/SQLite.cs

#mv ./*.exe ./bin

