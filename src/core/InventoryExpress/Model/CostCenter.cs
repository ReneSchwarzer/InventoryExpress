using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    [Table("COSTCENTER")]
    public class CostCenter : ItemTag
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CostCenter()
            : base()
        {
        }
    }
}
