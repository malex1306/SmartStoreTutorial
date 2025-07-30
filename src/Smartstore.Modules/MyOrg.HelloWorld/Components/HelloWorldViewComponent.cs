using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.HelloWorld.Models;
using Smartstore.Core.Data;
using Smartstore.Web.Components;
using Smartstore.Web.Models.Catalog;

public class HelloWorldViewComponent : SmartViewComponent
{
    private readonly SmartDbContext _db;

    public HelloWorldViewComponent(SmartDbContext db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object model)
    {
        if (widgetZone != "productdetails_pictures_top")
        {
            return Empty();
        }

        if (model.GetType() != typeof(ProductDetailsModel))
        {
            return Empty();
        }

        var productModel = (ProductDetailsModel)model;
        var product = await _db.Products.FindAsync(productModel.Id);
        var attributeValue = product.GenericAttributes.Get<string>("HelloWorldMyTabValue");

        var viewComponentModel = new ViewComponentModel
        {
            MyTabValue = attributeValue
        };

        return View(viewComponentModel);
    }
}