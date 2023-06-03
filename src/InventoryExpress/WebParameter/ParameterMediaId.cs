using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterMediaId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterMediaId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterMediaId(string value)
            : base("MediaId", value, ParameterScope.Url)
        {

        }
    }
}
