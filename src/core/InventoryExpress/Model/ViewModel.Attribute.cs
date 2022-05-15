using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Attribut-URL
        /// </summary>
        /// <param name="guid">Die AttributID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetAttributeUri(string guid)
        {
            return $"{ RootUri }/setting/attributes/{ guid }";
        }

        /// <summary>
        /// Liefert alle Attribute
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Attribute beinhaltet</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var attributes = Instance.Attributes.Select(x => new WebItemEntityAttribute(x));

                return wql.Apply(attributes.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert alle Attribute einer Vorlage
        /// </summary>
        /// <param name="template">Die Vorlage</param>
        /// <returns>Eine Aufzählung, welche die Attribute beinhaltet</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(WebItemEntityTemplate template)
        {
            lock (Instance.Database)
            {
                var attributes = from t in Instance.Templates
                                 join ta in Instance.TemplateAttributes on t.Id equals ta.TemplateId
                                 join a in Instance.Attributes on ta.AttributeId equals a.Id
                                 where t.Guid == template.ID
                                 select new WebItemEntityAttribute(a);

                return attributes;
            }
        }

        /// <summary>
        /// Liefert ein Attribute
        /// </summary>
        /// <param name="id">Die interne AttributID</param>
        /// <returns>Das Attribut oder null</returns>
        public static WebItemEntityAttribute GetAttribute(int? id)
        {
            lock (Instance.Database)
            {
                var attribute = Instance.Attributes.Where(x => x.Id == id).Select(x => new WebItemEntityAttribute(x)).FirstOrDefault();

                return attribute;
            }
        }
    }
}