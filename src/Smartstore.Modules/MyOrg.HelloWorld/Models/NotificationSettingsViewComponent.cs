using Microsoft.AspNetCore.Mvc;
using Smartstore.Core.Configuration;
using Smartstore.Web.Components;
using MyOrg.HelloWorld.Models;
using Smartstore.Core.Content.Menus; 

namespace MyOrg.HelloWorld.Components
{
    public class NotificationSettingsViewComponent : SmartViewComponent
    {
        private readonly ISettingService _settingService;

        public NotificationSettingsViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IViewComponentResult Invoke()
        {
            var model = _settingService.LoadingSettings<NotificationSettingsModel>();
            return View(model);
        }
    }
}