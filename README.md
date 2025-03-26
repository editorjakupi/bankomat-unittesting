# bankomat-dotnet

Yadda


Sammanfattning
Testklasser för varje fil: Varje fil (Account, Card och Bankomat) har sin egen testklass enligt instruktionen.

Ett unit-test per metod: Varje metod i varje klass testas antingen genom att verifiera ett returvärde eller genom att mäta en effekt (t.ex. insatt meddelande i Bankomat).

Scenario-test för Bankomat: Det sista testfallet i BankomatTest (metoden ScenarioTest_FullFlow) genomför det exakta flödet som specificerats, med kontroller av varje genererat meddelande.

Förbättringar av källkoden: Om man under testningen upptäcker behov av förändringar (t.ex. att hantera en situation utan insatt kort eller fler felhanteringsfall), kan man vidareutveckla både koden och testerna enligt den feedbacken.


Sammanfattningsvis: Jag har inte behövt göra större förändringar i den ursprungliga koden, eftersom den redan var designad med mätbara effekter (genom returvärden och meddelanden). Istället har jag anpassat mina tester (bland annat genom att “rensa” meddelandekön efter varje operation) för att säkerställa att varje enskild metod testas isolerat och att effekterna blir tydligt mätbara. Om jag hade upptäckt brister – som att vissa operationer saknade en verifierbar effekt – hade jag naturligtvis ändrat koden för att förbättra testbarheten, till exempel genom att lägga till fler felkontroller eller separera logik från UI/direkta meddelanden.





Är detta en komplett bankomat?
I den nuvarande implementationen hanteras grunderna för kortinmatning, PIN-verifiering, uttag samt utmatning av meddelanden. Detta räcker för att demonstrera en enkel bankomatlogik med tanke på de testbara effekterna. Dock är det långt ifrån en "komplett" bankomat i verkligheten, och här följer några förslag på vad som skulle behöva läggas till eller förbättras:

Säkerhet: En riktig bankomat bör hantera exempelvis begränsningar på antalet felaktiga PIN-inmatningsförsök, vilket kan leda till en spärr av kortet vid upprepade fel. Det kan även innebära att kortet inaktiveras efter för många felaktiga försök eller att användaren kontaktas i bankens system. Testas genom att simulera flera misslyckade PIN-försök och verifiera att rätt larm och ev. spärrlogik aktiveras.

Flera Transaktionstyper: Förutom uttag bör en komplett bankomat även hantera insättningar, överföringar och kontoinformation. Varje transaktionstyp ska ha en tydlig och mätbar effekt, vilket gör det enkelt att skriva enhetstester för dessa flöden. Testas genom att skapa tester för insättningsmetoden, kontohämtning och eventuellt överföringar mellan konton.

Fysisk Logistik för Sedlar: Den verkliga bankomaten behöver ett sätt att räkna ut och betala ut fysiska sedlar med olika valörer. Det innebär komplex logik för att räkna ut vilka sedlar som ska ges ut utifrån tillgängliga fysiska reserver. Här skulle tester kunna inkludera kontroll av att rätt kombination av sedlar ges ut, givet en viss summa.

Kortprocess och Inloggning: Bankomaten bör kontrollera om ett kort redan är insatt innan andra operationer genomförs, och ge tydliga felmeddelanden om ingen ordentlig session har startats. Det innebär att metoderna bör ha inbyggda kontroller för att säkerställa att inga operationer sker om inget kort är inmatat. Testas genom att försöka genomföra transaktioner utan att ett kort är insatt och verifiera att korrekt felhantering sker.

Loggning och Spårbarhet: För felsökning samt säkerhetsrevisioner i en verklig bankomatsystem är det viktigt med loggning av alla transaktioner och händelser. Detta går även att testa, genom att kontrollera att loggposter läggs till vid varje kritisk operation.

Integration med Bankens System: I en verklig miljö kommunicerar bankomaten med bankens servrar för att validera transaktioner, uppdatera konton och hantera eventuell bedrägeriförebyggande logik. Här skulle integrationstester eller simulatorer kunna användas för att verifiera att kommunikationen mellan bankomaten och banksystemet flödar korrekt.

Så även om den nuvarande lösningen är bra för en demonstration av grundläggande funktionalitet och för att visa att varje del kan testas, så är den långt ifrån komplett. Att bygga en komplett bankomatslösning kräver hantering av många fler scenarier och säkerhetsaspekter, med robust logik och error handling som gör systemet mer resilient och säkert.
