using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;
using System.Diagnostics;
using System.Data;
using System.Net.NetworkInformation;
using System.Collections.Generic;

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

            var result = await command.ExecuteScalarAsync() as string;
            return result ?? string.Empty;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hiba a QR-kód lekérdezésekor: {ex.Message}");
            return string.Empty;
        }
    }

    public async Task<string> GenerateQRCodeAsync(MySqlConnection connection)
    {
        var query = "SELECT MAX(CAST(SUBSTRING(qr, 3) AS UNSIGNED)) FROM felhasznalok WHERE qr LIKE 'QR%'";
        using var command = new MySqlCommand(query, connection);

        var result = await command.ExecuteScalarAsync();

        int number = 0;

        if (result != DBNull.Value)
        {
            number = Convert.ToInt32(result) + 1;
        }

        var qrCodeText = $"QR{number:D3}";

        return qrCodeText;
    }

    public async Task<bool> AddUserToDatabase(string fullName, string email, string username, string password)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string qrCode = await GenerateQRCodeAsync(connection);

            var query = "INSERT INTO felhasznalok (qr, nev, email, felh_nev, jelszo) VALUES (@QRCode, @FullName, @Email, @Username, @Password)";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@QRCode", qrCode);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hiba: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ValidateUser(string username, string password)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM felhasznalok WHERE felh_nev = @Username AND jelszo = @Password";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba: {ex.Message}");
            return false;
        }
    }

    public async Task<List<Blokk>> GetBlokkokAsync()
    {
        var blokkok = new List<Blokk>();

        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT blk_id, fhk_qr, adatok, vasar_dt, vasar_ido, vasar_hely, uzlet FROM blokkok";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                blokkok.Add(new Blokk
                {
                    Id = reader.GetInt32("blk_id"),
                    QR = reader.GetString("fhk_qr"),
                    Adatok = reader.GetString("adatok"),
                    VasarDatum = reader.GetDateTime("vasar_dt"),
                    VasarIdo = reader.GetString("vasar_ido"),
                    VasarHely = reader.GetString("vasar_hely"),
                    Uzlet = reader.GetString("uzlet")
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hiba a blokkok lekérdezésekor: {ex.Message}");
        }

        return blokkok;
    }
}

public class Blokk
{
    public int Id { get; set; }
    public string QR { get; set; }
    public string Adatok { get; set; }
    public DateTime VasarDatum { get; set; }
    public string VasarIdo { get; set; }
    public string VasarHely { get; set; }
    public string Uzlet { get; set; }
}