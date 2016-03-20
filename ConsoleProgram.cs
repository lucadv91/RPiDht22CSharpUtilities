using System;
using WiringPi;
using System.Configuration;

class Dht22Console 
{
	public static void Main (string[] args)
	{
		Init.WiringPiSetup();
		int currentReadAttempt = 0;
		int maxReadAttempts = 20;
		Dht22Data data;

		var reader = new Dht22Reader(Int32.Parse(ConfigurationManager.AppSettings["ReadPin"]));	
		do
		{
			if ( currentReadAttempt > 0 )
				Timing.delay(100);
				
			data = reader.ReadDHT22Data();
			currentReadAttempt++;
		} while (!data.IsValid && currentReadAttempt <= maxReadAttempts);	

		var saver = new Dht22SQLiteSaver(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
		saver.SaveData(data);
	}
}
