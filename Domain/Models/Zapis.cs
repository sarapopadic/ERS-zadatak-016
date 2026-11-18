namespace Domain.Models;
public abstract class Zapis
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Naziv { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public string Izdavac { get; set; } = string.Empty;
    public int MinIgraca { get; set; }
    public int MaxIgraca { get; set; }
    public bool JePozajmljena { get; set; } = false;
    public DateTimeOffset KreiranoUtc { get; init; } = DateTimeOffset.UtcNow;

    public Zapis(string naziv, string opis, string izdavac, int minIgraca, int maxIgraca)
    {
        Naziv = naziv;
        Opis = opis;
        Izdavac = izdavac;
        MinIgraca = minIgraca;
        MaxIgraca = maxIgraca;
    }

    public virtual bool JeIspravan(out string porukaGreske)
    {
        if (string.IsNullOrWhiteSpace(Naziv))
        {
            porukaGreske = "Naziv igre ne sme biti prazan.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Opis))
        {
            porukaGreske = "Opis igre ne sme biti prazan.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Izdavac))
        {
            porukaGreske = "Izdavac igre ne sme biti prazan.";
            return false;
        }

        if (MinIgraca < 1 || MinIgraca > MaxIgraca)
        {
            porukaGreske = "Minimalan broj igraca mora biti najmanje 1 i ne veci od maksimalnog";
            return false;
        }

        porukaGreske = string.Empty;
        return true;
    }

    public bool Pozajmi(out string porukaGreske)
    {
        if (JePozajmljena)
        {
            porukaGreske = "Igra je vec pozajmljena.";
            return false;
        }

        JePozajmljena = true;
        porukaGreske = string.Empty;
        return true;
    }

    public bool Razduzi(out string porukaGreske)
    {
        if (!JePozajmljena)
        {
            porukaGreske = "Igra nije pozajmljena i ne moze se razduziti.";
            return false;
        }

        JePozajmljena = false;
        porukaGreske = string.Empty;
        return true;
    }
}
