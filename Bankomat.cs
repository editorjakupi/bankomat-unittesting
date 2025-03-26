namespace banko;
using System.Collections.Generic;


// Definierar den publika klassen Bankomat som innehåller bankomatens logik.
public class Bankomat
{
    // Deklarerar en boolesk flagga som anger om ett kort är insatt.
    bool cardInserted = false;
    // Deklarerar en variabel för att lagra det insatta kortet.
    Card card;
    // Deklarerar en variabel för ett belopp (används ej i denna version).
    int amount;
    // Deklarerar bankomatens pengar – startbalansen är 11000.
    int machineBalance = 11000;
    // Skapar en lista för att lagra meddelanden som genereras av operationerna.
    List<string> msgs = new List<string>();

    // Deklarerar en räknare för antalet felaktiga PIN-försök.
    int incorrectPinAttempts = 0;
    // Deklarerar en flagga som anger om kortet har spärrats efter för många felaktiga försök.
    bool isCardLocked = false;
    // Deklarerar en flagga som anger om en giltig PIN har angetts.
    bool pinEntered = false;

    // ----------------------------------------------------------------------------------------
    // Metod: getMessage()
    // Syfte: Returnerar nästa meddelande från meddelandekön (används i tester för att verifiera effekter).
    // ----------------------------------------------------------------------------------------
    public string getMessage()
    {
        // Skapar en variabel för att hålla meddelandet.
        var msg = "";
        // Om det finns ett eller flera meddelanden i kön:
        if (msgs.Count > 0)
        {
            // Tilldelar det första meddelandet till variabeln.
            msg = msgs[0];
            // Tar bort det första meddelandet från listan.
            msgs.RemoveAt(0);
        }
        // Returnerar meddelandet.
        return msg;
    }

    // ----------------------------------------------------------------------------------------
    // Metod: insertCard(Card)
    // Syfte: Tar emot och registrerar ett insatt kort samt återställer säkerhetsstatus.
    // Instruktion: "Sätt in ett kort i bankomaten. (Bankomaten ska veta att ett kort är inne)"
    // ----------------------------------------------------------------------------------------
    public void insertCard(Card card)
    {
        // Sätter flaggan att ett kort är insatt.
        cardInserted = true;
        // Associerar det insatta kortet med bankomaten.
        this.card = card;
        // Återställer flaggan att ingen giltig PIN har angetts än.
        pinEntered = false;
        // Återställer antalet felaktiga PIN-försök.
        incorrectPinAttempts = 0;
        // Återställer spärrstatusen.
        isCardLocked = false;
        // Lägger till meddelandet "Card inserted" i kön.
        msgs.Add("Card inserted");
    }

    // ----------------------------------------------------------------------------------------
    // Metod: ejectCard()
    // Syfte: Tar ut kortet och återställer bankomatens säkerhetsstatus.
    // Instruktion: "Mata ut kortet ur bankomaten."
    // ----------------------------------------------------------------------------------------
    public void ejectCard()
    {
        // Sätter flaggan att inget kort är insatt.
        cardInserted = false;
        // Återställer flaggan för giltig PIN.
        pinEntered = false;
        // Återställer antalet felaktiga PIN-försök.
        incorrectPinAttempts = 0;
        // Återställer spärrstatusen.
        isCardLocked = false;
        // Lägger till meddelandet "Card removed, don't forget it!" i kön.
        msgs.Add("Card removed, don't forget it!");
    }

