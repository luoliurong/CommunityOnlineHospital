using System;
using System.Runtime.Serialization;

namespace COHS.AppServices
{
	[Serializable]
	public class ParseXMLException : Exception
	{
		public string XmlSource { get; set; }
		public ParseXMLException() { }
		public ParseXMLException(string source)
		{
			XmlSource = source;
		}
		public ParseXMLException(string source, Exception innerException):base(source, innerException)
		{

		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("XmlSource", XmlSource);
		}
	}
}
