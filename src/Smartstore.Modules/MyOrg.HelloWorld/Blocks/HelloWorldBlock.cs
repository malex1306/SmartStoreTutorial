using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smartstore.Core.Content.Blocks;
using Smartstore.Core.Widgets;
using Smartstore.Web.Modelling;
using Smartstore.Web.Models.Catalog;

namespace MyOrg.HelloWorld.Blocks
{

    [Block("helloworld", FriendlyName = "Hello World", Icon = "fa fa-eye")]
    public class HelloWorldBlockHandler : BlockHandlerBase<HelloWorldBlock>
    {
        protected override HelloWorldBlock Load(IBlockEntity entity, StoryViewMode viewMode)
        {
            var block = base.Load(entity, viewMode);

            if (viewMode == StoryViewMode.Edit)
            {
                // This is called only in Edit mode.
                block.MyLocalVar += " - Running in Edit-Mode";
            }
            else if (viewMode == StoryViewMode.Preview)
            {
                // This is called only in Preview-Mode
                block.MyLocalVar += " - Running in Preview-Mode";
            }
            else if (viewMode == StoryViewMode.GridEdit)
            {
                // This is called only in Grid-Edit-Mode
                block.MyLocalVar += " - Running in Grid-Edit-Mode";
            }
            else if (viewMode == StoryViewMode.Public)
            {
                // This is called only in Public-Mode
                block.MyLocalVar += " - Running in Public-Mode";
            }

            return block;
        }

        protected override Task RenderCoreAsync(IBlockContainer element, IEnumerable<string> templates, IHtmlHelper htmlHelper, TextWriter textWriter)
        {
            if (templates.First() == "Edit")
            {
                return base.RenderCoreAsync(element, templates, htmlHelper, textWriter);
            }
            else
            {
                return RenderByWidgetAsync(element, templates, htmlHelper, textWriter);
            }
        }

        protected override Widget GetWidget(IBlockContainer element, IHtmlHelper htmlHelper, string template)
        {
            var block = (HelloWorldBlock)element.Block;

            return new ComponentWidget(typeof(HelloWorldViewComponent), new
            {
                widgetZone = "productdetail_pictures",
                model = new ProductDetailsModel { Id = 1 }
            });
        }
    }
    public class HelloWorldBlock : IBlock
    {
        [LocalizedDisplay("Plugins.MyOrg.HelloWorld.Name")]
        public string Name { get; set; }
        public string MyLocalVar { get; set; } = "Initialised in Block";
    }

    public partial class HelloWorldBlockValidator : AbstractValidator<HelloWorldBlock>
    {
        public HelloWorldBlockValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}