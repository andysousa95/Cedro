using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cedro_WebApi.Controllers
{
    public class RestauranteListaController : Controller {
        
        //GET: RestauranteLista
        
        public ActionResult Index(){
            return View();
        }
    }
}