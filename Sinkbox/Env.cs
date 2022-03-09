using System;
// ReSharper disable InconsistentNaming

namespace Sinkbox
{
	public static class Env
	{
		public static OSType GetOS()
		{
			var dotnetOS = Environment.OSVersion.Platform;
			switch (dotnetOS)
			{
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.Win32NT:
				case PlatformID.WinCE:
					return OSType.Windows;
				case PlatformID.MacOSX:
					// only happens on silverlight - on netcore it thinks macos is unix
					return OSType.MacOS;
				case PlatformID.Xbox:
				case PlatformID.Other:
					return OSType.Other;
				
				case PlatformID.Unix:
					var unameProc = Process.StartRedirect("uname");
					var uname = unameProc!.StandardOutput.ReadToEnd();
					return uname switch
					{
						"Linux" => OSType.Linux,
						// if anyone runs this successfully on ios ill change this
						"Darwin"  => OSType.MacOS,
						"OpenBSD" => OSType.OpenBSD,
						"FreeBSD" => OSType.FreeBSD,
						"NetBSD"  => OSType.NetBSD,
						_ => uname.EndsWith("bsd", StringComparison.InvariantCultureIgnoreCase)
								 ? OSType.BSD
								 : OSType.Unix
					};

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static bool IsBSD(this OSType osType)
			=> osType is OSType.FreeBSD or OSType.OpenBSD or OSType.NetBSD or OSType.BSD;
	}

	public enum OSType
	{
		Windows,
		MacOS,
		Linux,
		OpenBSD,
		FreeBSD,
		NetBSD,
		BSD,
		Unix,
		Other
	}
}