namespace banko;

public class Card {

    public string pin = "0123";
    public Account account;

    public Card(Account account){
        this.account = account;
    }

}
