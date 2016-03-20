using System;
using WiringPi;

class Dht22Console 
{
	public static void Main (string[] args)
	{
		Init.WiringPiSetup();
		int currentReadAttempt = 0;
		int maxReadAttempts = 20;
		Dht22Data data;

		var reader = new Dht22Reader(7);	
		do
		{
			if ( currentReadAttempt > 0 )
				Timing.delay(100);
				
			data = reader.ReadDHT22Data();
			currentReadAttempt++;
		} while (!data.IsValid && currentReadAttempt <= maxReadAttempts);	

		var saver = new Dht22SQLiteSaver("Data Source=/home/vlad/sensors_data.db;Version=3;");
		saver.SaveData(data);
	}
}
