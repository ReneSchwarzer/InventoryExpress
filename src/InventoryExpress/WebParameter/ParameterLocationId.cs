using WebExpress.WebCore.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterLocationId : WebExpress.WebCore.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterLocationId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterLocationId(string value)
            : base("LocationId", value, ParameterScope.Url)
        {

        }
    }
}
