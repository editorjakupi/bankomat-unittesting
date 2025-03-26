using Xunit;
using banko;

// Definierar namespace för tester av Card.
namespace BankomatUnitTests
{
    // ====================================================
    // Testklass för Card.
    // Syfte: Verifiera att Card korrekt lagrar en referens till Account och har rätt standard PIN.
    // ====================================================
    public class CardTest
    {
        // ----------------------------------------------------
        // Test: Bekräftar att ett Card lagrar en giltig referens till Account och standard PIN "0123".
        // Instruktion: "Skriv enhetstest för Card."
        // ----------------------------------------------------
        [Fact]
        public void Card_ShouldStoreAccountAndDefaultPin()
        {
            // Arrange: Skapa en Account-instans.
            Account account = new Account();
            // Act: Skapa ett nytt Card med koppling till Account.
            Card card = new Card(account);
            // Assert: Kontrollera att card.account inte är null.
            Assert.NotNull(card.account);
            // Assert: Kontrollera att standard PIN-koden är "0123".
            Assert.Equal("0123", card.pin);
        }
    }
}
