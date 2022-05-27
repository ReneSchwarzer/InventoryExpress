using InventoryExpress.Model.Entity;
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
        /// <param name="guid">Die ID des Attributes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetAttributeUri(string guid)
        {
            return $"{RootUri}/setting/attributes/{guid}";
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
        /// Liefert ein Attribut
        /// </summary>
        /// <param name="id">Die ID des Attributes</param>
        /// <returns>Das Attribut oder null</returns>
        public static WebItemEntityAttribute GetAttribute(string id)
        {
            lock (Instance.Database)
            {
                var attribute = Instance.Attributes.Where(x => x.Guid == id).Select(x => new WebItemEntityAttribute(x)).FirstOrDefault();

                return attribute;
            }
        }

        /// <summary>
        /// Fügt ein Attribut hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        public static void AddOrUpdateAttribute(WebItemEntityAttribute attribute)
        {
            lock (Instance.Database)
            {
                var availableEntity = Instance.Attributes.Where(x => x.Guid == attribute.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Attribute()
                    {
                        Guid = attribute.ID,
                        Name = attribute.Name,
                        Description = attribute.Description,
                        Created = System.DateTime.Now,
                        Updated = System.DateTime.Now,
                        Media = new Media()
                        {
                            Guid = attribute.Media?.ID,
                            Name = attribute.Media?.Name ?? "",
                            Description = attribute.Media?.Description,
                            Tag = attribute.Media?.Tag,
                            Created = System.DateTime.Now,
                            Updated = System.DateTime.Now
                        }
                    };

                    Instance.Attributes.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = attribute.Media != null ? Instance.Media.Where(x => x.Guid == attribute.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = attribute.Name;
                    availableEntity.Description = attribute.Description;
                    availableEntity.Updated = System.DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = attribute.Media?.ID,
                            Name = attribute.Media?.Name,
                            Description = attribute.Media?.Description,
                            Tag = attribute.Media?.Tag,
                            Created = System.DateTime.Now,
                            Updated = System.DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(attribute.Media.Name))
                    {
                        availableMedia.Name = attribute.Media?.Name;
                        availableMedia.Description = attribute.Media?.Description;
                        availableMedia.Tag = attribute.Media?.Tag;
                        availableMedia.Updated = System.DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Attribut
        /// </summary>
        /// <param name="id">Die ID des Attributes</param>
        public static void DeleteAttribute(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.Attributes.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.Attributes.Remove(entity);
                }
            }
        }

        /// <summary>
        /// Prüft ob das Attribut in Verwendung ist
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetAttributeInUse(WebItemEntityAttribute attribute)
        {
            lock (Instance.Database)
            {
                var used = from i in Instance.Inventories
                           join ia in Instance.InventoryAttributes on i.Id equals ia.InventoryId
                           join a in Instance.Attributes on ia.AttributeId equals a.Id
                           where a.Guid == attribute.ID
                           select a;

                return used.Any();
            }
        }
    }
}