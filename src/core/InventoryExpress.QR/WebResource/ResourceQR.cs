using QRCoder;
using System.Text;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebResource;

namespace InventoryExpress.QR.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("QR")]
    [Title("Assets")]
    [SegmentGuid("InventoryID", "")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("InventoryExpress.QR")]
    public sealed class ResourceQR : ResourceBinary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceQR()
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var id = request.GetParameter("InventoryID")?.Value;

            var link = $"{ ApplicationManager.Context.Uri.ToString().TrimEnd('/') }/{ Context.Application.ContextPath.Append(id).ToString().TrimStart('/')}";

            var qrGenerator = new QRCodeGenerator();
            var qrCode = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);

            var svg = new StringBuilder();

            svg.Append(@"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" version=""1.1"" width=""100"" height=""100"" viewBox=""0 0 2000 2000"" x=""0"" y=""0"" shape-rendering=""crispEdges"">");
            svg.Append(@"<rect x=""0"" y=""0"" width=""2000"" height=""2000"" fill=""#ffffff""/>");

            var height = 2000 / qrCode.ModuleMatrix.Count;
            var width = 2000 / qrCode.ModuleMatrix.Count;

            for (int y = 0; y < qrCode.ModuleMatrix.Count; y++)
            {
                var row = qrCode.ModuleMatrix[y];

                for (int x = 0; x < qrCode.ModuleMatrix.Count; x++)
                {
                    var item = row[x];
                    if (item)
                    {
                        svg.Append($"<rect x=\"{ x * width }\" y=\"{ y * height }\" width=\"{ width }\" height=\"{ height }\" fill=\"#000000\"/>");
                    }
                }
            }

            svg.Append(@"</svg>");

            Data = Encoding.UTF8.GetBytes(svg.ToString());

            var response = base.Process(request);
            response.HeaderFields.CacheControl = "no-cache";
            response.HeaderFields.ContentType = "image/svg+xml";

            return response;
        }
    }
}