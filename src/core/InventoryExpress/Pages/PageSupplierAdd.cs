using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageSupplierAdd : PageBase, ISupplier
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularSupplier form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierAdd()
            : base("Lieferant hinzufügen")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularSupplier(this)
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

            form.SupplierName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Lieferant wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e)=>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var supplier = new Supplier()
                {
                    Name = form.SupplierName.Value,
                    //Tag = form.Tag.Value,
                    Discription = form.Discription.Value
                };

                DB.Instance.Suppliers.Add(supplier);
                DB.Instance.SaveChanges();
            };
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
