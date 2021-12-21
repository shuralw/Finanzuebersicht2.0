########## Modules ##########

contractor add domain Accounting

########## Entities ##########

contractor add entity Accounting.AccountingEntry:AccountingEntries -s UserManagement.EmailUser:EmailUsers
contractor add entity Accounting.Category:Categories -s UserManagement.EmailUser:EmailUsers
contractor add entity Accounting.CategorySearchTerm:CategorySearchTerms -s UserManagement.EmailUser:EmailUsers

########## Relationen ##########

contractor add relation 1:n Accounting.Category:Categories Accounting.AccountingEntry:AccountingEntries
contractor add relation 1:n Accounting.Category:Categories Accounting.Category:Categories -n SuperCategory:ChildCategories
contractor add relation 1:n Accounting.Category:Categories Accounting.CategorySearchTerm:CategorySearchTerms

########## Properties ##########

contractor add property integer LegacyBetriebsschluessel -e Ausbildungspartner.Betrieb:Betriebe
contractor add property string:16 LegacyBetriebskuerzel -e Ausbildungspartner.Betrieb:Betriebe
contractor add property boolean StandardDatenfreigabeFuerAnmeldung -e Ausbildungspartner.Betrieb:Betriebe
contractor add property string:30 -e Accounting.Auftragskonto
contractor add property DateTime -e Accounting.Buchungsdatum
contractor add property DateTime -e Accounting.ValutaDatum
contractor add property string:300 -e Accounting.Buchungstext
contractor add property string:300 -e Accounting.Verwendungszweck
contractor add property string:100 -e Accounting.GlaeubigerId
contractor add property string:100 -e Accounting.Mandatsreferenz
contractor add property string:100 -e Accounting.Sammlerreferenz
contractor add property double -e Accounting.Lastschrift_Ursprungsbetrag
contractor add property string:100 -e Accounting.Auslagenersatz_Ruecklastschrift
contractor add property string:200 -e Accounting.Beguenstigter
contractor add property string:22 -e Accounting.IBAN
contractor add property string:15 -e Accounting.BIC
contractor add property double -e Accounting.Betrag
contractor add property string:10 -e Accounting.Waehrung
contractor add property string:100 -e Accounting.Info

