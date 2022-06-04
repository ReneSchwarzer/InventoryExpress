using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Liefert die Anlagen eins Inventargegenstandes
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Die Anlagen des Inventargegenstandes</returns>
        public static IEnumerable<WebItemEntityMedia> GetInventoryAttachments(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var attachments = from i in DbContext.Inventories
                                  join ia in DbContext.InventoryAttachments on i.Id equals ia.InventoryId
                                  join m in DbContext.Media on ia.MediaId equals m.Id
                                  where i.Guid == inventory.Id
                                  select new WebItemEntityMedia(m);

                return attachments.ToList();
            }
        }

        /// <summary>
        /// Liefert die Anlagen eins Inventargegenstandes
        /// </summary>
        /// <param name="media">Das zu entfernende Dokument</param>
        public static void DeleteInventoryAttachments(WebItemEntityMedia media)
        {
            lock (DbContext)
            {
                var mediaEntity = DbContext.Media.Where(x => x.Guid == media.Id).FirstOrDefault();
                var attachmentEntity = DbContext.InventoryAttachments.Where(x => x.MediaId == mediaEntity.Id).FirstOrDefault();

                DbContext.InventoryAttachments.Remove(attachmentEntity);
                DbContext.Media.Remove(mediaEntity);
                DbContext.SaveChanges();
            }

            File.Delete(Path.Combine(MediaDirectory, media.Id));
        }

        /// <summary>
        /// Zählt die Anlagen eines Inventargegenstandes
        /// </summary>
        /// <param name="guid">Die Id des Inventargegenstandes</param>
        /// <returns>Die Anzahl der Anlagen, welche dem Inventargegenstand zugeordnet sind</returns>
        public static long CountInventoryAttachments(string guid)
        {
            lock (DbContext)
            {
                var attachments = from i in DbContext.Inventories
                                  join a in DbContext.InventoryAttachments
                                  on i.Id equals a.InventoryId
                                  where i.Guid == guid
                                  select a;

                return attachments.LongCount();
            }
        }
    }
}