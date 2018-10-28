using System;
using System.Runtime.Serialization;

namespace COHS.AppServices
{
	[Serializable]
	public class SessionTimeoutException : Exception
	{
		private string sessionName;
		public string SessionName
		{
			get { return this.sessionName; }
			set { this.sessionName = value; }
		}
		public SessionTimeoutException() { }
		public SessionTimeoutException(string message) : base(message) { }
		public SessionTimeoutException(string message, string sessionName) : base(message)
		{
			this.sessionName = sessionName;
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("SessionName", sessionName);
		}
	}
}