namespace banko;

// Definierar den publika klassen Card som representerar ett bankkort.
public class Card
{
    // Deklarerar ett fält för PIN-koden med standardvärde "0123".
    public string pin = "0123";
    // Deklarerar en referens till ett Account-objekt.
    public Account account;

    // ----------------------------------------------------------------------------------------
    // Konstruktor: Card(Account)
    // Syfte: Skapar ett nytt Card och kopplar det till ett givet konto.
    // ----------------------------------------------------------------------------------------
    public Card(Account account)
    {
        // Tilldelar det angivna kontot till fältet account.
        this.account = account;
    }
}
