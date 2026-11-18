using Presentation.Composition;
using Presentation.Interfaces;

namespace Presentation.Console;

// Meni treba dopuniti operacijama F1-F6 iz dodeljenog zadatka.
public sealed class ConsoleMenu
{
    private readonly IConsoleIO _io;
    private readonly AppContainer _container;

    public ConsoleMenu(IConsoleIO io, AppContainer container)
    {
        _io = io;
        _container = container;
    }

    public void Run()
    {
        bool izlaz = false;
        while (!izlaz)
        {
            _io.WriteLine("ERS27 - prakticni zadatak");
            _io.WriteLine("F1-kreiranje novog zapisa");
            _io.WriteLine("F2-pregled svih zapisa");
            _io.WriteLine("F3-pretraga zapisa po ID-ju");
            _io.WriteLine("F4-pozajmljivanje zapisa");
            _io.WriteLine("F5-razduzivanje zapisa");
            _io.WriteLine("F6-brisanje zapisa");
            _io.WriteLine("0. Izlaz");
            _io.WriteLine("Izaberi opciju:");

            string? izbor = _io.ReadLine();

            switch (izbor)
            {
                case "1":
                    F1_Dodaj();
                    break;
                case "2":
                    F2_PrikaziSve();
                    break;
                case "3":
                    F3_PronadjiPoIzdavcu();
                    break;
                case "4":
                    F4_Pozajmi();
                    break;
                case "5":
                    F5_Razduzi();
                    break;
                case "6":
                    F6_Obrisi();
                    break;
                case "0":
                    izlaz = true;
                    _io.WriteLine("Izlaz iz aplikacije");
                    break;
                default:
                    _io.WriteLine("Nepoznata opcija, pokusajte ponovo.");
                    break;
            }
        }
    }

    private void F1_Dodaj()
    {
        _io.WriteLine("Kreiranje novog zapisa");

        _io.Write("Unesite naziv:");
        string naziv = _io.ReadLine() ?? string.Empty;

        _io.Write("Unesite opis:");
        string opis = _io.ReadLine() ?? string.Empty;

        _io.Write("Unesite izdavaca:");
        string izdavac = _io.ReadLine() ?? string.Empty;

        _io.Write("Unesite minimalni broj igraca:");
        if (!int.TryParse(_io.ReadLine(), out int minIgraca))
        {
            _io.WriteLine("Greska");
            return;
        }

        _io.Write("Unesite maksimalan broj igraca:");
        if (!int.TryParse(_io.ReadLine(), out int maxIgraca))
        {
            _io.WriteLine("Greska");
            return;
        }

        _io.WriteLine("Proverite da li koristite konkretnu izvedenu klasu.");

    }

    private void F2_PrikaziSve()
    {
        _io.WriteLine("Pregled svih zapisa");
        var rezultat = _container.Repozitorijum.Svi();

        if (!rezultat.IsSuccess)
        {
            _io.WriteLine("Greska");
            return;
        }

        var zapisi = rezultat.Value;
        if (zapisi == null || zapisi.Count == 0)
        {
            _io.WriteLine("Nema sacuvanih zapisa.");
            return;
        }

        foreach (var z in zapisi)
        {
            _io.WriteLine("ID: | Naziv: | Izdavac: | Pozajmljena? ");
        }

    }

    private void F3_PronadjiPoIzdavcu()
    {
        _io.WriteLine("Pretraga po id-ju");
        _io.Write("Unesite id:");
        string izdavac = _io.ReadLine() ?? string.Empty;

        if (!Guid.TryParse(_io.ReadLine(), out Guid id))
        {
            _io.WriteLine("Nevalidan format id-ja.");
            return;
        }

        var rezultat = _container.Servis.PronadjiPoIzdavacu(izdavac);
        if (rezultat.IsSuccess && rezultat.Value != null)
        {
            var z = rezultat.Value;
            _io.WriteLine("Pronadjeno");
        }

        else
        {
            _io.WriteLine("Zapis nije pronadjen.");
        }
    }

    private void F4_Pozajmi()
    {
        _io.WriteLine("Pozajmljivanje zapisa");
        _io.Write("Unesite id zapis koji se pozajmljuje:");
        if (!Guid.TryParse(_io.ReadLine(), out Guid id))
        {
            _io.WriteLine("Nevalidan format id-ja.");
            return;
        }

        var rezultat = _container.Servis.Pozajmi(id);
        if (rezultat.IsSuccess)
        {
            _io.WriteLine("Zapis je uspesno pozajmljen.");
        }
        else
        {
            _io.WriteLine("Greska");
        }
    }

    private void F5_Razduzi()
    {
        _io.WriteLine("Razduzivanje zapisa");
        _io.Write("Unesite id zapis koji se razduzuje:");
        if (!Guid.TryParse(_io.ReadLine(), out Guid id))
        {
            _io.WriteLine("Nevalidan format id-ja.");
            return;
        }

        var rezultat = _container.Servis.Razduzi(id);
        if (rezultat.IsSuccess)
        {
            _io.WriteLine("Zapis je uspesno razduzen.");
        }
        else
        {
            _io.WriteLine("Greska");
        }
    }

    private void F6_Obrisi()
    {
        _io.WriteLine("Brisanje zapisa");
        _io.Write("Unesite id zapis koji se brisanje:");
        if (!Guid.TryParse(_io.ReadLine(), out Guid id))
        {
            _io.WriteLine("Nevalidan format id-ja.");
            return;
        }

        var rezultat = _container.Repozitorijum.Obrisi(id);
        if (rezultat.IsSuccess)
        {
            _io.WriteLine("Zapis je uspesno obrisan.");
        }
        else
        {
            _io.WriteLine("Greska");
        }
    }
}
