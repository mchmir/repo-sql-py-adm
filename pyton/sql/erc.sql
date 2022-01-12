select 
  Account, --1
  Street, --2
  HouseNumber, --3
  HouseNumberChar, --4
  Flat, --5
  fio, --6
  PersUstr, --7
  CAST(ROUND(SaldoNachGaz, 2)AS decimal(16,2)) SaldoNachGaz, --8
  CAST(ROUND(SaldoNachPenya, 2) AS decimal(16,2)) SaldoNachPenya, --9
  CAST(ROUND(SaldoNachSudVzysk, 2) AS decimal(16,2)) SaldoNachSudVzysk, --10
  CAST(ROUND(SaldoNachSudIzder, 2) AS decimal(16,2)) SaldoNachSudIzder, --11
  CAST(ROUND(SaldoNachDopUslug, 2) AS decimal(16,2)) SaldoNachDopUslug, --12
  CAST(ROUND(SaldoNachItog, 2) AS decimal(16,2)) SaldoNachItog, --13
  CAST(ROUND(OplachenoGaz, 2) AS decimal(16,2)) OplachenoGaz, --14
  CAST(ROUND(OplachenoPenya, 2) AS decimal(16,2)) OplachenoPenya, --15 
  CAST(ROUND(OplachenoSudVzysk, 2) AS decimal(16,2)) OplachenoSudVzysk, --16
  CAST(ROUND(OplachenoSudIzder, 2) AS decimal(16,2)) OplachenoSudIzder, --17
  CAST(ROUND(OplachenoDopUslug, 2) AS decimal(16,2)) OplachenoDopUslug, --18
  CAST(ROUND(OplachenoItog, 2) AS decimal(16,2)) OplachenoItog, --19
  CAST(ROUND(PredPokaz, 2) AS decimal(16,2)) PredPokaz, --20
  CAST(ROUND(TekPokaz, 2) AS decimal(16,2)) TekPokaz, --21
  CAST(ROUND(Cena, 2) AS decimal(16,2)) Cena, --22
  Kolvo, --23
  CAST(ROUND(SummaNach, 2) AS decimal(16,2)) SummaNach, --24
  CAST(ROUND(SummNachislGaz, 2) AS decimal(16,2)) SummNachislGaz, --25
  CAST(ROUND(SummNachislPenya, 2) AS decimal(16,2)) SummNachislPenya, --26
  CAST(ROUND(SummNachislVzysk, 2) AS decimal(16,2)) SummNachislVzysk, --27
  CAST(ROUND(SummNachislIzder, 2) AS decimal(16,2)) SummNachislIzder, --28
  CAST(ROUND(SummNachislDopUslug, 2) AS decimal(16,2)) SummNachislDopUslug, --29
  CAST(ROUND(SummNachislItog, 2) AS decimal(16,2)) SummNachislItog, --30
  CAST(ROUND(SaldoKonecGaz, 2) AS decimal(16,2)) SaldoKonecGaz, --31
  CAST(ROUND(SaldoKonecPenya, 2) AS decimal(16,2)) SaldoKonecPenya, --32
  CAST(ROUND(SaldoKonecVzysk, 2) AS decimal(16,2)) SaldoKonecVzysk, --33
  CAST(ROUND(SaldoKonecIzder, 2) AS decimal(16,2)) SaldoKonecIzder, --34
  CAST(ROUND(SaldoKonecDopUslug, 2) AS decimal(16,2)) SaldoKonecDopUslug, --35
  CAST(ROUND(SaldoKonecItog, 2) AS decimal(16,2)) SaldoKonecItog, --36
  Primechanie --37
from AAAERC7
ORDER BY Account