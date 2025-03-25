using Xunit;
using banko;

namespace BankomatUnitTests
{
    // ====================================================
    // Testklass för Card
    // ====================================================
    public class CardTest
    {
        // ----------------------------------------------------
        // Test: Bekräftar att ett Card lagrar en referens till Account
        // samt att standard PIN-koden är "0123".
        // ----------------------------------------------------
        [Fact]
        public void Card_ShouldStoreAccountAndDefaultPin()
        {
            // Arrange: Skapar ett nytt Account-objekt.
            Account account = new Account();
            // Act: Skapar ett Card-objekt med satt koppling till Account.
            Card card = new Card(account);

            // Assert: Kontrollera att kortet har en giltig kontoreferens.
            Assert.NotNull(card.account);
            // Assert: Kontrollera att PIN-koden är satt till standardvärdet "0123".
            Assert.Equal("0123", card.pin);
        }
    }
}
