using Xunit;
using banko;
using System.Collections.Generic;

namespace BankomatUnitTests
{
    // ====================================================
    // Testklass för Bankomat
    // Syfte: Testa bankomatens funktioner: autentisering, uttag, säkerhetsåtgärder,
    // samt att kontrollerna fungerar enligt kravspecifikationen.
    // ====================================================
    public class BankomatTest
    {
        // -----------------------------------------------
        // Test: Verifierar att metoden insertCard() genererar meddelandet "Card inserted".
        // Instruktion: "Sätt in ett kort i bankomaten. (Bankomaten ska veta att ett kort är inne)"
        // -----------------------------------------------
        [Fact]
        public void InsertCard_ShouldAddCardInsertedMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card med koppling till Account.
            Card card = new Card(account);
            // Act: Sätt in kortet i bankomaten.
            bankomat.insertCard(card);
            // Act: Hämta nästa genererade meddelande.
            string msg = bankomat.getMessage();
            // Assert: Kontrollera att meddelandet är "Card inserted".
            Assert.Equal("Card inserted", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att ejectCard() genererar meddelandet "Card removed, don't forget it!".
        // Instruktion: "Mata ut kortet ur bankomaten."
        // -----------------------------------------------
        [Fact]
        public void EjectCard_ShouldAddCardRemovedMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card med koppling till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Act: Mata ut kortet.
            bankomat.ejectCard();
            // Act: Hämta det genererade meddelandet.
            string msg = bankomat.getMessage();
            // Assert: Kontrollera att meddelandet är "Card removed, don't forget it!".
            Assert.Equal("Card removed, don't forget it!", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att en korrekt PIN ("0123") accepteras.
        // Instruktion: "Mata in den korrekta pinkoden 0123."
        // -----------------------------------------------
        [Fact]
        public void EnterPin_CorrectPin_ShouldReturnTrueAndMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card och koppla till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Act: Mata in korrekt PIN ("0123").
            bool result = bankomat.enterPin("0123");
            // Act: Hämta det genererade meddelandet.
            string msg = bankomat.getMessage();
            // Assert: Kontrollera att metoden returnerar true.
            Assert.True(result);
            // Assert: Kontrollera att meddelandet är "Correct pin".
            Assert.Equal("Correct pin", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att en felaktig PIN ("1234") inte accepteras.
        // Instruktion: "Mata in den felaktiga pinkoden 1234 i bankomaten."
        // -----------------------------------------------
        [Fact]
        public void EnterPin_IncorrectPin_ShouldReturnFalseAndMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card med koppling till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Act: Mata in felaktig PIN ("1234").
            bool result = bankomat.enterPin("1234");
            // Act: Hämta det genererade meddelandet.
            string msg = bankomat.getMessage();
            // Assert: Kontrollera att metoden returnerar false.
            Assert.False(result);
            // Assert: Kontrollera att meddelandet är "Incorrect pin".
            Assert.Equal("Incorrect pin", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att ett giltigt uttag (t.ex. 2000) genomförs korrekt.
        // Instruktion: "Ange 5000 kr att ta ut via bankomaten" (här testas med 2000 men principen är densamma).
        // -----------------------------------------------
        [Fact]
        public void Withdraw_ValidAmount_ShouldReturnAmountAndMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card med koppling till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Arrange: Mata in korrekt PIN ("0123").
            bankomat.enterPin("0123");
            // Arrange: Rensa meddelandet "Correct pin".
            bankomat.getMessage();
            // Arrange: Definiera uttagsbeloppet, t.ex. 2000.
            int amountToWithdraw = 2000;
            // Act: Försök att ta ut det specificerade beloppet.
            int withdrawn = bankomat.withdraw(amountToWithdraw);
            // Act: Hämta meddelandet som genererats vid uttaget.
            string msg = bankomat.getMessage();
            // Assert: Verifiera att det uttagna beloppet är 2000.
            Assert.Equal(amountToWithdraw, withdrawn);
            // Assert: Verifiera att meddelandet är "Withdrawing 2000".
            Assert.Equal("Withdrawing " + amountToWithdraw, msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att ett uttag som överstiger bankomatens medel misslyckas.
        // Instruktion: "Ange 7000 att ta ut. (Maskinen saknar medel)"
        // -----------------------------------------------
        [Fact]
        public void Withdraw_MachineInsufficientFunds_ShouldReturnZeroAndMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt med saldo 5000.
            Account account = new Account();
            // Arrange: Skapa ett Card kopplat till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Arrange: Mata in korrekt PIN ("0123").
            bankomat.enterPin("0123");
            // Arrange: Rensa ut PIN-meddelandet.
            bankomat.getMessage();
            // Act: Försök att ta ut ett belopp (t.ex. 12000) som överstiger bankomatens saldo.
            int withdrawn = bankomat.withdraw(12000);
            // Act: Hämta genererat meddelande.
            string msg = bankomat.getMessage();
            // Assert: Uttaget skall returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Meddelandet skall vara "Machine has insufficient funds".
            Assert.Equal("Machine has insufficient funds", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att ett uttag som överstiger kontots saldo misslyckas.
        // Instruktion: "Ange 6000 att ta ut. (Kontot saknar medel)"
        // -----------------------------------------------
        [Fact]
        public void Withdraw_CardInsufficientFunds_ShouldReturnZeroAndMessage()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt med saldo 5000.
            Account account = new Account();
            // Arrange: Skapa ett Card kopplat till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Arrange: Mata in korrekt PIN ("0123").
            bankomat.enterPin("0123");
            // Arrange: Rensa ut meddelandet "Correct pin".
            bankomat.getMessage();
            // Act: Försök att ta ut 6000 (mer än kontots saldo).
            int withdrawn = bankomat.withdraw(6000);
            // Act: Hämta meddelandet.
            string msg = bankomat.getMessage();
            // Assert: Uttaget skall returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Meddelandet skall vara "Card has insufficient funds".
            Assert.Equal("Card has insufficient funds", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att uttag utan att ange PIN misslyckas.
        // Instruktion: "Uttag utan att ange pinkod → Nekas"
        // -----------------------------------------------
        [Fact]
        public void Withdraw_WithoutEnteringPin_ShouldFail()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card kopplat till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Act: Försök att ta ut 1000 utan att ha angett PIN.
            int withdrawn = bankomat.withdraw(1000);
            // Act: Hämta meddelandet.
            string msg = bankomat.getMessage();
            // Assert: Uttaget skall returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Meddelandet skall vara "You must enter a valid pin first".
            Assert.Equal("You must enter a valid pin first", msg);
        }

        // -----------------------------------------------
        // Test: Verifierar att kortet låses efter tre felaktiga PIN-försök.
        // Instruktion: "Fel pinkod tillåts ett visst antal gånger (3 försök → spärr)"
        // -----------------------------------------------
        [Fact]
        public void EnterPin_ThreeIncorrectAttempts_ShouldLockCard()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett Card med koppling till Account.
            Card card = new Card(account);
            // Arrange: Sätt in kortet.
            bankomat.insertCard(card);
            // Arrange: Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();
            // Act: Första felaktiga PIN-försöket med "0000".
            bool attempt1 = bankomat.enterPin("0000");
            // Act: Hämta meddelandet för försök 1.
            string msg1 = bankomat.getMessage();
            // Assert: Försöket skall returnera false.
            Assert.False(attempt1);
            // Assert: Meddelandet skall vara "Incorrect pin".
            Assert.Equal("Incorrect pin", msg1);
            // Act: Andra felaktiga PIN-försöket med "1111".
            bool attempt2 = bankomat.enterPin("1111");
            // Act: Hämta meddelandet för försök 2.
            string msg2 = bankomat.getMessage();
            // Assert: Försöket skall returnera false.
            Assert.False(attempt2);
            // Assert: Meddelandet skall vara "Incorrect pin".
            Assert.Equal("Incorrect pin", msg2);
            // Act: Tredje felaktiga PIN-försöket med "2222" – detta ska låsa kortet.
            bool attempt3 = bankomat.enterPin("2222");
            // Act: Hämta meddelandet för försök 3.
            string msg3 = bankomat.getMessage();
            // Assert: Försöket skall returnera false.
            Assert.False(attempt3);
            // Assert: Meddelandet skall vara "Card locked due to too many incorrect pin attempts".
            Assert.Equal("Card locked due to too many incorrect pin attempts", msg3);
            // Act: Ytterligare försök med korrekt PIN ("0123") skall misslyckas.
            bool attempt4 = bankomat.enterPin("0123");
            // Act: Hämta meddelandet.
            string msg4 = bankomat.getMessage();
            // Assert: Försöket skall returnera false.
            Assert.False(attempt4);
            // Assert: Meddelandet skall vara "Card is locked".
            Assert.Equal("Card is locked", msg4);
        }

        // -----------------------------------------------
        // Ny Theory-test: Fullständiga flöden (scenario) med varierade operationssekvenser.
        // Syfte: Testa flera scenario-flöden med olika operationer, exempelvis:
        //          Scenario 1: Insättning, felaktig PIN, korrekt PIN, uttag 5000, utmatning.
        //          Scenario 2: Insättning, uttag utan PIN.
        //          Scenario 3: Insättning, korrekt PIN, uttag > kontosaldo, utmatning.
        // Instruktion: Använd [Theory] för att mata in flera olika flöden.
        // -----------------------------------------------
        // Definierar MemberData med flera scenario-flöden.
        public static IEnumerable<object[]> FullFlowScenarios =>
            new List<object[]>
            {
                // Scenario 1: Standardflöde med ett uttag som matchar kontots och maskinens saldo.
                new object[] {
                    new string[] { "insert", "enterIncorrect", "enterCorrect", "withdraw:5000", "eject" },
                    new string[] { "Card inserted", "Incorrect pin", "Correct pin", "Withdrawing 5000", "Card removed, don't forget it!" }
                },
                // Scenario 2: Försök att ta ut utan att ange PIN.
                new object[] {
                    new string[] { "insert", "withdraw:1000" },
                    new string[] { "Card inserted", "You must enter a valid pin first" }
                },
                // Scenario 3: Insättning, korrekt PIN, uttag där beloppet överskrider kontosaldo.
                new object[] {
                    new string[] { "insert", "enterCorrect", "withdraw:6000", "eject" },
                    new string[] { "Card inserted", "Correct pin", "Card has insufficient funds", "Card removed, don't forget it!" }
                }
            };

        // Definierar ett Theory-test som använder MemberData med fullständiga scenario-flöden.
        [Theory]
        [MemberData(nameof(FullFlowScenarios))]
        public void ScenarioTest_FullFlow_Theory(string[] operations, string[] expectedMessages)
        {
            // Arrange: Skapa en ny Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapa ett nytt Account-objekt.
            Account account = new Account();
            // Arrange: Skapa ett nytt Card med koppling till Account.
            Card card = new Card(account);
            // Skapa en lista för att samla alla genererade meddelanden.
            List<string> actualMessages = new List<string>();

            // Iterera genom varje operation i det givna flödet.
            foreach (string op in operations)
            {
                // Om operationen är "insert"
                if (op == "insert")
                {
                    // Anropa metoden insertCard med vårt card.
                    bankomat.insertCard(card);
                    // Hämta och lägg till meddelandet i listan.
                    actualMessages.Add(bankomat.getMessage());
                }
                // Om operationen är "enterIncorrect"
                else if (op == "enterIncorrect")
                {
                    // Anropa enterPin med felaktig PIN ("1234").
                    bankomat.enterPin("1234");
                    // Hämta och lägg till meddelandet.
                    actualMessages.Add(bankomat.getMessage());
                }
                // Om operationen är "enterCorrect"
                else if (op == "enterCorrect")
                {
                    // Anropa enterPin med korrekt PIN ("0123").
                    bankomat.enterPin("0123");
                    // Hämta och lägg till meddelandet.
                    actualMessages.Add(bankomat.getMessage());
                }
                // Om operationen börjar med "withdraw:"
                else if (op.StartsWith("withdraw:"))
                {
                    // Extrahera beloppet från strängen.
                    int amount = int.Parse(op.Split(':')[1]);
                    // Anropa withdraw med det extraherade beloppet.
                    bankomat.withdraw(amount);
                    // Hämta och lägg till meddelandet.
                    actualMessages.Add(bankomat.getMessage());
                }
                // Om operationen är "eject"
                else if (op == "eject")
                {
                    // Anropa ejectCard.
                    bankomat.ejectCard();
                    // Hämta och lägg till meddelandet.
                    actualMessages.Add(bankomat.getMessage());
                }
            }

            // Assert: Jämför den insamlade listan med de förväntade meddelandena.
            Assert.Equal(expectedMessages, actualMessages);
        }

        // -----------------------------------------------
        // Test: Kontrollerar bankomatens saldo med GetMachineBalance().
        // Instruktion: "TestGetMachineBalance()"
        // -----------------------------------------------
        [Fact]
        public void Test_GetMachineBalance()
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Act: Hämta bankomatens saldo.
            int balance = bankomat.GetMachineBalance();
            // Assert: Kontrollera att saldot är 11000.
            Assert.Equal(11000, balance);
        }

        // -----------------------------------------------
        // Theory-test: Lägger till ett belopp med AddToMachineBalance() och verifierar det nya saldot.
        // Instruktion: "TheoryTestGetMachineBalance"
        // -----------------------------------------------
        [Theory]
        [InlineData(11000, 5000, 16000)]
        [InlineData(11000, 1000, 12000)]
        [InlineData(11000, 0, 11000)]
        [InlineData(11000, -1000, 11000)]
        [InlineData(11000, 2500, 13500)] // Ytterligare testfall där 2500 adderas.
        public void TheoryTest_GetMachineBalance_AddToMachineBalance(int initialBalance, int amount, int expectedBalance)
        {
            // Arrange: Skapa en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Act: Hämta bankomatens aktuella saldo.
            int currentBalance = bankomat.GetMachineBalance();
            // Assert: Säkerställ att det aktuella saldot är lika med initialBalance (11000).
            Assert.Equal(initialBalance, currentBalance);
            // Act: Lägg till det angivna beloppet med metoden AddToMachineBalance.
            bankomat.AddToMachineBalance(amount);
            // Act: Hämta det nya saldot.
            int actualBalance = bankomat.GetMachineBalance();
            // Assert: Kontrollera att det nya saldot matchar expectedBalance.
            Assert.Equal(expectedBalance, actualBalance);
        }
    }
}
