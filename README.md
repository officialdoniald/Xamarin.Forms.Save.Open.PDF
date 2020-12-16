# Xamarin.Forms.Save.Open.PDF

NuGet Package: https://www.nuget.org/packages/Xamarin.Forms.SaveOpenPDFPackage/

1. Download this Package just in your Standard Library.
2. Platform specific things:

Android:

In the AndroidManifest.xml file, you have to add theese lines:

a, this is between the <android> tags:

<provider android:name="android.support.v4.content.FileProvider" android:authorities="${applicationId}.provider" android:exported="false" android:grantUriPermissions="true">
			<meta-data android:name="android.support.FILE_PROVIDER_PATHS" android:resource="@xml/provider_paths" />
</provider>

b, this is outside the <android> tags:

<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

Create, if you don't create yet, an "xml" folder into the "Resources" folder in the Android Project, and create an xml file, and call it: "provider_paths.xml".
The file should contain:
<?xml version="1.0" encoding="UTF-8" ?>
<paths xmlns:android="http://schemas.android.com/apk/res/android">
	<root-path name="root" path="." />
</paths>

Android ready, let's go to iOS.

iOS:

Don't need any nessessary thing.

3. Use the packge's function:

await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(Guid.NewGuid() + ".pdf", "application/pdf", stream, PDFOpenContext.InApp); // if you want to open the PDF in your app
await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(Guid.NewGuid() + ".pdf", "application/pdf", stream, PDFOpenContext.ChooseApp); // if you want to open the PDF outside the app

First parameter: Guid.NewGuid() + ".pdf" -> I created a uniqe name of the pdf file, but you can change whatever you want
Second parameter: a stream (System.IO.Stream), so you need to download or open an existing pdf, the point is you have to convert your pdf into a stream
Third parameter: how you want to open your pdf
