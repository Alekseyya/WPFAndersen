using DAL.Repositories;
using DAL.Repositories.Base;
using Ninject;

namespace WPF_Andersen.IoC
{
     public static class IoC
    {
        private static IKernel _kernel;

        static IoC()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IClientRepository>().To<ClientRepository>();
            //_kernel.bind.to.....

        }
        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
