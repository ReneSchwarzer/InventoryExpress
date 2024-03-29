﻿using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System.IO;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebResource
{
    /// <summary>
    /// Lieferung ein Exportdatei
    /// </summary>
    [Title("Export")]
    [SegmentGuid<ParameterExportId>("")]
    [ContextPath("/export")]
    [IncludeSubPaths(false)]
    [Module<Module>]
    public sealed class ResourceExport : ResourceBinary
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceExport()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public override Response Process(Request request)
        {
            var guid = request.GetParameter<ParameterExportId>()?.Value.ToLower();
            var path = ViewModel.ExportDirectory;

            Data = File.ReadAllBytes(Path.Combine(path, guid + ".zip"));

            var response = base.Process(request);
            response.Header.ContentDisposition = "attatchment; filename=" + Path.GetFileName(guid + ".zip") + "; size=" + Data.LongLength;
            response.Header.ContentType = "application/zip";

            request.ServerContext.Log.Debug(message: I18N("webexpress:resource.file"), args: new object[] { request.RemoteEndPoint, request.Uri });

            return response;
        }
    }
}
