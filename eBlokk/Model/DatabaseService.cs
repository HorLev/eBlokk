using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;
using System.Diagnostics;
using System.Data;
using System.Net.NetworkInformation;

public class DatabaseService
{
    private readonly string connectionString = "Server=localhost;Port=3306;Database=eblokk;User ID=root;Password=;";

    public async Task<string> GetUserQRCode(string username)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT qr FROM felhasznalok WHERE felh_nev = @Username LIMIT 1";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);

            var result = await command.ExecuteScalarAsync();
            return result != null ? result.ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hiba a QR-kód lekérdezésekor: {ex.Message}");
            return string.Empty;
        }
    }

    // QR kód generálása az adatbázisból
    public async Task<string> GenerateQRCodeAsync(MySqlConnection connection)
    {
        var query = "SELECT MAX(CAST(SUBSTRING(qr, 3) AS UNSIGNED)) FROM felhasznalok WHERE qr LIKE 'QR%'";
        using var command = new MySqlCommand(query, connection);

        // Lekérdezzük a legnagyobb QR-kód számot
        var result = await command.ExecuteScalarAsync();

        int number = 0;

        if (result != DBNull.Value)
        {
            number = Convert.ToInt32(result) + 1; // Növeljük az értéket
        }

        // Új QR-kód formázása
        var qrCodeText = $"QR{number:D3}";

        return qrCodeText;
    }

    // Felhasználó adatainak beszúrása
    // Felhasználó adatainak beszúrása
    public async Task<bool> AddUserToDatabase(string fullName, string email, string username, string password)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            // QR kód generálása
            string qrCode = await GenerateQRCodeAsync(connection);

            var query = "INSERT INTO felhasznalok (qr, nev, email, felh_nev, jelszo) VALUES (@QRCode, @FullName, @Email, @Username, @Password)";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@QRCode", qrCode);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = await command.ExecuteNonQueryAsync();
            return result > 0; // Sikeres beszúrás
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hiba: {ex.Message}");
            return false; // Sikeretelen beszúrás hiba esetén
        }
    }

    // Felhasználó validálása (bejelentkezés)
    public async Task<bool> ValidateUser(string username, string password)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM felhasznalok WHERE felh_nev = @Username AND jelszo = @Password";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);  // Titkosított jelszó

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0; // Ha 1-nél nagyobb a találat, akkor létezik a felhasználó
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba: {ex.Message}");
            return false;
        }
    }
}