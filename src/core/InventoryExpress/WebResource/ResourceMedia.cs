using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;

namespace InventoryExpress.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("Media")]
    [Title("Media")]
    [SegmentGuid("MediaID", "")]
    [Path("/Media")]
    [IncludeSubPaths(true)]
    [Module("InventoryExpress")]
    public sealed class ResourceMedia : WebExpress.WebResource.ResourceBinary
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
        public override void Initialization()
        {
            lock (Context)
            {
                base.Initialization();
                
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            lock (Context)
            {
                var guid = GetParamValue("MediaID").ToLower();
                var media = ViewModel.Instance.Media.Where(x => x.Guid.ToLower() == guid).FirstOrDefault();

                Data = media?.Data;

                var response = base.Process(request);
                response.HeaderFields.CacheControl = "public, max-age=31536000";

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

                Context.Log.Debug(request.Client + ": Datei '" + request.URL + "' wurde geladen.");

                return response;
            }
        }
    }
}
