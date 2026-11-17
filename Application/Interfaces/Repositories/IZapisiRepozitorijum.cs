using Domain.Models;
using Shared;

namespace Application.Interfaces.Repositories;

public interface IZapisiRepozitorijum
{
    Result Sacuvaj(Zapis zapis);
    Result<Zapis> Pronadji(Guid id);
    Result<IReadOnlyList<Zapis>> Svi();
    Result Obrisi(Guid id);
}
