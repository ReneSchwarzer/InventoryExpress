using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageInventoryAdd : PageBase
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAdd()
            : base("Inventargegenstand hinzufügen")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularInventory()
            {
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Main.Content.Add(form);

            form.InventoryName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Name wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var inventory = new Inventory()
                {
                    Name = form.InventoryName.Value,
                    //Tag = form.Tag.Value,
                    Discription = form.InventoryName.Value
                };

                ViewModel.Instance.Inventories.Add(inventory);
                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
