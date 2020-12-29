using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Attribute eines Inventars
    /// </summary>
    [Table("INVENTORYATTRIBUTE")]
    public partial class InventoryAttribute
    {
        public int InventoryId { get; set; }

        public int AttributeId { get; set; }

        /// <summary>
        /// Der Wert
        /// </summary>
        [Column("VALUE")]
        public string Value { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
