using System;
using System.IO;
using Microsoft.Maui.Controls;
using eBlokk.Model;
using IronPdf;

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
            License.LicenseKey = "IRONSUITE.HLEVI200408.GMAIL.COM.21949-3BB8CD7E6D-P33BF-LDKSYC4UGUCA-SPT7ZLSR6IDD-7FLXC4BB5RDN-OOVSNZUZU5B7-LNIVUVC4KMM4-4LNCJIP57BA7-BYPG3Y-TYQ5GNF7OFGPUA-DEPLOYMENT.TRIAL-ETUF53.TRIAL.EXPIRES.17.JUN.2025";
            var Renderer = new ChromePdfRenderer();

            var penztaros = "2M/H";
            var penztar = "001";
            var nyugtaszam = $"{Random.Shared.Next(1000, 9999)}/{Random.Shared.Next(10000, 99999)}";
            var keltezes = blokk.VasarDatum.ToString("yyyy.MM.dd") + " " + (blokk.VasarIdo ?? "12:00");

            var formazottAdatok = blokk.Adatok?.Replace(";", ";<br>") ?? "Nincs adat";

            string html = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: monospace;
                        font-size: 16px;
                        color: black;
                        margin: 0;
                        padding: 0;
                        display: flex;
                        justify-content: center;
                    }}
                    .blokk {{
                        width: 380px; /* fix blokk-szélesség */
                        padding: 30px;
                    }}
                    .center {{ text-align: center; }}
                    .bold {{ font-weight: bold; }}
                    .line {{ border-top: 1px dashed black; margin: 12px 0; }}
                    .row {{ display: flex; justify-content: space-between; margin: 6px 0; }}
                </style>
            </head>
            <body>
                <div class='blokk'>
                    <div class='center bold' style='font-size: 20px;'>eBlokk</div>
                    <div class='center'>Digitális blokk generátor</div>
                    <div class='center'>--------------------------------</div>
                    <div class='center bold' style='font-size: 18px;'>NYUGTA</div>

                    <div class='row'><div>Dátum:</div><div>{blokk.VasarDatum:yyyy.MM.dd}</div></div>
                    <div class='row'><div>Idõ:</div><div>{blokk.VasarIdo ?? "Ismeretlen"}</div></div>
                    <div class='row'><div>Hely:</div><div>{blokk.VasarHely ?? "N/A"}</div></div>
                    <div class='row'><div>Üzlet:</div><div>{blokk.Uzlet ?? "N/A"}</div></div>
                    <div class='line'></div>

                    <div class='bold'>Termékek:</div>
                    <div>{formazottAdatok}</div>

                    <div class='line'></div>
                    <div class='row'><div>Pénztáros:</div><div>{penztaros}</div></div>
                    <div class='row'><div>Pénztár:</div><div>{penztar}</div></div>
                    <div class='row'><div>Kelt:</div><div>{keltezes}</div></div>
                    <div class='row'><div>Nyugtaszám:</div><div>{nyugtaszam}</div></div>
                    <div class='center'> {Random.Shared.NextInt64(705000000, 705999999)}</div>
                </div>
            </body>
            </html>";

            var pdf = Renderer.RenderHtmlAsPdf(html);

            var base64 = Convert.ToBase64String(pdf.BinaryData);
            var htmlWrapper = $@"
            <html><body style='margin:0;padding:0;'>
                <embed width='100%' height='100%' type='application/pdf' src='data:application/pdf;base64,{base64}' />
            </body></html>";

            PdfWebView.Source = new HtmlWebViewSource { Html = htmlWrapper };
        }

        private async void DownloadPDF(object sender, EventArgs e)
        {
            License.LicenseKey = "IRONSUITE.HLEVI200408.GMAIL.COM.21949-3BB8CD7E6D-P33BF-LDKSYC4UGUCA-SPT7ZLSR6IDD-7FLXC4BB5RDN-OOVSNZUZU5B7-LNIVUVC4KMM4-4LNCJIP57BA7-BYPG3Y-TYQ5GNF7OFGPUA-DEPLOYMENT.TRIAL-ETUF53.TRIAL.EXPIRES.17.JUN.2025";
            var Renderer = new ChromePdfRenderer();

            var penztaros = "2M/H";
            var penztar = "001";
            var nyugtaszam = $"{Random.Shared.Next(1000, 9999)}/{Random.Shared.Next(10000, 99999)}";
            var keltezes = blokk.VasarDatum.ToString("yyyy.MM.dd") + " " + (blokk.VasarIdo ?? "12:00");

            var formazottAdatok = blokk.Adatok?.Replace(";", ";<br>") ?? "Nincs adat";

            string html = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: monospace;
                        font-size: 16px;
                        color: black;
                        margin: 0;
                        padding: 0;
                        display: flex;
                        justify-content: center;
                    }}
                    .blokk {{
                        width: 380px;
                        padding: 30px;
                    }}
                    .center {{ text-align: center; }}
                    .bold {{ font-weight: bold; }}
                    .line {{ border-top: 1px dashed black; margin: 12px 0; }}
                    .row {{ display: flex; justify-content: space-between; margin: 6px 0; }}
                </style>
            </head>
            <body>
                <div class='blokk'>
                    <div class='center bold' style='font-size: 20px;'>eBlokk</div>
                    <div class='center'>Digitális blokk generátor</div>
                    <div class='center'>--------------------------------</div>
                    <div class='center bold' style='font-size: 18px;'>NYUGTA</div>

                    <div class='row'><div>Dátum:</div><div>{blokk.VasarDatum:yyyy.MM.dd}</div></div>
                    <div class='row'><div>Idõ:</div><div>{blokk.VasarIdo ?? "Ismeretlen"}</div></div>
                    <div class='row'><div>Hely:</div><div>{blokk.VasarHely ?? "N/A"}</div></div>
                    <div class='row'><div>Üzlet:</div><div>{blokk.Uzlet ?? "N/A"}</div></div>
                    <div class='line'></div>

                    <div class='bold'>Termékek:</div>
                    <div>{formazottAdatok}</div>

                    <div class='line'></div>
                    <div class='row'><div>Pénztáros:</div><div>{penztaros}</div></div>
                    <div class='row'><div>Pénztár:</div><div>{penztar}</div></div>
                    <div class='row'><div>Kelt:</div><div>{keltezes}</div></div>
                    <div class='row'><div>Nyugtaszám:</div><div>{nyugtaszam}</div></div>
                    <div class='center'> {Random.Shared.NextInt64(705000000, 705999999)}</div>
                </div>
            </body>
            </html>";

            var pdf = Renderer.RenderHtmlAsPdf(html);

            var destPath = Path.Combine(FileSystem.AppDataDirectory, $"blokk_{blokk.Id}.pdf");
            await File.WriteAllBytesAsync(destPath, pdf.BinaryData);

            await DisplayAlert("Sikeres letöltés", $"A fájl elérhetõ itt:\n{destPath}", "OK");
        }

        private string GenerateHtmlContent()
        {
            return $@"
                <html>
                    <head>
                        <meta charset='UTF-8'>
                        <style>
                            body {{ font-family: Arial, sans-serif; margin: 40px; }}
                            h2 {{ margin-bottom: 10px; }}
                            p {{ margin: 5px 0; }}
                        </style>
                    </head>
                    <body>
                        <h2>Blokk adatok</h2>
                        <p><strong>Dátum:</strong> {blokk.VasarDatum:yyyy-MM-dd}</p>
                        <p><strong>Idõ:</strong> {blokk.VasarIdo ?? "Ismeretlen"}</p>
                        <p><strong>Hely:</strong> {blokk.VasarHely ?? "N/A"}</p>
                        <p><strong>Üzlet:</strong> {blokk.Uzlet ?? "N/A"}</p>
                        <p><strong>Adatok:</strong> {blokk.Adatok ?? "Nincs adat"}</p>
                    </body>
                </html>";
        }
    }
}