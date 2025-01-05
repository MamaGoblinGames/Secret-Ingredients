public class PlayerInfo
{
    public Flavor flavor;
    public PlayerCharge charge;
    public int playerNumber;
    public FlavorNumbers flavorNumbers;

    public PlayerInfo(Flavor flavor, PlayerCharge charge, int playerNumber, FlavorNumbers flavorNumbers)
    {
        this.flavor = flavor;
        this.charge = charge;
        this.playerNumber = playerNumber;
        this.flavorNumbers = flavorNumbers;
    }
}