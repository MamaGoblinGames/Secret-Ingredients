using UnityEngine;

[CreateAssetMenu(fileName = "PlayersInfo", menuName = "Scriptable Objects/Playes Info")]
public class PlayersInfo : ScriptableObject
{
    [Range(0, 4)]
    public int numPlayers = 0;
    public Flavor player1Flavor;
    public PlayerCharge player1Charge;
    public Flavor player2Flavor;
    public PlayerCharge player2Charge;

    public Flavor player3Flavor;
    public PlayerCharge player3Charge;

    public Flavor player4Flavor;
    public PlayerCharge player4Charge;

    public void OnEnable() {
        ResetPlayers();
    }

    public void ResetPlayers() {
        numPlayers = 0;
        player1Flavor.Neutralize();
        player1Charge.Reset();
        player2Flavor.Neutralize();
        player2Charge.Reset();
        player3Flavor.Neutralize();
        player3Charge.Reset();
        player4Flavor.Neutralize();
        player4Charge.Reset();
    }

    public PlayerInfo RegisterPlayer(string playerComponentName) {
        numPlayers++;
        Debug.Log("Registering player: "+numPlayers+" "+playerComponentName);
        PlayerInfo playerInfo = null;
        if (numPlayers == 1) {
            // return object with player1Flavor and player1Charge
            playerInfo = new PlayerInfo(player1Flavor, player1Charge, numPlayers);
        }
        else if (numPlayers == 2) {
            // return object with player2Flavor and player2Charge
            playerInfo = new PlayerInfo(player2Flavor, player2Charge, numPlayers);
        }
        else if (numPlayers == 3) {
            // return object with player3Flavor and player3Charge
            playerInfo = new PlayerInfo(player3Flavor, player3Charge, numPlayers);
        }
        else if (numPlayers == 4) {
            // return object with player4Flavor and player4Charge
            playerInfo = new PlayerInfo(player4Flavor, player4Charge, numPlayers);
        }
        return playerInfo;
    }
}
