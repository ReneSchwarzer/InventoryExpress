using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageManufactorEdit : PageBase, IPageManufactor
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularManufactor form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactorEdit()
            : base("Hersteller bearbeiten")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularManufactor(this)
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

            var id = Convert.ToInt32(GetParam("id"));
            var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.ID == id).FirstOrDefault();

            Content.Content.Add(form);

            form.ManufactorName.Value = manufacturer?.Name;
            form.Discription.Value = manufacturer?.Discription;

            form.ManufactorName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Manufacturers.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                manufacturer.Name = form.ManufactorName.Value;
                //Tag = form.Tag.Value;
                manufacturer.Discription = form.Discription.Value;

                ViewModel.Instance.SaveChanges();
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
