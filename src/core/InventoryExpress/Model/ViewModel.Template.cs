using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Vorlagen-URL
        /// </summary>
        /// <param name="guid">Die VorlagenID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetTemplateUri(string guid)
        {
            return $"{ RootUri }/templates/{ guid }";
        }

        /// <summary>
        /// Liefert alle Vorlagen
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Vorlagen beinhaltet</returns>
        public static IEnumerable<WebItemEntityTemplate> GetTemplates(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var templates = Instance.Templates.Select(x => new WebItemEntityTemplate(x));

                return wql.Apply(templates.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert eine Vorlage
        /// </summary>
        /// <param name="id">Die interne VorlagenID</param>
        /// <returns>Die Vorlagen oder null</returns>
        public static WebItemEntityTemplate GetTemplate(int? id)
        {
            lock (Instance.Database)
            {
                var template = Instance.Templates.Where(x => x.Id == id).Select(x => new WebItemEntityTemplate(x)).FirstOrDefault();

                return template;
            }
        }

        /// <summary>
        /// Liefert die Vorlage, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Die Vorlage oder null</returns>
        public static WebItemEntityTemplate GetTemplate(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var template = from i in Instance.Inventories
                               join t in Instance.Templates on i.ConditionId equals t.Id
                               where i.Guid == inventory.ID
                               select new WebItemEntityTemplate(t);

                return template.FirstOrDefault();
            }
        }
    }
}