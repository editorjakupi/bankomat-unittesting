using Xunit;                              
using banko;                              

namespace BankomatUnitTests             
{
    // ====================================================
    // Testklass för Bankomat
    // ====================================================
    public class BankomatTest
    {
        // ----------------------------------------------------
        // Test: Verifierar att metoden insertCard()
        // genererar meddelandet "Card inserted" och registrerar kortet.
        // Instruktion: "Sätt in ett kort i bankomaten. (Bankomaten ska veta att ett kort är inne)"
        // ----------------------------------------------------
        [Fact]
        public void InsertCard_ShouldAddCardInsertedMessage()
        {
            // Arrange: Skapar en Bankomat-instans.
            Bankomat bankomat = new Bankomat();
            // Arrange: Skapar ett konto då ett kort behöver vara kopplat till ett konto.
            Account account = new Account();
            // Arrange: Skapar ett Card-objekt med kopplat konto.
            Card card = new Card(account);

            // Act: Sätter in kortet i bankomaten.
            bankomat.insertCard(card);
            // Hämta meddelandet som genereras efter insättning.
            string msg = bankomat.getMessage();

            // Assert: Kontrollerar att meddelandet är "Card inserted".
            Assert.Equal("Card inserted", msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar metoden ejectCard()
        // som ska generera meddelandet "Card removed, don't forget it!".
        // Instruktion: "Mata ut kortet ur bankomaten."
        // ----------------------------------------------------
        [Fact]
        public void EjectCard_ShouldAddCardRemovedMessage()
        {
            // Arrange: Skapar bankomat, konto och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);
            // Först måste kortet sättas in i bankomaten.
            bankomat.insertCard(card);
            // Rensa ut det första meddelandet ("Card inserted").
            bankomat.getMessage();

            // Act: Matar ut kortet.
            bankomat.ejectCard();
            // Hämta meddelandet som genereras vid utmatning.
            string msg = bankomat.getMessage();

            // Assert: Meddelandet ska vara "Card removed, don't forget it!".
            Assert.Equal("Card removed, don't forget it!", msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar metoden enterPin() med korrekt PIN ("0123").
        // Instruktion: "Mata in den korrekta pinkoden 0123."
        // ----------------------------------------------------
        [Fact]
        public void EnterPin_CorrectPin_ShouldReturnTrueAndMessage()
        {
            // Arrange: Skapa bankomat, konto och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);
            // Sätt in kortet.
            bankomat.insertCard(card);
            // Rensa ut meddelandet "Card inserted".
            bankomat.getMessage();

            // Act: Mata in den korrekta pinkoden.
            bool result = bankomat.enterPin("0123");
            // Hämta meddelandet genererat vid korrekt pin.
            string msg = bankomat.getMessage();

            // Assert: Metoden ska returnera true.
            Assert.True(result);
            // Assert: Meddelandet ska vara "Correct pin".
            Assert.Equal("Correct pin", msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar metoden enterPin() med felaktigt PIN ("1234").
        // Instruktion: "Mata in den felaktiga pinkoden 1234 i bankomaten."
        // ----------------------------------------------------
        [Fact]
        public void EnterPin_IncorrectPin_ShouldReturnFalseAndMessage()
        {
            // Arrange: Skapa bankomat, konto och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);
            // Sätt in kortet.
            bankomat.insertCard(card);
            // Rensa meddelandet "Card inserted".
            bankomat.getMessage();

            // Act: Försök mata in den felaktiga pinkoden.
            bool result = bankomat.enterPin("1234");
            // Hämta meddelandet från felinmatningen.
            string msg = bankomat.getMessage();

            // Assert: Resultatet ska vara false.
            Assert.False(result);
            // Assert: Meddelandet ska vara "Incorrect pin".
            Assert.Equal("Incorrect pin", msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar withdraw() med ett giltigt uttagsbelopp.
        // Instruktion: "Ange 5000 kr att ta ut via bankomaten. Balansen ska tas från kontot"
        // (här testas med ett lägre belopp, 2000, men principen är densamma).
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_ValidAmount_ShouldReturnAmountAndMessage()
        {
            // Arrange: Skapa bankomat, konto och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);
            // Sätt in kortet.
            bankomat.insertCard(card);
            bankomat.getMessage(); // Rensar "Card inserted".
            // Mata in rätt pinkod.
            bankomat.enterPin("0123");
            bankomat.getMessage(); // Rensar "Correct pin".

            // Definiera ett giltigt uttagsbelopp.
            int amountToWithdraw = 2000;

            // Act: Försök ta ut beloppet.
            int withdrawn = bankomat.withdraw(amountToWithdraw);
            // Hämta meddelandet efter uttaget.
            string msg = bankomat.getMessage();

            // Assert: Det uttagna beloppet ska vara lika med det begärda.
            Assert.Equal(amountToWithdraw, withdrawn);
            // Assert: Meddelandet ska vara "Withdrawing [belopp]".
            Assert.Equal("Withdrawing " + amountToWithdraw, msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar withdraw() när bankomaten saknar tillräckliga medel.
        // Instruktion: "Ange 7000 att ta ut. (Nu ska det inte finnas pengar så det räcker på bankomaten)"
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_MachineInsufficientFunds_ShouldReturnZeroAndMessage()
        {
            // Arrange: Skapa bankomat (startar med 11000) samt konto (med 5000) och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);
            // Sätt in kortet.
            bankomat.insertCard(card);
            bankomat.getMessage();  // Rensar "Card inserted".
            // Mata in korrekt pinkod.
            bankomat.enterPin("0123");
            bankomat.getMessage();  // Rensar "Correct pin".

            // Act: Försök ta ut ett belopp större än bankomatens medel (t.ex. 12000).
            int withdrawn = bankomat.withdraw(12000);
            // Hämta meddelandet som indikerar att uttaget misslyckats.
            string msg = bankomat.getMessage();

            // Assert: Uttaget ska misslyckas (returnera 0).
            Assert.Equal(0, withdrawn);
            // Assert: Meddelandet ska vara "Machine has insufficient funds".
            Assert.Equal("Machine has insufficient funds", msg);
        }

        // ----------------------------------------------------
        // Test: Verifierar withdraw() när kontot (kortet) saknar medel.
        // Instruktion: "Ange 6000 att ta ut. (Nu räckte bankomatens pengar precis, men inte pengarna på kontot)"
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_CardInsufficientFunds_ShouldReturnZeroAndMessage()
        {
            // Arrange: Skapa bankomat och ett konto med standard saldo (5000) samt kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account(); // Saldo: 5000 kr.
            Card card = new Card(account);
            // Sätt in kortet.
            bankomat.insertCard(card);
            bankomat.getMessage();  // Rensar "Card inserted".
            // Mata in korrekt pinkod.
            bankomat.enterPin("0123");
            bankomat.getMessage();  // Rensar "Correct pin".

            // Act: Försök ta ut 6000, vilket överstiger kontots saldo.
            int withdrawn = bankomat.withdraw(6000);
            // Hämta meddelandet som genereras.
            string msg = bankomat.getMessage();

            // Assert: Uttaget ska returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Meddelandet ska vara "Card has insufficient funds".
            Assert.Equal("Card has insufficient funds", msg);
        }

        // ----------------------------------------------------
        // Scenario-test: Fullständigt flöde enligt instruktionerna
        // Instruktion: 
        // "Sätt in ett kort i bankomaten.
        //  Mata in den felaktiga pinkoden 1234 i bankomaten.
        //  Mata in den korrekta pinkoden 0123.
        //  Ange 5000 kr att ta ut via bankomaten.
        //  Mata ut kortet ur bankomaten.
        //  Sätt in samma kort i samma bankomat igen.
        //  Mata in pinkoden 0123.
        //  Ange 7000 att ta ut.
        //  Ange 6000 att ta ut."
        // ----------------------------------------------------
        [Fact]
        public void ScenarioTest_FullFlow()
        {
            // Arrange: Skapa bankomat, konto (saldo 5000) och kort.
            Bankomat bankomat = new Bankomat();
            Account account = new Account();
            Card card = new Card(account);

            // 1. Sätt in kortet: förväntar "Card inserted"
            bankomat.insertCard(card);
            string msg1 = bankomat.getMessage();
            Assert.Equal("Card inserted", msg1);  // Del: Kort in.

            // 2. Mata in felaktig pinkod "1234": förväntar "Incorrect pin"
            bool pinResultWrong = bankomat.enterPin("1234");
            string msg2 = bankomat.getMessage();
            Assert.False(pinResultWrong);
            Assert.Equal("Incorrect pin", msg2);  // Del: Felaktig pinkod.

            // 3. Mata in korrekt pinkod "0123": förväntar "Correct pin"
            bool pinResultCorrect = bankomat.enterPin("0123");
            string msg3 = bankomat.getMessage();
            Assert.True(pinResultCorrect);
            Assert.Equal("Correct pin", msg3);   // Del: Korrekt pinkod.

            // 4. Ange 5000 kr att ta ut: balanserna ska uppdateras korrekt.
            int withdrawn1 = bankomat.withdraw(5000);
            string msg4 = bankomat.getMessage();
            Assert.Equal(5000, withdrawn1);
            Assert.Equal("Withdrawing 5000", msg4); // Del: Uttag av 5000 kr.
            // Efter uttaget: Konto = 0 kr, Bankomatens saldo = 11000 – 5000 = 6000 kr.

            // 5. Mata ut kortet: förväntar "Card removed, don't forget it!"
            bankomat.ejectCard();
            string msg5 = bankomat.getMessage();
            Assert.Equal("Card removed, don't forget it!", msg5);  // Del: Kort ut.

            // 6. Sätt in samma kort igen: förväntar "Card inserted"
            bankomat.insertCard(card);
            string msg6 = bankomat.getMessage();
            Assert.Equal("Card inserted", msg6);   // Del: Kort in igen.

            // 7. Mata in pinkoden "0123": förväntar "Correct pin"
            bool pinResultSecond = bankomat.enterPin("0123");
            string msg7 = bankomat.getMessage();
            Assert.True(pinResultSecond);
            Assert.Equal("Correct pin", msg7);   // Del: Rätt pinkod.

            // 8. Ange 7000 att ta ut: bankomaten har ej tillräckliga medel.
            int withdrawn2 = bankomat.withdraw(7000);
            string msg8 = bankomat.getMessage();
            Assert.Equal(0, withdrawn2);
            Assert.Equal("Machine has insufficient funds", msg8); // Del: Bankomat saknar medel.

            // 9. Ange 6000 att ta ut: bankomaten har 6000, men kontot (kortet) saknar medel.
            int withdrawn3 = bankomat.withdraw(6000);
            string msg9 = bankomat.getMessage();
            Assert.Equal(0, withdrawn3);
            Assert.Equal("Card has insufficient funds", msg9);   // Del: Konto saknar medel.
        }
    }
}
