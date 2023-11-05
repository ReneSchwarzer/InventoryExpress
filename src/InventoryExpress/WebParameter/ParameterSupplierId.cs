using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterSupplierId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterSupplierId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterSupplierId(string value)
            : base("SupplierId", value, ParameterScope.Url)
        {

        }
    }
}
