using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace DigitalVaccination
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            System.Web.Mvc.ControllerBuilder.Current.DefaultNamespaces.Add("DigitalVaccination.ApiController");
            
            RouteTable.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                    ).RouteHandler = new SessionStateRouteHandler();

        }
    }

    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionableControllerHandler(RouteData routeData)
            : base(routeData)
        { }
    }
    public class SessionStateRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionableControllerHandler(requestContext.RouteData);
        }
    }  
}
