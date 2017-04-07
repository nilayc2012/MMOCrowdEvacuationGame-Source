using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSquadCallerMulti : MonoBehaviour {

    public GameObject GodCam;
    public GameObject panel;
    public GameObject distantPanel;

    public GameObject approachPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallBombSquad()
    {
        panel.SetActive(false);

        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().soldierObj.GetComponent<NavigationControllerBSMulti>().enabled = true;


        //GameObject activedrone = null;
        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<DistanceCheckerMulti>().enabled = false;
        distantPanel.SetActive(false);
        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().enabled = false;
        //GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.transform.FindChild("Main Camera").gameObject.SetActive(false);

       // GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<GameStateChecker>().initiateSquadCall();
        approachPanel.SetActive(true);


        approachPanel.GetComponent<SoldierApproachMulti>().detectedbomb = GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().detectedBomb;
        approachPanel.GetComponent<SoldierApproachMulti>().soldier = GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().soldierObj;
        approachPanel.GetComponent<SoldierApproachMulti>().enabled = true;
    }
}
