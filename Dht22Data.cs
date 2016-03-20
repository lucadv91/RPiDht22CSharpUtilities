using SQLite;
using System;

[Table("dht22data")]
public class Dht22Data
{
		[PrimaryKey]
		[AutoIncrement]
		public int id { get; set; }
	
		[Ignore]	
		public bool IsValid
		{
			get { return this.temperature != 0 && this.humidity != 0; }
		}

		public float temperature{ get; protected set;}

		public float humidity { get; protected set;}
		
		public string timestamp {get; protected set; }
		
		public Dht22Data(float t, float h)
		{
			this.temperature = t;
			this.humidity = h;
			this.date = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
		}
}
