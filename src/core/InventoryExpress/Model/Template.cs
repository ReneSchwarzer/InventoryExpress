using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlage
    /// </summary>
    [Table("TEMPLATE")]
    public class Template : ItemTag
    {
        /// <summary>
        /// Die URL des Providers
        /// </summary>
        //public string Url { get; set; }

        /// <summary>
        /// Die Attribute der Vorlage
        /// </summary>
        //public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// Die ungenutzten Attribute der Vorlage
        /// </summary>
        //public List<Attribute> UnusedAttributes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Template()
            : base()
        {
            // Attributes = new List<Attribute>();
        }
    }
}
