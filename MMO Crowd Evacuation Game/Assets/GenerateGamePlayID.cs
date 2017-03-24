using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerateGamePlayID : NetworkBehaviour {

    [SyncVar]
    public string gameplayID;

    IEnumerator Start()
    {

            string url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/generate_gameplay.php";

            WWW www = new WWW(url);
            yield return www;
            gameplayID = www.text;
            Debug.Log("hameplay : " + www.text + " "+gameplayID+" end");
        
    }
}
