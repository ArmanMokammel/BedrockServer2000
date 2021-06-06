using System;
using System.Diagnostics;

namespace BedrockServer2000
{
	public class Events
	{
		public static void bedrockServerProcess_Exited(object sender, EventArgs e)
		{

		}

		public static void bedrockServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine($"Error ocurred: {e.Data}");
		}

		public static void bedrockServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}
	}
}