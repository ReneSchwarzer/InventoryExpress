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
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionsUri()
        {
            return $"{RootUri}/setting/conditions";
        }

        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="guid">Die ID des Zustandes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionUri(string guid)
        {
            return $"{GetConditionsUri()}/{guid}";
        }

        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="guid">Die ID des Zustandes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionAddUri(string guid)
        {
            return $"{GetConditionsUri()}/add/{guid}";
        }

        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="guid">Die ID des Zustandes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionEditUri(string guid)
        {
            return $"{GetConditionsUri()}/edit/{guid}";
        }

        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="grade">Der Zustand</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionIamgeUri(int grade)
        {
            return $"{RootUri}/assets/img/condition_{grade}.svg";
        }

        /// <summary>
        /// Liefert alle Zustände
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Zustände beinhaltet</returns>
        public static IEnumerable<WebItemEntityCondition> GetConditions(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var conditions = Instance.Conditions.Select(x => new WebItemEntityCondition(x));

                return wql.Apply(conditions.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert ein Zustand
        /// </summary>
        /// <param name="id">Die ID des Zustandes</param>
        /// <returns>Der Zustand oder null</returns>
        public static WebItemEntityCondition GetCondition(string id)
        {
            lock (Instance.Database)
            {
                var condition = Instance.Conditions.Where(x => x.Guid == id).Select(x => new WebItemEntityCondition(x)).FirstOrDefault();

                return condition;
            }
        }

        /// <summary>
        /// Liefert den Zustand, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Zustand oder null</returns>
        public static WebItemEntityCondition GetCondition(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var condition = from i in Instance.Inventories
                                join c in Instance.Conditions on i.ConditionId equals c.Id
                                where i.Guid == inventory.ID
                                select new WebItemEntityCondition(c);

                return condition.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Zustand hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="condition">Der Zustand</param>
        public static void AddOrUpdateCondition(WebItemEntityCondition condition)
        {
            lock (Instance.Database)
            {
                var availableEntity = Instance.Conditions.Where(x => x.Guid == condition.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Condition()
                    {
                        Guid = condition.ID,
                        Name = condition.Name,
                        Description = condition.Description,
                        Grade = condition.Grade,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = condition.Media?.ID,
                            Name = condition.Media?.Name ?? "",
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    Instance.Conditions.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = condition.Media != null ? Instance.Media.Where(x => x.Guid == condition.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = condition.Name;
                    availableEntity.Description = condition.Description;
                    availableEntity.Grade = condition.Grade;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = condition.Media?.ID,
                            Name = condition.Media?.Name,
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(condition.Media.Name))
                    {
                        availableMedia.Name = condition.Media?.Name;
                        availableMedia.Description = condition.Media?.Description;
                        availableMedia.Tag = condition.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Zustand
        /// </summary>
        /// <param name="id">Die ID des Zustandes</param>
        public static void DeleteCondition(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.Conditions.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.Conditions.Remove(entity);
                }
            }
        }

        /// <summary>
        /// Prüft ob der Zustand in Verwendung ist
        /// </summary>
        /// <param name="condition">Der Zustand</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetConditionInUse(WebItemEntityCondition condition)
        {
            lock (Instance.Database)
            {
                var used = from i in Instance.Inventories
                           join c in Instance.Conditions on i.ConditionId equals c.Id
                           where c.Guid == condition.ID
                           select c;

                return used.Any();
            }
        }
    }
}