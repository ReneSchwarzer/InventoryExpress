using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterLocationId : Parameter
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
