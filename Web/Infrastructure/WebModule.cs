using System;
using Ninject.Modules;
using Service;
using Service.Interfaces;

namespace Web.Infrastructure
{
	public class WebModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IAccountService>().To<AccountService>();
			Bind<ICategoryService>().To<CategoryService>();
		}
	} // class
} // namespace