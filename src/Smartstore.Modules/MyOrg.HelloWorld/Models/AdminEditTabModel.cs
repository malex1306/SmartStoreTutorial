using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartstore.Web.Modelling;

namespace MyOrg.HelloWorld.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.HelloWorld.AdminEditTab.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}
