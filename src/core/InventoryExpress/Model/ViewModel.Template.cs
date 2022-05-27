using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
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
        /// <param name="guid">Die ID der Vorlage</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetTemplateUri(string guid)
        {
            return $"{RootUri}/setting/templates/{guid}";
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
        /// <param name="id">Die ID der Vorlage</param>
        /// <returns>Die Vorlage oder null</returns>
        public static WebItemEntityTemplate GetTemplate(string id)
        {
            lock (Instance.Database)
            {
                var template = Instance.Templates.Where(x => x.Guid == id).Select(x => new WebItemEntityTemplate(x)).FirstOrDefault();

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

        /// <summary>
        /// Fügt eine Vorlage hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="template">Die Vorlage</param>
        public static void AddOrUpdateTemplate(WebItemEntityTemplate template)
        {
            lock (Instance.Database)
            {
                var availableEntity = Instance.Templates.Where(x => x.Guid == template.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Template()
                    {
                        Guid = template.ID,
                        Name = template.Name,
                        Description = template.Description,
                        Tag = template.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = template.Media?.ID,
                            Name = template.Media?.Name ?? "",
                            Description = template.Media?.Description,
                            Tag = template.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    Instance.Templates.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = template.Media != null ? Instance.Media.Where(x => x.Guid == template.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = template.Name;
                    availableEntity.Description = template.Description;
                    availableEntity.Tag = template.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = template.Media?.ID,
                            Name = template.Media?.Name,
                            Description = template.Media?.Description,
                            Tag = template.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(template.Media.Name))
                    {
                        availableMedia.Name = template.Media?.Name;
                        availableMedia.Description = template.Media?.Description;
                        availableMedia.Tag = template.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Standort
        /// </summary>
        /// <param name="id">Die ID des Standortes</param>
        public static void DeleteTemplate(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.Templates.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.Templates.Remove(entity);
                }
            }
        }
    }
}