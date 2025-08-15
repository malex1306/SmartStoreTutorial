using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Smartstore.Web.Modelling;


namespace MyOrg.HelloWorld.Models
{
    [LocalizedDisplay("Plugins.MyOrg.HelloWorld.NotificationSettings.")]
    public class NotificationSettingsModel : ModelBase
    {
        [LocalizedDisplay("*NumberOfDaysToDisplay")]
        [UIHint("Int32")]
        public int NumberOfDaysToDisplay { get; set; } = 7;
    }
}
