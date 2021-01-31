# FAQ

## Wieso verbindet sich das Backend nicht mit der Datenbank?
Mögliche Ursachen:
1. Da die appsettings.json auf JSON basiert, muss der Connection String mit zwei Backslashes angegeben werden.
2. Das Problem kann entstehen, weil TLS auf dem SQL-Server nativ vom Windows Server genutzt wird und es unterschiedliche Verfahren zwischen den Windows Server Installationen gibt. Die empfohlene Zusammenstellung kann aus den Voraussetzungen aus der README.md entnommen werden.

## Wieso loggt das Backend nicht in die Datenbank?
Da die nlog.config auf XML basiert, muss der Connection String mit nur einem Backslash angegeben werden.

## An wen wende ich mich bei weiteren Fragen oder Vorschlägen?
Jonas Hammerschmidt - jonas.hammerschmidt@outlook.de
