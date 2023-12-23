using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebPage;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Create a task to import the data.
        /// </summary>
        /// <param name="context">The context in which the task is valid.</param>
        /// <param name="data">The zip file.</param>
        public static void CreateImportTask(RenderContext context, byte[] data)
        {
            var id = $"inventoryexpress_import_{context.Request.Session.Id}";
            var root = ModuleContext.ContextPath;

            if (!ComponentManager.TaskManager.ContainsTask(id))
            {
                var notification = ComponentManager.GetComponent<NotificationManager>()?.AddNotification
                (
                    context?.Request,
                    InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.import.task.inprogress")
                );

                notification.Heading = InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.import.task.heading");
                notification.Progress = 0;
                notification.Type = TypeNotification.Light;
                notification.Icon = root + "/assets/img/import.svg";
                var task = ComponentManager.TaskManager.CreateTask(id, context);

                task.Process += (s, e) =>
                {
                    using var memoryStream = new MemoryStream(data);
                    using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);

                    Import(archive, ViewModel.MediaDirectory, i =>
                    {
                        task.Progress = i;
                        notification.Progress = i;
                    });
                };

                task.Finish += (s, e) =>
                {
                    notification.Message = string.Format(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.import.task.done"));
                    notification.Progress = -1;
                    notification.Type = TypeNotification.Success;
                };

                task.Run();
            }
        }

        /// <summary>
        /// Import of data.
        /// </summary>
        /// <param name="archive">The archive.</param>
        /// <param name="dataPath">The directory where the attachments are stored.</param>
        /// <param name="progress">The progress of imports.</param>
        public static void Import(ZipArchive archive, string dataPath, Action<int> progress)
        {
            var dict = new Dictionary<string, XElement>();

            foreach (var entry in archive.Entries)
            {
                using var entryStream = entry.Open();
                using var xmlreader = XmlReader.Create(entryStream, new XmlReaderSettings() { });
                xmlreader.Read();
                var doc = XDocument.ReadFrom(xmlreader);

                dict.Add(entry.Name, doc as XElement);
            }

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            progress(5);

            // media
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("media")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var m = GetMedia(guid);

                m.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                m.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                m.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                m.Tag = v.Element(XName.Get("tag", string.Empty))?.Value;

                var data = Convert.FromBase64String(v.Element(XName.Get("data", string.Empty))?.Value ?? string.Empty);
                if (data != null)
                {
                    File.WriteAllBytes(Path.Combine(dataPath, m.Guid), data);
                }

                AddOrUpdateMedia(m);
            }

            progress(10);

            // attributes
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("attribute")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var attribute = GetAttribute(guid);

                attribute.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                attribute.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                attribute.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                attribute.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                attribute.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);

                AddOrUpdateAttribute(attribute);
            }

            progress(20);

            // states
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("condition")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var condition = GetCondition(guid);

                condition.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                condition.Grade = Convert.ToInt32(v.Element(XName.Get("grade", string.Empty))?.Value);
                condition.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                condition.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                condition.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                condition.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);

                AddOrUpdateCondition(condition);
            }

            progress(30);

            // templates
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("template")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var template = GetTemplate(guid);

                template.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                template.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                template.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                template.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                template.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                template.Tag = v.Element(XName.Get("tag", string.Empty))?.Value;

                template.Attributes = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new WebItemEntityAttribute()
                  {
                      Guid = y.Element(XName.Get("guid", string.Empty))?.Value,
                      Name = y.Element(XName.Get("name", string.Empty))?.Value,
                      Description = y.Element(XName.Get("description", string.Empty))?.Value,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value)
                  })
                );

                AddOrUpdateTemplate(template);
            }

            progress(40);

            // locations
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("location")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var location = GetLocation(guid);

                location.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                location.Address = v.Element(XName.Get("address", string.Empty))?.Value;
                location.Zip = v.Element(XName.Get("zip", string.Empty))?.Value;
                location.Place = v.Element(XName.Get("place", string.Empty))?.Value;
                location.Building = v.Element(XName.Get("building", string.Empty))?.Value;
                location.Room = v.Element(XName.Get("room", string.Empty))?.Value;
                location.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                location.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                location.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                location.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                location.Tag = v.Element(XName.Get("tag", string.Empty))?.Value;

                AddOrUpdateLocation(location);
            }

            progress(50);

            // cost centers
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("costcenter")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var costCenter = GetCostCenter(guid);

                costCenter.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                costCenter.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                costCenter.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                costCenter.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                costCenter.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                costCenter.Tag = v.Element(XName.Get("tag", string.Empty)).Value;

                AddOrUpdateCostCenter(costCenter);
            }

            progress(60);

            // manufacturer
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("manufacturer")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var manufacturer = GetManufacturer(guid);

                manufacturer.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                manufacturer.Address = v.Element(XName.Get("address", string.Empty))?.Value;
                manufacturer.Zip = v.Element(XName.Get("zip", string.Empty))?.Value;
                manufacturer.Place = v.Element(XName.Get("place", string.Empty))?.Value;
                manufacturer.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                manufacturer.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                manufacturer.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                manufacturer.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                manufacturer.Tag = v.Element(XName.Get("tag", string.Empty)).Value;

                AddOrUpdateManufacturer(manufacturer);
            }

            progress(70);

            // suppliers
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("supplier")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var supplier = GetSupplier(guid);

                supplier.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                supplier.Address = v.Element(XName.Get("address", string.Empty))?.Value;
                supplier.Zip = v.Element(XName.Get("zip", string.Empty))?.Value;
                supplier.Place = v.Element(XName.Get("place", string.Empty))?.Value;
                supplier.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                supplier.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                supplier.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                supplier.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                supplier.Tag = v.Element(XName.Get("tag", string.Empty)).Value;

                AddOrUpdateSupplier(supplier);
            }

            progress(80);

            // ledger accounts
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("ledgeraccount")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var ledgerAccount = GetLedgerAccount(guid);

                ledgerAccount.Name = v.Element(XName.Get("name", string.Empty))?.Value;
                ledgerAccount.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                ledgerAccount.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                ledgerAccount.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                ledgerAccount.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                ledgerAccount.Tag = v.Element(XName.Get("tag", string.Empty)).Value;

                AddOrUpdateLedgerAccount(ledgerAccount);
            }

            progress(90);

            // inventory
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            {
                var guid = v.Element(XName.Get("guid", string.Empty))?.Value;
                var inventory = GetInventory(guid);

                inventory.Name = v.Element(XName.Get("name", string.Empty)).Value;
                inventory.CostValue = (decimal)Convert.ToDouble(v.Element(XName.Get("costvalue", string.Empty)).Value, CultureInfo.InvariantCulture);
                inventory.PurchaseDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("purchasedate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("purchasedate", string.Empty))?.Value) : null;
                inventory.DerecognitionDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) : null;
                inventory.Template = GetTemplate(v.Element(XName.Get("template", string.Empty))?.Value);
                inventory.Location = GetLocation(v.Element(XName.Get("location", string.Empty))?.Value);
                inventory.CostCenter = GetCostCenter(v.Element(XName.Get("costcenter", string.Empty))?.Value);
                inventory.Manufacturer = GetManufacturer(v.Element(XName.Get("manufacturer", string.Empty))?.Value);
                inventory.Condition = GetCondition(v.Element(XName.Get("condition", string.Empty))?.Value);
                inventory.Supplier = GetSupplier(v.Element(XName.Get("supplier", string.Empty))?.Value);
                inventory.LedgerAccount = GetLedgerAccount(v.Element(XName.Get("ledgeraccount", string.Empty))?.Value);
                inventory.Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value));
                inventory.Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value);
                inventory.Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value);
                inventory.Media = GetMedia(v.Element(XName.Get("media", string.Empty))?.Value);
                inventory.Tag = v.Element(XName.Get("tag", string.Empty)).Value;

                inventory.Attachments = v.Descendants(XName.Get("attachments", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attachment", string.Empty)).Select(y => new WebItemEntityInventoryAttachment()
                  {
                      Guid = y.Element(XName.Get("guid", string.Empty))?.Value,
                      Name = y.Element(XName.Get("name", string.Empty))?.Value,
                      Description = y.Element(XName.Get("description", string.Empty))?.Value,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value)
                  })
                );

                inventory.Attributes = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new WebItemEntityInventoryAttribute()
                  {
                      Guid = y.Element(XName.Get("guid", string.Empty))?.Value,
                      Name = y.Element(XName.Get("name", string.Empty))?.Value,
                      Description = y.Element(XName.Get("description", string.Empty))?.Value,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                      Value = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("value", string.Empty))?.Value))
                  })
                );

                inventory.Comments = v.Descendants(XName.Get("comments", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("comment", string.Empty)).Select(y => new WebItemEntityComment()
                  {
                      Guid = y.Element(XName.Get("guid", string.Empty))?.Value,
                      Name = y.Element(XName.Get("name", string.Empty))?.Value,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                      Comment = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("text", string.Empty))?.Value))
                  })
                );

                inventory.Journal = v.Descendants(XName.Get("journals", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("jornal", string.Empty)).Select(y =>
                  {
                      var ij = new WebItemEntityJournal()
                      {
                          Guid = y.Element(XName.Get("guid", string.Empty))?.Value,
                          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                          Action = !string.IsNullOrWhiteSpace(y.Element(XName.Get("action", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("action", string.Empty))?.Value)) : null
                      };

                      ij.Parameters = v.Descendants(XName.Get("params", string.Empty)).SelectMany(y => y.Descendants("param").Select(y => new WebItemEntityJournalParameter()
                      {
                          Guid = y.Element(XName.Get("guid", string.Empty)).Value,
                          Name = y.Element(XName.Get("name", string.Empty)).Value,
                          OldValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("oldvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("oldvalue", string.Empty))?.Value)) : null,
                          NewValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("newvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("newvalue", string.Empty))?.Value)) : null
                      })).ToList();

                      return ij;
                  })
                );

                AddOrUpdateInventory(inventory);
            }

            // inventory (link)
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            {
                var inventory = GetInventory(v.Element(XName.Get("guid", string.Empty)).Value);
                inventory.Parent = GetInventory(v.Element(XName.Get("parent", string.Empty)).Value);

                AddOrUpdateInventory(inventory);
            }

            progress(100);
        }
    }
}
