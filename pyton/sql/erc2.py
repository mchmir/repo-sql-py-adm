select 
  Account, --1
  Street, --2
  HouseNumber, --3
  HouseNumberChar, --4
  Flat, --5
  fio, --6
  PersUstr, --7
  ROUND(SaldoNachGaz, 2) SaldoNachGaz, --8
  CAST(ROUND(SaldoNachPenya, 2) AS decimal(16,2)) SaldoNachPenya, --9
  ROUND(SaldoNachSudVzysk, 2) SaldoNachSudVzysk, --10
  ROUND(SaldoNachSudIzder, 2) SaldoNachSudIzder, --11
  ROUND(SaldoNachDopUslug, 2) SaldoNachDopUslug, --12
  ROUND(SaldoNachItog, 2) SaldoNachItog, --13
  ROUND(OplachenoGaz, 2) OplachenoGaz, --14
  CAST(ROUND(OplachenoPenya, 2) AS decimal(16,2)) OplachenoPenya, --15 
  ROUND(OplachenoSudVzysk, 2) OplachenoSudVzysk, --16
  ROUND(OplachenoSudIzder, 2) OplachenoSudIzder, --17
  ROUND(OplachenoDopUslug, 2) OplachenoDopUslug, --18
  ROUND(OplachenoItog, 2) OplachenoItog, --19
  ROUND(PredPokaz, 2) PredPokaz, --20
  ROUND(TekPokaz, 2) TekPokaz, --21
  ROUND(Cena, 2) Cena, --22
  Kolvo, --23
  ROUND(SummaNach, 2) SummaNach, --24
  ROUND(SummNachislGaz, 2) SummNachislGaz, --25
  CAST(ROUND(SummNachislPenya, 2) AS decimal(16,2)) SummNachislPenya, --26
  ROUND(SummNachislVzysk, 2) SummNachislVzysk, --27
  ROUND(SummNachislIzder, 2) SummNachislIzder, --28
  ROUND(SummNachislDopUslug, 2) SummNachislDopUslug, --29
  ROUND(SummNachislItog, 2) SummNachislItog, --30
  ROUND(SaldoKonecGaz, 2) SaldoKonecGaz, --31
  CAST(ROUND(SaldoKonecPenya, 2) AS decimal(16,2)) SaldoKonecPenya, --32
  ROUND(SaldoKonecVzysk, 2) SaldoKonecVzysk, --33
  ROUND(SaldoKonecIzder, 2) SaldoKonecIzder, --34
  ROUND(SaldoKonecDopUslug, 2) SaldoKonecDopUslug, --35
  ROUND(SaldoKonecItog, 2) SaldoKonecItog, --36
  Primechanie --37
from AAAERC6