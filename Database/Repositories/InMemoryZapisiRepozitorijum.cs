using Application.Interfaces.Repositories;
using Domain.Models;
using Shared;
using System.ComponentModel.Design;

namespace Database.Repositories;

// TODO: implementirati skladištenje u memoriji.

public sealed class InMemoryZapisiRepozitorijum : IZapisiRepozitorijum
{
    private readonly List<Zapis> _zapisi = new();

    public Result Sacuvaj(Zapis zapis)
    {
        if (zapis == null)
        {
            return Result.Failure("zapis.null", "Zapis ne sme biti null.");
        }

        var postojeciIndex = _zapisi.FindIndex(z => z.Id == zapis.Id);
        if (postojeciIndex >= 0)
        {
            _zapisi[postojeciIndex] = zapis;
        }
        else
        {
            _zapisi.Add(zapis);
        }

        return Result.Success();
    }

    public Result<Zapis> Pronadji(Guid id)
    {
        var zapis = _zapisi.FirstOrDefault(z => z.Id == id);
        if (zapis == null)
        {
            return Result<Zapis>.Failure("zapis.nepostoji", "Zapis sa unetim ID-jem ne postoji.");
        }

        return Result<Zapis>.Success(zapis);
    }

    public Result<IReadOnlyList<Zapis>> Svi()
    {
        IReadOnlyList<Zapis> lista = _zapisi.ToList();
        return Result<IReadOnlyList< Zapis >>.Success(lista);
    }

    public Result Obrisi(Guid id)
    {
        var zapis = _zapisi.FirstOrDefault(z => z.Id == id);
        if (zapis == null)
        {
            return Result.Failure("zapis.nepostojeci", "Zapis ne postoji.");
        }

        _zapisi.Remove(zapis);
        return Result.Success();
    }

}



