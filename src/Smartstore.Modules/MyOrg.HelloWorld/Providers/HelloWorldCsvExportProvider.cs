using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using MyOrg.HelloWorld.Components;
using MyOrg.HelloWorld.Models;
using Smartstore.Core.Catalog.Pricing;
using Smartstore.Core.Catalog.Products;
using Smartstore.Core.DataExchange;
using Smartstore.Core.DataExchange.Csv;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Localization;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;
using Smartstore;

namespace MyOrg.HelloWorld.Providers
{
    [SystemName("MyOrg.HelloWorld.ProductCsv")]
    [FriendlyName("Hello world CS product feed")]
    [ExportFeatures(Features =
        ExportFeatures.CreatesInitialPublicDeployment |
        ExportFeatures.OffersBrandFallback)]
    public class HelloWorldCsvExportProvider : ExportProviderBase
    {
        public override string FileExtension => "CSV";
        public Localizer T { get; set; } = NullLocalizer.Instance;
        private CsvConfiguration _csvConfiguration;
        private CsvConfiguration CsvConfiguration
        {
            get {
                _csvConfiguration ??= new CsvConfiguration
                {
                    Delimiter = ';',
                    SupportsMultiline = false
                };
                return _csvConfiguration;
            }
        }

        public override ExportConfigurationInfo ConfigurationInfo => new() // hier musste ich ohne async machen
        {
            ConfigurationWidget = new ComponentWidget<HelloWorldConfigurationViewComponent>(),
            ModelType = typeof(ProfileConfigurationModel)
        };


        protected override async Task ExportAsync(ExportExecuteContext context,
            CancellationToken cancelToken)
        {
            var config = (context.ConfigurationData as ProfileConfigurationModel) ?? new ProfileConfigurationModel();
            using var writer = new CsvWriter(new StreamWriter(context.DataStream, Encoding.UTF8, 1024, true));
            writer.WriteFields(new string[]
            {
                "ProductName",
                "SKU",
                "Price",
                "Savings",
                "Descriptions"
            });
            writer.NextRow();

            while (context.Abort == DataExchangeAbortion.None && await context.DataSegmenter.ReadNextSegmentAsync())
            {
                var segment = await context.DataSegmenter.GetCurrentSegmentAsync();
                foreach (dynamic product in segment)
                {
                    if (context.Abort != DataExchangeAbortion.None)
                    {
                        break;
                    }
                    Product entity = product.Entity;

                    try
                    {
                        var calculatedPrice = (CalculatedPrice)product._Price;
                        var saving = calculatedPrice.Saving;
                        writer.WriteFields(new string[]
                        {
                            product.Name,
                            product.Sku,
                            ((decimal)product.Price).ToString(CultureInfo.InvariantCulture), // FormatInvariant() existiert nur für string, daher erst decimal → string (kulturell invariant).
                            saving.HasSaving ? saving.SavingPrice.Amount.ToStringInvariant() : string.Empty,  // Für Amount existiert eine Smartstore-Erweiterung ToStringInvariant(), die direkt auf decimal funktioniert. 
                            ((string)product.FullDescription).Truncate(5000)
                        });
                        writer.NextRow();
                        ++context.RecordsSucceeded;

                        if (context.RecordsSucceeded >= config.NumberOfExportedRows)
                        {
                            context.Abort = DataExchangeAbortion.Soft;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        context.RecordOutOfMemoryException(ex, entity.Id, T);
                        context.Abort = DataExchangeAbortion.Hard;
                        throw;
                    }
                    catch (Exception ex)
                    {
                        context.RecordException(ex, entity.Id);
                    }
                }
            }

        }
    }
}
