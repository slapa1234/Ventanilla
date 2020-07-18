using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace SisATU.Util
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VerificarAcceso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            String action = filterContext.ActionDescriptor.ActionName.ToUpper();
            String controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
            // Validar si se quiere acceder al Login
            if (action.Equals("INICIO") && controller.Equals("ACCESO"))
            {
                // Validar si hay algún usuario con sesion activa
                if (filterContext.HttpContext.Session["NRO_DOCUMENTO"] != null)
                {
                    RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("Index", "Electronico");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            //enviarformulario
            else if (action.Equals("PROCEDIMIENTONOTUPA") && controller.Equals("ELECTRONICO"))
            {
                return;
            }
            else if (action.Equals("INGRESAR") && controller.Equals("ACCESO"))
            {
                return;
            }

            else if (action.Equals("GETMODALIDADES") && controller.Equals("MODALIDADSERVICIO"))
            {
                return;
            }
            else if (action.Equals("GETTRAMITEBYMODALIDAD") && controller.Equals("MODALIDADSERVICIO"))
            {
                return;
            }
            else if (action.Equals("GETPLACASVECINOSEMP") && controller.Equals("PLACASCONADIS"))
            {
                return;
            }
            else if (action.Equals("REGISTRARCUENTA") && controller.Equals("ACCESO"))
            {
                return;
            }
            else if (action.Equals("CONSULTADNI") && controller.Equals("CONDUCTOR"))
            {
                return;
            }
            else if (action.Equals("CONSULTACE") && controller.Equals("CONDUCTOR"))
            {
                return;
            }
            else if (action.Equals("CONSULTAPTP") && controller.Equals("CONDUCTOR"))
            {
                return;
            }
            else if (action.Equals("CONSULTARUC") && controller.Equals("EMPRESA"))
            {
                return;
            }
            else if (action.Equals("REGISTRARADMINISTRADO") && controller.Equals("ACCESO"))
            {
                return;
            }
            else if (action.Equals("RECUPERARCONTRASENIA") && controller.Equals("ACCESO"))
            {
                return;
            }
            else if (action.Equals("INDEX") && controller.Equals("BACKOFFICE"))
            {
                return;
            }
            else if (action.Equals("BUSCAR_PAG") && controller.Equals("BACKOFFICE"))
            {
                return;
            }
            else if (controller.Equals("BACKOFFICE"))
            {
                return;
            }
            else
            {
                if (filterContext.HttpContext.Session["NRO_DOCUMENTO"] == null)
                {
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        if (action.Equals("VALIDARACCESO") && controller.Equals("ACCESO"))
                        {
                            // Validar si hay algún usuario con sesion activa
                            if (filterContext.HttpContext.Session["USUARIO"] != null)
                            {
                                RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("INICIO", "ACCESO");
                                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                            }
                        }
                        else
                        {
                            RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("INICIO", "ACCESO");
                            filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        }
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/../");
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private RouteValueDictionary ObtenerRutaRedireccionamiento(string action, string controller)
        {
            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            redirectTargetDictionary.Add("controller", controller);
            redirectTargetDictionary.Add("action", action);
            return redirectTargetDictionary;
        }
    }
}