using Autofac;
using Smartstore.Core.Content.Menus;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Builders;
using Smartstore.Engine.Modularity;
using Smartstore.Http;
using System.Threading.Tasks;
using Smartstore.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Smartstore.Core.Common;
using Smartstore.Core.Data;
using Smartstore.Core.DataExchange.Export;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Smartstore; // muss ins using damit EachAsync funktioniert

namespace MyOrg.HelloWorld
{
    public class Module : ModuleBase, IConfigurable, IActivatableWidget, IStarter
    {

        private readonly SmartDbContext _db;
        private readonly IExportProfileService _exportProfileService;

        public Module()
        {
            
        }
        public bool Matches(IApplicationContext appContext) => appContext.IsInstalled;

        public void ConfigureContainer(ContainerBuilder builder, IApplicationContext appContext)
        {
            builder.RegisterType<AdminMenu>().As<IMenuProvider>().InstancePerLifetimeScope();
        }

        public RouteInfo GetConfigurationRoute()
            => new("Configure", "HelloWorldAdmin", new { area = "Admin" });

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            await ImportLanguageResourcesAsync();
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Dynamic LINQ wird hier verwendet, um mehrere Provider-Systemnamen in einer Abfrage zu filtern
            var profiles = await _db.ExportProfiles
                .Include(x => x.Deployments)
                .Include(x => x.Task)
                .Where("ProviderSystemName == @0 || ProviderSystemName == @1",
                "MyOrg.HelloWorld.ProductCsv", "MyOrg.HelloWorld.ProductXml").ToListAsync(); 
            await profiles.EachAsync(x => _exportProfileService.DeleteExportProfileAsync(x, true));
            await DeleteLanguageResourcesAsync();
            await base.UninstallAsync();


        }

        public Widget GetDisplayWidget(string widgetZone, object model, int storeId) =>
            new ComponentWidget(typeof(HelloWorldViewComponent), new { widgetZone, model, storeId });

        public string[] GetWidgetZones()
        {
            return new string[] { "target_widget_zone_name" };
        }

        public int Order => 0;

        public void ConfigureServices(IServiceCollection services, IApplicationContext appContext) { }

        public void ConfigureMvc(IMvcBuilder mvcBuilder, IServiceCollection services, IApplicationContext appContext) { }

        public void BuildPipeline(RequestPipelineBuilder builder) { }

        public void MapRoutes(EndpointRoutingBuilder builder) { }

        public string Key => "MyOrg.HelloWorld";

        public string[] DependsOn => new string[0];
    }
}