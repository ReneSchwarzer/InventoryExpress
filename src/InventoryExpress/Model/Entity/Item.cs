using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    public class Item
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the last change.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Returns or sets the guid.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Returns or sets the media id.
        /// </summary>
        public int? MediaId { get; set; }

        /// <summary>
        /// Returns or sets the media.
        /// </summary>
        public virtual Media Media { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Conversion to string.
        /// </summary>
        /// <returns>The object cast as a string.</returns>
        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
