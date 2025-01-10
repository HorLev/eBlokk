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

            // QR-k�d gener�l�sa �s megjelen�t�se
            await GenerateAndDisplayQRCodeAsync();
        }

        private async Task GenerateAndDisplayQRCodeAsync()
        {
            try
            {
                // Bejelentkezett felhaszn�l� QR-k�dj�nak lek�rdez�se
                string qrCodeText = UserSession.QRCode;

                if (string.IsNullOrEmpty(qrCodeText))
                {
                    Debug.WriteLine("QR-k�d nem tal�lhat� a bejelentkezett felhaszn�l�hoz.");
                    await DisplayAlert("Hiba", "QR-k�d nem tal�lhat� a bejelentkezett felhaszn�l�hoz.", "OK");
                    return;
                }
                else
                {
                    Debug.WriteLine($"QR-k�d adatok: {qrCodeText}");
                }

                // QR-k�d k�p gener�l�sa
                var qrCodeImage = GenerateQRCodeImage(qrCodeText);

                // K�p megjelen�t�se az Image vez�rl�ben
                QRCodeImage.Source = BitmapToImageSource(qrCodeImage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hiba", $"QR-k�d gener�l�si hiba: {ex.Message}", "OK");
            }
        }

        private SKBitmap GenerateQRCodeImage(string qrCodeText)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, ECCLevel.L);

                // QR-k�d m�ret �s cellam�ret meghat�roz�sa
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
                                if (qrCodeData.ModuleMatrix[row][col]) // Modul s�t�t (true) vagy vil�gos (false)
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
                // SKBitmap �talak�t�sa SKImage-re, majd PNG form�tumban k�dol�s
                var skImage = SKImage.FromBitmap(bitmap);
                skImage.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);

                // A mem�ria stream-t �talak�tjuk egy RandomAccessStream-re
                ms.Seek(0, System.IO.SeekOrigin.Begin);  // Miel�tt visszaadjuk a stream-et, �ll�tsuk be a poz�ci�t
                return ImageSource.FromStream(() => new System.IO.MemoryStream(ms.ToArray()));  // K�sz�t�nk egy �j mem�ria stream-et
            }
        }
    }
}
