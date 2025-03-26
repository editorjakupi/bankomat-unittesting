namespace banko;

// Definierar den publika klassen Account som representerar ett bankkonto.
public class Account{
    // Deklarerar ett fält för kontots saldo med initialvärde 5000.
    int balance = 5000;
    
    // ----------------------------------------------------------------------------------------
    // Metod: withdraw(int)
    // Syfte: Drar bort ett belopp från kontots saldo om villkoren är uppfyllda.
    // ----------------------------------------------------------------------------------------
    public int withdraw(int amount){
        // Om beloppet är positivt och kontot har tillräckligt med pengar:
        if(amount > 0 && balance >= amount){
            // Minska saldot med det angivna beloppet.
            balance -= amount;
            // Returnera det uttagna beloppet.
            return amount;
        } else {
            // Om villkoren inte är uppfyllda, returnera 0.
            return 0;
        }
    }

    // ----------------------------------------------------------------------------------------
    // Metod: getBalance()
    // Syfte: Returnerar det aktuella saldot på kontot.
    // ----------------------------------------------------------------------------------------
    public int getBalance(){
        // Returnerar värdet av saldo.
        return balance;
    }
}
