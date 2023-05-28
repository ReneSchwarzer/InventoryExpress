using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Es wird nur vergleichen, ob es sich um zwei identische Attribute handelt. Eine Werteverglich erfolgt nicht. 
    /// </summary>
    public class WebItemEntityInventoryAttributeComparer : IEqualityComparer<WebItemEntityInventoryAttribute>
    {
        /// <summary>
        /// Vergleicht zwei Inventarattribute.
        /// </summary>
        /// <param name="x">Das erste Attribut</param>
        /// <param name="y">Das zweite Attribut</param>
        /// <returns>true wenn die Gleichheit besteht, false sonst</returns>
        public bool Equals(WebItemEntityInventoryAttribute x, WebItemEntityInventoryAttribute y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// Ermittelt den Hashcode
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        /// <returns>Der Hashcode</returns>
        public int GetHashCode([DisallowNull] WebItemEntityInventoryAttribute attribute)
        {
            return attribute.Id.GetHashCode();
        }
    }
}
