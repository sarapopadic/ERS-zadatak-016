using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
using Shared;

namespace Application.Services;

// Polazna struktura servisa; poslovne operacije nisu implementirane.
public sealed class ZapisServis
{
    private readonly IZapisiRepozitorijum _repozitorijum;
    private readonly ILoggerServis _logger;

    public ZapisServis(IZapisiRepozitorijum repozitorijum, ILoggerServis logger)
    {
        _repozitorijum = repozitorijum ?? throw new ArgumentNullException(nameof(repozitorijum));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Result Dodaj(Zapis zapis)
    {
        if (zapis == null)
        {
            return Result.Failure("zapis.null", "Zapis ne sme biti null.");
        }

        if (!zapis.JeIspravan(out string porukaGreske))
        {
            return Result.Failure("zapis.neispravan", porukaGreske);
        }

        _repozitorijum.Sacuvaj(zapis);
        return Result.Success();
    }

    public Result<IReadOnlyList<Zapis>> PronadjiPoIzdavacu(string izdavac)
    {
        if (string.IsNullOrWhiteSpace(izdavac))
        {
            return Result<IReadOnlyList<Zapis>>.Failure("izdavac.prazan", "Naziv izdavaca ne sme biti prazan");
        }

        var rezultatiSvi = _repozitorijum.Svi();
        if (!rezultatiSvi.IsSuccess)
        {
            return Result<IReadOnlyList<Zapis>>.Failure(rezultatiSvi.Error.Code, rezultatiSvi.Error.Message);
        }

        if (rezultatiSvi.Value == null)
        {
            return Result<IReadOnlyList<Zapis>>.Failure("rezultati.prazni", "Nema dostupnih zapisa");
        }

        var filtrirani = rezultatiSvi.Value
            .Where(z => z != null && z.Izdavac == null && z.Izdavac.Equals(izdavac, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (filtrirani.Count == 0)
        {
            return Result<IReadOnlyList<Zapis>>.Failure("rezultati.nema", "Nema pronadjenih zapisa za unetog izvodjaca.");
        }

        return Result<IReadOnlyList<Zapis>>.Success(filtrirani);
    }

    public Result Pozajmi(Guid id)
    {
        var rezultatPronalazenja = _repozitorijum.Pronadji(id);
        if (!rezultatPronalazenja.IsSuccess || rezultatPronalazenja.Value == null)
        {
            return Result.Failure("zapis.nepostojeci", "Zapis sa unetim ID-jem nepostoji");
        }

        var zapis = rezultatPronalazenja.Value;
        
        if (!zapis.Pozajmi(out string porukaGreske))
        {
            return Result.Failure("zapis.neispravan", porukaGreske);
        }

        _repozitorijum.Sacuvaj(zapis);
        return Result.Success();
    }

    public Result Razduzi(Guid id)
    {
        var rezultatPronalazenja = _repozitorijum.Pronadji(id);
        if (!rezultatPronalazenja.IsSuccess || rezultatPronalazenja.Value == null)
        {
            return Result.Failure("zapis.nepostoji", "Zapis sa unetim ID-jem ne postoji");
        }

        var zapis = rezultatPronalazenja.Value;
        if (!zapis.Pozajmi(out string porukaGreske))
        {
            return Result.Failure("zapis.neispravan", porukaGreske);
        }

        _repozitorijum.Sacuvaj(zapis);
        return Result.Success();
    }
}
