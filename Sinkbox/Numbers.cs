using System;
using System.Globalization;

namespace Sinkbox
{
	public static class Numbers
	{
		public static double Scale(double val, double originalMin, double originalMax, double newMin, double newMax)
			=> ((val - originalMin) / (originalMax - originalMin) * (newMax - newMin)) + newMin;

		public static float Scale(float val, float originalMin, float originalMax, float newMin, float newMax)
			=> (float) Scale((double) val, originalMin, originalMax, newMin, newMax);

		public static Half Scale(Half val, Half originalMin, Half originalMax, Half newMin, Half newMax)
			=> (Half) Scale((double) val, (double) originalMin, (double) originalMax, (double) newMin, (double) newMax);

		public static decimal Scale(decimal val, decimal originalMin, decimal originalMax, decimal newMin,
									decimal newMax)
			=> ((val - originalMin) / (originalMax - originalMin) * (newMax - newMin)) + newMin;

		public static string LeftPad(decimal num, int len, bool useZero = false)
			=> num.ToString(CultureInfo.CurrentCulture).PadLeft(len, useZero ? '0' : ' ');

		public static string LeftPad(double num, int len, bool useZero = false)
			=> num.ToString(CultureInfo.CurrentCulture).PadLeft(len, useZero ? '0' : ' ');

		public static string LeftPad(long num, int len, bool useZero = false)
			=> num.ToString(CultureInfo.CurrentCulture).PadLeft(len, useZero ? '0' : ' ');
		public static string LeftPad(ulong num, int len, bool useZero = false)
			=> num.ToString(CultureInfo.CurrentCulture).PadLeft(len, useZero ? '0' : ' ');
		
		public static string LeftPad(float  num, int len, bool useZero = false) => LeftPad((double) num, len, useZero);
		public static string LeftPad(Half   num, int len, bool useZero = false) => LeftPad((double) num, len, useZero);
		public static string LeftPad(int    num, int len, bool useZero = false) => LeftPad((long) num,   len, useZero);
		public static string LeftPad(short  num, int len, bool useZero = false) => LeftPad((long) num,   len, useZero);
		public static string LeftPad(uint   num, int len, bool useZero = false) => LeftPad((ulong) num,  len, useZero);
		public static string LeftPad(ushort num, int len, bool useZero = false) => LeftPad((ulong) num,  len, useZero);
	}
}