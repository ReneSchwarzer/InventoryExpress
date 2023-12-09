using WebExpress.WebCore.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterInventoryId : WebExpress.WebCore.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterInventoryId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterInventoryId(string value)
            : base("InventoryId", value, ParameterScope.Url)
        {

        }
    }
}
