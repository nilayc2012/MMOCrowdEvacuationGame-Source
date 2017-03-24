using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombDefuserMulti : MonoBehaviour {

    public GameObject panel;
    public GameObject diffusedPanel;
    public GameObject helicopter;
    public GameObject panel2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

        int count = 0;
        foreach (GameObject bomb in bombs)
        {
            Vector3 temp = new Vector3(bomb.transform.GetChild(0).position.x, transform.position.y, bomb.transform.GetChild(0).position.z);
            if (Vector3.Distance(transform.position, temp) <= 3.0f && !bomb.GetComponent<BombDetectorMulti>().isDiffused && bomb.GetComponent<BombDetectorMulti>().detected)
            {
                panel.SetActive(true);
                break;
            }
            count++;
        }

        /*    if(count==bombs.Length)
            {
                panel.SetActive(false);
            }*/
    }

    public void DiffuseBomb()
    {
        panel.SetActive(false);


        GameObject detectedbomb = GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().detectedBomb;


        detectedbomb.GetComponent<BombDetectorMulti>().detected = false;
        detectedbomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        detectedbomb.SetActive(false);

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<PrizeCounter>().ballcount++;
        }
        else if (gmc.ctypeid == "2")
        {
            if (GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
        }

        diffusedPanel.SetActive(true);
        diffusedPanel.GetComponent<DiffuseCompletion>().enabled = true;
        diffusedPanel.GetComponent<DiffuseCompletion>().complete = true;
    }

    public void ResumeSearch()
    {
        panel2.SetActive(false);


        GameObject soldierobj = GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().soldierObj;

        soldierobj.GetComponent<PlayerControllerBSMulti>().enabled = false;
        soldierobj.GetComponent<NavigationControllerBS>().enabled = false;


        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,30f,Camera.main.transform.position.z);
        Camera.main.gameObject.SetActive(false);
        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.transform.Find("Main Camera").gameObject.SetActive(true);
        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().enabled = true;
        GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<DistanceCheckerMulti>().enabled = true;
    }
}
