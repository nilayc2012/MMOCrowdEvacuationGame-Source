using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);

        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        PlayerController1 localplayer = gamePlayer.GetComponent<PlayerController1>();

        localplayer.pname = lobby.playerName;
        localplayer.playerColor = lobby.playerColor;
    }
}
