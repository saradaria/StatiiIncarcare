using StatiiIncarcare.Models.DB;
using System;
using System.Runtime.Serialization;
 
namespace StatiiIncarcare.Models.ViewModels
{
	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class Point
	{
		public Point(string label, int y)
		{
			this.Label = label;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<int> Y = null;
	}
}