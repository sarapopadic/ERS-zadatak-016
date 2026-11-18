
## US-01 (F1) - dodavanje nove drustvene igre
Kao administrator kluba drustvenih igara, želim da unesem novu igru sa nazivom, opisom, izdavačem i brojem igrača, kako bih proširio katalog dostupnik igara za članove.

Sistem mora da prihvata unos samo ako su naziv, opis i izavač popunjeni. Minimalan broj igrača mora biti najmanje 1 i ne sme biti veći od maksimalnog broja igrača. Ukoliko podaci nisu ispravni, sistem prikazuje odgovarajuću poruku o grešci i ne čuva zapis.

## US-02 (F2) - Pregled svih igara u katalogu
Kao korisnik aplikacije, želim da vidim listu svih unetih društvenih igara sa njihovim osnovnim detaljima i statusom dostupnosti, kako bih imala uvid u kompletnu ponudu kluba.

Lista prikazuje naziv igre, izdavača, preporučen broj igrača i informacija da li je igra trenutno pozajmljena. Ako u sistemu nema nijedna igra, prikazuje se jasna poruka da je katalog trenutno prazan.

## US-03 (F3) — Pretraga igara po izdavaču

Kao korisnik aplikacije,  
želim da pretražim društvene igre prema nazivu izdavača,  
kako bih brže pronašao naslove određenog proizvođača.

Kriterijumi prihvatanja:
Pretraga vraća sve igre čiji se izdavač poklapa sa unetim pojмом (bez obzira na velika i mala slova).
Ukoliko se unese prazan pojam za pretragu, sistem vraća odgovarajuću grešku validacije.

## US-04 (F4) — Pozajmljivanje društvene igre

Kao član kluba,  
želim da pozajmim slobodnu društvenu igru iz kataloga,  
kako bih mogao da je igram van prostorija kluba.

Kriterijumi prihvatanja:
Status igre se nakon uspešnog pozajmljivanja menja u „Pozajmljena“.
Sistem ne dozvoljava pozajmljivanje igre koja je već pozajmljena i prikazuje poruku upozorenja.
  
## US-05 (F5) — Razduživanje društvene igre

Kao član kluba,  
želim da vratim (razdužim) pozajmljenu društvenu igru,  
kako bi ona ponovo bila dostupna drugim članovima.

Kriterijumi prihvatanja:
Status igre se nakon razduživanja vraća u „Dostupna“.
Sistem onemogućava razduživanje igre koja u tom trenutku nije ni pozajmljena.

## US-06 (F6) — Trajno čuvanje podataka u JSON formatu

Kao korisnik sistema,  
želim da se svi unosi i izmene statusa igara automatski čuvaju u fajlu,  
kako se podaci ne bi izgubili nakon zatvaranja aplikacije.

Kriterijumi prihvatanja:
Svaka dodata, pozajmljena ili razdužena igra se odmah ažurira u JSON repozitorijumu.
Prilikom ponovnog pokretanja aplikacije, svi prethodno sačuvani zapisi se uspešno učitavaju.
