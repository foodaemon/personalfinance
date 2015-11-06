using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace Core.Logging
{
	public class Logger<T> : ILogger
	{
		private static volatile Core.Logging.Logger<T> _instance;
		private static object syncRoot = new Object();

		private static NLog.ILogger _logger;
		//private const string DefaultLogger = "AppLogger";

		static Logger()
		{
			var config = new LoggingConfiguration();

			// add targets to config
			var consoleTarget = new ColoredConsoleTarget();
			config.AddTarget("console", consoleTarget);

			var fileTarget = new FileTarget();
			config.AddTarget("file", fileTarget);

			// set up target properties (ISO 8601 date format)
			var messageLayout = "${date:format=yyyy-MM-ddTHH\\:mm\\:ss.fff} ${logger} ${message}";

			consoleTarget.Layout = messageLayout;

			fileTarget.FileName = "${basedir}/logs/app.log";
			fileTarget.Layout = messageLayout;

			// rule

			// console traget rule
			var consoleTargetRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
			config.LoggingRules.Add(consoleTargetRule);

			// Set up asynchronous logging
			var asyncWrapper = new AsyncTargetWrapper(fileTarget);
			config.AddTarget("async", asyncWrapper);

			// file target rule
			//var fileTargetRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
			var fileTargetRule = new LoggingRule("*", LogLevel.Debug, asyncWrapper);
			config.LoggingRules.Add(fileTargetRule);

			// activate config
			LogManager.Configuration = config;
			_logger = LogManager.GetLogger(typeof(T).FullName);

		}

		public static Logger<T> GetInstance
		{
			get
			{
				if (_instance == null)
				{
					lock (syncRoot)
					{
						if (_instance == null)
							_instance = new Core.Logging.Logger<T>();
					}
				}
				return _instance;
			}
		}

		public void Debug(string message)
		{
			_logger.Log(LogLevel.Debug, message);
		}

		public void Error(string message)
		{
			_logger.Log(LogLevel.Error, message);
		}

		public void Info(string message)
		{
			_logger.Log(LogLevel.Info, message);
		}

		public void Trace(string message)
		{
			_logger.Log(LogLevel.Trace, message);
		}
	} // class
} // namespace