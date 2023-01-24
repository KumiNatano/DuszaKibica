## Inventory
### Opis
Klasa odpowiadająca za ekwipunek, narazie jedyne co robi to przechowuje potki i pozwala je użyć oraz zlicza zebrane szaliki, koniecznie do refactoru.
### Atrybuty
- scarfNumber - w int, zlicza zebrane szaliki.
- potions - przechowuje potki.
  od tego miejsca wysoce prawdobodobne zmiany przy refactoru.
- upgradeText - ShowStatUpgrade, wyświetla tekst przy zwiększeniu statystyk.
- stamina - StaminaSytem, potrzebne by zwiększyć poziom staminy.
- health - HealthSytem, potrzebne by zwiększyć poziom życia.
### Metody
- Update() - tu jest odczytanie czy wciśnięto przycisk do leczenia, jest tu bo nie wiem jak lepiej to zaimplementować.
- usePotion() - używa potki;