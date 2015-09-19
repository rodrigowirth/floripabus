using System;
using Ninject;
using Ninject.Modules;

namespace FloripaBus
{
	public class Ninja
	{
		private static Object _lock = new Object();
		private static IKernel _kernel = new StandardKernel();	

		public static void LoadModule(NinjectModule module)
		{
			_kernel.Load (module);
		}

		public static T Get<T>()
		{
			return _kernel.Get<T>();
		}
	}
}

