using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageDetails : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageDetails()
            : base("Details")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            ToolBar.Add(new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Edit),
                Text = "Ändern",
                Title = "Bearbeiten",
                Uri = Uri.Root.Append("edit"),
                TextColor = new PropertyColorText(TypeColorText.White),
                //Modal = new ControlModalEdit(this)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Print),
                Uri = Uri.Root.Append("print"),
                Title = "Drucken",
                Size = new PropertySizeText(TypeSizeText.Default),
                TextColor = new PropertyColorText(TypeColorText.White)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.TrashAlt),
                Title = "Löschen",
                Size = new PropertySizeText(TypeSizeText.Default),
                TextColor = new PropertyColorText(TypeColorText.White),
                Modal = new ControlModalDel(this)
                {

                }
            });
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var id = string.Format("{0}-{1}-{2}-{3}-{4}", GetParam("id1"), GetParam("id2"), GetParam("id3"), GetParam("id4"), GetParam("id5"));
            var inventory = ViewModel.Instance.Inventories.Where(x => x.ID.Equals(id)).FirstOrDefault();

            Main.Content.Add(new ControlText(this)
            {
                Text = inventory?.Name,
                Format = TypeFormatText.H1,
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Bild:",
                Icon = new PropertyIcon(TypeIcon.Image),
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlImage(this)
            {
                //Source = new UriRelative("/data/" + inventory?.Image)

            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Beschreibung:",
                Icon = new PropertyIcon(TypeIcon.Comment),
                TextColor = new PropertyColorText(TypeColorText.Dark)

            });

            Main.Content.Add(new ControlText(this)
            {
                Text = inventory?.Discription,
                Format = TypeFormatText.Paragraph,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Hersteller:",
                Icon = new PropertyIcon(TypeIcon.Industry),
                Value = inventory?.Manufacturer?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Standort:",
                Icon = new PropertyIcon(TypeIcon.Map),
                Value = inventory?.Location?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Lieferant:",
                Icon = new PropertyIcon(TypeIcon.Truck),
                Value = inventory?.Supplier?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Sachkonto:",
                Icon = new PropertyIcon(TypeIcon.At),
                Value = inventory?.GLAccount?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Kostenstelle:",
                Icon = new PropertyIcon(TypeIcon.ShoppingBag),
                Value = inventory?.CostCenter?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Zustand:",
                Icon = new PropertyIcon(TypeIcon.Star),
                Value = inventory?.State?.Name,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            foreach (var v in inventory?.Attributes)
            {
                Main.Content.Add(new ControlAttribute(this)
                {
                    Name = v.Name + ":",
                    Icon = new PropertyIcon(TypeIcon.MapMarker),
                    Value = v.Value,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                });
            }

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Anschaffungskosten:",
                Icon = new PropertyIcon(TypeIcon.EuroSign),
                Value = inventory?.CostValue.ToString() + " €",
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Main.Content.Add(new ControlAttribute(this)
            {
                Name = "Anschaffungsdatum:",
                Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                Value = inventory?.PurchaseDate.ToString(),
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            if (inventory.DerecognitionDate.HasValue)
            {
                Main.Content.Add(new ControlAttribute(this)
                {
                    Name = "Abgangsdatum:",
                    Icon = new PropertyIcon(TypeIcon.CalendarMinus),
                    Value = inventory?.DerecognitionDate.ToString(),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                });
            }

        }
    }
}
