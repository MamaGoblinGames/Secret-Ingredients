public class PlayerInfo
{
    public Flavor flavor;
    public PlayerCharge charge;
    public int playerNumber;

    public PlayerInfo(Flavor flavor, PlayerCharge charge, int playerNumber)
    {
        this.flavor = flavor;
        this.charge = charge;
        this.playerNumber = playerNumber;
    }
}