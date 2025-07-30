﻿using System.Threading.Tasks;
using MyOrg.HelloWorld.Models;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Events;
using Smartstore.Web.Modelling;
using Smartstore.Web.Rendering.Events;

namespace MyOrg.HelloWorld
{
    public class Events : IConsumer
    {
        public async Task HandleEventAsync(TabStripCreated eventMessage)
        {
            var tabStripName = eventMessage.TabStripName;

            if (tabStripName == "product-edit")
            {
                var entityId = ((TabbableModel)eventMessage.Model).Id;

                // Add a custom tab
                await eventMessage.TabFactory.AppendAsync(builder => builder
                    .Text("My Tab")
                    .Name("tab-MyTab")
                    .Icon("star", "bi")
                    .LinkHtmlAttributes(new { data_tab_name = "MyTab" })
                    .Action("AdminEditTab", "HelloWorldAdmin", new { entityId })
                    .Ajax());
            }
        }

        public async Task HandleEventAsync(ModelBoundEvent message, SmartDbContext db)
        {
            if (!message.BoundModel.CustomProperties.ContainsKey("MyTab"))
                return;

            if (message.BoundModel.CustomProperties["MyTab"] is not AdminEditTabModel model)
                return;

            var product = await db.Products.FindByIdAsync(model.EntityId);
            product.GenericAttributes.Set("HelloWorldMyTabValue", model.MyTabValue);

            await db.SaveChangesAsync();
        }
    }
}