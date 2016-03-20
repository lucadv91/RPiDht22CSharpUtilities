#!/bin/bash

rm ./bin/*

gcc -lwiringPi -shared -Wall -I./lib -o ./bin/libdht22reader.so ./lib/libdht22reader.c

mkdir ./bin
cp ./lib/*.so ./bin
cp ./lib/*.dll ./bin
cp *.config ./bin

mcs -unsafe -r:System.Data.dll,System.Configuration.dll Dht22Console.cs Dht22Reader.cs WiringPi.cs Dht22SQLiteSaver.cs Dht22Data.cs SQLite.cs

mv ./*.exe ./bin

