using System;
using System.Web.Http.Routing;
using System.Collections.Generic;

namespace Web.Infrastructure
{
	internal class VersionedRoute : RouteFactoryAttribute
	{
		/// <summary>
		/// Gets the allowed version.
		/// </summary>
		public int AllowedVersion { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionedRoute"/> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="allowedVersion">The allowed version.</param>
		public VersionedRoute(string template, int allowedVersion)
			: base(template)
		{
			AllowedVersion = allowedVersion;
		}

		/// <summary>
		/// Gets the route constraints, if any; otherwise null.
		/// </summary>
		/// <returns>The route constraints, if any; otherwise null.</returns>
		public override IDictionary<string, object> Constraints
		{
			get
			{
				var constraints = new HttpRouteValueDictionary();
				constraints.Add("version", new VersionConstraint(AllowedVersion));
				return constraints;
			}
		}
	} // class
}