using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlTableInventories : ControlTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlTableInventories()
        {
            AddColumn("inventoryexpress:inventoryexpress.inventory.label");
            AddColumn("inventoryexpress:inventoryexpress.template.label");
            AddColumn("inventoryexpress:inventoryexpress.manufacturer.label");
            AddColumn("inventoryexpress:inventoryexpress.supplier.label");
            AddColumn("inventoryexpress:inventoryexpress.location.label");
            AddColumn("inventoryexpress:inventoryexpress.costcenter.label");
            AddColumn("inventoryexpress:inventoryexpress.ledgeraccount.label");
            AddColumn("inventoryexpress:inventoryexpress.condition.label");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Rows.Clear();

            lock (ViewModel.Instance.Database)
            {
                foreach (var inventory in ViewModel.Instance.Inventories.OrderBy(x => x.Name))
                {
                    AddRow
                    (
                        new ControlLink() { Text = inventory.Name, Uri = context.Uri.Append(inventory.Guid) },
                        new ControlLink() { Text = inventory.Template?.Name, Uri = context.Uri.Append("templates").Append(inventory.Guid) },
                        new ControlLink() { Text = inventory.Manufacturer?.Name, Uri = context.Uri.Append("manufacturers").Append(inventory.Manufacturer?.Guid) },
                        new ControlLink() { Text = inventory.Supplier?.Name, Uri = context.Uri.Append("suppliers").Append(inventory.Supplier?.Guid) },
                        new ControlLink() { Text = inventory.Location?.Name, Uri = context.Uri.Append("locations").Append(inventory.Location?.Guid) },
                        new ControlLink() { Text = inventory.CostCenter?.Name, Uri = context.Uri.Append("costcenters").Append(inventory.CostCenter?.Guid) },
                        new ControlLink() { Text = inventory.LedgerAccount?.Name, Uri = context.Uri.Append("ledgeraccounts").Append(inventory.LedgerAccount?.Guid) },
                        new ControlText() { Text = inventory.Condition?.Name }

                    );
                }
            }

            return base.Render(context);
        }
    }
}
