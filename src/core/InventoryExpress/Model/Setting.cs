using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Einstellungen
    /// </summary>
    [Table("SETTING")]
    public class Setting
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Die Währung
        /// </summary>
        [StringLength(10), Column("CURRENCY")]
        public string Currency { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Setting()
            : base()
        {
        }
    }
}
