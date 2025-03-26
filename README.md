# Bankomat – Enhetstester & Utökad Funktionalitet

## Översikt

Detta projekt är en simulerad bankomat (ATM) som har utvecklats med syftet att:
- Lära sig skriva enhetstester för program med tillstånd och logik (state/logic).
- Validera säkerhetskritisk funktionalitet (t.ex. att spärr kortet vid för många felaktiga PIN‑försök).
- Säkerställa att kontosaldo och bankomatens kontanta medel hanteras korrekt.
- Börja tänka som en testdriven utvecklare (TDD).

Projektet använder .NET 9.0 och xUnit för enhetstester. Testfallen omfattar både positiva och negativa scenarier.

---

## Testfall

### Positiva Testfall
- **InsertCard_ShouldAddCardInsertedMessage**  
  *Scenario:* Sätt in ett kort.  
  *Förväntat resultat:* Meddelandet "Card inserted" genereras.

- **EnterPin_CorrectPin_ShouldReturnTrueAndMessage**  
  *Scenario:* Ange korrekt pinkod ("0123").  
  *Förväntat resultat:* Returnerar `true` med meddelandet "Correct pin".

- **Withdraw_ValidAmount_ShouldReturnAmountAndMessage**  
  *Scenario:* Utför ett giltigt uttag (t.ex. 2000 kr).  
  *Förväntat resultat:* Uttaget genomförs, och meddelandet "Withdrawing 2000" genereras.

- **Test_GetMachineBalance**  
  *Scenario:* Hämta bankomatens aktuella saldo.  
  *Förväntat resultat:* Bankomatens saldo returneras, t.ex. 11000 kr.

- **TheoryTest_GetMachineBalance_AddToMachineBalance**  
  *Scenario:* Lägg till olika positiva belopp i bankomatens saldo och kontrollera att det korrigeras korrekt.  
  *Förväntat resultat:* Det nya saldot motsvarar det förväntade värdet (t.ex. 11000 + 2500 = 13500).

- **ScenarioTest_FullFlow_Theory**  
  *Scenario:* Implementerar kompletta flöden med operationssekvenser (insättning, felaktig PIN, korrekt PIN, uttag, utmatning, etc.) i flera varianter, med [Theory]-tester.  
  *Förväntat resultat:* Alla steg returnerar rätt meddelanden beroende på vilket flöde som körs.

### Negativa Testfall
- **EnterPin_IncorrectPin_ShouldReturnFalseAndMessage**  
  *Scenario:* Ange en felaktig pinkod (t.ex. "1234").  
  *Förväntat resultat:* Returnerar `false` med meddelandet "Incorrect pin".

- **Withdraw_MachineInsufficientFunds_ShouldReturnZeroAndMessage**  
  *Scenario:* Försök att ta ut ett belopp som överstiger bankomatens saldo.  
  *Förväntat resultat:* Uttaget nekas med meddelandet "Machine has insufficient funds".

- **Withdraw_CardInsufficientFunds_ShouldReturnZeroAndMessage**  
  *Scenario:* Försök att ta ut mer än kontots saldo (t.ex. 6000 kr när saldo är 5000).  
  *Förväntat resultat:* Uttaget nekas med meddelandet "Card has insufficient funds".

- **Withdraw_WithoutEnteringPin_ShouldFail**  
  *Scenario:* Försök att ta ut pengar utan att ange en giltig PIN.  
  *Förväntat resultat:* Uttaget nekas med meddelandet "You must enter a valid pin first".

- **EnterPin_ThreeIncorrectAttempts_ShouldLockCard**  
  *Scenario:* Ange felaktiga PIN (t.ex. "0000", "1111", "2222") tre gånger.  
  *Förväntat resultat:* Kortet spärras och meddelandet "Card locked due to too many incorrect pin attempts" returneras, följt av att ytterligare försök nekas med "Card is locked".

