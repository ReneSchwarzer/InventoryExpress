using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The state..
    /// </summary>
    public class WebItemEntityCondition : WebItemEntity
    {
        /// <summary>
        /// Returns or sets the state as a note.
        /// </summary>
        [JsonPropertyName("grade")]
        public int Grade { get; set; }

        /// <summary>
        /// Determines whether the state is in use or not.
        /// </summary>
        [JsonPropertyName("isinuse")]
        public bool IsInUse => ViewModel.GetConditionInUse(this);

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityCondition()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="condition">The database object of the state.</param>
        internal WebItemEntityCondition(Condition condition)
            : base(condition)
        {
            Grade = condition.Grade;
            Uri = ViewModel.GetUri(this);
            Image = ViewModel.GetUri(null as WebItem).Append($"/img/condition_{Grade}.svg");
        }
    }
}
