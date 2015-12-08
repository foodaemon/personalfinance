using System;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace Web.Infrastructure
{
	/// <summary>
	/// A Constraint implementation that matches an HTTP header against an expected version value.
	/// </summary>
	internal class VersionConstraint : IHttpRouteConstraint
	{
		/// <summary>
		/// The version header name
		/// </summary>
		public const string VersionHeaderName = "api-version";

		/// <summary>
		/// The default version
		/// </summary>
		private const int DefaultVersion = 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionConstraint"/> class.
		/// </summary>
		/// <param name="allowedVersion">The allowed version.</param>
		public VersionConstraint(int allowedVersion)
		{
			AllowedVersion = allowedVersion;
		}

		/// <summary>
		/// Gets the allowed version.
		/// </summary>
		public int AllowedVersion
		{
			get;
			private set;
		}

		/// <summary>
		/// Determines whether this instance equals a specified route.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="route">The route to compare.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="values">A list of parameter values.</param>
		/// <param name="routeDirection">The route direction.</param>
		/// <returns>
		/// True if this instance equals a specified route; otherwise, false.
		/// </returns>
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (routeDirection == HttpRouteDirection.UriResolution)
			{
				int version = GetVersionHeader(request) ?? DefaultVersion;
				if (version == AllowedVersion)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Gets the version header.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		private int? GetVersionHeader(HttpRequestMessage request)
		{
			string versionAsString;
			IEnumerable<string> headerValues;
			if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
			{
				versionAsString = headerValues.First();
			}
			else
			{
				return null;
			}

			int version;
			if (versionAsString != null && Int32.TryParse(versionAsString, out version))
			{
				return version;
			}

			return null;
		}
	} // class
}