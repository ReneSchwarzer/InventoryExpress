using WebExpress.WebCore.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterTemplateId : WebExpress.WebCore.WebMessage.Parameter
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
