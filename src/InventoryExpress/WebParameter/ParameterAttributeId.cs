using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterAttributeId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterAttributeId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterAttributeId(string value)
            : base("AttributeId", value, ParameterScope.Url)
        {

        }
    }
}
