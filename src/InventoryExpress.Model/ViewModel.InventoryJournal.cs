using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Adds a journal entry.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <param name="journal">The journal entry.</param>
        public static void AddInventoryJournal(WebItemEntityInventory inventory, WebItemEntityJournal journal)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Guid).FirstOrDefault();
                var journalEntity = new InventoryJournal()
                {
                    InventoryId = inventoryEntity.Id,
                    Guid = journal.Guid,
                    Created = journal.Created,
                    Action = journal.Action
                };

                DbContext.InventoryJournals.Add(journalEntity);
                DbContext.SaveChanges();

                DbContext.InventoryJournalParameters.AddRange(journal.Parameters.Select(x => new InventoryJournalParameter()
                {
                    InventoryJournalId = journalEntity.Id,
                    Name = x.Name,
                    Guid = x.Guid,
                    OldValue = x.OldValue,
                    NewValue = x.NewValue
                }));
                DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the journal entries for an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>A enumeration of journal entries.</returns>
        public static IEnumerable<WebItemEntityJournal> GetInventoryJournals(WebItemEntityInventory inventory)
        {
            var journal = new List<WebItemEntityJournal>();
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories
                    .Where(x => x.Guid == inventory.Guid)
                    .FirstOrDefault();

                if (inventoryEntity != null)
                {
                    journal.AddRange
                    (
                        DbContext.InventoryJournals
                            .Where(x => x.InventoryId == inventoryEntity.Id)
                            .Select(x => new WebItemEntityJournal(x)
                            {
                                Parameters = DbContext.InventoryJournalParameters
                                    .Where(y => y.InventoryJournalId == x.Id)
                                    .Select(y => new WebItemEntityJournalParameter(y)).ToList()
                            })
                    );
                }
            }

            return journal.OrderByDescending(x => x.Created);
        }
    }
}