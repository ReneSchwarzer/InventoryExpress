using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlagenattribute
    /// </summary>
    [Table("ATTRIBUTE")]
    public class Attribute : Item
    {
        /// <summary>
        /// Gibt an oder legt den Standardwert fest
        /// </summary>
        //public string DefaultValue { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Attribute()
            : base()
        {

        }
    }
}
