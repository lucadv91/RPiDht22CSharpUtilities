using System.Runtime.InteropServices;
using System;

internal class Dht22Reader
{
	[DllImport("libdht22reader.so", EntryPoint = "readDHT22Data")]     
	private static extern IntPtr _ReadDHT22Data(int inputPin);

	private int readPin;
	
	public Dht22Reader(int readPin)
	{
		this.readPin = readPin;
	}

	public Dht22Data ReadDHT22Data()
	{
		var returnArray = new float[2];
		var pointer = _ReadDHT22Data(this.readPin);
		Marshal.Copy(pointer, returnArray, 0, 2);
		return new Dht22Data (t: returnArray[0], h: returnArray[1]);
	}

}
