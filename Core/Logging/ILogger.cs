using System;

namespace Core.Logging
{
	public interface ILogger
	{
		void Info(string message);

		void Debug(string message);

		void Error(string message);

		void Trace(string message);
	}
}