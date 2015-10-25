using System;
using Ninject.Modules;
using Data;
using Core.Domains;

namespace Service
{
	public class ServiceModule: NinjectModule
	{
		public override void Load()
		{
			Bind<IRepository<Account>>().To<Repository<Account>>();
			Bind<IRepository<Category>>().To<Repository<Category>>();
		}
	} // class
} // namespace