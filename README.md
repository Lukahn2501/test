# README.md

## Environnement poste de développement
- Visual Studio 2022 Community
- SQL Server 2022 Express

## Quickstart

Sur l'instance SQL Server
- Créer une base nommée MovieRanker
- Jouer les sripts dans le dossier script

Ouvrir la solution
- Installer les dépendances nuget
- Mettre à jour les chaîne de connexion
- Génération du modèle pour vérifier que tout va bien
```powershell
Scaffold-DbContext “Server=DESKTOP-1T6TSP0\SQLEXPRESS;Database=MovieRanker;Trusted_Connection=True; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```
- Run

## TODO
- clean les chaînes de connexion
- clarifier le body PUT/POST (présence de l'id, inutilité du nom prénom pour le post movie)