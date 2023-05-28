using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Einstellungen
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Die Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Die Währung
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
