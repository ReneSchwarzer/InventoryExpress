﻿using InventoryExpress.Model.Entity;
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
        /// <param name="Guid">Die Id des Attributes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetAttributeUri(string Guid)
        {
            return $"{RootUri}/setting/attributes/{Guid}";
        }

        /// <summary>
        /// Liefert alle Attribute
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Attribute beinhaltet</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(WqlStatement wql)
        {
            lock (DbContext)
            {
                var attributes = DbContext.Attributes.Select(x => new WebItemEntityAttribute(x));

                return wql.Apply(attributes.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert alle Attribute einer Vorlage
        /// </summary>
        /// <param name="template">The template</param>
        /// <returns>Eine Aufzählung, welche die Attribute beinhaltet</returns>
        public static IEnumerable<WebItemEntityAttribute> GetAttributes(WebItemEntityTemplate template)
        {
            lock (DbContext)
            {
                var attributes = from t in DbContext.Templates
                                 join ta in DbContext.TemplateAttributes on t.Id equals ta.TemplateId
                                 join a in DbContext.Attributes on ta.AttributeId equals a.Id
                                 where t.Guid == template.Id
                                 select new WebItemEntityAttribute(a);

                return attributes.ToList();
            }
        }

        /// <summary>
        /// Liefert ein Attribut
        /// </summary>
        /// <param name="id">Die Id des Attributes</param>
        /// <returns>Das Attribut oder null</returns>
        public static WebItemEntityAttribute GetAttribute(string id)
        {
            lock (DbContext)
            {
                var attribute = DbContext.Attributes.Where(x => x.Guid == id).Select(x => new WebItemEntityAttribute(x)).FirstOrDefault();

                return attribute;
            }
        }

        /// <summary>
        /// Fügt ein Attribut hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        public static void AddOrUpdateAttribute(WebItemEntityAttribute attribute)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Attributes.Where(x => x.Guid == attribute.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Attribute()
                    {
                        Guid = attribute.Id,
                        Name = attribute.Name,
                        Description = attribute.Description,
                        Created = System.DateTime.Now,
                        Updated = System.DateTime.Now,
                        Media = new Media()
                        {
                            Guid = attribute.Media?.Id,
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
                    // Update
                    var availableMedia = attribute.Media != null ? DbContext.Media.Where(x => x.Guid == attribute.Media.Id).FirstOrDefault() : null;

                    availableEntity.Name = attribute.Name;
                    availableEntity.Description = attribute.Description;
                    availableEntity.Updated = System.DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = attribute.Media?.Id,
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
        /// Löscht ein Attribut
        /// </summary>
        /// <param name="id">Die Id des Attributes</param>
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
        /// Prüft ob das Attribut in Verwendung ist
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetAttributeInUse(WebItemEntityAttribute attribute)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join ia in DbContext.InventoryAttributes on i.Id equals ia.InventoryId
                           join a in DbContext.Attributes on ia.AttributeId equals a.Id
                           where a.Guid == attribute.Id
                           select a;

                return used.Any();
            }
        }
    }
}