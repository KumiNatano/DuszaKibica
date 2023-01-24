## EnemyDrop
### Opis
Klasa odpowiadająca za drop po pokonaniu przeciwnika, narazie pozwala tylko na dropnięcie standardowego szalika, jest możliwość ustawienia szansy dropu.
### Atrybuty
- dropChance - w float, szansa 0-100 na wypadnięcie itemu
- itemID - tu końcowo chce dodać kolekcje id itemów które mogą wypaść z przeciwnika czy coś w tym stylu, teraz to nic nie robi.
- scarf - ClubScarf, potrzebny by działał drop.
### Metody
- dropLottery() - losuje i tworzy na ten moment tylko szalik.