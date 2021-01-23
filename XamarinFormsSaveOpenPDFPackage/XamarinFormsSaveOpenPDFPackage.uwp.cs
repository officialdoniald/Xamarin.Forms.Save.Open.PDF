using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.Data.Pdf;

namespace Plugin.XamarinFormsSaveOpenPDFPackage
{
    /// <summary>
    /// Interface for XamarinFormsSaveOpenPDFPackage
    /// </summary>
    public class XamarinFormsSaveOpenPDFPackageImplementation : IXamarinFormsSaveOpenPDFPackage
    {
        private const string fileExtension = ".pdf";

        public async Task SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context)
        {
            var targetFileName = $"{fileName}{fileExtension}}";
            if (System.IO.Path.GetExtension(fileName).Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase))
            {
                targetFileName = $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}{fileExtension}}";
            }

            var file = await CreateToDownloadFolderAsync(targetFileName);
            await WriteBufferToFileAsync(file, memoryStream);

            switch (context)
            {
                case PDFOpenContext.InApp:
                    await LaunchInAppAsync();
                    break;

                default:
                case PDFOpenContext.ChooseApp:
                    await LaunchDefaultPdfAppAsync(file);
                    break;
            }
        }

        private async Task<IStorageFile> CreateToLocalFolderAsync(string targetFileName)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(targetFileName, CreationCollisionOption.ReplaceExisting);
            return file;
        }

        private async Task<IStorageFile> CreateToDownloadFolderAsync(string targetFileName, string defaultDownloadFolder = "PDF_Download")
        {
            var folder = await DownloadsFolder.CreateFolderAsync(defaultDownloadFolder, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(targetFileName, CreationCollisionOption.ReplaceExisting);
            return file;
        }

        private async Task WriteBufferToFileAsync(IStorageFile file, MemoryStream memoryStream)
        {
            using (var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            using (var stream = fileStream.GetOutputStreamAt(0))
            using (var writer = new DataWriter(stream))
            {
                var buffer = System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeBufferExtensions.GetWindowsRuntimeBuffer(memoryStream);
                writer.WriteBuffer(buffer);
                await writer.StoreAsync();
                await stream.FlushAsync();
            }
        }

        private Task LaunchInAppAsync()
        {
            return Task.CompletedTask;
        }

        private async Task<bool> LaunchDefaultPdfAppAsync(IStorageFile file, bool IsShowAppPicker = false)
        {
            return await Launcher.LaunchFileAsync(file, new LauncherOptions
            {
                DisplayApplicationPicker = IsShowAppPicker
            });
        }
    }
}
