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

        public PDFPreviewPage(Blokk blokk)
        {
            InitializeComponent();
            this.blokk = blokk;
            GenerateAndDisplayPDF();
        }

        private void GenerateAndDisplayPDF()
        {
            using var memoryStream = new MemoryStream();

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
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var base64 = Convert.ToBase64String(memoryStream.ToArray());

            // Beágyazott PDF megjelenítése WebView-ben
            var html = $@"
                <html>
                    <body style='margin:0;padding:0;'>
                        <embed width='100%' height='100%' type='application/pdf' src='data:application/pdf;base64,{base64}' />
                    </body>
                </html>";

            PdfWebView.Source = new HtmlWebViewSource { Html = html };
        }

        private async void DownloadPDF(object sender, EventArgs e)
        {
            using var memoryStream = new MemoryStream();

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
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var destPath = Path.Combine(FileSystem.AppDataDirectory, $"blokk_{blokk.Id}.pdf");

            using var fileStream = File.Create(destPath);
            memoryStream.CopyTo(fileStream);

            await DisplayAlert("Sikeres letöltés", $"A fájl elérhetõ itt:\n{destPath}", "OK");
        }
    }
}
