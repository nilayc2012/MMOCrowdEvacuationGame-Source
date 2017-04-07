using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameControllerBSMulti : NetworkBehaviour {

    public GameObject bombobj;
    public Text bombcounttext;

    public Text bombDiffuseText;

    
    public int bombcount = 3;

    //public bool start;

    public GameObject soldier;


    public GameObject localplayerobj=null;
    // Use this for initialization
    void Start()
    {

        RenderSettings.fog = false;



        //bombcounttext.text = bombcount.ToString();

        bombDiffuseText.text = "0";

   }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (localplayerobj != null)
        {
            GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

            if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
            {
                bombDiffuseText.text = localplayerobj.GetComponent<PrizeCounter>().ballcount.ToString();
            }
            else if (gmc.ctypeid == "2")
            {
                if (localplayerobj.gameObject.GetComponent<PrizeCounter>().teamno == 1)
                {
                    bombDiffuseText.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                    
                }
                else
                {
                    bombDiffuseText.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2.ToString();
                }

            }
            else if (gmc.ctypeid == "3")
            {
                bombDiffuseText.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
            }
            

        }
            

        if (!this.GetComponent<FinalOutComeBSMultiTask>().finish)
        {

            GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

            bombcounttext.text = bombs.Length.ToString();

        }
    }
}
