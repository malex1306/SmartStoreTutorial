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

namespace MyOrg.HelloWorld
{
    internal class Module : ModuleBase, IConfigurable, IActivatableWidget, IStarter
    {
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