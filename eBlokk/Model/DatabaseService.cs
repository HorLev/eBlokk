using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using eBlokk.Model;
using System.Data;

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

        return $"QR{number:D3}";
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

            var query = "SELECT qr FROM felhasznalok WHERE felh_nev = @Username AND jelszo = @Password LIMIT 1";
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                UserSession.Username = username;
                UserSession.QRCode = result.ToString();
                return true;
            }

            return false;
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

        if (string.IsNullOrEmpty(UserSession.QRCode))
        {
            Debug.WriteLine("Nincs bejelentkezett felhasználó QR-kód.");
            return blokkok;
        }

        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT blk_id, fhk_qr, adatok, vasar_dt, vasar_ido, vasar_hely, uzlet FROM blokkok WHERE fhk_qr = @QR";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@QR", UserSession.QRCode);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                try
                {
                    int id = reader.GetInt32("blk_id");
                    string qr = reader.GetString("fhk_qr");
                    string adatok = reader.GetString("adatok");
                    DateTime vasarDatum = reader.GetDateTime("vasar_dt");
                    TimeSpan vasarIdoTimeSpan = (TimeSpan)reader["vasar_ido"];
                    string vasarIdo = vasarIdoTimeSpan.ToString(@"hh\:mm\:ss");
                    string vasarHely = reader.GetString("vasar_hely");
                    string uzlet = reader.GetString("uzlet");

                    blokkok.Add(new Blokk
                    {
                        Id = id,
                        QR = qr,
                        Adatok = adatok,
                        VasarDatum = vasarDatum,
                        VasarIdo = vasarIdo,
                        VasarHely = vasarHely,
                        Uzlet = uzlet
                    });

                    Debug.WriteLine($"Blokk hozzáadva: {id} - {qr} - {adatok}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Hiba egy blokk olvasásakor: {ex.Message}");
                }
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
    public string QR { get; set; } = string.Empty;
    public string Adatok { get; set; } = string.Empty;
    public DateTime VasarDatum { get; set; }
    public string VasarIdo { get; set; } = string.Empty;
    public string VasarHely { get; set; } = string.Empty;
    public string Uzlet { get; set; } = string.Empty;
}