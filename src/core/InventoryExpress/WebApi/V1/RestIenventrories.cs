using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.WebResource;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Inventargegenstände
    /// </summary>
    [ID("RestInventoriesV1")]
    [Segment("inventories", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestInventories : ResourceRestCrud
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestInventories()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<ResourceRestCrudColumn> GetColumns(Request request)
        {
            return new ResourceRestCrudColumn[]
            {
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.inventory.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Uri + \"'>\" + item.Name + \"</a>\");",
                    Width = 10
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.template.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.TemplateUri + \"'>\" + item.Template + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.manufacturer.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.ManufacturerUri + \"'>\" + item.Manufacturer + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.supplier.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.SupplierUri + \"'>\" + item.Supplier + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.location.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.LocationUri + \"'>\" + item.Location + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.costcenter.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.CostCenterUri + \"'>\" + item.CostCenter + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.ledgeraccount.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.LedgerAccountUri + \"'>\" + item.LedgerAccount + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.label"))
                {
                    Render = "return $(\"<img style='height:1em;' src='\" + item.ConditionUri + \"' alt='\" + item.Condition + \"'/>\");"
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="id">Die ID oder null wenn nicht gefiltert werden soll</param>
        /// <param name="search">Ein Suchstring oder null wenn nicht gefiltert werden soll</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<object> GetData(string id, string search, Request request)
        {
            var root = request.Uri.Root;

            lock (ViewModel.Instance.Database)
            {
                var inventories = from i in ViewModel.Instance.Inventories
                                  join t in ViewModel.Instance.Templates on i.TemplateId equals t.Id into tj
                                  from t in tj.DefaultIfEmpty()
                                  join m in ViewModel.Instance.Manufacturers on i.ManufacturerId equals m.Id into mj
                                  from m in mj.DefaultIfEmpty()
                                  join s in ViewModel.Instance.Suppliers on i.SupplierId equals s.Id into sj
                                  from s in sj.DefaultIfEmpty()
                                  join l in ViewModel.Instance.Locations on i.LocationId equals l.Id into lj
                                  from l in lj.DefaultIfEmpty()
                                  join c in ViewModel.Instance.CostCenters on i.CostCenterId equals c.Id into cj
                                  from c in cj.DefaultIfEmpty()
                                  join la in ViewModel.Instance.LedgerAccounts on i.LedgerAccountId equals la.Id into laj
                                  from la in laj.DefaultIfEmpty()
                                  join co in ViewModel.Instance.Conditions on i.ConditionId equals co.Id into coj
                                  from co in coj.DefaultIfEmpty()
                                  select new
                                  {
                                      ID = i.Guid,
                                      Uri = root.Append(i.Guid).ToString(),
                                      Name = i.Name,
                                      TemplateUri = t.Guid != null ? root.Append($"setting/templates/{ t.Guid }").ToString() : "",
                                      Template = t.Name ?? "",
                                      ManufacturerUri = m.Guid != null ? root.Append($"manufacturers/{ m.Guid }").ToString() : "",
                                      Manufacturer = m.Name ?? "",
                                      SupplierUri = s.Guid != null ? root.Append($"suppliers/{ s.Guid }").ToString() : "",
                                      Supplier = s.Name ?? "",
                                      LocationUri = l.Guid != null ? root.Append($"locations/{ l.Guid }").ToString() : "",
                                      Location = l.Name ?? "",
                                      CostCenterUri = c.Guid != null ? root.Append($"costcenters/{ c.Guid }").ToString() : "",
                                      CostCenter = c.Name ?? "",
                                      LedgerAccountUri = la.Guid != null ? root.Append($"ledgeraccounts/{ la.Guid }").ToString() : "",
                                      LedgerAccount = la.Name ?? "",
                                      ConditionUri = co.Guid != null ? root.Append($"assets/img/condition_{ co.Grade }.svg").ToString() : "",
                                      Condition = co.Name ?? ""
                                  };

                if (id != null)
                {
                    inventories = inventories.Where(x => x.ID.Equals(id));
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    inventories = inventories.Where
                    (
                        x =>
                        x.Name.ToLower().Contains(search)
                    );
                }

                return inventories.ToList();
            }
        }

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(Request request)
        {
            return true;
        }
    }
}
