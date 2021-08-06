using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    public class ItemTag : Item
    {
        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemTag()
        {
        }
    }
}
