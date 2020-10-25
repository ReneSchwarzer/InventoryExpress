using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    [Table("GLACCOUNT")]
    public class GLAccount : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public GLAccount()
            : base()
        {
        }
    }
}
