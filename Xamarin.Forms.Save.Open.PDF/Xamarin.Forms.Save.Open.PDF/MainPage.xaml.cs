using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.IO;
using System.Net.Http;

namespace Xamarin.Forms.Save.Open.PDF
{
    public partial class MainPage : ContentPage
    {
        private string _uri = "https://officialdoniald.com/testpdf.pdf";

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save and open the PDF file in the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var uri = new Uri(_uri);
            
            Stream content;
            MemoryStream stream = new MemoryStream();
            
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStreamAsync();
                content.CopyTo(stream);
            }

            await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(Guid.NewGuid() + ".pdf", "application/pdf", stream, PDFOpenContext.InApp);
            
        }

        /// <summary>
        /// Save and open the PDF file with "choose app".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var uri = new Uri(_uri);

            Stream content;
            MemoryStream stream = new MemoryStream();

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStreamAsync();
                content.CopyTo(stream);
            }

            await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(Guid.NewGuid() + ".pdf", "application/pdf", stream, PDFOpenContext.ChooseApp);
        }
    }
}