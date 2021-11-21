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

namespace InventoryExpress.Model
{
    internal static class ImportExport
    {
        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public static void Export(string fileName, string dataPath, Action<int> progress)
        {
            var dict = new Dictionary<string, XElement>();
            var viewModel = ViewModel.Instance;

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

            lock (viewModel.Database)
            {
                //ascriptions = viewModel.Ascriptions.ToList();
                attributes = viewModel.Attributes.ToList();
                templates = viewModel.Templates.ToList();
                templateattributes = viewModel.TemplateAttributes.ToList();
                locations = viewModel.Locations.ToList();
                costcenters = viewModel.CostCenters.ToList();
                manufacturers = viewModel.Manufacturers.ToList();
                conditions = viewModel.Conditions.ToList();
                suppliers = viewModel.Suppliers.ToList();
                ledgeraccounts = viewModel.LedgerAccounts.ToList();
                inventories = viewModel.Inventories.ToList();
                inventoryjournalparameters = viewModel.InventoryJournalParameters.ToList();
                inventoryattachments = viewModel.InventoryAttachments.ToList();
                inventoryattributes = viewModel.InventoryAttributes.ToList();
                inventorycomments = viewModel.InventoryComments.ToList();
                inventoryjournals = viewModel.InventoryJournals.ToList();
                inventorytags = viewModel.InventoryTags.ToList();
                tag = viewModel.Tags.ToList();
                media = viewModel.Media.ToList();
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

                var path = Path.Combine(dataPath, "media");

                if (File.Exists(Path.Combine(path, v.Guid)))
                {
                    xml.Add(new XElement("data", Convert.ToBase64String(File.ReadAllBytes(Path.Combine(path, v.Guid)))));
                }

                dict.Add(v.Guid, xml);
            }

            progress(95);

            using var stream = File.Create(fileName);
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

        /// <summary>
        /// Import der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public static void Import(string fileName, string dataPath, Action<int> progress)
        {
            var dict = new Dictionary<string, XElement>();
            var viewModel = ViewModel.Instance;
            using var stream = File.OpenRead(fileName);
            using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
            var media = new List<Media>();
            var attributes = new List<Entity.Attribute>();
            var templates = new List<Template>();
            var templateattributes = new List<TemplateAttribute>();
            var locations = new List<Location>();
            var costcenters = new List<CostCenter>();
            var manufacturers = new List<Manufacturer>();
            var conditions = new List<Condition>();
            var suppliers = new List<Supplier>();
            var ledgeraccounts = new List<LedgerAccount>();
            var inventories = new List<Inventory>();
            var inventoryattachments = new List<InventoryAttachment>();
            var inventoryattributes = new List<InventoryAttribute>();
            var inventorycomments = new List<InventoryComment>();
            var inventoryjournals = new List<InventoryJournal>();
            var inventoryjournalparameters = new List<InventoryJournalParameter>();
            var inventorytags = new List<InventoryTag>();
            var tag = new List<Tag>();

            foreach (var entry in archive.Entries)
            {
                using var entryStream = entry.Open();
                using var xmlreader = XmlReader.Create(entryStream, new XmlReaderSettings() { });
                xmlreader.Read();
                var doc = XDocument.ReadFrom(xmlreader);

                dict.Add(entry.Name, doc as XElement);
            }

            var path = Path.Combine(dataPath, "media");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            progress(5);

            // Medien
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("media")))
            {
                var m = new Media()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    Tag = v.Element(XName.Get("tag", string.Empty))?.Value
                };

                var data = Convert.FromBase64String(v.Element(XName.Get("data", string.Empty))?.Value);
                if (data != null)
                {
                    File.WriteAllBytes(Path.Combine(path, m.Guid), data);
                }

                media.Add(m);
            }

            progress(10);

            lock (viewModel.Database)
            {
                viewModel.Media.AddRange(media);
                viewModel.SaveChanges();
            }

            progress(15);

