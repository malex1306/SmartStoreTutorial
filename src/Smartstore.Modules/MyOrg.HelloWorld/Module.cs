using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smartstore;
using Smartstore.Core.Content.Menus;
using Smartstore.Core.Data;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Widgets;
using Smartstore.Engine;
using Smartstore.Engine.Builders;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace MyOrg.HelloWorld
{
    public class Module : ModuleBase, IConfigurable, IActivatableWidget, IStarter
    {
        private readonly SmartDbContext _db;
        private readonly IExportProfileService _exportProfileService;

        public Module() { }

        // Abhängigkeiten werden über den Konstruktor injiziert
        public Module(SmartDbContext db, IExportProfileService exportProfileService)
        {
            _db = db;
            _exportProfileService = exportProfileService;
        }

        public void ConfigureServices(IServiceCollection services, IApplicationContext appContext)
        {
            
        }

        public bool Matches(IApplicationContext appContext) => appContext.IsInstalled;

        public void ConfigureContainer(ContainerBuilder builder, IApplicationContext appContext)
        {
            builder.RegisterType<AdminMenu>().As<IMenuProvider>().InstancePerLifetimeScope();
        }

        public RouteInfo GetConfigurationRoute()
        {
            return new RouteInfo("Configure", "HelloWorldAdmin", new { area = "Admin" });
        }

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            await ImportLanguageResourcesAsync();
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Hier wird der Konstruktor-injizierte Service verwendet
            var profiles = await _db.ExportProfiles
                .Include(x => x.Deployments)
                .Include(x => x.Task)
                .Where(x => x.ProviderSystemName == "MyOrg.HelloWorld.ProductCsv"
                         || x.ProviderSystemName == "MyOrg.HelloWorld.ProductXml")
                .ToListAsync();

            foreach (var profile in profiles)
            {
                await _exportProfileService.DeleteExportProfileAsync(profile, true);
            }

            await DeleteLanguageResourcesAsync();
            await base.UninstallAsync();
        }

        public Widget GetDisplayWidget(string widgetZone, object model, int storeId) =>
            new ComponentWidget(typeof(HelloWorldViewComponent), new { widgetZone, model, storeId });

        public string[] GetWidgetZones() => new string[] { "target_widget_zone_name" };
        public int Order => 0;

        public void ConfigureMvc(IMvcBuilder mvcBuilder, IServiceCollection services, IApplicationContext appContext) { }
        public void BuildPipeline(RequestPipelineBuilder builder) { }
        public void MapRoutes(EndpointRoutingBuilder builder) { }

        public string Key => "MyOrg.HelloWorld";
        public string[] DependsOn => new string[0];
    }
}