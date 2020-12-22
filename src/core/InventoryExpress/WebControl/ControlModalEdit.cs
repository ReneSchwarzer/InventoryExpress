using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlModalEdit : ControlModalForm
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Inventargegenstandes
        /// </summary>
        private new ControlFormularItemInputTextBox Name { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        public ControlModalEdit()
            : base("modal_edit")
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Header = "Bearbeiten";

            Name = new ControlFormularItemInputTextBox()
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
