## DropItem
### Opis
DropItem jest klasą główną każdego objektu klasyfikowanego jako Drop ( zo znaczy że nie jest przeznaczona dla przedmiotów w inventory przynajmniej na razie).
### Atrybuty
- itemID - w int na razie, przyszłościowo przeznaczone na ID dla itemów
- name - string nazwa Dropu
- degreesPerSecond - w int, od tej wartości zależy prędkość obrotu w teorii, po kilku zmianach nie widziałem żadnej różnicy.
### Metody
- setery + gettery dla name i itemID
- rotating() - funkcja odpowiadająca za obrót, dodana z powodu przesłaniania funkcji Update w klasach pochodnych jak się w nich cokolwiek doda. Planowane przeniesienie obrotu na korutyne, wtedy nie powinno być problemu.