using SkiaSharp;
using SkiaSharp.QrCode;
using SkiaSharp.QrCode.Models;
using Microsoft.Maui.Controls;
using eBlokk;
using System.Diagnostics;
using eBlokk.Model;
using System.Net.NetworkInformation;

namespace eBlokk
{
    public partial class QRPage1 : ContentPage
    {
        public QRPage1()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await GenerateAndDisplayQRCodeAsync();
        }

        private async Task GenerateAndDisplayQRCodeAsync()
        {
            try
            {
                string qrCodeText = UserSession.QRCode;

                if (string.IsNullOrEmpty(qrCodeText))
                {
                    Debug.WriteLine("QR-kód nem található a bejelentkezett felhasználóhoz.");
                    await DisplayAlert("Hiba", "QR-kód nem található a bejelentkezett felhasználóhoz.", "OK");
                    return;
                }
                else
                {
                    Debug.WriteLine($"QR-kód adatok: {qrCodeText}");
                }

                var qrCodeImage = GenerateQRCodeImage(qrCodeText);

                QRCodeImage.Source = BitmapToImageSource(qrCodeImage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hiba", $"QR-kód generálási hiba: {ex.Message}", "OK");
            }
        }

        private SKBitmap GenerateQRCodeImage(string qrCodeText)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, ECCLevel.L);

                int cellSize = 10;
                int qrCodeSize = qrCodeData.ModuleMatrix.Count * cellSize;

                var bitmap = new SKBitmap(qrCodeSize, qrCodeSize);

                using (var canvas = new SKCanvas(bitmap))
                {
                    canvas.Clear(SKColors.White);

                    using (var paint = new SKPaint { Color = SKColors.Black, Style = SKPaintStyle.Fill })
                    {
                        for (int row = 0; row < qrCodeData.ModuleMatrix.Count; row++)
                        {
                            for (int col = 0; col < qrCodeData.ModuleMatrix.Count; col++)
                            {
                                if (qrCodeData.ModuleMatrix[row][col])
                                {
                                    var x = col * cellSize;
                                    var y = row * cellSize;

                                    canvas.DrawRect(new SKRect(x, y, x + cellSize, y + cellSize), paint);
                                }
                            }
                        }
                    }
                }

                return bitmap;
            }
        }

        private Microsoft.Maui.Controls.ImageSource BitmapToImageSource(SKBitmap bitmap)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var skImage = SKImage.FromBitmap(bitmap);
                skImage.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);

                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return ImageSource.FromStream(() => new System.IO.MemoryStream(ms.ToArray()));
            }
        }
    }
}
