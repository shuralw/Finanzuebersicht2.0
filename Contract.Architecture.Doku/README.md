<!-- Kopfzeile -->
<p align="center">
  <img src="https://i.imgur.com/501odUk.png" alt="Logo" height="150">

  <h2 align="center">Contract Architecture</h2>

  <p align="center">
    <i>Ein Backend Architekturstil für organisierte ASP.NET 5 APIs.</i>
  </p>
</p>

[[_TOC_]]

# Allgmeines

Die Contract Architektur ist eine Grundlage für organisierte Implementierung von Backendaufgaben.

Hier ein kurzer Auszug aus dem Feature-Katalog:
- Anmeldung mit E-Mail-Benutzer (+ Passwort zurücksetzen)
- Zeitgesteuerte Hintergrundprozesse
- Automatisierte API-Dokumentation
- Datei-Logging, Datenbank-Logging oder Syslog-Logging
- Unterstützung für Docker Container
- Azure DevOps Build Pipelines für Docker
- Azure DevOps Build Pipelines für Pull Requests mit Quality Gates (Tests, Code Coverage)
- Umfassende Dokumentation

### Eingesetzte Komponenten
Der Technologie Stack der Contract Architektur umfasst folgenden Komponenten:
- Frontend: [Angular 9](https://angular.io/docs)
- Backend: [ASP.NET 5](https://docs.microsoft.com/de-de/aspnet/core/?view=aspnetcore-5.0)
- Datenbank: [Microsoft SQL Server 2016](https://docs.microsoft.com/de-de/sql/sql-server/?view=sql-server-ver15) auf einem Microsoft Windows Server 2016+ oder localDB von Visual Studio

Darüber hinaus wurden folgende Frameworks und Bibliotheken verwendet:
- Frontend (Angular)
  - tslint (für statische Codeanalyse)
- Backend (ASP.NET 5)
  - Entity Framework (für den Zugriff auf die Datenbank)
  - nLog (ein Logging-Framework)
  - Swashbuckle (für die API-Dokumentation)
  - Rest-Sharp (Für den Aufruf von anderen HTTP basierten Services)
  - UAParser (zur Erkennung von Browser und Betriebssystem aus dem emailUserAgent-Header)
  - MSTest (ein Testing-Framework)
  - Moq (ein Mocking-Framework)
  - StyleCop (für statische Codeanalyse)
- Datenbank (Microsoft SQL Server 2016+ auf einem Microsoft Windows Server 2016+ oder localDB von Visual Studio)
  - SQL Server Data Tools in Visual Studio (SSDT, um die Datenbank versionieren zu können)
- Docker
  - [nginx:stable-alpine](https://hub.docker.com/_/nginx) (das Basis-Image vom Frontend-Container)
  - [mcr.microsoft.com/dotnet/runtime:5.0-buster-slim](https://hub.docker.com/_/microsoft-dotnet-runtime/) (das Basis-Image vom Backend-Container)


# Loslegen

In den folgenden Schritten wird kurz erklärt, wie die Contract Architektur in der Entwicklungsumgebung eingerichtet werden kann.

## Technische Voraussetzungen

Für das Ausführen der Contract Architektur sind folgende technische Voraussetzungen zu schaffen:
- Visual Studio 2015+ Enterprise (optional auch Community möglich) mit den Workloads
  - [ASP.NET 5](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50)
  - [SQL Server Data Tools](https://docs.microsoft.com/de-de/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-ver15)
- Microsoft SQL Server 2016+ auf einem Microsoft Windows Server 2016+ oder localDB von Visual Studio
- Visual Studio Code (die folgenden Erweiterungen sind optional, jedoch erleichtern sie die Arbeit enorm)
  - [Angular Language Service](https://marketplace.visualstudio.com/items?itemName=Angular.ng-template)
  - [SCSS Formatter](https://marketplace.visualstudio.com/items?itemName=sibiraj-s.vscode-scss-formatter)
  - [TSLint](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-typescript-tslint-plugin)
  - [Typescript Hero](https://marketplace.visualstudio.com/items?itemName=rbbit.typescript-hero)
- [NodeJS 12](https://nodejs.org/en/)
- Angular CLI 9
  - `npm install -g @angular/cli@v9-lts`
- [Git Bash](https://gitforwindows.org/)

## Installation
Es folgt eine kurze Beschreibung, wie das Contract Architektur aufgesetzt werden kann. Eine ausführlichere Beschreibung finden Sie im Wiki unter `Installationsbeschreibung`.

1. Klone das Repository
```sh
git clone https://jhammerschmidt@dev.azure.com/jhammerschmidt/Contract.Architecture/_git/Contract.Architecture
```

2. Öffne das Visual Studio Projekt mit der Contract.Architecture.sln

3. Im SQL Server Object Explorer muss nun in der localDB eine neue Datenbank angelegt werden.
```
Name: ContractArchitecture
```

4. Anschließend wird das Datenbank-Schema erstellt. Dazu in Visual Studio die Datei _Contract.Architecture.DB/ApplicationScopedSqlSchemaCompare.scmp_ öffnen. Hier die Zieldatenbank auswählen, auf "Compare" und anschließend auf Update drücken.

5. Nun können Beispieldaten in die Datenbank geladen werden. Hierzu soll das Skript _Contract.Architecture.DB/InsertDevData.sql_ ausgeführt werden.

6. Nun müssen Konfigurationsdateien angepasst werden. Eine genaue Beschreibung finden Sie im Wiki unter `Installationsbeschreibung / Backend`.

7. Anschließend kann das Contract.Architecture.API-Projekt ausgeführt werden.

8. Mit dem Befehl `npm install` im Contract.Architecture.Web-Projekt können nun die Abhängigkeiten heruntergeladen werden.

9. Zuletzt kann das Frontend mit dem Befehl `ng serve` im Contract.Architecture.Web-Projekt gestartet werden.

10. Über die URL `http://localhost:4200` kann die Anwendung nun genutzt werden.
```
Anmeldedaten:
E-Mail: test@example.org
Passwort: 123QWEasd!
```

# Kontakt

Jonas Hammerschmidt - jonas.hammerschmidt@outlook.de

Projekt Link: [https://dev.azure.com/jhammerschmidt/_git/Contract.Architecture](https://dev.azure.com/jhammerschmidt/_git/Contract.Architecture)
