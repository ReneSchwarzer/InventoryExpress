using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterTemplateId : Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterTemplateId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterTemplateId(string value)
            : base("TemplateId", value, ParameterScope.Url)
        {

        }
    }
}
