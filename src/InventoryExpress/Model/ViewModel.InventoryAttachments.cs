﻿using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebExpress.WebMessage;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Delivers the attachments of an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The attachment of the inventory item.</returns>
        public static IEnumerable<WebItemEntityMedia> GetInventoryAttachments(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var attachments = from i in DbContext.Inventories
                                  join ia in DbContext.InventoryAttachments on i.Id equals ia.InventoryId
                                  join m in DbContext.Media on ia.MediaId equals m.Id
                                  where i.Guid == (inventory != null ? inventory.Guid : null)
                                  select new WebItemEntityMedia(m);

                return attachments;
            }
        }

        /// <summary>
        /// Adds an attachment.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <param name="file">The attachment.</param>
        public static void AddOrUpdateInventoryAttachment(WebItemEntityInventory inventory, ParameterFile file)
        {
            var root = Path.Combine(ModuleContext.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.inventory.attachment.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Guid).FirstOrDefault();
                var availableEntity = (from i in DbContext.Media
                                       join ia in DbContext.InventoryAttachments on i.Id equals ia.InventoryId
                                       join m in DbContext.Media on ia.MediaId equals m.Id
                                       where i.Id == inventoryEntity.Id & m.Name == filename
                                       select m).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = file.Value,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    var link = new InventoryAttachment()
                    {
                        InventoryId = inventoryEntity.Id,
                        MediaId = entity.Id
                    };

                    DbContext.InventoryAttachments.Add(link);
                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.attachment.add",
                        Parameters = new[] { journalParameter }
                    };

                    AddInventoryJournal(inventory, journal);
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    // update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.attachment.edit",
                        Parameters = new[] { journalParameter }
                    };

                    AddInventoryJournal(inventory, journal);
                }
            }

            if (inventory.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file?.Data);
            }
        }

        /// <summary>
        /// Delivers the attachments of an inventory item.
        /// </summary>
        /// <param name="media">The document to remove.</param>
        public static void DeleteInventoryAttachments(WebItemEntityMedia media)
        {
            lock (DbContext)
            {
                var mediaEntity = DbContext.Media.Where(x => x.Guid == media.Guid).FirstOrDefault();
                var attachmentEntity = DbContext.InventoryAttachments.Where(x => x.MediaId == mediaEntity.Id).FirstOrDefault();

                DbContext.InventoryAttachments.Remove(attachmentEntity);
                DbContext.Media.Remove(mediaEntity);
                DbContext.SaveChanges();
            }

            File.Delete(Path.Combine(MediaDirectory, media.Guid));
        }

        /// <summary>
        /// Counts the attachments of an inventory item.
        /// </summary>
        /// <param name="guid">The id of the inventory item.</param>
        /// <returns>The number of attachments that are assigned to the inventory item.</returns>
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