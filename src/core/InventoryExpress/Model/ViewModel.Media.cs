using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.IO;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="guid">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetMediaUri(string guid)
        {
            return $"{RootUri}/media/{guid}";
        }

        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="id">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetMediaUri(int? id)
        {
            if (!id.HasValue) { return ApplicationIcon; }

            lock (Instance.Database)
            {
                var media = Instance.Media.Where(x => x.Id == id).Select(x => GetMediaUri(x.Guid)).FirstOrDefault();

                return media ?? ApplicationIcon;
            }
        }

        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="id">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static WebItemEntityMedia GetMedia(int? id)
        {
            if (!id.HasValue) { return new WebItemEntityMedia(); }

            lock (Instance.Database)
            {
                var media = Instance.Media.Where(x => x.Id == id).Select(x => new WebItemEntityMedia(x)).FirstOrDefault();

                return media ?? new WebItemEntityMedia();
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="media">Die Medien</param>
        public static void AddOrUpdateMedia(WebItemEntityMedia media, byte[] data)
        {
            var root = Path.Combine(Context.DataPath, "media");

            lock (Instance.Database)
            {
                var availableEntity = Instance.Media.Where(x => x.Guid == media.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = media.ID,
                        Name = media.Name,
                        Description = media.Description,
                        Tag = media.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    Instance.Media.Add(entity);
                }
                else
                {
                    // Update
                    availableEntity.Name = media.Name;
                    availableEntity.Description = media.Description;
                    availableEntity.Tag = media.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }
                }

                File.WriteAllBytes(Path.Combine(root, media.ID), data);
            }
        }

        /// <summary>
        /// Löscht die Medien
        /// </summary>
        /// <param name="id">Die ID des Dokuementes</param>
        public static void DeleteMedia(string id)
        {
            var root = Path.Combine(Context.DataPath, "media");

            lock (Instance.Database)
            {
                var entity = Instance.Media.Where(x => x.Guid == id).FirstOrDefault();

                if (entity != null)
                {
                    Instance.Media.Remove(entity);

                    if (File.Exists(Path.Combine(root, entity.Guid)))
                    {
                        File.Delete(Path.Combine(root, entity.Guid));
                    }
                }
            }
        }
    }
}