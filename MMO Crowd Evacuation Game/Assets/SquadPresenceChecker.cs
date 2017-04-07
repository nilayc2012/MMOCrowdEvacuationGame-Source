using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SquadPresenceChecker : NetworkBehaviour {

    int count1;

    public Text message;
    public GameObject panel;
    public GameObject panel2;

    public static bool isSquadHere;

    // Use this for initialization
    void Start () {
        count1 = 0;
        isSquadHere = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (isLocalPlayer)
        {
            int count = 0;
            GameObject[] soldiers = GameObject.FindGameObjectsWithTag("soldier");

            GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();



            foreach (GameObject soldier in soldiers)
            {
                if (gmc.ctypeid != "4")
                {
                    if (soldier.GetComponent<NavigationControllerBSMulti>().isHere)
                    {
                        count++;
                    }
                }
                else
                {
                    if (soldier.GetComponent<NavigationControllerBS>().isHere)
                    {
                        count++;
                    }
                }
            }
            if (count == soldiers.Length)
            {
                message.text = "Choose Soldiers";
                count1++;

                if (gmc.ctypeid != "4")
                {
                    panel2.GetComponent<SoldierApproachMulti>().enabled = false;
                }
                else
                {
                    Camera.main.transform.position = new Vector3(GameObject.FindGameObjectWithTag("soldier").transform.position.x, 30, GameObject.FindGameObjectWithTag("soldier").transform.position.z);


                    panel2.GetComponent<SoldierApproach>().enabled = false;
                }

                panel.SetActive(true);
                if (count1 == 50)
                {
                    panel.SetActive(false);
                    count1 = 0;
                    count = 0;
                    foreach (GameObject soldier in soldiers)
                    {
                        if (gmc.ctypeid != "4")
                        {
                            soldier.GetComponent<NavigationControllerBSMulti>().isHere = false;
                        }
                        else
                        {
                            soldier.GetComponent<NavigationControllerBS>().isHere = false;
                        }
                    }
                }
            }

        }
    }
}
