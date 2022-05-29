using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Fügt ein Journaleintrag hinzu
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        public static void AddInventoryJournal(WebItemEntityInventory inventory, WebItemEntityJournal journal)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.ID).FirstOrDefault();
                var journalEntity = new InventoryJournal()
                {
                    Guid = journal.ID,
                    Created = journal.Created,
                    Action = journal.Action
                };

                DbContext.InventoryJournals.Add(journalEntity);
                DbContext.SaveChanges();

                DbContext.InventoryJournalParameters.AddRange(journal.Parameters.Select(x => new InventoryJournalParameter()
                {
                    InventoryJournalId = journalEntity.Id,
                    Guid = x.ID,
                    OldValue = x.OldValue,
                    NewValue = x.NewValue
                }));
                DbContext.SaveChanges();
            }
        }
    }
}