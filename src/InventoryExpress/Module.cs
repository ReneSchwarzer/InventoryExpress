using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using InventoryExpress.WebPageSetting;
using InventoryExpress.WebResource;
using System;
using WebExpress.WebApp.Model;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebModule;
using WebExpress.WebCore.WebUri;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryExpress
{
    [Name("module.name")]
    [Description("module.description")]
    [Icon("/assets/img/Logo.png")]
    [AssetPath("")]
    [ContextPath("/")]
    [Application<Application>]
    public sealed class Module : IModule
    {
        /// <summary>
        /// The context of the module.
        /// </summary>
        private IModuleContext ModuleContext { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialization of the module. Here, for example, managed resources can be loaded. 
        /// </summary>
        /// <param name="moduleContext">The context of the module that applies to the execution.</param>
        public void Initialization(IModuleContext moduleContext)
        {
            ModuleContext = moduleContext;

            var applicationContext = moduleContext.ApplicationContext;

            ViewModel.Initialization(applicationContext, ModuleContext, GetUri);
        }

        /// <summary>
        /// Called when the module starts working. The call is concurrent.
        /// </summary>
        public void Run()
        {
        }

        /// <summary>
        /// Release unmanaged resources that have been reserved during use.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Returns the root uri of a web item.
        /// </summary>
        /// <param name="item">The web item.</param>
        private UriResource GetUri(WebItem item) 
        {
            return item switch
            {
                WebItemEntityAttribute => ComponentManager.SitemapManager.GetUri<PageSettingAttributes>(new ParameterAttributeId(item.Guid)),
                WebItemEntityCondition => ComponentManager.SitemapManager.GetUri<PageSettingConditionEdit>(new ParameterConditionId(item.Guid)),
                WebItemEntityCostCenter => ComponentManager.SitemapManager.GetUri<PageCostCenterEdit>(new ParameterCostCenterId(item.Guid)),
                WebItemEntityInventory => ComponentManager.SitemapManager.GetUri<PageInventoryDetails>(new ParameterInventoryId(item.Guid)),
                WebItemEntityLedgerAccount => ComponentManager.SitemapManager.GetUri<PageLedgerAccountEdit>(new ParameterLedgerAccountId(item.Guid)),
                WebItemEntityLocation => ComponentManager.SitemapManager.GetUri<PageLocationEdit>(new ParameterLocationId(item.Guid)),
                WebItemEntityManufacturer => ComponentManager.SitemapManager.GetUri<PageManufacturerEdit>(new ParameterManufacturerId(item.Guid)),
                //WebItemEntitySettings => ComponentManager.SitemapManager.GetUri<PageSettingGeneral>(new ParameterSettingsId(item.Guid)),
                WebItemEntitySupplier => ComponentManager.SitemapManager.GetUri<PageSupplierEdit>(new ParameterSupplierId(item.Guid)),
                WebItemEntityTemplate => ComponentManager.SitemapManager.GetUri<PageSettingTemplateEdit>(new ParameterTemplateId(item.Guid)),
                WebItemEntityMedia => ComponentManager.SitemapManager.GetUri<ResourceMedia>(new ParameterMediaId(item.Guid)),
                _ => ComponentManager.SitemapManager.GetUri<ResourceAsset>()
            };
           
        }
    }
}
