using System.Collections.Generic;
using System.Diagnostics;

namespace Sinkbox
{
	public static class Process
	{
		public static System.Diagnostics.Process? StartRedirect(string filename)
			=> StartRedirect(new ProcessStartInfo(filename));

		public static System.Diagnostics.Process? StartRedirect(string filename, string arguments)
			=> StartRedirect(new ProcessStartInfo(filename, arguments));

		public static System.Diagnostics.Process? StartRedirect(string filename, IEnumerable<string> arguments)
		{
			var startInfo = new ProcessStartInfo(filename);
			foreach (var argument in arguments) startInfo.ArgumentList.Add(argument);
			return StartRedirect(startInfo);
		}

		public static System.Diagnostics.Process? StartRedirect(string filename, params string[] arguments)
			=> StartRedirect(filename, (IEnumerable<string>) arguments);

		public static System.Diagnostics.Process? StartRedirect(ProcessStartInfo info)
		{
			info.RedirectStandardInput  = true;
			info.RedirectStandardOutput = true;
			info.RedirectStandardError  = true;
			return System.Diagnostics.Process.Start(info);
		}
	}
}