    // ----------------------------------------------------------------------------------------
    // Metod: enterPin(string)
    // Syfte: Validerar att den inmatade PIN-koden är korrekt.
    // Om en felaktig PIN anges ökas räknaren; vid ≥3 fel sätts kortet i spärr.
    // Instruktioner:
    // "Mata in den felaktiga pinkoden 1234 i bankomaten."
    // "Mata in den korrekta pinkoden 0123."
    // "Fel pinkod tillåts ett visst antal gånger (t.ex. 3 försök → spärr)."
    // ----------------------------------------------------------------------------------------
    public bool enterPin(string pin)
    {
        // Kontrollerar om inget kort är insatt eller om card är null.
        if (!cardInserted || card == null)
        {
            // Lägger till ett meddelande om att inget kort finns insatt.
            msgs.Add("No card inserted");
            // Returnerar false då operationen misslyckas.
            return false;
        }
        // Kontrollerar om kortet redan är spärrat.
        if (isCardLocked)
        {
            // Lägger till meddelandet att kortet är spärrat.
            msgs.Add("Card is locked");
            // Returnerar false.
            return false;
        }
        // Om den inmatade PIN-koden stämmer överens med kortets PIN.
        if (card.pin == pin)
        {
            // Sätter flaggan att en giltig PIN har angetts.
            pinEntered = true;
            // Återställer antalet felaktiga försök.
            incorrectPinAttempts = 0;
            // Lägger till meddelandet "Correct pin".
            msgs.Add("Correct pin");
            // Returnerar true.
            return true;
        }
        else
        {
            // Ökar räknaren för felaktiga PIN-försök.
            incorrectPinAttempts++;
            // Om felaktiga försök nått tre eller fler.
            if (incorrectPinAttempts >= 3)
            {
                // Sätter kortet i spärr.
                isCardLocked = true;
                // Lägger till meddelandet om att kortet är spärrat.
                msgs.Add("Card locked due to too many incorrect pin attempts");
            }
            else
            {
                // Om antalet felaktiga försök är under tre, läggs ett "Incorrect pin"-meddelande till.
                msgs.Add("Incorrect pin");
            }
            // Returnerar false.
            return false;
        }
    }

    // ----------------------------------------------------------------------------------------
    // Metod: withdraw(int)
    // Syfte: Tillåter uttag av pengar om ett giltigt PIN är angivet, samt om beloppet är korrekt.
    // Instruktioner:
    // "Ange 5000 kr att ta ut via bankomaten." 
    // "Ange 7000 att ta ut. (Maskinen saknar medel)"
    // "Ange 6000 att ta ut. (Konto saknar medel)"
    // "Uttag utan att ange pinkod → Nekas"
    // ----------------------------------------------------------------------------------------
    public int withdraw(int amount)
    {
        // Kontrollerar att en giltig PIN har angetts.
        if (!pinEntered)
        {
            // Lägger till meddelandet att PIN måste anges.
            msgs.Add("You must enter a valid pin first");
            // Returnerar 0 eftersom uttaget ej är tillåtet.
            return 0;
        }
        // Kontrollerar att beloppet är positivt, och att både maskinen och kontot har tillräckliga medel.
        if (amount > 0 && amount <= machineBalance && amount <= card.account.getBalance())
        {
            // Minskar maskinens saldo med det uttagna beloppet.
            machineBalance -= amount;
            // Drar bort beloppet från kontots saldo.
            card.account.withdraw(amount);
            // Lägger till ett meddelande som beskriver uttaget.
            msgs.Add("Withdrawing " + amount);
            // Returnerar det uttagna beloppet.
            return amount;
        }
        else
        {
            // Om uttagsbeloppet överstiger maskinens saldo:
            if (amount > machineBalance)
            {
                // Lägger till meddelandet om otillräckliga medel i maskinen.
                msgs.Add("Machine has insufficient funds");
            }
            // Om uttagsbeloppet överstiger kontots saldo:
            else if (amount > card.account.getBalance())
            {
                // Lägger till meddelandet om otillräckliga medel på kortet.
                msgs.Add("Card has insufficient funds");
            }
            else
            {
                // För ogiltiga belopp (0 eller negativt) läggs ett meddelande till.
                msgs.Add("You can not withdraw 0 or less money");
            }
            // Returnerar 0, eftersom uttaget ej kunde genomföras.
            return 0;
        }
    }

    // ----------------------------------------------------------------------------------------
    // Ny metod: GetMachineBalance()
    // Syfte: Returnerar bankomatens aktuella kontanta saldo (används i tester).
    // Exempel: TestGetMachineBalance()
    // ----------------------------------------------------------------------------------------
    public int GetMachineBalance()
    {
        // Returnerar det interna värdet för machineBalance.
        return machineBalance;
    }

    // ----------------------------------------------------------------------------------------
    // Ny metod: AddToMachineBalance(int)
    // Syfte: Lägger till pengar till bankomatens saldo.
    // Exempel: TheoryTestGetMachineBalance
    // ----------------------------------------------------------------------------------------
    public void AddToMachineBalance(int amount)
    {
        // Om beloppet är positivt:
        if (amount > 0)
        {
            // Ökar machineBalance med det angivna beloppet.
            machineBalance += amount;
        }
    }
}
