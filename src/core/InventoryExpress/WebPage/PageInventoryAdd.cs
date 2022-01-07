using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebPage
{
    [ID("InventoryAdd")]
    [Title("inventoryexpress:inventoryexpress.inventory.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.inventory.add.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageInventoryAdd : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var guid = Guid.NewGuid().ToString();

            var form = new ControlFormularInventory
            {
                EnableCancelButton = true,
                BackUri = context.Uri.Take(-1),
                RedirectUri = context.Uri.Root.Append(guid).Append("edit")
            };

            visualTree.Content.Primary.Add(form);

            form.InitializeFormular += (s, e) =>
            {
                form.InventoryName.Validation += (s, e) =>
                {
                    if (e.Value.Length < 1)
                    {
                        e.Results.Add(new ValidationResult(TypesInputValidity.Error, "Geben Sie einen gültigen Namen ein!"));
                    }
                    else if (ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Any())
                    {
                        e.Results.Add(new ValidationResult(TypesInputValidity.Error, "Der Name wird bereits verwendet. Geben Sie einen anderen Namen an!"));
                    }
                };
            };

            form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    // Neues Herstellerobjekt erstellen und speichern
                    var inventory = new Inventory()
                    {
                        Name = form.InventoryName.Value,
                        Manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == form.Manufacturer.Value).FirstOrDefault(),
                        Location = ViewModel.Instance.Locations.Where(x => x.Guid == form.Location.Value).FirstOrDefault(),
                        Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == form.Supplier.Value).FirstOrDefault(),
                        LedgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == form.LedgerAccount.Value).FirstOrDefault(),
                        CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == form.CostCenter.Value).FirstOrDefault(),
                        Condition = ViewModel.Instance.Conditions.Where(x => x.Guid == form.Condition.Value).FirstOrDefault(),
                        Parent = ViewModel.Instance.Inventories.Where(x => x.Guid == form.Parent.Value).FirstOrDefault(),
                        Template = ViewModel.Instance.Templates.Where(x => x.Guid == form.Template.Value).FirstOrDefault(),
                        CostValue = !string.IsNullOrWhiteSpace(form.CostValue.Value) ? Convert.ToDecimal(form.CostValue.Value, Culture) : 0,
                        PurchaseDate = !string.IsNullOrWhiteSpace(form.PurchaseDate.Value) ? Convert.ToDateTime(form.PurchaseDate.Value, Culture) : null,
                        DerecognitionDate = !string.IsNullOrWhiteSpace(form.DerecognitionDate.Value) ? Convert.ToDateTime(form.DerecognitionDate.Value, Culture) : null,
                        Tag = form.Tag.Value,
                        Description = form.Description.Value,
                        Guid = guid
                    };

                    ViewModel.Instance.Inventories.Add(inventory);
                    ViewModel.Instance.SaveChanges();

                    // neue Tags ermitteln
                    var newTags = form.Tag.Value.Split(';');

                    foreach (var n in newTags)
                    {
                        var tag = ViewModel.Instance.Tags.Where(x => x.Label.ToLower() == n.ToLower()).FirstOrDefault();
                        if (tag == null)
                        {
                            // Tag in DB neu Anlegen
                            tag = new Tag() { Label = n };
                            ViewModel.Instance.Tags.Add(tag);
                            ViewModel.Instance.SaveChanges();
                        }
                        ViewModel.Instance.InventoryTags.Add(new InventoryTag() { InventoryId = inventory.Id, TagId = tag.Id });
                        ViewModel.Instance.SaveChanges();
                    }

                    var journal = new InventoryJournal()
                    {
                        InventoryId = inventory.Id,
                        Action = "inventoryexpress.journal.action.inventory.add",
                        Created = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };

                    ViewModel.Instance.InventoryJournals.Add(journal);
                    ViewModel.Instance.SaveChanges();

                    NotificationManager.CreateNotification(context.Request, I18N("inventoryexpress:inventoryexpress.journal.action.inventory.add"), 15000);
                }
            };
        }
    }
}
