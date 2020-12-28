using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    [Table("LEDGERACCOUNT")]
    public class LedgerAccount : ItemTag
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public LedgerAccount()
            : base()
        {
        }
    }
}
