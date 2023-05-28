using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Inventar-URL
        /// </summary>
        /// <param name="Guid">Die InventarId</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetInventoryUri(string Guid)
        {
            return $"{RootUri}/{Guid}";
        }

        /// <summary>
        /// Liefert alle Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Inventargegenstände beinhaltet</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventories(WqlStatement wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories.Select(x => new WebItemEntityInventory(x));

                return wql.Apply(inventorys.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Zählt die Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filteroptinen</param>
        /// <returns>Die Anzahl der Inventargegenstände, welche der Suchanfrage entspricht</returns>
        public static long CountInventories(WqlStatement wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories;

                return wql.Apply(inventorys.AsQueryable()).LongCount();
            }
        }

        /// <summary>
        /// Ermittelt die Investitionskosten der Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filteroptinen</param>
        /// <returns>Die Investitionskosten der Inventargegenstände, welche der Suchanfrage entsprichen</returns>
        public static float GetInventoriesCapitalCosts(WqlStatement wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories;

                return wql.Apply(inventorys.AsQueryable()).Sum(x => (float)x.CostValue);
            }
        }

        /// <summary>
        /// Liefert ein Inventargegenstand
        /// </summary>
        /// <param name="giud">Die InventarId</param>
        /// <returns>Der Inventargegenstände oder null</returns>
        public static WebItemEntityInventory GetInventory(string Guid)
        {
            lock (DbContext)
            {
                var inventory = DbContext.Inventories.Where(x => x.Guid == Guid).Select(x => new WebItemEntityInventory(x)).FirstOrDefault();

                return inventory;
            }
        }

        /// <summary>
        /// Liefert ein übergeordnetes Inventargegenstand
        /// </summary>
        /// <param name="inventory">Das übergeordnete Inventargegenstand</param>
        /// <returns>Der Inventargegenstände oder null</returns>
        public static WebItemEntityInventory GetInventoryParent(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entity = from i in DbContext.Inventories
                             join p in DbContext.Inventories on i.ParentId equals p.Id
                             where i.Guid == inventory.Id
                             select new WebItemEntityInventory(p);

                return entity.FirstOrDefault();
            }
        }

        /// <summary>
        /// Liefert alle untergeordneten Inventargegenstände
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>Eine Aufzählung mit allen untergeordneten Inventargegenstände</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventoryChildren(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entities = from i in DbContext.Inventories
                               join c in DbContext.Inventories on i.Id equals c.ParentId
                               where i.Guid == inventory.Id
                               select new WebItemEntityInventory(c);

                return entities;
            }
        }

        /// <summary>
        /// Liefert alle Attribute einees Inventargegenstandes
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>Eine Aufzählung, welche die Attribute beinhaltet</returns>
        public static IEnumerable<WebItemEntityInventoryAttribute> GetInventoryAttributes(WebItemEntityInventory inventory)
        {
            var template = inventory.Template?.Id;

            lock (DbContext)
            {
                var inventoryAttributes = from i in DbContext.Inventories
                                          join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                          where i.Guid == inventory.Id
                                          select ia;

                var templateAttributes = from t in DbContext.Templates
                                         join ta in DbContext.TemplateAttributes on t.Id equals ta.TemplateId
                                         join a in DbContext.Attributes on ta.AttributeId equals a.Id
                                         where t.Guid == template
                                         select a;

                return templateAttributes.Select
                    (
                        x => new WebItemEntityInventoryAttribute(inventoryAttributes
                            .Where(y => y.AttributeId == x.Id)
                            .FirstOrDefault(), x)
                    ).ToList();
            }
        }

        /// <summary>
        /// Fügt ein Inventargegenstand hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        public static void AddOrUpdateInventory(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    var newEntity = new Inventory();

                    newEntity.Name = inventory.Name;
                    newEntity.ManufacturerId = inventory.Manufacturer != null ? DbContext.Manufacturers.Where(x => x.Guid == inventory.Manufacturer.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.LocationId = inventory.Location != null ? DbContext.Locations.Where(x => x.Guid == inventory.Location.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.SupplierId = inventory.Supplier != null ? DbContext.Suppliers.Where(x => x.Guid == inventory.Supplier.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.LedgerAccountId = inventory.LedgerAccount != null ? DbContext.LedgerAccounts.Where(x => x.Guid == inventory.LedgerAccount.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.CostCenterId = inventory.CostCenter != null ? DbContext.CostCenters.Where(x => x.Guid == inventory.CostCenter.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.ConditionId = inventory.Condition != null ? DbContext.Conditions.Where(x => x.Guid == inventory.Condition.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.ParentId = inventory.Parent != null ? DbContext.Inventories.Where(x => x.Guid == inventory.Parent.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.TemplateId = inventory.Template != null ? DbContext.Templates.Where(x => x.Guid == inventory.Template.Id).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.CostValue = inventory.CostValue;
                    newEntity.PurchaseDate = inventory.PurchaseDate;
                    newEntity.DerecognitionDate = inventory.DerecognitionDate;
                    newEntity.Tag = inventory.Tag;
                    newEntity.Description = inventory.Description;
                    newEntity.Guid = inventory.Id;
                    newEntity.Created = DateTime.Now;
                    newEntity.Updated = DateTime.Now;

                    DbContext.Inventories.Add(newEntity);
                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.add"
                    };

                    AddInventoryJournal(inventory, journal);

                    // Attribute speichern
                    AddOrUpdateInventoryAttributes(inventory);
                }
                else
                {
                    // Geänderte Werte ermitteln
                    var changed = new List<Tuple<string, string, string, bool>>();
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.name.label", availableEntity.Name, inventory.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.manufacturer.label", availableEntity.Manufacturer?.Name, inventory.Manufacturer?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.location.label", availableEntity.Location?.Name, inventory.Location?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.supplier.label", availableEntity.Supplier?.Name, inventory.Supplier?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.ledgeraccount.label", availableEntity.LedgerAccount?.Name, inventory.LedgerAccount?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.costcenter.label", availableEntity.CostCenter?.Name, inventory.CostCenter?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.condition.label", availableEntity.Condition?.Name, inventory.Condition?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.parent.label", availableEntity.Parent?.Name, inventory.Parent?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.template.label", availableEntity.Template?.Name, inventory.Template?.Name, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.costvalue.label", availableEntity.CostValue.ToString(), inventory.CostValue.ToString(), changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.purchasedate.label", availableEntity.PurchaseDate?.ToString(), inventory.PurchaseDate?.ToString(), changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.derecognitiondate.label", availableEntity.DerecognitionDate?.ToString(), inventory.DerecognitionDate?.ToString(), changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.tags.label", availableEntity.Tag, inventory.Tag, changed, true);
                    ComparisonInventory("inventoryexpress:inventoryexpress.inventory.description.label", availableEntity?.Description, inventory?.Description, changed, false);

                    availableEntity.Name = inventory.Name;
                    availableEntity.ManufacturerId = inventory.Manufacturer != null ? DbContext.Manufacturers.Where(x => x.Guid == inventory.Manufacturer.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.LocationId = inventory.Location != null ? DbContext.Locations.Where(x => x.Guid == inventory.Location.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.SupplierId = inventory.Supplier != null ? DbContext.Suppliers.Where(x => x.Guid == inventory.Supplier.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.LedgerAccountId = inventory.LedgerAccount != null ? DbContext.LedgerAccounts.Where(x => x.Guid == inventory.LedgerAccount.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.CostCenterId = inventory.CostCenter != null ? DbContext.CostCenters.Where(x => x.Guid == inventory.CostCenter.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.ConditionId = inventory.Condition != null ? DbContext.Conditions.Where(x => x.Guid == inventory.Condition.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.ParentId = inventory.Parent != null ? DbContext.Inventories.Where(x => x.Guid == inventory.Parent.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.TemplateId = inventory.Template != null ? DbContext.Templates.Where(x => x.Guid == inventory.Template.Id).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.CostValue = inventory.CostValue;
                    availableEntity.PurchaseDate = inventory.PurchaseDate;
                    availableEntity.DerecognitionDate = inventory.DerecognitionDate;
                    availableEntity.Tag = inventory.Tag;
                    availableEntity.Description = inventory.Description;
                    availableEntity.Updated = DateTime.Now;

                    if (changed.Any())
                    {
                        DbContext.SaveChanges();

                        var journal = new WebItemEntityJournal()
                        {
                            Action = "inventoryexpress:inventoryexpress.journal.action.inventory.edit",
                            Parameters = changed.Select(x => new WebItemEntityJournalParameter()
                            {
                                Name = x.Item1,
                                OldValue = x.Item4 ? x.Item2?.Length > 15 ? $" {x.Item2?.Substring(0, 15)}..." : x.Item2 : "...",
                                NewValue = x.Item4 ? x.Item3?.Length > 15 ? $" {x.Item3?.Substring(0, 15)}..." : x.Item3 : "..."
                            })
                        };

                        AddInventoryJournal(inventory, journal);
                    }

                    // Attribute speichern
                    AddOrUpdateInventoryAttributes(inventory);
                }
            }
        }

        // <summary>
        /// Fügt die Attribute dem Inventargegenstand hinzu oder aktuallisiert diese
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        public static void AddOrUpdateInventoryAttributes(WebItemEntityInventory inventory)
        {
            var changed = new List<Tuple<string, string, string, bool>>();

            lock (DbContext)
            {
                var existens = (from i in DbContext.Inventories
                                join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                join a in DbContext.Attributes on ia.AttributeId equals a.Id
                                where i.Guid == inventory.Id
                                select new WebItemEntityInventoryAttribute(ia, a)).ToList();

                var creates = inventory.Attributes.Except(existens, new WebItemEntityInventoryAttributeComparer());
                var updates = inventory.Attributes.Intersect(existens, new WebItemEntityInventoryAttributeComparer());
                var removes = existens.Except(inventory.Attributes, new WebItemEntityInventoryAttributeComparer());

                // Ersetllung
                foreach (var attribute in creates)
                {
                    var inventoryEntity = DbContext.Inventories
                        .Where(x => x.Guid == inventory.Id)
                        .FirstOrDefault();

                    var attributeEntity = DbContext.Attributes
                        .Where(x => x.Guid == attribute.Id)
                        .FirstOrDefault();

                    DbContext.InventoryAttributes.Add(new InventoryAttribute()
                    {
                        AttributeId = attributeEntity.Id,
                        InventoryId = inventoryEntity.Id,
                        Value = attribute.Value,
                        Created = DateTime.Now
                    });

                    ComparisonInventory(attribute.Name, null, attribute.Value, changed, true);
                }

                // Änderung
                foreach (var attribute in updates)
                {
                    var availableEntity = (from i in DbContext.Inventories
                                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                                           where i.Guid == inventory.Id && a.Guid == attribute.Id
                                           select ia).FirstOrDefault();

                    if (availableEntity != null && availableEntity.Value != attribute.Value)
                    {
                        ComparisonInventory(attribute.Name, availableEntity.Value, attribute.Value, changed, true);

                        availableEntity.Value = attribute.Value;
                    }
                }

                // Entfernung
                foreach (var attribute in removes)
                {
                    var availableEntity = (from i in DbContext.Inventories
                                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                                           where i.Guid == inventory.Id && a.Guid == attribute.Id
                                           select ia).FirstOrDefault();

                    DbContext.InventoryAttributes.Remove(availableEntity);

                    ComparisonInventory(attribute.Name, availableEntity.Value, null, changed, true);
                }

                DbContext.SaveChanges();
            }

            if (changed.Any())
            {
                DbContext.SaveChanges();

                var journal = new WebItemEntityJournal()
                {
                    Action = "inventoryexpress:inventoryexpress.journal.action.inventory.attribute.edit",
                    Parameters = changed.Select(x => new WebItemEntityJournalParameter()
                    {
                        Name = x.Item1,
                        OldValue = x.Item4 ? x.Item2?.Length > 15 ? $" {x.Item2?.Substring(0, 15)}..." : x.Item2 : "...",
                        NewValue = x.Item4 ? x.Item3?.Length > 15 ? $" {x.Item3?.Substring(0, 15)}..." : x.Item3 : "..."
                    })
                };

                AddInventoryJournal(inventory, journal);
            }
        }

        /// <summary>
        /// Ermittlung der geänderten Werte durch Vergleich alter- und neuer Werte.
        /// </summary>
        /// <param name="name">Der Attributname</param>
        /// <param name="orgValue">Der Wert vor der Änderung</param>
        /// <param name="newValue">Der geänderte Wert</param>
        /// <param name="list">Eine Liste mit den Änderungen</param>
        /// <param name="apply">Die Werte werden im Journal übernommen</param>
        private static void ComparisonInventory(string name, string orgValue, string newValue, List<Tuple<string, string, string, bool>> list, bool apply)
        {
            if (orgValue != newValue)
            {
                list.Add(new Tuple<string, string, string, bool>(name, orgValue, newValue, apply));
            }
        }

        /// <summary>
        /// Löscht ein Inventargegenstand
        /// </summary>
        /// <param name="inventory">Der zu löschende Inventargegenstand</param>
        public static void DeleteInventory(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();
                //var entityComments = DbContext.InventoryComments.Where(x => x.InventoryId == entity.Id);
                //var entityJournal = DbContext.InventoryJournals.Where(x => x.InventoryId == entity.Id);

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Inventories.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob The inventory item. in Verwendung ist
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetInventoryInUse(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join p in DbContext.Inventories on i.Id equals p.ParentId
                           where i.Guid == inventory.Id
                           select i;

                return used.Any();
            }
        }
    }
}