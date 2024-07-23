using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Presentacion_Web.App_Start;

namespace Presentacion_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Registrar el servicio ApiService
            DependencyResolver.SetResolver(new MyDependencyResolver());
        }
    }
}
