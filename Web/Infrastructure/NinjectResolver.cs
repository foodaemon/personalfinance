using System.Web.Http.Dependencies;
using Ninject;

namespace Web.Infrastructure
{
	public class NinjectResolver : NinjectScope, IDependencyResolver
	{
		/// <summary>
		/// The _kernel
		/// </summary>
		private readonly IKernel _kernel;

		/// <summary>
		/// Initializes a new instance of the <see cref="NinjectResolver"/> class.
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		public NinjectResolver(IKernel kernel)
			: base(kernel)
		{
			_kernel = kernel;
		}

		/// <summary>
		/// Starts a resolution scope.
		/// </summary>
		/// <returns>
		/// The dependency scope.
		/// </returns>
		public IDependencyScope BeginScope()
		{
			return new NinjectScope(_kernel.BeginBlock());
		}

	} // class
}