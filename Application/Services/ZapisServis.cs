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
}