- **ScenarioTest_FullFlow**  
  *Scenario:* Ett komplett flöde med blandade operationer (insättning, PIN-inmatning, uttag, utmatning) där vissa operationer skall misslyckas (t.ex. uttag som överskrider saldo).  
  *Förväntat resultat:* Stegvisa meddelanden bekräftar att systemet hanterar både lyckade och misslyckade operationer korrekt.

---

## Utökningar Implementerade i Bankomat.cs

För att uppfylla kraven var det nödvändigt att utöka den ursprungliga Bankomat-klassen med följande ändringar:

1. **Säkerhets- och valideringsfunktioner:**
   - **Fält tillagda:**  
     - `incorrectPinAttempts`: Räknar antalet felaktiga PIN‑försök.  
     - `isCardLocked`: Anger om kortet är spärrat efter 3 felaktiga försök.  
     - `pinEntered`: Indikerar att en giltig PIN har angetts.
   
   - **Ändringar i `enterPin`:**  
     - Kontrollerar inmatad PIN. Vid felaktiga försök ökas räknaren, och när tre fel har gjorts spärras kortet. Detta uppfyller kraven för säkerhetskritisk funktionalitet och hantering av felaktiga PIN‑försök.

2. **Hantering av uttag:**
   - **Ändringar i `withdraw`:**  
     - Uttag kontrolleras först så att en giltig PIN har angetts (`pinEntered == true`). Detta förhindrar att uttag sker utan en giltig autentisering.  
     - Om beloppet överskrider bankomatens eller kontots saldo returneras 0 med specifika felmeddelanden ("Machine has insufficient funds" eller "Card has insufficient funds").

3. **Metoder för saldohantering:**
   - **Ny metod `GetMachineBalance`:**  
     - Returnerar det aktuella kontanta saldot i bankomaten (används för att verifiera systemets tillstånd genom testerna).
   
   - **Ny metod `AddToMachineBalance`:**  
     - Tillåter att lägga till pengar i bankomatens kassa. Metoden validerar att endast positiva belopp läggs till, vilket gör det möjligt att testa saldoändringar via [Theory]-tester.

Dessa utökningar var nödvändiga för att:
- Validera säkerhetskraven (spärring av kort och förhindrande av uttag utan PIN).
- Kontrollera att både kontots och bankomatens saldon uppdateras korrekt.
- Skriva enhetstester för både positiva och negativa scenarier med [Fact] och [Theory]-tester.

---

## Eventuella Vidare Utökningar

För att ytterligare förbättra bankomaten skulle man kunna implementera:
- **Insättning:** Implementera en funktion för att sätta in pengar på kontot via bankomaten samt tester för att validera att saldot ökar korrekt.
- **Byta PIN:** Lägga till funktionalitet för att byta kortets PIN-kod med relevanta tester för att validera den nya PIN-koden.
- **Kvittohantering:** Generera detaljerade kvitton med information om transaktioner (datum, tid, belopp), vilket även kan testas.
- **Logga ut:** Implementera en loggut-funktion som avslutar sessionen utan att behöva mata ut kortet, och säkerställa att en ny inloggning krävs för vidare transaktioner.
- **Flerkontosystem:** Lägga till stöd för att hantera flera konton kopplade till ett kort.

---

## Teknisk Miljö

- **Ramverk:** .NET 9.0  
- **Testverktyg:** xUnit  
- **CI/CD & Byggmiljö:** GitHub Actions (workflow med .NET 9 SDK)  
- **Utvecklingsmiljö:** Visual Studio / Visual Studio Code

---

## Instruktioner för körning av tester

För att köra alla enhetstester, använd följande kommando i terminalen:

bash
- Det vanliga kommandot är att köra alla tester:

dotnet test


- Om du däremot vill köra tester som tillhör en enskild testklass (som motsvarar en fil) kan du använda filtreringsalternativet. Exempelvis:

dotnet test --filter FullyQualifiedName~BankomatTest




---

OBS! Jag har också kommenterat koden, det känns som att jag lär mig mer när jag gör det och för sennare referenser blir det lättare ocskå.