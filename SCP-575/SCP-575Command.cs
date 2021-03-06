﻿using Smod2.Commands;
using MEC;

namespace SCP575
{
	public class Scp575Command : ICommandHandler
	{
		private readonly Scp575 plugin;
		public Scp575Command(Scp575 plugin) => this.plugin = plugin;

		public string GetCommandDescription() => "";

		public string GetUsage() =>
			"SCP575 Commands \n" +
			"[SCP575 / 575] HELP \n" +
			"SCP575 TOGGLE \n" +
			"SCP575 ENABLE \n" +
			"SCP575 DISABLE \n" +
			"SCP575 ANNOUNCE ON \n" +
			"SCP575 ANNOUNCE OFF \n";

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (args.Length <= 0) return new[] { GetUsage() };
			if (!plugin.Functions.IsAllowed(sender)) return new string[] { "Permission denied." };
			switch (args[0].ToLower())
			{
				case "help":
					return new string[]
					{
						"SCP575 Command List",
						"SCP575 toggle - Toggles a manual SCP-575 on/off.",
						"SCP575 enable - Enables timed SCP-575 events.",
						"SCP575 disable - Disables timed SCP-575 events.",
						"SCP575 announce on - Enables CASSIE announcements for events.",
						"SCP575 announce off - Disables CASSIE announcements for events."
					};
				case "toggle":
					{
						plugin.Functions.ToggleBlackout();

						return new string[] { "Manual 575 event toggled." };
					}
				case "enable":
					{
						plugin.Functions.EnableBlackouts();

						return new string[] { "Timed events enabled." };
					}
				case "disable":
					{
						plugin.Functions.DisableBlackouts();

						return new string[] { "Timed events disabled." };
					}
				case "announce":
					{
						if (args.Length <= 1) return new string[] { "No arguments supplied." };

						switch (args[1].ToLower())
						{
							case "on":
								{
									plugin.Functions.EnableAnnounce();

									return new string[] { "Announcements enabled." };
								}
							case "off":
								{
									plugin.Functions.DisableAnnounce();

									return new string[] { "Announcements disabled." };
								}
							default:
								{
									return new string[] { "Invalid argument." };
								}
						}
					}
				case "halt":
					{
						foreach (CoroutineHandle handle in plugin.Coroutines) Timing.KillCoroutines(handle);
						plugin.Coroutines.Clear();

						return new string[] { "Halted all active Coroutines." };
					}
				default:
					{
						return new string[] { GetUsage() };
					}
			}
		}
	}
}