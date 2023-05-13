using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WebExpress.Internationalization;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebComponent;
using WebExpress.WebPage;
using WebExpress.WebTask;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Aufgabe zum exportieren der Daten erstellen
        /// </summary>
        /// <param name="context">Der Kontext, indem die Aufgabe gültig ist</param>
        public static void CreateExportTask(RenderContext context)
        {
            var id = $"inventoryexpress_export_{context.Request.Session.ID}";
            var file = Path.Combine(ExportDirectory, $"{Guid.NewGuid()}.zip");
            var root = ModuleContext.ContextPath;

            if (!TaskManager.ContainsTask(id))
            {
                var notification = ComponentManager.GetComponent<NotificationManager>()?.AddNotification
                (
                    context?.Request,
                    InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.export.task.inprogress")
                );

                notification.Heading = InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.export.task.heading");
                notification.Progress = 0;
                notification.Type = TypeNotification.Light;
                notification.Icon = root + "/assets/img/export.svg";
                var task = TaskManager.CreateTask(id, context);

                task.Process += (s, e) =>
                {
                    Export(file, ModuleContext.AssetPath, i =>
                    {
                        task.Progress = i;
                        notification.Progress = i;
                    });
                };
                task.Finish += (s, e) =>
                {
                    notification.Message = string.Format(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.export.task.done"), $"<a href='{root.Append("/export/" + Path.GetFileNameWithoutExtension(file))}'>{Path.GetFileName(file)}</a>");
                    notification.Progress = -1;
                    notification.Type = TypeNotification.Success;
                };

                task.Run();
            }
        }

        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="file">Der Dateiname der Exportdatei</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public static void Export(string file, string dataPath, Action<int> progress)
        {
            var dict = new Dictionary<string, XElement>();


            //var ascriptions = null as List<Ascription>;
            var attributes = null as List<Entity.Attribute>;
            var templates = null as List<Template>;
            var templateattributes = null as List<TemplateAttribute>;
            var locations = null as List<Location>;
            var costcenters = null as List<CostCenter>;
            var manufacturers = null as List<Manufacturer>;
            var conditions = null as List<Condition>;
            var suppliers = null as List<Supplier>;
            var ledgeraccounts = null as List<LedgerAccount>;
            var inventories = null as List<Inventory>;
            var inventoryjournalparameters = null as List<InventoryJournalParameter>;
            var inventoryattachments = null as List<InventoryAttachment>;
            var inventoryattributes = null as List<InventoryAttribute>;
            var inventorycomments = null as List<InventoryComment>;
            var inventoryjournals = null as List<InventoryJournal>;
            var inventorytags = null as List<InventoryTag>;
            var tag = null as List<Tag>;
            var media = null as List<Media>;

            lock (DbContext)
            {
                //ascriptions = DbContext.Ascriptions.ToList();
                attributes = DbContext.Attributes.ToList();
                templates = DbContext.Templates.ToList();
                templateattributes = DbContext.TemplateAttributes.ToList();
                locations = DbContext.Locations.ToList();
                costcenters = DbContext.CostCenters.ToList();
                manufacturers = DbContext.Manufacturers.ToList();
                conditions = DbContext.Conditions.ToList();
                suppliers = DbContext.Suppliers.ToList();
                ledgeraccounts = DbContext.LedgerAccounts.ToList();
                inventories = DbContext.Inventories.ToList();
                inventoryjournalparameters = DbContext.InventoryJournalParameters.ToList();
                inventoryattachments = DbContext.InventoryAttachments.ToList();
                inventoryattributes = DbContext.InventoryAttributes.ToList();
                inventorycomments = DbContext.InventoryComments.ToList();
                inventoryjournals = DbContext.InventoryJournals.ToList();
                inventorytags = DbContext.InventoryTags.ToList();
                tag = DbContext.Tags.ToList();
                media = DbContext.Media.ToList();
            }


            //// Zuschreibungen
            //foreach (var v in Ascriptions)
            //{
            //    var media = Media.ToList();
            //    var xml = new XElement("attribute");
            //    xml.Add(new XAttribute("version", 1));
            //    // Item
            //    xml.Add(new XElement("guid", v.Guid));
            //    xml.Add(new XElement("name", v.Name));
            //    xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
            //    xml.Add(new XElement("created", v.Created));
            //    xml.Add(new XElement("updated", v.Updated));
            //    xml.Add(new XElement("media", v.Media?.Guid));
            //    // ItemTag
            //    xml.Add(new XElement("tag", v.Tag));

            //    dict.Add(v.Guid, xml);
            //}

            progress(5);

            // Attribute
            foreach (var v in attributes)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("attribute");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));

                dict.Add(v.Guid, xml);
            }

            progress(10);

            // Zustände
            foreach (var v in conditions)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("condition");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("grade", v.Grade));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));

                dict.Add(v.Guid, xml);
            }

            progress(20);

            // Kostenstellen
            foreach (var v in costcenters)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("costcenter");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));

                dict.Add(v.Guid, xml);
            }

            progress(30);

            // Inventar
            foreach (var v in inventories)
            {
                var templateGuid = templates.Where(x => x.Id == v.TemplateId).Select(x => x.Guid).FirstOrDefault();
                var locationGuid = locations.Where(x => x.Id == v.LocationId).Select(x => x.Guid).FirstOrDefault();
                var costcenterGuid = costcenters.Where(x => x.Id == v.CostCenterId).Select(x => x.Guid).FirstOrDefault();
                var manufacturerGuid = manufacturers.Where(x => x.Id == v.ManufacturerId).Select(x => x.Guid).FirstOrDefault();
                var conditionGuid = conditions.Where(x => x.Id == v.ConditionId).Select(x => x.Guid).FirstOrDefault();
                var supplierGuid = suppliers.Where(x => x.Id == v.SupplierId).Select(x => x.Guid).FirstOrDefault();
                var ledgeraccountGuid = ledgeraccounts.Where(x => x.Id == v.LedgerAccountId).Select(x => x.Guid).FirstOrDefault();
                var parentGuid = v.Parent?.Guid;
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("inventory");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("costvalue", v.CostValue.ToString(CultureInfo.InvariantCulture)));
                xml.Add(new XElement("purchasedate", v.PurchaseDate));
                xml.Add(new XElement("derecognitiondate", v.DerecognitionDate));
                xml.Add(new XElement("template", templateGuid));
                xml.Add(new XElement("location", locationGuid));
                xml.Add(new XElement("costcenter", costcenterGuid));
                xml.Add(new XElement("manufacturer", manufacturerGuid));
                xml.Add(new XElement("condition", conditionGuid));
                xml.Add(new XElement("supplier", supplierGuid));
                xml.Add(new XElement("ledgeraccount", ledgeraccountGuid));
                xml.Add(new XElement("parent", parentGuid));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));

                xml.Add(new XElement("attachments", inventoryattachments.Where(x => x.InventoryId == v.Id).Select(x =>
                        new XElement("attachment", new XElement("created", x.Created), new XElement("media", media.Where(y => y.Id == x.MediaId).Select(y => y.Guid).FirstOrDefault()))
                )));

                xml.Add(new XElement("attributes", inventoryattributes.Where(x => x.InventoryId == v.Id).Select(x =>
                    new XElement("attribute",
                        new XElement("created", x.Created),
                        new XElement("guid", attributes.Where(y => y.Id == x.AttributeId).Select(y => y.Guid).FirstOrDefault()),
                        new XElement("value", !string.IsNullOrWhiteSpace(x.Value) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(x.Value)) : string.Empty))
                )));

                xml.Add(new XElement("comments", inventorycomments.Where(x => x.InventoryId == v.Id).Select(x =>
                    new XElement("comment", new XElement("created", x.Created), new XElement("updated", x.Updated), new XElement("guid", x.Guid), new XElement("text", Convert.ToBase64String(Encoding.UTF8.GetBytes(x.Comment))))
                )));

                xml.Add(new XElement("journals", inventoryjournals.Where(x => x.InventoryId == v.Id).Select(x =>
                    new XElement("jornal", new XElement("created", x.Created), new XElement("guid", x.Guid), new XElement("action", Convert.ToBase64String(Encoding.UTF8.GetBytes(x.Action))),
                        new XElement("params", inventoryjournalparameters.Where(y => y.InventoryJournalId == x.Id).Select(y =>
                            new XElement("param",
                                new XElement("name", y.Name),
                                new XElement("guid", y.Guid),
                                new XElement("oldvalue", !string.IsNullOrWhiteSpace(y.OldValue) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(y.OldValue)) : string.Empty),
                                new XElement("newvalue", !string.IsNullOrWhiteSpace(y.NewValue) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(y.NewValue)) : string.Empty)
                            )
                         ))
                    ))
                ));

                xml.Add(new XElement("tags", inventorytags.Where(x => x.InventoryId == v.Id).Select(x =>
                   new XElement("tag", new XElement("label", x.Tag?.Label)))
               ));

                dict.Add(v.Guid, xml);
            }

            progress(40);

            // Sachkonten
            foreach (var v in ledgeraccounts)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("ledgeraccount");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));

                dict.Add(v.Guid, xml);
            }

            progress(50);

            // Standort
            foreach (var v in locations)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("location");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));
                // ItemAaddress
                xml.Add(new XElement("address", v.Address));
                xml.Add(new XElement("zip", v.Zip));
                xml.Add(new XElement("place", v.Place));
                //
                xml.Add(new XElement("building", v.Building));
                xml.Add(new XElement("room", v.Room));

                dict.Add(v.Guid, xml);
            }

            progress(60);

            // Hersteller
            foreach (var v in manufacturers)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("manufacturer");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));
                // ItemAaddress
                xml.Add(new XElement("address", v.Address));
                xml.Add(new XElement("zip", v.Zip));
                xml.Add(new XElement("place", v.Place));

                dict.Add(v.Guid, xml);
            }

            progress(70);

            // Lieferant
            foreach (var v in suppliers)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("supplier");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));
                // ItemAaddress
                xml.Add(new XElement("address", v.Address));
                xml.Add(new XElement("zip", v.Zip));
                xml.Add(new XElement("place", v.Place));

                dict.Add(v.Guid, xml);
            }

            progress(80);

            // Template
            foreach (var v in templates)
            {
                var mediaGuid = media.Where(x => x.Id == v.MediaId).Select(x => x.Guid).FirstOrDefault();

                var xml = new XElement("template");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", !string.IsNullOrWhiteSpace(v.Description) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(v.Description)) : string.Empty));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", mediaGuid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));
                //
                xml.Add(new XElement("attributes", templateattributes.Where(x => x.TemplateId == v.Id).Select(x =>
                    new XElement("attribute", new XElement("created", x.Created), new XElement("guid", x.Attribute?.Guid)))
                ));

                dict.Add(v.Guid, xml);
            }

            progress(90);

            // Medien
            foreach (var v in media)
            {
                var xml = new XElement("media");
                xml.Add(new XAttribute("version", 1));
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("tag", v.Tag));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));

                var path = MediaDirectory;

                if (File.Exists(Path.Combine(path, v.Guid)))
                {
                    xml.Add(new XElement("data", Convert.ToBase64String(File.ReadAllBytes(Path.Combine(path, v.Guid)))));
                }

                dict.Add(v.Guid, xml);
            }

            progress(95);

            if (!Directory.Exists(Path.GetDirectoryName(file)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file));
            }

            using var stream = File.Create(file);
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create);

            var xmlWriterSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            foreach (var entryFile in dict)
            {
                var entry = archive.CreateEntry(entryFile.Key, CompressionLevel.Optimal);
                using var entryStream = entry.Open();
                using var xmlwriter = XmlWriter.Create(entryStream, xmlWriterSettings);
                var doc = new XDocument(entryFile.Value);

                doc.WriteTo(xmlwriter);
            }

            progress(100);
        }
    }
}
