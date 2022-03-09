using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sinkbox
{
	public static class Streams
	{
		public static Stream Create(string obj, Encoding? enc = null) => new MemoryStream((enc ?? Encoding.Default).GetBytes(obj));
		public static Stream Create(bool   obj) => new MemoryStream(new[] { (byte) (obj ? 1 : 0) });
		public static Stream Create(char   obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(double obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(float  obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(short  obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(int    obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(long   obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(ushort obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(uint   obj) => new MemoryStream(BitConverter.GetBytes(obj));
		public static Stream Create(ulong  obj) => new MemoryStream(BitConverter.GetBytes(obj));

		public static byte[] ReadAll(this Stream stream)
		{
			var  working  = new List<byte>();
			int? previous = null;
			
			while (stream.CanRead && previous != -1)
			{
				if (previous.HasValue)
					working.Add((byte) previous.Value);

				previous = stream.ReadByte();
			}

			return working.ToArray();
		}

		public static string ReadAllString(this Stream stream)
		{
			var bytes = stream.ReadAll().ToArray();
			return Encoding.Default.GetString(bytes);
		}
	}
}