            // Attribute
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("attribute")))
            {
                var attribute = new Entity.Attribute()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id
                };

                attributes.Add(attribute);
            }

            progress(20);

            lock (viewModel.Database)
            {
                viewModel.Attributes.AddRange(attributes);
                viewModel.SaveChanges();
            }

            // Zustände
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("condition")))
            {
                var condition = new Condition()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Grade = Convert.ToInt32(v.Element(XName.Get("grade", string.Empty))?.Value),
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id
                };

                conditions.Add(condition);
            }

            progress(25);

            lock (viewModel.Database)
            {
                viewModel.Conditions.AddRange(conditions);
                viewModel.SaveChanges();
            }

            // Vorlagen
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("template")))
            {
                var template = new Template()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty))?.Value
                };

                templates.Add(template);

                var ta = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new TemplateAttribute()
                  {
                      Template = template,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Attribute = attributes.Where(z => z.Guid == y.Element(XName.Get("guid", string.Empty))?.Value).FirstOrDefault()
                  })
                ).ToList();

                templateattributes.AddRange(ta);
            }

            progress(30);

            lock (viewModel.Database)
            {
                viewModel.Templates.AddRange(templates);
                viewModel.TemplateAttributes.AddRange(templateattributes);
                viewModel.SaveChanges();
            }

            // Standorte
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("location")))
            {
                var location = new Location()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Address = v.Element(XName.Get("address", string.Empty))?.Value,
                    Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
                    Place = v.Element(XName.Get("place", string.Empty))?.Value,
                    Building = v.Element(XName.Get("building", string.Empty))?.Value,
                    Room = v.Element(XName.Get("room", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty))?.Value
                };

                locations.Add(location);
            }

            progress(35);

            lock (viewModel.Database)
            {
                viewModel.Locations.AddRange(locations);
                viewModel.SaveChanges();
            }

            // Kostenstellen
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("costcenter")))
            {
                var costcenter = new CostCenter()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty)).Value
                };

                costcenters.Add(costcenter);
            }

            progress(40);

            lock (viewModel.Database)
            {
                viewModel.CostCenters.AddRange(costcenters);
                viewModel.SaveChanges();
            }

            // Hersteller
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("manufacturer")))
            {
                var manufacturer = new Manufacturer()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Address = v.Element(XName.Get("address", string.Empty))?.Value,
                    Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
                    Place = v.Element(XName.Get("place", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty)).Value
                };

                manufacturers.Add(manufacturer);
            }

            progress(45);

            lock (viewModel.Database)
            {
                viewModel.Manufacturers.AddRange(manufacturers);
                viewModel.SaveChanges();
            }

            // Lieferant
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("supplier")))
            {
                var supplier = new Supplier()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Address = v.Element(XName.Get("address", string.Empty))?.Value,
                    Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
                    Place = v.Element(XName.Get("place", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty)).Value
                };

                suppliers.Add(supplier);
            }

            progress(50);

            lock (viewModel.Database)
            {
                viewModel.Suppliers.AddRange(suppliers);
                viewModel.SaveChanges();
            }

            // Sachkonto
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("ledgeraccount")))
            {
                var ledgeraccount = new LedgerAccount()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty))?.Value,
                    Name = v.Element(XName.Get("name", string.Empty))?.Value,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty)).Value
                };

                ledgeraccounts.Add(ledgeraccount);
            }

            progress(55);

            lock (viewModel.Database)
            {
                viewModel.LedgerAccounts.AddRange(ledgeraccounts);
                viewModel.SaveChanges();
            }

            // Inventar
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            {
                var inventory = new Inventory()
                {
                    Guid = v.Element(XName.Get("guid", string.Empty)).Value,
                    Name = v.Element(XName.Get("name", string.Empty)).Value,
                    CostValue = (decimal)Convert.ToDouble(v.Element(XName.Get("costvalue", string.Empty)).Value, CultureInfo.InvariantCulture),
                    PurchaseDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("purchasedate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("purchasedate", string.Empty))?.Value) : null,
                    DerecognitionDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) : null,
                    TemplateId = templates.Where(x => x.Guid == v.Element(XName.Get("template", string.Empty))?.Value).FirstOrDefault()?.Id,
                    LocationId = locations.Where(x => x.Guid == v.Element(XName.Get("location", string.Empty))?.Value).FirstOrDefault()?.Id,
                    CostCenterId = costcenters.Where(x => x.Guid == v.Element(XName.Get("costcenter", string.Empty))?.Value).FirstOrDefault()?.Id,
                    ManufacturerId = manufacturers.Where(x => x.Guid == v.Element(XName.Get("manufacturer", string.Empty))?.Value).FirstOrDefault()?.Id,
                    ConditionId = conditions.Where(x => x.Guid == v.Element(XName.Get("condition", string.Empty))?.Value).FirstOrDefault()?.Id,
                    SupplierId = suppliers.Where(x => x.Guid == v.Element(XName.Get("supplier", string.Empty))?.Value).FirstOrDefault()?.Id,
                    LedgerAccountId = ledgeraccounts.Where(x => x.Guid == v.Element(XName.Get("ledgeraccount", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
                    Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
                    Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
                    MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
                    Tag = v.Element(XName.Get("tag", string.Empty)).Value
                };

                inventories.Add(inventory);

                var ia = v.Descendants(XName.Get("attachments", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attachment", string.Empty)).Select(y => new InventoryAttachment()
                  {
                      Inventory = inventory,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      MediaId = media.Where(z => z.Guid == y.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault().Id
                  })
                );

                inventoryattachments.AddRange(ia);

                var iattr = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new InventoryAttribute()
                  {
                      Inventory = inventory,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      AttributeId = attributes.Where(z => z.Guid == y.Element(XName.Get("guid", string.Empty))?.Value).FirstOrDefault().Id,
                      Value = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("value", string.Empty))?.Value))
                  })
                );

                inventoryattributes.AddRange(iattr);

                var ic = v.Descendants(XName.Get("comments", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("comment", string.Empty)).Select(y => new InventoryComment()
                  {
                      Inventory = inventory,
                      Guid = y.Element(XName.Get("guid", string.Empty)).Value,
                      Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                      Updated = Convert.ToDateTime(y.Element(XName.Get("updated", string.Empty))?.Value),
                      Comment = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("text", string.Empty))?.Value))
                  })
                );

                inventorycomments.AddRange(ic);

                var j = v.Descendants(XName.Get("journals", string.Empty)).SelectMany
                (
                  x => x.Descendants(XName.Get("jornal", string.Empty)).Select(y =>
                  {
                      var ij = new InventoryJournal()
                      {
                          Guid = y.Element(XName.Get("guid", string.Empty)).Value,
                          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
                          Inventory = inventory,
                          Action = !string.IsNullOrWhiteSpace(y.Element(XName.Get("action", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("action", string.Empty))?.Value)) : null
                      };

                      ij.InventoryJournalParameters = v.Descendants(XName.Get("params", string.Empty)).SelectMany(y => y.Descendants("param").Select(y => new InventoryJournalParameter()
                      {
                          InventoryJournal = ij,
                          Name = y.Element(XName.Get("name", string.Empty)).Value,
                          Guid = y.Element(XName.Get("guid", string.Empty)).Value,
                          OldValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("oldvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("oldvalue", string.Empty))?.Value)) : null,
                          NewValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("newvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("newvalue", string.Empty))?.Value)) : null
                      })).ToList();

                      return ij;
                  })
                );

                inventoryjournalparameters.AddRange(j.SelectMany(x => x.InventoryJournalParameters));
                inventoryjournals.AddRange(j);
            }

            progress(60);

            lock (viewModel.Database)
            {
                viewModel.InventoryJournalParameters.AddRange(inventoryjournalparameters);
                viewModel.InventoryJournals.AddRange(inventoryjournals);
                viewModel.InventoryComments.AddRange(inventorycomments);
                viewModel.InventoryAttributes.AddRange(inventoryattributes);
                viewModel.InventoryAttachments.AddRange(inventoryattachments);
                viewModel.Inventories.AddRange(inventories);
                viewModel.SaveChanges();
            }

            // Inventar (Verknüpfung)
            foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            {
                var inventory = inventories.Where(x => x.Guid == v.Element(XName.Get("guid", string.Empty)).Value).FirstOrDefault();
                inventory.ParentId = inventories.Where(x => x.Guid == v.Element(XName.Get("parent", string.Empty)).Value).FirstOrDefault()?.Id;
            }

            progress(65);

            lock (viewModel.Database)
            {
                viewModel.SaveChanges();
            }

            progress(100);
        }
    }
}
