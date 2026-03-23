# APBD - Ćwiczenia 2 - Wypożyczalnia sprzętu

Aplikacja konsolowa w C# symulująca system wypożyczalni sprzętu na uczelni.

## Uruchomienie

1. Sklonować repozytorium
2. Otworzyć solucję w Visual Studio lub Rider
3. Uruchomić projekt ConsoleApp2

Po uruchomieniu ładowane są dane testowe i pojawia się interaktywne menu.

## Struktura projektu

```
ConsoleApp2/
├── Device.cs          - abstrakcyjna klasa bazowa dla sprzętu
├── Laptop.cs          - typ sprzętu: laptop
├── Projector.cs       - typ sprzętu: projektor
├── Camera.cs          - typ sprzętu: aparat
├── User.cs            - użytkownik + enum UserType
├── RentInstance.cs    - pojedyncze wypożyczenie, obliczanie opłat
├── Service.cs         - logika biznesowa
├── OperationResult.cs - wynik operacji zwracany przez serwis
├── ConsoleUI.cs       - obsługa menu i wejścia/wyjścia
└── Main.cs            - punkt wejścia
```

## Decyzje projektowe

### Podział warstw
Starałem się oddzielić logikę od interfejsu. `Service` nie ma żadnych wywołań "Console.WriteLine" - zajmuje się tylko logiką i zwraca `OperationResult`.
Całe wypisywanie do konsoli jest wyłącznie w `ConsoleUI`.
Dzięki temu gdyby trzeba było zmienić interfejs (np. na webowy), `Service` zostaje bez zmian.

### OperationResult
Zamiast rzucać wyjątkami przy przewidywalnych błędach (np. sprzęt niedostępny, przekroczony limit wypożyczeń) metody serwisu zwracają obiekt z flagą `Success` i komunikatem.
Uznałem, że to czytelniejsze niż try-catch w UI przy każdym wywołaniu.

### Kohezja
Każda klasa ma jedno zadanie:
- `Device` i podklasy - tylko dane, żadnej logiki
- `RentInstance` - przechowuje dane wypożyczenia i liczy należność
- `Service` - pilnuje reguł biznesowych
- `ConsoleUI` - obsługuje użytkownika
- `OperationResult` - przenosi wynik operacji między warstwami

### Coupling
`Service` zależy tylko od klas domenowych (`Device`, `User`, `RentInstance`). `ConsoleUI` zależy tylko od `Service` i `OperationResult`.
Nie ma zależności w drugą stronę.

### Dziedziczenie
`Device` jest klasą abstrakcyjną, bo każdy typ sprzętu ma wspólne pola (id, nazwa, cena, dostępność) ale też własne specyficzne.
Dziedziczenie wynika z modelu domeny.

### Reguły biznesowe
Limity wypożyczeń są w `User` w jednym miejscu (switch expression na `UserType`). Stawki za opóźnienie są przypisane do każdego urządzenia w konstruktorze.
