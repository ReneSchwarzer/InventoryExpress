using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.supplier.edit.label")]
    [WebExSegmentGuid("SupplierId", "inventoryexpress:inventoryexpress.supplier.edit.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageSuppliers))]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    [WebExContext("supplieredit")]
    public sealed class PageSupplierEdit : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
        };

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSupplierEdit()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
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
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Lieferant ändern und speichern
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

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.edit"),
                    new ControlLink()
                    {
                        Text = Supplier.Name,
                        Uri = ViewModel.GetSupplierUri(Supplier.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Supplier.Image,
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("SupplierId")?.Value;
            Supplier = ViewModel.GetSupplier(guid);

            Uri.Display = Supplier.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
