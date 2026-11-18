using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class KartarskaIgra : Zapis
    {
        public int BrojKarata { get; private set; }

        public override bool JeIspravan(out string porukaGreske)
        {
            if (!base.JeIspravan(out porukaGreske))
                return false;

            if (BrojKarata <= 0)
            {
                porukaGreske = "Broj karata mora biti veci od 0.";
                return false;
            }

            return true;
        }

        public KartarskaIgra(string naziv, string opis, string izdavac, int minIgraca, int maxIgraca, int brojKarata)
            : base(naziv, opis, izdavac, minIgraca, maxIgraca)
        {
            if (brojKarata <= 0)
                throw new ArgumentException("Broj karata mora biti veci od 0.");

            BrojKarata = brojKarata;
        }
    }
}
