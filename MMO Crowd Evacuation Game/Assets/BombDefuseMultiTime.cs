using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BombDefuseMultiTime : NetworkBehaviour {

    public GameObject panel;
    public GameObject diffusedPanel;
    public GameObject helicopter;
    public GameObject panel2;

    //blic GameObject bombPrefab;

    // Use this for initialization
    void Start()
    {

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        foreach (GameObject g in objects)
        {
            if (g.name == "Panel (3)")
            {
                panel = g;
            }
            if (g.name == "Panel (12)")
            {
                diffusedPanel = g;
            }
            if (g.name == "Panel (4)")
            {
                panel2 = g;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (helicopter.GetComponent<HeliControlMulti>().localplayer)
        {
            GameObject bomb = helicopter.GetComponent<HeliControlMulti>().detectedBomb;
            if (bomb != null)
            {
                Vector3 temp = new Vector3(bomb.transform.GetChild(0).position.x, transform.position.y, bomb.transform.GetChild(0).position.z);
                if (Vector3.Distance(transform.position, temp) <= 3.0f && !bomb.GetComponent<BombDetectorMulti>().isDiffused && bomb.GetComponent<BombDetectorMulti>().detected)
                {
                    panel.SetActive(true);
                }
            }
        }

        /*    if(count==bombs.Length)
            {
                panel.SetActive(false);
            }*/
    }

    public void DiffuseBomb()
    {
        panel.SetActive(false);


        GameObject detectedbomb = GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<HeliControlMulti>().detectedBomb;

        //Network.Destroy(detectedbomb);

        List<RegionPos> regions = GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().bomPosis;
        regions.Add(new RegionPos(detectedbomb.GetComponent<BombDetectorMulti>().regionx, detectedbomb.GetComponent<BombDetectorMulti>().regiony, detectedbomb.GetComponent<BombDetectorMulti>().regionz));

        //ameObject bomb = Instantiate(bombPrefab);

        int regionIndex = UnityEngine.Random.Range(0, regions.Count);

        GameObject findregion=null;

        foreach(GameObject region in GameObject.FindGameObjectsWithTag("region"))
        {
            if(region.transform.position.x== regions[regionIndex].x && region.transform.position.y == regions[regionIndex].y && region.transform.position.z == regions[regionIndex].z)
            {
                findregion = region;
            }
        }

        int index = UnityEngine.Random.Range(0, findregion.transform.childCount - 1);
        GameObject bombpos = findregion.transform.GetChild(index).gameObject;

        GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<GameStateChecker>().initiateBombDefuse(bombpos.transform.position.x, 0.1f, bombpos.transform.position.z);
        detectedbomb.GetComponent<BombDetectorMulti>().detected = false;
        //detectedbomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        //detectedbomb.SetActive(false);

        //bomb.SetActive(true);

        detectedbomb.transform.position = new Vector3(bombpos.transform.position.x, detectedbomb.transform.position.y, bombpos.transform.position.z);

        detectedbomb.GetComponent<BombDetectorMulti>().initateSetRegion(findregion.transform.position.x, findregion.transform.position.y, findregion.transform.position.z);

        //NetworkServer.Spawn(bomb);
        

        //GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<GameStateChecker>() = true;



        /*   GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

           if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
           {
               GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<PrizeCounter>().ballcount++;
           }
           else if (gmc.ctypeid == "2")
           {
               if (GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<PrizeCounter>().teamno == 1)
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
           }*/

        diffusedPanel.SetActive(true);
        diffusedPanel.GetComponent<DiffuseCompletion>().enabled = true;
        diffusedPanel.GetComponent<DiffuseCompletion>().complete = true;
    }

    public void ResumeSearch()
    {
        panel2.SetActive(false);

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        foreach (GameObject g in objects)
        {
            if (g.name == "distancepanel")
            {
                g.SetActive(true);
                break;
            }
        }
        GameObject soldierobj = GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<HeliControlMulti>().soldierObj;

        soldierobj.GetComponent<PlayerControllerBSMulti>().enabled = false;
        soldierobj.GetComponent<NavigationControllerBSMulti>().enabled = false;


        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,30f,Camera.main.transform.position.z);
        //Camera.main.gameObject.SetActive(false);
        // GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.transform.Find("Main Camera").gameObject.SetActive(true);
        GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<HeliControlMulti>().enabled = true;
        GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj.GetComponent<DistanceCheckerMulti>().enabled = true;

    }
}
