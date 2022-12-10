## StaminaSystem
### Opis
Klasa zajmująca się systemem staminy, nie wykonuje działań (ataków etc.) ale sprawdza czy dane działanie wymagające staminy może być wykonane, do tego komunikuje się z paskiem staminy i tworzy jego prostą animacje.
### Atrybuty
- staminaAmount - w int, aktualny stan staminy.
- maxStaminaAmount - w int, maksymalny stan staminy.
- regenTick - w float, co ile ma występować regenRate.
- regenRate - w int, ile pkt staminy ma sie odnawiać co regenTick;
- timer - w float, mierzy czas dla odnawiania sie staminy.
- staminaBar - BarParent, pasek staminy.
### Metody
- TakeAction(int cost) - sprawdza czy można wykonać akcje, jeśli tak to zwraca true i zmniejsza aktualny stan staminy, w przeciwnym wypadku zwraca false i nic nie robi.
- RefillTick() - sprawdza i odnawia stamine, również aktualizuje pasek staminy.
- setMaxStaminaAmount(int max) - ustawia maksymalną stamine, również ustawia maksymalną wartość dla paska.