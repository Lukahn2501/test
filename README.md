# README.md

## Environnement poste de développement
- Visual Studio 2022 Community
- SQL Server 2022 Express
- IIS Express

## Quickstart

Ouvrir la solution
- Installer les dépendances nuget
- Mettre à jour les chaîne de connexion
- Appliquer les migrations via
> .NET Core CLI
`dotnet ef database update`

>Gestionnaire de paquets
`Update-Datase`
- Run (change the targer to IIS Express)

## Architecture Decision Records

[1 - API Structure](./ADRs/1_api_structure.md)

[2 - var vs Explicite Types](./ADRs/2_var_vs_types.md)

[3 - Rating Deletion](./ADRs/3_rating_deletion.md)
