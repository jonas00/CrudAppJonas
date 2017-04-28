using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using CrudApp_Core.Interface;
using CrudApp_Infrastructure;

namespace CrudApp_Web
{
    public class DependencyConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<NoteRepository>().As<INoteRepository>();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}