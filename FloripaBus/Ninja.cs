using System;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace FloripaBus
{
	public class Ninja
	{		
		private static IKernel _kernel = new StandardKernel();	

		public static void LoadModule(NinjectModule module)
		{
			_kernel.Load (module);
		}

		public static T Get<T>(params IParameter[] parameters)
		{
			return _kernel.Get<T>(parameters);
		}
	}
}

