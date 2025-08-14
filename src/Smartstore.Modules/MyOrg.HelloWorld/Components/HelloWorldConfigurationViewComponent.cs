using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.HelloWorld.Models;
using Smartstore.Web.Components;

namespace MyOrg.HelloWorld.Components
{
    public class HelloWorldConfigurationViewComponent : SmartViewComponent
    {
        public IViewComponentResult Invoke(object data)
        {
            var model = data as ProfileConfigurationModel;
            return View(model);
        }
    }
}
