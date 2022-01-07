using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("InventoryEdit")]
    [Title("inventoryexpress:inventoryexpress.inventory.edit.label")]
    [Segment("edit", "inventoryexpress:inventoryexpress.inventory.edit.display")]
    [Path("/Details")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("inventoryedit")]
    public sealed class PageInventoryEdit : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory Form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
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

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("InventoryID")?.Value;
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                var tags = from i in ViewModel.Instance.InventoryTags
                           join t in ViewModel.Instance.Tags
                           on i.TagId equals t.Id
                           where i.InventoryId == inventory.Id
                           select t;
                var attributesForm = new List<ControlFormularItemInputTextBox>();
                var attributes = new List<InventoryAttribute>();

                Form = new ControlFormularInventory("inventory")
                {
                    RedirectUri = context.Uri.Take(-1),
                    Edit = true,
                    EnableCancelButton = true,
                    BackUri = context.Uri.Take(-1)
                };

                Form.InitializeFormular += (s, e) =>
                {
                    var templateGUID = context.Request.HasParameter(Form.Template?.Name) ?
                    context.Request.GetParameter(Form.Template?.Name)?.Value :
                    ViewModel.Instance.Templates.Where(x => x.Id == inventory.TemplateId).FirstOrDefault()?.Guid;

                    var template = ViewModel.Instance.Templates.Where(x => x.Guid == templateGUID).FirstOrDefault();

                    // nur gefüllte Attribute übernehmen
                    attributes.AddRange(ViewModel.Instance.InventoryAttributes.Where(x => x.InventoryId == inventory.Id && !string.IsNullOrWhiteSpace(x.Value)));

                    // Template-Attribute übernehmen
                    if (template != null)
                    {
                        foreach (var ta in ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == template.Id))
                        {
                            var att = ViewModel.Instance.Attributes.Where(x => x.Id == ta.AttributeId).FirstOrDefault();

                            if (attributes.Find
                                (
                                    f =>
                                    f.Attribute.Name.Equals
                                    (
                                        att?.Name,
                                        StringComparison.OrdinalIgnoreCase
                                    )
                                ) == null)
                            {
                                attributes.Add(new InventoryAttribute()
                                {
                                    AttributeId = ta.AttributeId,
                                    InventoryId = inventory.Id,
                                    Attribute = att,
                                    Inventory = inventory,
                                    Created = DateTime.Now
                                });
                            }
                        }
                    }

                    foreach (var attribute in attributes)
                    {
                        attributesForm.Add(new ControlFormularItemInputTextBox()
                        {
                            Name = "attribute_" + attribute.Attribute.Guid,
                            Label = attribute.Attribute.Name,
                            Help = attribute.Attribute.Description,
                            Tag = attribute
                        });
                    }

                    attributesForm.ForEach(x => Form.Attributes?.Items.Add(x));

                    Form.InventoryName.Validation += (s, e) =>
                    {
                        if (e.Value.Count() < 1)
                        {
                            e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress.inventory.validation.name.invalid"));
                        }
                        else if (!inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                        {
                            e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress.inventory.validation.name.used"));
                        }
                    };

                    Form.CostValue.Validation += (s, e) =>
                    {
                        try
                        {
                            if (Convert.ToDecimal(Form.CostValue.Value, Culture) < 0)
                            {
                                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress.inventory.validation.costvalue.negativ"));
                            };
                        }
                        catch
                        {
                            e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress.inventory.validation.costvalue.invalid"));
                        }
                    };
                };

                Form.FillFormular += (s, e) =>
                {
                    Form.InventoryName.Value = inventory?.Name;
                    Form.Manufacturer.Value = ViewModel.Instance.Manufacturers.Where(x => x.Id == inventory.ManufacturerId).FirstOrDefault()?.Guid;
                    Form.Location.Value = ViewModel.Instance.Locations.Where(x => x.Id == inventory.LocationId).FirstOrDefault()?.Guid;
                    Form.Supplier.Value = ViewModel.Instance.Suppliers.Where(x => x.Id == inventory.SupplierId).FirstOrDefault()?.Guid;
                    Form.LedgerAccount.Value = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == inventory.LedgerAccountId).FirstOrDefault()?.Guid;
                    Form.CostCenter.Value = ViewModel.Instance.CostCenters.Where(x => x.Id == inventory.CostCenterId).FirstOrDefault()?.Guid;
                    Form.Condition.Value = ViewModel.Instance.Conditions.Where(x => x.Id == inventory.ConditionId).FirstOrDefault()?.Guid;
                    Form.Parent.Value = ViewModel.Instance.Inventories.Where(x => x.Id == inventory.ParentId).FirstOrDefault()?.Guid;
                    Form.Template.Value = ViewModel.Instance.Templates.Where(x => x.Id == inventory.TemplateId)?.FirstOrDefault()?.Guid;
                    Form.CostValue.Value = inventory.CostValue.ToString(Culture);
                    Form.PurchaseDate.Value = inventory.PurchaseDate.HasValue ? inventory.PurchaseDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                    Form.DerecognitionDate.Value = inventory.DerecognitionDate.HasValue ? inventory.DerecognitionDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                    Form.Tag.Value = string.Join(";", tags.Select(x => x.Label));
                    Form.Description.Value = inventory?.Description;

                    // Attribute ermitteln
                    foreach (var attribute in attributesForm)
                    {
                        attribute.Value = (attribute.Tag as InventoryAttribute)?.Value;
                    }
                };

                // Ermittlung der geänderten Werte
                var comparison = new Action<string, string, string, List<Tuple<string, string, string, bool>>, bool>((name, orgValue, newValue, list, apply) =>
                {
                    if (orgValue != newValue)
                    {
                        list.Add(new Tuple<string, string, string, bool>(name, orgValue, newValue, apply));
                    }

                    return;
                });

                Form.ProcessFormular += (s, e) =>
                {
                    // Geänderte Werte ermitteln
                    var changed = new List<Tuple<string, string, string, bool>>();
                    comparison("inventoryexpress.inventory.name.label", inventory.Name, Form.InventoryName?.Value, changed, true);
                    comparison("inventoryexpress.inventory.manufacturers.label", inventory.Manufacturer?.Name, ViewModel.Instance.Manufacturers.Where(x => x.Guid == Form.Manufacturer.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.location.label", inventory.Location?.Name, ViewModel.Instance.Locations.Where(x => x.Guid == Form.Location.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.supplier.label", inventory.Supplier?.Name, ViewModel.Instance.Suppliers.Where(x => x.Guid == Form.Supplier.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.ledgeraccount.label", inventory.LedgerAccount?.Name, ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == Form.LedgerAccount.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.costcenter.label", inventory.CostCenter?.Name, ViewModel.Instance.CostCenters.Where(x => x.Guid == Form.CostCenter.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.condition.label", inventory.Condition?.Name, ViewModel.Instance.Conditions.Where(x => x.Guid == Form.Condition.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.parent.label", inventory.Parent?.Name, ViewModel.Instance.Inventories.Where(x => x.Guid == Form.Parent.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.template.label", inventory.Template?.Name, ViewModel.Instance.Templates.Where(x => x.Guid == Form.Template.Value).FirstOrDefault()?.Name, changed, true);
                    comparison("inventoryexpress.inventory.costvalue.label", inventory.CostValue.ToString(), !string.IsNullOrWhiteSpace(Form.CostValue.Value) ? Convert.ToDecimal(Form.CostValue.Value, Culture).ToString() : "0", changed, true);
                    comparison("inventoryexpress.inventory.purchasedate.label", inventory.PurchaseDate?.ToString(), !string.IsNullOrWhiteSpace(Form.PurchaseDate.Value) ? Convert.ToDateTime(Form.PurchaseDate.Value, Culture).ToString() : null, changed, true);
                    comparison("inventoryexpress.inventory.derecognitiondate.label", inventory.DerecognitionDate?.ToString(), !string.IsNullOrWhiteSpace(Form.DerecognitionDate.Value) ? Convert.ToDateTime(Form.DerecognitionDate.Value, Culture).ToString() : null, changed, true);
                    comparison("inventoryexpress.inventory.tags.label", inventory.Tag, Form.Tag?.Value, changed, true);
                    comparison("inventoryexpress.inventory.description.label", inventory.Description, Form.Description?.Value, changed, false);

                    // Neues Inventarobjekt erstellen und speichern
                    inventory.Name = Form.InventoryName.Value;
                    inventory.Manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == Form.Manufacturer.Value).FirstOrDefault();
                    inventory.Location = ViewModel.Instance.Locations.Where(x => x.Guid == Form.Location.Value).FirstOrDefault();
                    inventory.Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == Form.Supplier.Value).FirstOrDefault();
                    inventory.LedgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == Form.LedgerAccount.Value).FirstOrDefault();
                    inventory.CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == Form.CostCenter.Value).FirstOrDefault();
                    inventory.Condition = ViewModel.Instance.Conditions.Where(x => x.Guid == Form.Condition.Value).FirstOrDefault();
                    inventory.Parent = ViewModel.Instance.Inventories.Where(x => x.Guid == Form.Parent.Value).FirstOrDefault();
                    inventory.Template = ViewModel.Instance.Templates.Where(x => x.Guid == Form.Template.Value).FirstOrDefault();
                    inventory.CostValue = !string.IsNullOrWhiteSpace(Form.CostValue.Value) ? Convert.ToDecimal(Form.CostValue.Value, Culture) : 0;
                    inventory.PurchaseDate = !string.IsNullOrWhiteSpace(Form.PurchaseDate.Value) ? Convert.ToDateTime(Form.PurchaseDate.Value, Culture) : null;
                    inventory.DerecognitionDate = !string.IsNullOrWhiteSpace(Form.DerecognitionDate.Value) ? Convert.ToDateTime(Form.DerecognitionDate.Value, Culture) : null;
                    inventory.Tag = Form.Tag.Value;
                    inventory.Description = Form.Description.Value;
                    inventory.Updated = DateTime.Now;

                    // Attribute ermitteln
                    foreach (var attribute in attributes)
                    {
                        var value = context.Request.GetParameter("attribute_" + attribute.Attribute.Guid)?.Value;
                        var contains = ViewModel.Instance.InventoryAttributes.Contains(attribute);

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            attribute.Value = value;

                            if (!contains)
                            {
                                // Hinzufügen
                                ViewModel.Instance.InventoryAttributes.Add(attribute);
                            }

                            // Update
                            ViewModel.Instance.SaveChanges();
                        }
                        else
                        {
                            // Löschen
                            if (contains)
                            {
                                ViewModel.Instance.InventoryAttributes.Remove(attribute);
                            }
                        }
                    }

                    ViewModel.Instance.SaveChanges();

                    var journal = new InventoryJournal()
                    {
                        InventoryId = inventory.Id,
                        Action = "inventoryexpress.journal.action.inventory.edit",
                        Created = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };

                    ViewModel.Instance.InventoryJournals.Add(journal);
                    ViewModel.Instance.SaveChanges();

                    ViewModel.Instance.InventoryJournalParameters.AddRange(changed.Select(x => new InventoryJournalParameter()
                    {
                        InventoryJournalId = journal.Id,
                        Name = x.Item1,
                        OldValue = x.Item4 ? x.Item2?.Length > 15 ? $" { x.Item2?.Substring(0, 15) }..." : x.Item2 : "...",
                        NewValue = x.Item4 ? x.Item3?.Length > 15 ? $" { x.Item3?.Substring(0, 15) }..." : x.Item3 : "...",
                        Guid = Guid.NewGuid().ToString()
                    }));

                    ViewModel.Instance.SaveChanges();

                    // neue Tags ermitteln
                    var newTags = Form.Tag.Value.Split(';').Except(tags.Select(x => x.Label));

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

                    // zu entfernende Tags
                    var removeTags = tags.Select(x => x.Label).ToList().Except(Form.Tag.Value.Split(';'));
                    foreach (var r in removeTags)
                    {
                        var inventoryTag = from i in ViewModel.Instance.InventoryTags
                                           join t in ViewModel.Instance.Tags
                                           on i.TagId equals t.Id
                                           where i.InventoryId == inventory.Id && t.Label.ToLower() == r.ToLower()
                                           select i;

                        ViewModel.Instance.InventoryTags.RemoveRange(inventoryTag);
                        ViewModel.Instance.SaveChanges();

                        var tag = ViewModel.Instance.Tags.Where(x => x.Label.ToLower() == r.ToLower()).FirstOrDefault();

                        if (tag != null && !ViewModel.Instance.InventoryTags.Where(x => x.TagId == tag.Id).Any())
                        {
                            ViewModel.Instance.Tags.Remove(tag);
                            ViewModel.Instance.SaveChanges();
                        }
                    }
                };

                // Attribute im Formular erstellen
                context.VisualTree.Content.Primary.Add(Form);
                context.Uri.Display = context.Request.GetParameter("InventoryID")?.Value.Split('-').LastOrDefault();
            }
        }
    }
}
