using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;
using WebExpress.WebScope;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.supplier.edit.label")]
    [SegmentGuid<ParameterSupplierId>("inventoryexpress:inventoryexpress.supplier.edit.display")]
    [ContextPath("/")]
    [Parent<PageSuppliers>]
    [Module<Module>]
    public sealed class PageSupplierEdit : PageWebAppFormular<ControlFormularSupplier>, IPageSupplier, IScope
    {
        /// <summary>
        /// Returns or sets the supplier
        /// </summary>
        private WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSupplierEdit()
            : base("supplier")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            SetRedirectUri(ComponentManager.SitemapManager.GetUri<PageSuppliers>());
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnFillFormular(object sender, FormularEventArgs e)
        {
            Form.SupplierName.Value = Supplier?.Name;
            Form.Description.Value = Supplier?.Description;
            Form.Address.Value = Supplier?.Address;
            Form.Zip.Value = Supplier?.Zip;
            Form.Place.Value = Supplier?.Place;
            Form.Tag.Value = Supplier?.Tag;
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnProcessFormular(object sender, FormularEventArgs e)
        {
            // change and save supplier
            Supplier.Name = Form.SupplierName.Value;
            Supplier.Description = Form.Description.Value;
            Supplier.Address = Form.Address.Value;
            Supplier.Zip = Form.Zip.Value;
            Supplier.Place = Form.Place.Value;
            Supplier.Tag = Form.Tag.Value;
            Supplier.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateSupplier(Supplier);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.edit"),
                    new ControlLink()
                    {
                        Text = Supplier.Name,
                        Uri = ViewModel.GetSupplierUri(Supplier.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                Supplier.Name,
                new PropertyColorText(TypeColorText.Info),
                Supplier.Image
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter<ParameterSupplierId>()?.Value;
            Supplier = ViewModel.GetSupplier(guid);

            context.Uri.Display = Supplier.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
