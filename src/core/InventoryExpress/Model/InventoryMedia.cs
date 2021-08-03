using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Medien eines Inventargegenstandes
    /// </summary>
    public partial class InventoryMedia
    {
        public int InventoryId { get; set; }

        public int MediaId { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        public virtual Media Media { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
