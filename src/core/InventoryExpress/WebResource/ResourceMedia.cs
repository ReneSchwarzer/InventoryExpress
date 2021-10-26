using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebResource;

namespace InventoryExpress.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("MediaAsset")]
    [Title("Media")]
    [SegmentGuid("MediaID", "")]
    [Path("/media")]
    [IncludeSubPaths(false)]
    [Module("inventoryexpress")]
    public sealed class ResourceMedia : ResourceBinary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceMedia()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = request.GetParameter("MediaID")?.Value.ToLower();
                var media = ViewModel.Instance.Media.Where(x => x.Guid.ToLower() == guid).FirstOrDefault();

                Data = media?.Data;

                var response = base.Process(request);
                response.HeaderFields.CacheControl = "no-cache";

                var extension = System.IO.Path.GetExtension(media?.Name);
                extension = !string.IsNullOrWhiteSpace(extension) ? extension.ToLower() : "";

                switch (extension)
                {
                    case ".pdf":
                        response.HeaderFields.ContentType = "application/pdf";
                        break;
                    case ".txt":
                        response.HeaderFields.ContentType = "text/plain";
                        break;
                    case ".css":
                        response.HeaderFields.ContentType = "text/css";
                        break;
                    case ".xml":
                        response.HeaderFields.ContentType = "text/xml";
                        break;
                    case ".html":
                    case ".htm":
                        response.HeaderFields.ContentType = "text/html";
                        break;
                    case ".exe":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(media?.Name) + "; size=" + Data.LongLength;
                        response.HeaderFields.ContentType = "application/octet-stream";
                        break;
                    case ".zip":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(media?.Name) + "; size=" + Data.LongLength;
                        response.HeaderFields.ContentType = "application/zip";
                        break;
                    case ".doc":
                    case ".docx":
                        response.HeaderFields.ContentType = "application/msword";
                        break;
                    case ".xls":
                    case ".xlx":
                        response.HeaderFields.ContentType = "application/vnd.ms-excel";
                        break;
                    case ".ppt":
                        response.HeaderFields.ContentType = "application/vnd.ms-powerpoint";
                        break;
                    case ".gif":
                        response.HeaderFields.ContentType = "image/gif";
                        break;
                    case ".png":
                        response.HeaderFields.ContentType = "image/png";
                        break;
                    case ".svg":
                        response.HeaderFields.ContentType = "image/svg+xml";
                        break;
                    case ".jpeg":
                    case ".jpg":
                        response.HeaderFields.ContentType = "image/jpg";
                        break;
                    case ".ico":
                        response.HeaderFields.ContentType = "image/x-icon";
                        break;
                }

                Context.Log.Debug(request.Client + ": Datei '" + request.Uri + "' wurde geladen.");

                return response;
            }
        }
    }
}
