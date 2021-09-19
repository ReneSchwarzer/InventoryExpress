using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Ein Schlüsselwort
    /// </summary>
    public class Tag 
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Liefert oder setzt die Bezeichnung
        /// </summary>
        public string Label { get; set; }

        public virtual ICollection<InventoryTag> InventoryTag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Tag()
        {
        }
    }
}
