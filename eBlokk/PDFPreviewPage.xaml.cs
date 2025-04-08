using System;
using System.IO;
using Microsoft.Maui.Controls;
using eBlokk.Model;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace eBlokk
{
    public partial class PDFPreviewPage : ContentPage
    {
        private readonly Blokk blokk;
        private string pdfFilePath;

        public PDFPreviewPage(Blokk blokk)
        {
            InitializeComponent();
            this.blokk = blokk;

            GenerateAndDisplayPDF();
        }

        private void GenerateAndDisplayPDF()
        {
            string fileName = $"blokk_{blokk.Id}.pdf";
            pdfFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Content().Column(column =>
                    {
                        column.Item().Text($"eBlokk - Blokk adatai").Bold().FontSize(18);
                        column.Item().Text($"Dátum: {blokk.VasarDatum:yyyy-MM-dd}");
                        column.Item().Text($"Idõ: {blokk.VasarIdo}");
                        column.Item().Text($"Hely: {blokk.VasarHely}");
                        column.Item().Text($"Üzlet: {blokk.Uzlet}");
                        column.Item().Text($"Adatok: {blokk.Adatok}");
                    });
                });
            })
            .GeneratePdf(pdfFilePath);

            var webView = (WebView)FindByName("PdfWebView");
            if (webView != null)
            {
                webView.Source = new UrlWebViewSource { Url = pdfFilePath };
            }
        }

        private async void DownloadPDF(object sender, EventArgs e)
        {
            var destPath = Path.Combine(FileSystem.AppDataDirectory, Path.GetFileName(pdfFilePath));
            File.Copy(pdfFilePath, destPath, overwrite: true);

            await DisplayAlert("Sikeres letöltés", $"A fájl elérhetõ itt:\n{destPath}", "OK");
        }
    }
}