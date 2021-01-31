# SqlException: TLS Handshake failed
##Fehlermeldung:
Unhandled exception. Microsoft.Data.SqlClient.SqlException (0x80131904): A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: SSL Provider, error: 31 - Encryption(ssl/tls) handshake failed)

## Problem:
Die Backend-Anwendung (Docker/nativ) kann sich nicht mit dem SQL-Server verbinden, da ein Fehler beim SSL/TLS Handshake auftritt.

## Erklärung:
Das Problem kann entstehen, weil TLS auf dem SQL-Server nativ vom Windows Server genutzt wird und es unterschiedliche Verfahren zwischen den Windows Server Installationen gibt. Die empfohlene Zusammenstellung kann aus den Voraussetzungen aus der README.md entnommen werden.

## Lösung:
Verwendung von Microsoft SQL Server 2016+ auf einem Microsoft Windows Server 2016+