using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TablicnaIgra : Zapis
    {
        public int BrojFigurica { get; private set; }

        public override bool JeIspravan(out string porukaGreske)
        {
            if (!base.JeIspravan(out porukaGreske))
                return false;

            if (BrojFigurica < 0)
            {
                porukaGreske = "Broj figurica ne moze biti negativan. ";
                return false;
            }

            return true;
        }

        public TablicnaIgra(string naziv, string opis, string izdavac, int minIgraca, int maxIgraca, int brojFigurica)
            : base(naziv, opis, izdavac, minIgraca, maxIgraca)
        {
            if (brojFigurica < 0)
                throw new ArgumentException("Broj figurica ne moze biti negativan.");

            BrojFigurica = brojFigurica;
        }
    }
}
