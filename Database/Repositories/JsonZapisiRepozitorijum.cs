using Application.Interfaces.Repositories;
using Domain.Models;
using Shared;
using System.Text.Json;

namespace Database.Repositories;

// TODO: implementirati trajno cuvanje u JSON datoteci.
// Nacrtati/odabrati nacin serijalizacije polimorfnih modela konkretnog zadatka.
public sealed class JsonZapisiRepozitorijum : IZapisiRepozitorijum
{
    private readonly string _putanja;
    private readonly JsonSerializerOptions _opcije;
    private static readonly object _locker = new();

    public JsonZapisiRepozitorijum(string putanja)
    {
        _putanja = putanja;
        _opcije = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    private List<Zapis> UcitajIzFajla()
    {
        lock (_locker)
        {
            if (!File.Exists(_putanja))
            {
                return new List<Zapis>();
            }

            try
            {
                string json = File.ReadAllText(_putanja);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Zapis>();
                }

                var zapisi = JsonSerializer.Deserialize<List<Zapis>>(json, _opcije);
                return zapisi ?? new List<Zapis>();
            }
            catch
            {
                return new List<Zapis>();
            }
        }
    }

    private void SacuvajUFajlu(List<Zapis> zapisi)
    {
        lock (_locker)
        {
            string json = JsonSerializer.Serialize(zapisi, _opcije);
            File.WriteAllText(_putanja, json);
        }
    }

    public Result Sacuvaj(Zapis zapis)
    {
        if (zapis == null)
        {
            return Result.Failure("zapis.null", "Zapis ne sme biti nula.");
        }

        var zapisi = UcitajIzFajla();
        var index = zapisi.FindIndex(z => z.Id == zapis.Id);

        if (index >= 0)
        {
            zapisi.Add(zapis);
        }

        try
        {
            SacuvajUFajlu(zapisi);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("repo.greska_upisa", "Greska prilikom upisa JSON fajla.");
        }
    }

    public Result<Zapis> Pronadji(Guid id)
    {
        var zapisi = UcitajIzFajla();
        var zapis = zapisi.FirstOrDefault(z => z.Id == id);

        if (zapis == null)
        {
            return Result<Zapis>.Failure("zapis.nepostojeci", "Zapis sa unedim id-jem ne postoji");
        }

        return Result<Zapis>.Success(zapis);
    }

    public Result<IReadOnlyList<Zapis>> Svi()
    {
        var zapisi = UcitajIzFajla();
        IReadOnlyList<Zapis> lista = zapisi;
        return Result<IReadOnlyList<Zapis>>.Success(lista);
    }

    public Result Obrisi(Guid id)
    {
        var zapisi = UcitajIzFajla();
        var zapis = zapisi.FirstOrDefault(z => z.Id == id);

        if (zapis == null)
        {
            return Result.Failure("zapis.nepostojeci", "Zapis sa unetiim id-jem ne postoji");
        }

        zapisi.Remove(zapis);

        try
        {
            SacuvajUFajlu(zapisi);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("repo.greska_brisanja", "Greska prilikom brisanja iz JSON fajla.");
        }
    }
}
