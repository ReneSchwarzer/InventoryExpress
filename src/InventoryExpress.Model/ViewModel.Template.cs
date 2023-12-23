using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        ///// <summary>
        ///// Returns the template uri.
        ///// </summary>
        ///// <param name="guid">The id of the template.</param>
        ///// <returns>The uri or null.</returns>
        //public static string GetTemplateUri(string guid)
        //{
        //    return ComponentManager.SitemapManager.GetUri<PageSettingTemplateEdit>(new ParameterTemplateId(guid));
        //}

        /// <summary>
        /// Returns all templates.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the templates.</returns>
        public static IEnumerable<WebItemEntityTemplate> GetTemplates(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityTemplate>(wql);

            return GetTemplates(wqlStatement);
        }

        /// <summary>
        /// Returns all templates.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the templates.</returns>
        public static IEnumerable<WebItemEntityTemplate> GetTemplates(IWqlStatement<WebItemEntityTemplate> wql)
        {
            lock (DbContext)
            {
                var templates = DbContext.Templates.Select(x => new WebItemEntityTemplate(x));

                return wql.Apply(templates.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a template.
        /// </summary>
        /// <param name="id">The id of the template.</param>
        /// <returns>The template or null.</returns>
        public static WebItemEntityTemplate GetTemplate(string id)
        {
            lock (DbContext)
            {
                var template = DbContext.Templates.Where(x => x.Guid == id).Select(x => new WebItemEntityTemplate(x)).FirstOrDefault();

                return template;
            }
        }

        /// <summary>
        /// Returns the template that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The template or null.</returns>
        public static WebItemEntityTemplate GetTemplate(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var template = from i in DbContext.Inventories
                               join t in DbContext.Templates on i.TemplateId equals t.Id
                               where i.Guid == inventory.Guid
                               select new WebItemEntityTemplate(t);

                return template.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a template.
        /// </summary>
        /// <param name="template">The template.</param>
        public static void AddOrUpdateTemplate(WebItemEntityTemplate template)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Templates.Where(x => x.Guid == template.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Template()
                    {
                        Guid = template.Guid,
                        Name = template.Name,
                        Description = template.Description,
                        Tag = template.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = template.Media?.Guid,
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
                    // update
                    var availableMedia = template.Media != null ? DbContext.Media.Where(x => x.Guid == template.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = template.Name;
                    availableEntity.Description = template.Description;
                    availableEntity.Tag = template.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = template.Media?.Guid,
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
        /// Deletes a template.
        /// </summary>
        /// <param name="id">The id of the template.</param>
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
        /// Checks if the template is in use.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetTemplateInUse(WebItemEntityTemplate template)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join t in DbContext.Templates on i.TemplateId equals t.Id
                           where t.Guid == template.Guid
                           select t;

                return used.Any();
            }
        }
    }
}