using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The settings.
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Setting()
            : base()
        {
        }
    }
}
