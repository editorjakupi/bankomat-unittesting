using Xunit;                              
using banko;                              

namespace BankomatUnitTests             
{
    // ====================================================
    // Testklass för Account
    // ====================================================
    public class AccountTest
    {
        // ----------------------------------------------------
        // Test: Withdraw() med ett giltigt belopp
        // Detta test säkerställer att ett korrekt uttag drar bort rätt belopp från saldot.
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_ValidAmount_ShouldDeductBalance()
        {
            // Arrange: Skapar ett Account-objekt.
            Account account = new Account();
            // Hämta initialbalans (ska vara 5000) för senare jämförelse.
            int initialBalance = account.getBalance();
            // Definierar ett giltigt uttagsbelopp.
            int amountToWithdraw = 2000;

            // Act: Utför uttaget.
            int withdrawn = account.withdraw(amountToWithdraw);
            // Hämta det nya saldot efter uttag.
            int remainingBalance = account.getBalance();

            // Assert: Kontrollera att det uttagna beloppet är korrekt.
            Assert.Equal(amountToWithdraw, withdrawn);
            // Assert: Kontrollera att saldot minskat korrekt.
            Assert.Equal(initialBalance - amountToWithdraw, remainingBalance);
        }

        // ----------------------------------------------------
        // Test: Withdraw() med ett belopp över saldot
        // Detta test säkerställer att inga medel tas ut om man försöker ta ut mer än vad som finns.
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_ExcessAmount_ShouldReturnZeroAndNotChangeBalance()
        {
            // Arrange: Skapar ett nytt Account-objekt.
            Account account = new Account();
            // Försöker ta ut ett belopp som överstiger det initiala saldot (6000 > 5000).
            int amountToWithdraw = 6000;

            // Act: Försöker genomföra uttaget.
            int withdrawn = account.withdraw(amountToWithdraw);
            // Hämta saldot efter försöket.
            int balanceAfter = account.getBalance();

            // Assert: Förväntar att inget belopp tas ut (returnerar 0).
            Assert.Equal(0, withdrawn);
            // Assert: Saldo ska förbli 5000.
            Assert.Equal(5000, balanceAfter);
        }

        // ----------------------------------------------------
        // Test: Withdraw() med ett negativt belopp
        // Bekräftar att uttag med negativa värden inte påverkar kontot.
        // ----------------------------------------------------
        [Fact]
        public void Withdraw_NegativeAmount_ShouldReturnZeroAndNotChangeBalance()
        {
            // Arrange: Skapar ett nytt Account-objekt.
            Account account = new Account();

            // Act: Försöker att ta ut ett negativt belopp (-100).
            int withdrawn = account.withdraw(-100);
            // Hämta saldot efter uttagsförsöket.
            int balanceAfter = account.getBalance();

            // Assert: Uttaget ska returnera 0.
            Assert.Equal(0, withdrawn);
            // Assert: Saldo ska förbli oförändrat (5000).
            Assert.Equal(5000, balanceAfter);
        }
    }
}
