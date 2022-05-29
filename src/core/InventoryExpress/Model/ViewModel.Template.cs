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
            lock (DbContext)
            {
                var templates = DbContext.Templates.Select(x => new WebItemEntityTemplate(x));

                return wql.Apply(templates.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert eine Vorlage
        /// </summary>
        /// <param name="id">Die ID der Vorlage</param>
        /// <returns>Die Vorlage oder null</returns>
        public static WebItemEntityTemplate GetTemplate(string id)
        {
            lock (DbContext)
            {
                var template = DbContext.Templates.Where(x => x.Guid == id).Select(x => new WebItemEntityTemplate(x)).FirstOrDefault();

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
            lock (DbContext)
            {
                var template = from i in DbContext.Inventories
                               join t in DbContext.Templates on i.TemplateId equals t.Id
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
            lock (DbContext)
            {
                var availableEntity = DbContext.Templates.Where(x => x.Guid == template.ID).FirstOrDefault();

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

                    DbContext.Templates.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    var availableMedia = template.Media != null ? DbContext.Media.Where(x => x.Guid == template.Media.ID).FirstOrDefault() : null;

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

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(template.Media.Name))
                    {
                        availableMedia.Name = template.Media?.Name;
                        availableMedia.Description = template.Media?.Description;
                        availableMedia.Tag = template.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                    
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Löscht ein Standort
        /// </summary>
        /// <param name="id">Die ID des Standortes</param>
        public static void DeleteTemplate(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Templates.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Templates.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob die Vorlage in Verwendung ist
        /// </summary>
        /// <param name="template">Die Vorlage</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetTemplateInUse(WebItemEntityTemplate template)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join t in DbContext.Templates on i.TemplateId equals t.Id
                           where t.Guid == template.ID
                           select t;

                return used.Any();
            }
        }
    }
}