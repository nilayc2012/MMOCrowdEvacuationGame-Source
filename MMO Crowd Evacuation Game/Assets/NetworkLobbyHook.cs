using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);

        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        if(gmc.ruleid=="3"|| gmc.ruleid == "4")
        {
            if(gmc.ruleid == "3")
            {
                HeliControlMulti localplayer = gamePlayer.GetComponent<HeliControlMulti>();

                localplayer.pname = lobby.playerName;
                gamePlayer.GetComponent<PrizeCounter>().teamno = lobby.teamnnum+1;
            }
            else
            {

            }
        }
        else
        {
            PlayerController1 localplayer = gamePlayer.GetComponent<PlayerController1>();

            localplayer.pname = lobby.playerName;
            localplayer.playerColor = lobby.playerColor;
            gamePlayer.GetComponent<PrizeCounter>().teamno=lobby.teamnnum+1;
        }

    }
}
