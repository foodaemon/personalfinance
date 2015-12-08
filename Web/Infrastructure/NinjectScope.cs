using System;
using System.Web.Http.Dependencies;
using Ninject.Syntax;
using Ninject;

namespace Web.Infrastructure
{
	public class NinjectScope : IDependencyScope
	{
		/// <summary>
		/// The resolver
		/// </summary>
		IResolutionRoot resolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="NinjectScope"/> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		public NinjectScope(IResolutionRoot resolver)
		{
			this.resolver = resolver;
		}

		/// <summary>
		/// Retrieves a service from the scope.
		/// </summary>
		/// <param name="serviceType">The service to be retrieved.</param>
		/// <returns>
		/// The retrieved service.
		/// </returns>
		/// <exception cref="System.ObjectDisposedException">this;This scope has been disposed</exception>
		public object GetService(Type serviceType)
		{
			if (resolver == null)
				throw new ObjectDisposedException("this", "This scope has been disposed");

			return resolver.TryGet(serviceType);
		}

		/// <summary>
		/// Retrieves a collection of services from the scope.
		/// </summary>
		/// <param name="serviceType">The collection of services to be retrieved.</param>
		/// <returns>
		/// The retrieved collection of services.
		/// </returns>
		/// <exception cref="System.ObjectDisposedException">this;This scope has been disposed</exception>
		public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
		{
			if (resolver == null)
				throw new ObjectDisposedException("this", "This scope has been disposed");

			return resolver.GetAll(serviceType);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			var disposable = resolver as IDisposable;
			if (disposable != null)
				disposable.Dispose();

			resolver = null;
		}

	} // class
}