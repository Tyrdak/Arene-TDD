# Atelier 4 - Arène TDD

Implémentation en C# d'une logique de combat de gladiateur avec **TDD** (Red / Green / Refactor) et **inversion de contrôle** sur le dé pour rendre les tests déterministes.

## Stack

- .NET 10
- xUnit

## Structure

- `GladiatorArena/` — bibliothèque (Gladiator, IDice, Dice)
- `GladiatorArena.Tests/` — tests unitaires xUnit

## Lancer les tests

```bash
dotnet test
```

## Backlog (suivi en TDD)

1. Erreur : Dé truqué, hors 1..6
2. Erreur : Un gladiateur ne peut s'attaquer lui-même
3. Attaque standard (`<6`) : `health -= dé + force - armure`
4. 2 attaques standard consécutives
5. Armure bien plus forte : aucun dégât (jamais négatif)
6. Coup critique (dé == 6) : `(dé + force) * 2 - armure`
7. Mort de l'adversaire : `Health` clampé à 0
