using System;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Import of data.
        /// </summary>
        /// <param name="fileName">The archive.</param>
        /// <param name="dataPath">The directory where the attachments are stored.</param>
        /// <param name="progress">The progress of imports.</param>
        public static void Import(string fileName, string dataPath, Action<int> progress)
        {
            //var dict = new Dictionary<string, XElement>();
            //var viewModel = ViewModel.Instance;
            //using var stream = File.OpenRead(fileName);
            //using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
            //var media = new List<Media>();
            //var attributes = new List<Entity.Attribute>();
            //var templates = new List<Template>();
            //var templateattributes = new List<TemplateAttribute>();
            //var locations = new List<Location>();
            //var costcenters = new List<CostCenter>();
            //var manufacturers = new List<Manufacturer>();
            //var conditions = new List<Condition>();
            //var suppliers = new List<Supplier>();
            //var ledgeraccounts = new List<LedgerAccount>();
            //var inventories = new List<Inventory>();
            //var inventoryattachments = new List<InventoryAttachment>();
            //var inventoryattributes = new List<InventoryAttribute>();
            //var inventorycomments = new List<InventoryComment>();
            //var inventoryjournals = new List<InventoryJournal>();
            //var inventoryjournalparameters = new List<InventoryJournalParameter>();
            //var inventorytags = new List<InventoryTag>();
            //var tag = new List<Tag>();

            //    lock (DbContext)
            //    {
            //        using var transaction = DbContext.Database.BeginTransaction();

            //        DbContext.Conditions.RemoveRange(DbContext.Conditions);
            //        DbContext.Locations.RemoveRange(DbContext.Locations);
            //        DbContext.Manufacturers.RemoveRange(DbContext.Manufacturers);
            //        DbContext.Suppliers.RemoveRange(DbContext.Suppliers);
            //        DbContext.LedgerAccounts.RemoveRange(DbContext.LedgerAccounts);
            //        DbContext.CostCenters.RemoveRange(DbContext.CostCenters);
            //        DbContext.Inventories.RemoveRange(DbContext.Inventories);
            //        DbContext.InventoryAttributes.RemoveRange(DbContext.InventoryAttributes);
            //        DbContext.InventoryAttachments.RemoveRange(DbContext.InventoryAttachments);
            //        DbContext.InventoryComments.RemoveRange(DbContext.InventoryComments);
            //        DbContext.InventoryJournals.RemoveRange(DbContext.InventoryJournals);
            //        DbContext.InventoryJournalParameters.RemoveRange(DbContext.InventoryJournalParameters);
            //        DbContext.InventoryTags.RemoveRange(DbContext.InventoryTags);
            //        DbContext.Templates.RemoveRange(DbContext.Templates);
            //        DbContext.TemplateAttributes.RemoveRange(DbContext.TemplateAttributes);
            //        DbContext.Attributes.RemoveRange(DbContext.Attributes);
            //        DbContext.Media.RemoveRange(DbContext.Media);
            //        DbContext.Tags.RemoveRange(DbContext.Tags);
            //        //Database.ExecuteSqlCommand("vacum;");

            //        DbContext.SaveChanges();

            //        transaction.Commit();
            //    }

            //foreach (var entry in archive.Entries)
            //{
            //    using var entryStream = entry.Open();
            //    using var xmlreader = XmlReader.Create(entryStream, new XmlReaderSettings() { });
            //    xmlreader.Read();
            //    var doc = XDocument.ReadFrom(xmlreader);

            //    dict.Add(entry.Name, doc as XElement);
            //}

            //var path = Path.Combine(dataPath, "media");

            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            //progress(5);

            //// media
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("media")))
            //{
            //    var m = new Media()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        Tag = v.Element(XName.Get("tag", string.Empty))?.Value
            //    };

            //    var data = Convert.FromBase64String(v.Element(XName.Get("data", string.Empty))?.Value);
            //    if (data != null)
            //    {
            //        File.WriteAllBytes(Path.Combine(path, m.Guid), data);
            //    }

            //    media.Add(m);
            //}

            //progress(10);

            //lock (viewModel.Database)
            //{
            //    viewModel.Media.AddRange(media);
            //    viewModel.SaveChanges();
            //}

            //progress(15);

            //// attributes
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("attribute")))
            //{
            //    var attribute = new Entity.Attribute()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id
            //    };

            //    attributes.Add(attribute);
            //}

            //progress(20);

            //lock (viewModel.Database)
            //{
            //    viewModel.Attributes.AddRange(attributes);
            //    viewModel.SaveChanges();
            //}

            //// states
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("condition")))
            //{
            //    var condition = new Condition()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Grade = Convert.ToInt32(v.Element(XName.Get("grade", string.Empty))?.Value),
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id
            //    };

            //    conditions.Add(condition);
            //}

            //progress(25);

            //lock (viewModel.Database)
            //{
            //    viewModel.Conditions.AddRange(conditions);
            //    viewModel.SaveChanges();
            //}

            //// templates
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("template")))
            //{
            //    var template = new Template()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty))?.Value
            //    };

            //    templates.Add(template);

            //    var ta = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
            //    (
            //      x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new TemplateAttribute()
            //      {
            //          Template = template,
            //          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
            //          Attribute = attributes.Where(z => z.Guid == y.Element(XName.Get("Guid", string.Empty))?.Value).FirstOrDefault()
            //      })
            //    ).ToList();

            //    templateattributes.AddRange(ta);
            //}

            //progress(30);

            //lock (viewModel.Database)
            //{
            //    viewModel.Templates.AddRange(templates);
            //    viewModel.TemplateAttributes.AddRange(templateattributes);
            //    viewModel.SaveChanges();
            //}

            //// locations
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("location")))
            //{
            //    var location = new Location()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Address = v.Element(XName.Get("address", string.Empty))?.Value,
            //        Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
            //        Place = v.Element(XName.Get("place", string.Empty))?.Value,
            //        Building = v.Element(XName.Get("building", string.Empty))?.Value,
            //        Room = v.Element(XName.Get("room", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty))?.Value
            //    };

            //    locations.Add(location);
            //}

            //progress(35);

            //lock (viewModel.Database)
            //{
            //    viewModel.Locations.AddRange(locations);
            //    viewModel.SaveChanges();
            //}

            //// cost centers
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("costcenter")))
            //{
            //    var costcenter = new CostCenter()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty)).Value
            //    };

            //    costcenters.Add(costcenter);
            //}

            //progress(40);

            //lock (viewModel.Database)
            //{
            //    viewModel.CostCenters.AddRange(costcenters);
            //    viewModel.SaveChanges();
            //}

            //// manufacturer
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("manufacturer")))
            //{
            //    var manufacturer = new Manufacturer()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Address = v.Element(XName.Get("address", string.Empty))?.Value,
            //        Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
            //        Place = v.Element(XName.Get("place", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty)).Value
            //    };

            //    manufacturers.Add(manufacturer);
            //}

            //progress(45);

            //lock (viewModel.Database)
            //{
            //    viewModel.Manufacturers.AddRange(manufacturers);
            //    viewModel.SaveChanges();
            //}

            //// suppliers
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("supplier")))
            //{
            //    var supplier = new Supplier()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Address = v.Element(XName.Get("address", string.Empty))?.Value,
            //        Zip = v.Element(XName.Get("zip", string.Empty))?.Value,
            //        Place = v.Element(XName.Get("place", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty)).Value
            //    };

            //    suppliers.Add(supplier);
            //}

            //progress(50);

            //lock (viewModel.Database)
            //{
            //    viewModel.Suppliers.AddRange(suppliers);
            //    viewModel.SaveChanges();
            //}

            //// ledger accounts
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("ledgeraccount")))
            //{
            //    var ledgeraccount = new LedgerAccount()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty))?.Value,
            //        Name = v.Element(XName.Get("name", string.Empty))?.Value,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty)).Value
            //    };

            //    ledgeraccounts.Add(ledgeraccount);
            //}

            //progress(55);

            //lock (viewModel.Database)
            //{
            //    viewModel.LedgerAccounts.AddRange(ledgeraccounts);
            //    viewModel.SaveChanges();
            //}

            //// inventory
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            //{
            //    var inventory = new Inventory()
            //    {
            //        Guid = v.Element(XName.Get("Guid", string.Empty)).Value,
            //        Name = v.Element(XName.Get("name", string.Empty)).Value,
            //        CostValue = (decimal)Convert.ToDouble(v.Element(XName.Get("costvalue", string.Empty)).Value, CultureInfo.InvariantCulture),
            //        PurchaseDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("purchasedate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("purchasedate", string.Empty))?.Value) : null,
            //        DerecognitionDate = !string.IsNullOrWhiteSpace(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) ? Convert.ToDateTime(v.Element(XName.Get("derecognitiondate", string.Empty))?.Value) : null,
            //        TemplateId = templates.Where(x => x.Guid == v.Element(XName.Get("template", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        LocationId = locations.Where(x => x.Guid == v.Element(XName.Get("location", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        CostCenterId = costcenters.Where(x => x.Guid == v.Element(XName.Get("costcenter", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        ManufacturerId = manufacturers.Where(x => x.Guid == v.Element(XName.Get("manufacturer", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        ConditionId = conditions.Where(x => x.Guid == v.Element(XName.Get("condition", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        SupplierId = suppliers.Where(x => x.Guid == v.Element(XName.Get("supplier", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        LedgerAccountId = ledgeraccounts.Where(x => x.Guid == v.Element(XName.Get("ledgeraccount", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Description = Encoding.UTF8.GetString(Convert.FromBase64String(v.Element(XName.Get("description", string.Empty))?.Value)),
            //        Created = Convert.ToDateTime(v.Element(XName.Get("created", string.Empty))?.Value),
            //        Updated = Convert.ToDateTime(v.Element(XName.Get("updated", string.Empty))?.Value),
            //        MediaId = media.Where(x => x.Guid == v.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault()?.Id,
            //        Tag = v.Element(XName.Get("tag", string.Empty)).Value
            //    };

            //    inventories.Add(inventory);

            //    var ia = v.Descendants(XName.Get("attachments", string.Empty)).SelectMany
            //    (
            //      x => x.Descendants(XName.Get("attachment", string.Empty)).Select(y => new InventoryAttachment()
            //      {
            //          Inventory = inventory,
            //          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
            //          MediaId = media.Where(z => z.Guid == y.Element(XName.Get("media", string.Empty))?.Value).FirstOrDefault().Id
            //      })
            //    );

            //    inventoryattachments.AddRange(ia);

            //    var iattr = v.Descendants(XName.Get("attributes", string.Empty)).SelectMany
            //    (
            //      x => x.Descendants(XName.Get("attribute", string.Empty)).Select(y => new InventoryAttribute()
            //      {
            //          Inventory = inventory,
            //          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
            //          AttributeId = attributes.Where(z => z.Guid == y.Element(XName.Get("Guid", string.Empty))?.Value).FirstOrDefault().Id,
            //          Value = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("value", string.Empty))?.Value))
            //      })
            //    );

            //    inventoryattributes.AddRange(iattr);

            //    var ic = v.Descendants(XName.Get("comments", string.Empty)).SelectMany
            //    (
            //      x => x.Descendants(XName.Get("comment", string.Empty)).Select(y => new InventoryComment()
            //      {
            //          Inventory = inventory,
            //          Guid = y.Element(XName.Get("Guid", string.Empty)).Value,
            //          Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
            //          Updated = Convert.ToDateTime(y.Element(XName.Get("updated", string.Empty))?.Value),
            //          Comment = Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("text", string.Empty))?.Value))
            //      })
            //    );

            //    inventorycomments.AddRange(ic);

            //    var j = v.Descendants(XName.Get("journals", string.Empty)).SelectMany
            //    (
            //      x => x.Descendants(XName.Get("jornal", string.Empty)).Select(y =>
            //      {
            //          var ij = new InventoryJournal()
            //          {
            //              Guid = y.Element(XName.Get("Guid", string.Empty)).Value,
            //              Created = Convert.ToDateTime(y.Element(XName.Get("created", string.Empty))?.Value),
            //              Inventory = inventory,
            //              Action = !string.IsNullOrWhiteSpace(y.Element(XName.Get("action", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("action", string.Empty))?.Value)) : null
            //          };

            //          ij.InventoryJournalParameters = v.Descendants(XName.Get("params", string.Empty)).SelectMany(y => y.Descendants("param").Select(y => new InventoryJournalParameter()
            //          {
            //              InventoryJournal = ij,
            //              Name = y.Element(XName.Get("name", string.Empty)).Value,
            //              Guid = y.Element(XName.Get("Guid", string.Empty)).Value,
            //              OldValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("oldvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("oldvalue", string.Empty))?.Value)) : null,
            //              NewValue = !string.IsNullOrWhiteSpace(y.Element(XName.Get("newvalue", string.Empty))?.Value) ? Encoding.UTF8.GetString(Convert.FromBase64String(y.Element(XName.Get("newvalue", string.Empty))?.Value)) : null
            //          })).ToList();

            //          return ij;
            //      })
            //    );

            //    inventoryjournalparameters.AddRange(j.SelectMany(x => x.InventoryJournalParameters));
            //    inventoryjournals.AddRange(j);
            //}

            //progress(60);

            //lock (viewModel.Database)
            //{
            //    viewModel.InventoryJournalParameters.AddRange(inventoryjournalparameters);
            //    viewModel.InventoryJournals.AddRange(inventoryjournals);
            //    viewModel.InventoryComments.AddRange(inventorycomments);
            //    viewModel.InventoryAttributes.AddRange(inventoryattributes);
            //    viewModel.InventoryAttachments.AddRange(inventoryattachments);
            //    viewModel.Inventories.AddRange(inventories);
            //    viewModel.SaveChanges();
            //}

            //// inventory (link)
            //foreach (var v in dict.Values.Where(x => x.Name.LocalName.Equals("inventory")))
            //{
            //    var inventory = inventories.Where(x => x.Guid == v.Element(XName.Get("Guid", string.Empty)).Value).FirstOrDefault();
            //    inventory.ParentId = inventories.Where(x => x.Guid == v.Element(XName.Get("parent", string.Empty)).Value).FirstOrDefault()?.Id;
            //}

            //progress(65);

            //lock (viewModel.Database)
            //{
            //    viewModel.SaveChanges();
            //}

            //progress(100);
        }
    }
}
