using System;
using System.Diagnostics;
using System.Threading;

namespace AutoStarter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args == null || args.Length == 0)
			{
				Console.WriteLine("Parameter 1 : Process Name");
				return;
			}

			string processName = args[0];

			Process[] processes = Process.GetProcessesByName(processName);

			if (processes == null || processes.Length == 0)
			{
				Console.WriteLine("Process is not running");
				return;
			}

			string path = processes[0].MainModule.FileName;

			while(true)
			{
				Thread.Sleep(10000);

				processes = Process.GetProcessesByName(processName);

				if (processes != null && processes.Length != 0)
					continue;

				Console.WriteLine(DateTime.Now + "Process [" + processName + "] has been closed, going to run it");

				Process p = new Process();
				p.StartInfo.FileName = path;
				p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				p.Start();
			}
		}
	}
}
