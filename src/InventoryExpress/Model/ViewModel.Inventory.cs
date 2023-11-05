using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebComponent;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Returns the inventory uri.
        /// </summary>
        /// <param name="guid">The inventory id.</param>
        /// <returns>The uri or null.</returns>
        public static string GetInventoryUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageInventoryDetails>(new ParameterInventoryId(guid));
        }

        /// <summary>
        /// Returns all inventories.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the inventories.</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventories(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                .ExecuteWql<WebItemEntityInventory>(wql);

            return GetInventories(wqlStatement);
        }

        /// <summary>
        /// Returns all inventories.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the inventories.</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventories(IWqlStatement<WebItemEntityInventory> wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories.Select(x => new WebItemEntityInventory(x));

                return wql.Apply(inventorys.AsQueryable());
            }
        }

        /// <summary>
        /// Counts the inventory items.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>The number of inventory items that corresponds to the search query.</returns>
        public static long CountInventories(string wql = "")
        {
            lock (DbContext)
            {
                var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityInventory>(wql);

                return wqlStatement.Apply().LongCount();
            }
        }

        /// <summary>
        /// Retruns the investment costs of the inventory items.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>The investment costs of the inventory items that correspond to the search query.</returns>
        public static float GetInventoriesCapitalCosts(string wql = "")
        {
            lock (DbContext)
            {
                var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityInventory>(wql);

                return wqlStatement.Apply().Sum(x => (float)x.CostValue);
            }
        }

        /// <summary>
        /// Returns an inventory item.
        /// </summary>
        /// <param name="giud">The inventory id.</param>
        /// <returns>The inventory items or null.</returns>
        public static WebItemEntityInventory GetInventory(string guid)
        {
            lock (DbContext)
            {
                var inventory = DbContext.Inventories.Where(x => x.Guid == guid).Select(x => new WebItemEntityInventory(x)).FirstOrDefault();

                return inventory;
            }
        }

        /// <summary>
        /// Returns a parent inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item whose parent is to be determined.</param>
        /// <returns>The inventory items or null.</returns>
        public static WebItemEntityInventory GetInventoryParent(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entity = from i in DbContext.Inventories
                             join p in DbContext.Inventories on i.ParentId equals p.Id
                             where i.Guid == inventory.Guid
                             select new WebItemEntityInventory(p);

                return entity.FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns all child inventory items.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>A enumaration of all child inventory items.</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventoryChildren(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entities = from i in DbContext.Inventories
                               join c in DbContext.Inventories on i.Id equals c.ParentId
                               where i.Guid == inventory.Guid
                               select new WebItemEntityInventory(c);

                return entities;
            }
        }

        /// <summary>
        /// Returns all attributes of an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>A enumeration of attributes.</returns>
        public static IEnumerable<WebItemEntityInventoryAttribute> GetInventoryAttributes(WebItemEntityInventory inventory)
        {
            var template = inventory.Template?.Guid;

            lock (DbContext)
            {
                var inventoryAttributes = from i in DbContext.Inventories
                                          join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                          where i.Guid == inventory.Guid
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
        /// Adds or updates an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        public static void AddOrUpdateInventory(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    var newEntity = new Inventory();

                    newEntity.Name = inventory.Name;
                    newEntity.ManufacturerId = inventory.Manufacturer != null ? DbContext.Manufacturers.Where(x => x.Guid == inventory.Manufacturer.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.LocationId = inventory.Location != null ? DbContext.Locations.Where(x => x.Guid == inventory.Location.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.SupplierId = inventory.Supplier != null ? DbContext.Suppliers.Where(x => x.Guid == inventory.Supplier.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.LedgerAccountId = inventory.LedgerAccount != null ? DbContext.LedgerAccounts.Where(x => x.Guid == inventory.LedgerAccount.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.CostCenterId = inventory.CostCenter != null ? DbContext.CostCenters.Where(x => x.Guid == inventory.CostCenter.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.ConditionId = inventory.Condition != null ? DbContext.Conditions.Where(x => x.Guid == inventory.Condition.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.ParentId = inventory.Parent != null ? DbContext.Inventories.Where(x => x.Guid == inventory.Parent.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.TemplateId = inventory.Template != null ? DbContext.Templates.Where(x => x.Guid == inventory.Template.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    newEntity.CostValue = inventory.CostValue;
                    newEntity.PurchaseDate = inventory.PurchaseDate;
                    newEntity.DerecognitionDate = inventory.DerecognitionDate;
                    newEntity.Tag = inventory.Tag;
                    newEntity.Description = inventory.Description;
                    newEntity.Guid = inventory.Guid;
                    newEntity.Created = DateTime.Now;
                    newEntity.Updated = DateTime.Now;

                    DbContext.Inventories.Add(newEntity);
                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.add"
                    };

                    AddInventoryJournal(inventory, journal);

                    // save attributes
                    AddOrUpdateInventoryAttributes(inventory);
                }
                else
                {
                    // detecting changed values
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
                    availableEntity.ManufacturerId = inventory.Manufacturer != null ? DbContext.Manufacturers.Where(x => x.Guid == inventory.Manufacturer.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.LocationId = inventory.Location != null ? DbContext.Locations.Where(x => x.Guid == inventory.Location.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.SupplierId = inventory.Supplier != null ? DbContext.Suppliers.Where(x => x.Guid == inventory.Supplier.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.LedgerAccountId = inventory.LedgerAccount != null ? DbContext.LedgerAccounts.Where(x => x.Guid == inventory.LedgerAccount.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.CostCenterId = inventory.CostCenter != null ? DbContext.CostCenters.Where(x => x.Guid == inventory.CostCenter.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.ConditionId = inventory.Condition != null ? DbContext.Conditions.Where(x => x.Guid == inventory.Condition.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.ParentId = inventory.Parent != null ? DbContext.Inventories.Where(x => x.Guid == inventory.Parent.Guid).Select(x => x.Id).FirstOrDefault() : null;
                    availableEntity.TemplateId = inventory.Template != null ? DbContext.Templates.Where(x => x.Guid == inventory.Template.Guid).Select(x => x.Id).FirstOrDefault() : null;
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

                    // save attributes
                    AddOrUpdateInventoryAttributes(inventory);
                }
            }
        }

        // <summary>
        /// Adds or updates the attributes to the inventory item.
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
                                where i.Guid == inventory.Guid
                                select new WebItemEntityInventoryAttribute(ia, a)).ToList();

                var creates = inventory.Attributes.Except(existens, new WebItemEntityInventoryAttributeComparer());
                var updates = inventory.Attributes.Intersect(existens, new WebItemEntityInventoryAttributeComparer());
                var removes = existens.Except(inventory.Attributes, new WebItemEntityInventoryAttributeComparer());

                // creation
                foreach (var attribute in creates)
                {
                    var inventoryEntity = DbContext.Inventories
                        .Where(x => x.Guid == inventory.Guid)
                        .FirstOrDefault();

                    var attributeEntity = DbContext.Attributes
                        .Where(x => x.Guid == attribute.Guid)
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

                // changing
                foreach (var attribute in updates)
                {
                    var availableEntity = (from i in DbContext.Inventories
                                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                                           where i.Guid == inventory.Guid && a.Guid == attribute.Guid
                                           select ia).FirstOrDefault();

                    if (availableEntity != null && availableEntity.Value != attribute.Value)
                    {
                        ComparisonInventory(attribute.Name, availableEntity.Value, attribute.Value, changed, true);

                        availableEntity.Value = attribute.Value;
                    }
                }

                // deletion
                foreach (var attribute in removes)
                {
                    var availableEntity = (from i in DbContext.Inventories
                                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                                           where i.Guid == inventory.Guid && a.Guid == attribute.Guid
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
        /// Determination of the changed values by comparing old and new values.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="orgValue">The value before the change.</param>
        /// <param name="newValue">The changed value.</param>
        /// <param name="list">A list of the changes.</param>
        /// <param name="apply">The values are copied in the journal.</param>
        private static void ComparisonInventory(string name, string orgValue, string newValue, List<Tuple<string, string, string, bool>> list, bool apply)
        {
            if (orgValue != newValue)
            {
                list.Add(new Tuple<string, string, string, bool>(name, orgValue, newValue, apply));
            }
        }

        /// <summary>
        /// Deletes an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item to be deleted.</param>
        public static void DeleteInventory(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entity = DbContext.Inventories.Where(x => x.Guid == inventory.Guid).FirstOrDefault();
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
        /// Checks if the inventory item is in use.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetInventoryInUse(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join p in DbContext.Inventories on i.Id equals p.ParentId
                           where i.Guid == inventory.Guid
                           select i;

                return used.Any();
            }
        }
    }
}