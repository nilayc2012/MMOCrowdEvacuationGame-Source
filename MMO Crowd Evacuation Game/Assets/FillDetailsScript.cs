using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class FillDetailsScript : MonoBehaviour {

    public Text playername;
    public GameObject game;

    // Use this for initialization
    void Start () {
	
        playername.text= GameObject.Find("UserData").GetComponent<UserDataScript>().gamername;

        string url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/getallgames.php"; // calling the php script that returns game data for a particular UID
        WWWForm loginForm = new WWWForm();
        string uid = GameObject.Find("UserData").GetComponent<UserDataScript>().uid;
        loginForm.AddField("uid", uid, System.Text.Encoding.UTF8);
        WWW www = new WWW(url, loginForm);
        StartCoroutine(WaitForRequest(www));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            string[] dataarr=www.text.Split('~');
           
            foreach (string data in dataarr)
            {
                if (data.Length > 1)
                {
                    string[] result = data.Split(',');

                    GameObject gameobj = Instantiate(game);
                    gameobj.transform.parent = game.transform.parent;
                    gameobj.transform.GetComponentInChildren<Text>().text = result[1];
                    gameobj.GetComponent<GameInfoScript>().gid = result[0];
                    gameobj.GetComponent<GameInfoScript>().gname = result[1];

                    gameobj.GetComponent<GameInfoScript>().envid = result[2];

                    gameobj.GetComponent<GameInfoScript>().ruleid = result[3];

                    gameobj.GetComponent<GameInfoScript>().gameoverid = result[4];

                    gameobj.GetComponent<GameInfoScript>().diffid = result[5];

                    gameobj.GetComponent<GameInfoScript>().ctypeid = result[6];

                    gameobj.GetComponent<GameInfoScript>().minp = result[7];

                    gameobj.GetComponent<GameInfoScript>().maxp = result[8];

                    gameobj.GetComponent<GameInfoScript>().ownerId = result[9];

                    gameobj.GetComponent<GameInfoScript>().game_desc = result[10];
                    gameobj.SetActive(true);
                }
            }

        }
        else
        {
            //mytext.text="WWW Error: "+ www.error;
        }
    }
}
