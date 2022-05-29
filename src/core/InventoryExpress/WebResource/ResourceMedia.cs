using InventoryExpress.Model;
using System.IO;
using WebExpress.Message;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

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
            var guid = request.GetParameter("MediaID")?.Value.ToLower();
            var media = ViewModel.GetMedia(guid);
            var path = ViewModel.MediaDirectory;

            Data = File.ReadAllBytes(Path.Combine(path, guid));

            var response = base.Process(request);
            response.Header.CacheControl = "public, max-age=31536000";

            var extension = Path.GetExtension(media?.Name);
            extension = !string.IsNullOrWhiteSpace(extension) ? extension.ToLower() : "";

            switch (extension)
            {
                case ".pdf":
                    response.Header.ContentType = "application/pdf";
                    break;
                case ".txt":
                    response.Header.ContentType = "text/plain";
                    break;
                case ".css":
                    response.Header.ContentType = "text/css";
                    break;
                case ".xml":
                    response.Header.ContentType = "text/xml";
                    break;
                case ".html":
                case ".htm":
                    response.Header.ContentType = "text/html";
                    break;
                case ".exe":
                    response.Header.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(media?.Name) + "; size=" + Data.LongLength;
                    response.Header.ContentType = "application/octet-stream";
                    break;
                case ".zip":
                    response.Header.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(media?.Name) + "; size=" + Data.LongLength;
                    response.Header.ContentType = "application/zip";
                    break;
                case ".doc":
                case ".docx":
                    response.Header.ContentType = "application/msword";
                    break;
                case ".xls":
                case ".xlx":
                    response.Header.ContentType = "application/vnd.ms-excel";
                    break;
                case ".ppt":
                    response.Header.ContentType = "application/vnd.ms-powerpoint";
                    break;
                case ".gif":
                    response.Header.ContentType = "image/gif";
                    break;
                case ".png":
                    response.Header.ContentType = "image/png";
                    break;
                case ".svg":
                    response.Header.ContentType = "image/svg+xml";
                    break;
                case ".jpeg":
                case ".jpg":
                    response.Header.ContentType = "image/jpg";
                    break;
                case ".ico":
                    response.Header.ContentType = "image/x-icon";
                    break;
            }

            Context.Log.Debug(message: I18N("webexpress:resource.file"), args: new object[] { request.RemoteEndPoint, request.Uri });

            return response;
        }
    }
}
