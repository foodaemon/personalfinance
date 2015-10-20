using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Web
{
	public class LoggerConfig
	{
		public static void SetUp()
		{
			var config = new LoggingConfiguration();

			// add targets to config
			var consoleTarget = new ColoredConsoleTarget();
			config.AddTarget("console", consoleTarget);

			var fileTarget = new FileTarget();
			config.AddTarget("file", fileTarget);

			// set up target properties
			var messageLayout = "${date:format=yyyy-mm-dd HH\\:MM\\:ss} ${logger} ${message}";

			consoleTarget.Layout = messageLayout;

			fileTarget.FileName = "${basedir}/logs/log.txt";
			fileTarget.Layout = messageLayout;

			// rules
			var consoleTargetRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
			config.LoggingRules.Add(consoleTargetRule);

			var fileTargetRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
			config.LoggingRules.Add(fileTargetRule);

			// activate config
			LogManager.Configuration = config;
		}
	}
}