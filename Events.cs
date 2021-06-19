using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace BedrockServer2000
{
	public class Events
	{
		public static void AutoBackupEveryXTimer_TIck(object args)
		{
			// check if the configs are correct, cancel the backup if found any error
			if (!Directory.Exists(Program.serverConfigs.WorldPath))
			{
				CustomConsoleColor.SetColor_Error();
				Console.ResetColor();
				return;
			}
			if (!Directory.Exists(Program.serverConfigs.BackupPath))
			{
				CustomConsoleColor.SetColor_Error();
				Console.ResetColor();
				return;
			}
			if (Program.serverConfigs.BackupLimit <= 0)
			{
				CustomConsoleColor.SetColor_Error();
				Console.ResetColor();
				return;
			}

			Backup.PerformBackup(false);
		}

		public static void BedrockServerProcess_Exited(object sender, EventArgs e)
		{
			Program.serverConfigs.ServerRunning = false;
			Program.serverConfigs.ExitCompleted = true;

			Program.ExitTImeoutTImer.Change(Timeout.Infinite, Timeout.Infinite);

			CustomConsoleColor.SetColor_Success();
			Console.WriteLine($"{Timing.LogDateTime()} Server stopped.");
			Console.ResetColor();

			if (Program.serverConfigs.LoadRequest)
			{
				Backup.LoadBackup();
				Program.serverConfigs.LoadRequest = false;
			}
			else if (Program.serverConfigs.ExitRequest)
			{
				CustomConsoleColor.SetColor_Success();
				Console.WriteLine($"{Timing.LogDateTime()} Server wrapper stopped.");
				Console.ResetColor();
				Environment.Exit(0);
			}
		}

		public static void BedrockServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == null) return;
			else
			{
				string outputData = e.Data;
				if (outputData.StartsWith("NO LOG FILE! - ")) outputData = outputData.Remove(0, 15);
				Console.WriteLine($"{Timing.LogDateTime()} {outputData}");
			}

			if (e.Data.Contains("[INFO] Player connected: "))
			{
				if (Program.serverConfigs.AutoBackupEveryX && !Program.serverConfigs.PlayerJoinSinceLastBackup) Program.serverConfigs.PlayerJoinSinceLastBackup = true;

				string playerName = e.Data.Remove(0, e.Data.IndexOf("[INFO] PLayer connected: ") + 25).Split(',', StringSplitOptions.RemoveEmptyEntries)[0].Trim();
				if (!Program.serverConfigs.playerList.Exists(x => x == playerName))
				{
					Program.serverConfigs.playerList.Add(playerName);
					Program.serverConfigs.playerCount += 1;
				}
				foreach (string name in Program.serverConfigs.BanList)
				{
					if (name == playerName)
					{
						Console.WriteLine($"{Timing.LogDateTime()} Player name \"{playerName}\" found in ban list.");
						AutoKick(playerName, 5000);
						break;
					}
				}
			}
			else if (e.Data.Contains("[INFO] Player disconnected: "))
			{
				string playerName = e.Data.Remove(0, e.Data.IndexOf("[INFO] PLayer connected: ") + 25).Split(',', StringSplitOptions.RemoveEmptyEntries)[0].Trim();
				if (Program.serverConfigs.playerList.Exists(x => x == playerName))
				{
					Program.serverConfigs.playerList.Remove(playerName);
					if (Program.serverConfigs.playerCount > 0) Program.serverConfigs.playerCount -= 1;
				}
			}
		}

		private static async void AutoKick(string name, int delay)
		{
			Thread.Sleep(delay);
			Program.serverInputStream.WriteLine($"kick {name}");
		}

		public static void OnExit(object sender, EventArgs e)
		{
			if (Program.serverConfigs.ServerRunning && !Program.serverProcess.HasExited)
			{
				Program.serverProcess.Kill();
			}
		}

		public static void ExitTImeoutTImer_Tick(object args)
		{
			if (!Program.serverConfigs.ExitCompleted)
			{
				Console.WriteLine($"{Timing.LogDateTime()} Exit timed out.");
				Program.serverProcess.Kill();
				Console.WriteLine($"{Timing.LogDateTime()} Force killed server process.");
				Program.serverConfigs.ExitCompleted = true;

				Program.ExitTImeoutTImer.Change(Timeout.Infinite, Timeout.Infinite);
			}
		}
	}
}