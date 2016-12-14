using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Text.RegularExpressions;
using System;


public class ButtonTasks : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

	
	}

    //Login validation and action
    public void Login()
    {
        GameObject.Find("loginerrortext").GetComponent<Text>().text = "";
        string emailid = GameObject.Find("username").GetComponent<InputField>().text;
        string pwd = GameObject.Find("password").GetComponent<InputField>().text;


        GameObject errortext=null;

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject g in objects)
        {
            if (g.name == "loginerrortext")
            {

                errortext = g;

            }
        }

        if (emailid == "" || pwd == "" )
        {

            errortext.GetComponent<Text>().text = "No Field should be empty";
            errortext.SetActive(true);
        }
        else
        {
            string url;
#if UNITY_EDITOR
            url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/login.php"; //-- for test environment
#else
            url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/login.php"; //--for live environment
#endif
            WWWForm loginForm = new WWWForm();

            loginForm.AddField("uname", emailid, System.Text.Encoding.UTF8);
            loginForm.AddField("pwd", pwd, System.Text.Encoding.UTF8);


            WWW www = new WWW(url, loginForm);
            IEnumerator coroutine = WaitForRequestLogin(www,errortext);
            StartCoroutine(coroutine);
        }


    }



    IEnumerator WaitForRequestLogin(WWW www, GameObject errortext)
    {

        yield return www;

        // if Login successfull store the user details
        if (www.text.Contains("pass"))
        {
            string[] result = www.text.Split(',');
            GameObject.Find("UserData").GetComponent<UserDataScript>().uid = result[1];
            GameObject.Find("UserData").GetComponent<UserDataScript>().fullname = result[2]+" "+ result[3];
            GameObject.Find("UserData").GetComponent<UserDataScript>().gamername = result[4];
            SceneManager.LoadScene("gameselector");
        }
        else if (www.text.Equals("wrong pwd"))
        {
            errortext.GetComponent<Text>().text = "Wrong Password";
            errortext.SetActive(true);
        }
        else if (www.text.Equals("wrong username"))
        {
            errortext.GetComponent<Text>().text = "Wrong Username";
            errortext.SetActive(true);
        }
        else
        {
            errortext.GetComponent<Text>().text = "Connection failure";
            errortext.SetActive(true);
        }

    }

    // this methid used to store all Game details
    public void storeGameDetails(Button button)
    {
        GameInfoScript gic= button.gameObject.GetComponent<GameInfoScript>();
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        gmc.gid = gic.gid;
        gmc.gname = gic.gname;
        gmc.envid = gic.envid;
        gmc.ruleid = gic.ruleid;
        gmc.minp = gic.minp;
        gmc.maxp = gic.maxp;
        gmc.ownerId = gic.ownerId;

        SceneManager.LoadScene("lobbyscene");

    }

    public void goback()
    {
        Destroy(GameObject.Find("GameMetaData"));
        Destroy(GameObject.Find("LobbyManager"));
        SceneManager.LoadScene("gameselector");
    }


}
