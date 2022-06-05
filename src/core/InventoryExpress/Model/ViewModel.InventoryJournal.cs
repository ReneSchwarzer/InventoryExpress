using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Fügt ein Journaleintrag hinzu
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <param name="journal">Der Journaleintrag</param>
        public static void AddInventoryJournal(WebItemEntityInventory inventory, WebItemEntityJournal journal)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();
                var journalEntity = new InventoryJournal()
                {
                    InventoryId = inventoryEntity.Id,
                    Guid = journal.Id,
                    Created = journal.Created,
                    Action = journal.Action
                };

                DbContext.InventoryJournals.Add(journalEntity);
                DbContext.SaveChanges();

                DbContext.InventoryJournalParameters.AddRange(journal.Parameters.Select(x => new InventoryJournalParameter()
                {
                    InventoryJournalId = journalEntity.Id,
                    Name = x.Name,
                    Guid = x.Id,
                    OldValue = x.OldValue,
                    NewValue = x.NewValue
                }));
                DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Liefert die Journaleinträge zu einem Inventargegenstand
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Eine Aufzählung mit den Journaleinträgen</returns>
        public static IEnumerable<WebItemEntityJournal> GetInventoryJournals(WebItemEntityInventory inventory)
        {
            var journal = new List<WebItemEntityJournal>();
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories
                    .Where(x => x.Guid == inventory.Id)
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