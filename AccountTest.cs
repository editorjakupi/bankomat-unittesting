using Xunit;
using banko;

// Definierar namespace för tester av Account.
namespace BankomatUnitTests
{
    // ====================================================
    // Testklass för Account.
    // Syfte: Kontrollera att uttagslogiken i Account fungerar korrekt.
    // ====================================================
    public class AccountTest
    {
        // ----------------------------------------------------
        // Test: Withdraw() med ett giltigt belopp.
        // Instruktion: "Testa ett korrekt uttag, saldo minskar med rätt belopp."
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_ValidAmount_ShouldDeductBalance()
        {
            // Arrange: Skapa en Account-instans.
            Account account = new Account();
            // Arrange: Hämta initialbalansen, förväntas vara 5000.
            int initialBalance = account.getBalance();
            // Arrange: Definiera ett giltigt uttagsbelopp (2000).
            int amountToWithdraw = 2000;
            // Act: Utför uttaget.
            int withdrawn = account.withdraw(amountToWithdraw);
            // Act: Hämta det nya saldot.
            int remainingBalance = account.getBalance();
            // Assert: Det uttagna beloppet skall vara 2000.
            Assert.Equal(amountToWithdraw, withdrawn);
            // Assert: Det nya saldot skall vara initialbalans minus uttaget.
            Assert.Equal(initialBalance - amountToWithdraw, remainingBalance);
        }

        // ----------------------------------------------------
        // Test: Withdraw() med ett belopp som överstiger saldot.
        // Instruktion: "Uttag över saldo skall misslyckas och lämna saldot oförändrat."
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_ExcessAmount_ShouldReturnZeroAndNotChangeBalance()
        {
            // Arrange: Skapa ett nytt Account.
            Account account = new Account();
            // Arrange: Definiera ett uttagsbelopp som överstiger saldot (6000).
            int amountToWithdraw = 6000;
            // Act: Försök att ta ut beloppet.
            int withdrawn = account.withdraw(amountToWithdraw);
            // Act: Hämta saldot efter uttagsförsöket.
            int balanceAfter = account.getBalance();
            // Assert: Uttaget skall returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Saldo skall förbli oförändrat (5000).
            Assert.Equal(5000, balanceAfter);
        }

        // ----------------------------------------------------
        // Test: Withdraw() med ett negativt belopp.
        // Instruktion: "Negativa uttag skall ej genomföras och saldo skall förbli detsamma."
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_NegativeAmount_ShouldReturnZeroAndNotChangeBalance()
        {
            // Arrange: Skapa en Account-instans.
            Account account = new Account();
            // Act: Försök att ta ut ett negativt belopp (-100).
            int withdrawn = account.withdraw(-100);
            // Act: Hämta saldot efter uttagsförsöket.
            int balanceAfter = account.getBalance();
            // Assert: Uttaget skall returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Saldo skall fortfarande vara 5000.
            Assert.Equal(5000, balanceAfter);
        }
    }
}
