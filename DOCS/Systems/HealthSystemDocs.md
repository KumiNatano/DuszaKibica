## HealthSystem
   -   zależy od BarParent
  ### Atrybuty:
   -   healthAmount(int) -> aktualna wartość życia obiektu.
   -   maxHealthAmount(int) -> maksymalna wartość życia obiektu.
   -   isAlive(bool) -> czy obiekt żyje (nie wiadomo czy bdz wgl potrzebne)
   -   hpBar(BarParent) -> pasek życia ( jest wymagany )
  ### Metody:
   -    TakeDamage(int dmg) -> przyjmuje argument w int który jest wartością utraconych pkt życia. Jeśli obiekt utraci wszystkie pkt życia jest to jest niszczony.
   -    Heal(int heal) -> przyjmuje argument w int który jest wartością odnowionych pkt życia.
   -    CheckIfAlive -> zwraca isAlive.
