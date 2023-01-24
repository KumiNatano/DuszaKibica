## ClubScarf
### Opis
Główna klasa dla szalików, na razie również wszystkie szaliki bo nie ogarniam onDestroy().
### Atrybuty
-  timeLived - czas życia obiektu, dodane byt nie było sytuacji natychmiastowego zebrania szalika po pokonaniu dresa.
- timeToLive - w float, czas który obiekt musi przeżyć aby można go było podnieść.
### Metody
- Update() - rotacja + zliczanie timeLived.
- OnTriggerEnter2D(Collider2D collider) - przeniesione z inventory, odpowiada za poprawne zachowanie szalika po kolizji z graczem.