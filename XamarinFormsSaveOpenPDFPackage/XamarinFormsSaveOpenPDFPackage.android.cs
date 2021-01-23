using Android;
using Android.Content;
using Android.Content.PM;
using Android.Webkit;
using Android.App;

using AndroidX.Core.App;
using AndroidX.Core.Content;

using System;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Plugin.XamarinFormsSaveOpenPDFPackage
{
    /// <summary>
    /// Interface for XamarinFormsSaveOpenPDFPackage
    /// </summary>
    public class XamarinFormsSaveOpenPDFPackageImplementation : IXamarinFormsSaveOpenPDFPackage
    {
        public async Task SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context)
        {
            string exception = string.Empty;
            string root = null;

            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)Android.App.Application.Context, new string[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

            //if (Android.OS.Environment.IsExternalStorageEmulated)
            //{
            //    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            //}
            //else
            //    root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(Path.Combine(root, "PDFFiles"));
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            //file.CreateNewFile();

            try
            {
                Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            if (file.Exists() && contentType != "application/html")
            {
                string extension = MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                switch (context)
                {
                    default:
                    case PDFOpenContext.InApp:
                        Android.App.Application.Context.StartActivity(intent);
                        break;
                    case PDFOpenContext.ChooseApp:
                        Android.App.Application.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
                        break;
                }
            }
        }
    }
}