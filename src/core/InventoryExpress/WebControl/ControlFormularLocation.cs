using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLocation : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Standortes
        /// </summary>
        public ControlFormularItemInputTextBox LocationName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress.location.form.name.label",
            Help = "inventoryexpress.location.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress.location.form.description.label",
            Help = "inventoryexpress.location.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt),
            Rows = 10
        };

        /// <summary>
        /// Liefert die Aaddresse
        /// </summary>
        public ControlFormularItemInputTextBox Address { get; } = new ControlFormularItemInputTextBox("adress")
        {
            Name = "adress",
            Label = "inventoryexpress.location.form.adress.label",
            Help = "inventoryexpress.location.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormularItemInputTextBox Zip { get; } = new ControlFormularItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress.location.form.zip.label",
            Help = "inventoryexpress.location.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormularItemInputTextBox Place { get; } = new ControlFormularItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress.location.form.place.label",
            Help = "inventoryexpress.location.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert das Gebäude
        /// </summary>
        public ControlFormularItemInputTextBox Building { get; } = new ControlFormularItemInputTextBox("building")
        {
            Name = "building",
            Label = "inventoryexpress.location.form.building.label",
            Help = "inventoryexpress.location.form.building.description",
            Icon = new PropertyIcon(TypeIcon.Building)
        };

        /// <summary>
        /// Liefert den Room
        /// </summary>
        public ControlFormularItemInputTextBox Room { get; } = new ControlFormularItemInputTextBox("room")
        {
            Name = "room",
            Label = "inventoryexpress.location.form.room.label",
            Help = "inventoryexpress.location.form.room.description",
            Icon = new PropertyIcon(TypeIcon.DoorOpen)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress.location.form.image.label",
            Help = "inventoryexpress.location.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; } = new ControlFormularItemInputTag("tags")
        {
            Name = "tag",
            Label = "inventoryexpress.location.form.tag.label",
            Help = "inventoryexpress.location.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag)
        };

        /// <summary>
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularLocation(string id = null)
            : base(id)
        {
            Name = "location";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            var group1 = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group1.Items.Add(Zip);
            group1.Items.Add(Place);

            var group2 = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group2.Items.Add(Building);
            group2.Items.Add(Room);

            Add(LocationName);
            Add(Description);
            Add(Address);
            Add(new ControlFormularItemInputGroup(null, group1));
            Add(new ControlFormularItemInputGroup(null, group2));

            if (!Edit)
            {
                Add(Image);
            }

            Add(Tag);
        }
    }
}
