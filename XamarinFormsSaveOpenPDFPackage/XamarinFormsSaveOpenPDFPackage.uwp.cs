using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.XamarinFormsSaveOpenPDFPackage
{
    /// <summary>
    /// Interface for XamarinFormsSaveOpenPDFPackage
    /// </summary>
    public class XamarinFormsSaveOpenPDFPackageImplementation : IXamarinFormsSaveOpenPDFPackage
    {
        public Task SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context)
        {
            //throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }
}
