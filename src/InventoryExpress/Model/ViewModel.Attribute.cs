using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebPageSetting;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Returns the attribute uri.
        /// </summary>
        /// <param name="guid">The id of the attribute.</param>
        /// <returns>The uri or null.</returns>
        public static string GetAttributeUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingAttributes>()?.Append(guid);
        }

        /// <summary>
        /// Returns all attributes.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the attributes.</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityAttribute>(wql);

            return GetAttributes(wqlStatement);
        }

        /// <summary>
        /// Returns all attributes.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the attributes.</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(IWqlStatement<WebItemEntityAttribute> wql)
        {
            lock (DbContext)
            {
                var attributes = DbContext.Attributes.Select(x => new WebItemEntityAttribute(x));

                return wql.Apply(attributes.AsQueryable());
            }
        }

        /// <summary>
        /// Returns all the attributes of a template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>A enumeation of attributes.</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(WebItemEntityTemplate template)
        {
            lock (DbContext)
            {
                var attributes = from t in DbContext.Templates
                                 join ta in DbContext.TemplateAttributes on t.Id equals ta.TemplateId
                                 join a in DbContext.Attributes on ta.AttributeId equals a.Id
                                 where t.Guid == template.Guid
                                 select new WebItemEntityAttribute(a);

                return attributes.ToList();
            }
        }

        /// <summary>
        /// Returns an attribute.
        /// </summary>
        /// <param name="id">The id of the attribute.</param>
        /// <returns>The attribute or null.</returns>
        public static WebItemEntityAttribute GetAttribute(string id)
        {
            lock (DbContext)
            {
                var attribute = DbContext.Attributes.Where(x => x.Guid == id).Select(x => new WebItemEntityAttribute(x)).FirstOrDefault();

                return attribute;
            }
        }

        /// <summary>
        /// Adds or updates an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public static void AddOrUpdateAttribute(WebItemEntityAttribute attribute)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Attributes.Where(x => x.Guid == attribute.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Attribute()
                    {
                        Guid = attribute.Guid,
                        Name = attribute.Name,
                        Description = attribute.Description,
                        Created = System.DateTime.Now,
                        Updated = System.DateTime.Now,
                        Media = new Media()
                        {
                            Guid = attribute.Media?.Guid,
                            Name = attribute.Media?.Name ?? "",
                            Description = attribute.Media?.Description,
                            Tag = attribute.Media?.Tag,
                            Created = System.DateTime.Now,
                            Updated = System.DateTime.Now
                        }
                    };

                    DbContext.Attributes.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // update
                    var availableMedia = attribute.Media != null ? DbContext.Media.Where(x => x.Guid == attribute.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = attribute.Name;
                    availableEntity.Description = attribute.Description;
                    availableEntity.Updated = System.DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = attribute.Media?.Guid,
                            Name = attribute.Media?.Name,
                            Description = attribute.Media?.Description,
                            Tag = attribute.Media?.Tag,
                            Created = System.DateTime.Now,
                            Updated = System.DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(attribute.Media.Name))
                    {
                        availableMedia.Name = attribute.Media?.Name;
                        availableMedia.Description = attribute.Media?.Description;
                        availableMedia.Tag = attribute.Media?.Tag;
                        availableMedia.Updated = System.DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes an attribute.
        /// </summary>
        /// <param name="id">The id of the attribute.</param>
        public static void DeleteAttribute(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Attributes.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Attributes.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Checks if the attribute is in use.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetAttributeInUse(WebItemEntityAttribute attribute)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                           where a.Guid == attribute.Guid
                           select a;

                return used.Any();
            }
        }
    }
}