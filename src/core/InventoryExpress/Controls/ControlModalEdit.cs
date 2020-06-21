using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlModalEdit : ControlModalForm
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Inventargegenstandes
        /// </summary>
        private ControlFormularItemTextBox Name { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        public ControlModalEdit(IPage page)
            : base(page, "modal_edit")
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Header = "Bearbeiten";

            Name = new ControlFormularItemTextBox(this)
            {
                Name = "name",
                Label = "Name",
                Help = "Die Kurzbezeichnung des Inventargegenstandes"
            };

            Content.Add(Name);

            Name.Validation += (s, e) =>
            {
                e.Results.Add(new ValidationResult() { Text = "Fehler", Type = TypesInputValidity.Error });
            };
        }
    }
}
