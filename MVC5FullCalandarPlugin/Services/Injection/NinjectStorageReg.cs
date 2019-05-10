using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;
using Ninject.Modules;

namespace MVC5FullCalandarPlugin.Services.Injection
{
    public class NinjectStorageReg : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserDbSet>().To<TestDbSetUser>();
            Bind<IAuthenication>().To<Authenication>();
        }
    }
}