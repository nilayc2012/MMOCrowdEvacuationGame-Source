using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Net;
using UnityEngine.SceneManagement;

public class StoreSingleScript : MonoBehaviour {

    public bool flag;
    private IEnumerator coroutine;
    public string gameplayid;
    DataTrackerSingle dtobj;

    public string output;
    // Use this for initialization
    void Start()
    {
        flag = false;
        dtobj = GameObject.Find("DataTracker").GetComponent<DataTrackerSingle>();
    }

    // Update is called once per frame

    public void createXML()
    {
        if (!flag)
        {

            flag = true;


            string url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/create_report.php";


            WWWForm loginForm = new WWWForm();

            

            loginForm.AddField("game-id", dtobj.gmc.gid, System.Text.Encoding.UTF8);
            loginForm.AddField("game-name", dtobj.gmc.gname, System.Text.Encoding.UTF8);
            loginForm.AddField("owner-id", dtobj.gmc.ownerId, System.Text.Encoding.UTF8);
            loginForm.AddField("userid", GameObject.Find("UserData").GetComponent<UserDataScript>().uid, System.Text.Encoding.UTF8);


            if (dtobj.gmc.ruleid == "1" || dtobj.gmc.ruleid == "2")
            {
                Debug.Log(dtobj.localagent.GetComponent<PlayerController1single>().score);
                loginForm.AddField("score", dtobj.localagent.GetComponent<PlayerController1single>().score.ToString(), System.Text.Encoding.UTF8);
                loginForm.AddField("stype", dtobj.localagent.GetComponent<PlayerController1single>().scoretype, System.Text.Encoding.UTF8);
                loginForm.AddField("scrtype", dtobj.localagent.GetComponent<PlayerController1single>().scorerType, System.Text.Encoding.UTF8);
            }
            else if (dtobj.gmc.ruleid == "3" || dtobj.gmc.ruleid == "4")
            {
                loginForm.AddField("score", dtobj.localagent.GetComponent<HeliControlMulti>().score.ToString(), System.Text.Encoding.UTF8);
                loginForm.AddField("stype", dtobj.localagent.GetComponent<HeliControlMulti>().scoretype, System.Text.Encoding.UTF8);
                loginForm.AddField("scrtype", dtobj.localagent.GetComponent<HeliControlMulti>().scorerType, System.Text.Encoding.UTF8);
            }

            loginForm.AddField("gtype", dtobj.goaltype, System.Text.Encoding.UTF8);
            // Sending request:
            WWW www = new WWW(url, loginForm);
            coroutine = WaitForFirstRequest(www);
            StartCoroutine(coroutine);


        }
    }

    IEnumerator WaitForFirstRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            gameplayid = www.text;

            string url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/store_goal.php";

            string storeGoalData = "";
            string storePath = "";

            storeGoalData = storeGoalData + "borderbottomleft," + dtobj.borderBottomLeft.x + "," + dtobj.borderBottomLeft.z + "~";
            storeGoalData = storeGoalData + "borderbottomRight," + dtobj.borderBottomRight.x + "," + dtobj.borderBottomRight.z + "~";
            storeGoalData = storeGoalData + "borderTopLeft," + dtobj.borderTopLeft.x + "," + dtobj.borderTopLeft.z + "~";
            storeGoalData = storeGoalData + "borderTopRight," + dtobj.borderTopRight.x + "," + dtobj.borderTopRight.z + "~";

            foreach (Pos pos in dtobj.goalPositions)
            {
                storeGoalData = storeGoalData + "pos," + pos.x + "," + pos.z + "~";
            }

            WWWForm loginForm = new WWWForm();

            Debug.Log(storeGoalData.Length);

            loginForm.AddField("gameplayid", gameplayid, System.Text.Encoding.UTF8);

            //byte[] toBytes = Encoding.ASCII.GetBytes(storeGoalData);
            loginForm.AddField("querystring", storeGoalData);

            WWW www1 = new WWW(url, loginForm);
            coroutine = WaitForRequest(www1);
            StartCoroutine(coroutine);

            //storing path points/////////////////////////

            storePath = storePath + "borderbottomleft," + dtobj.borderBottomLeft.x + "," + dtobj.borderBottomLeft.z + "~";
            storePath = storePath + "borderbottomRight," + dtobj.borderBottomRight.x + "," + dtobj.borderBottomRight.z + "~";
            storePath = storePath + "borderTopLeft," + dtobj.borderTopLeft.x + "," + dtobj.borderTopLeft.z + "~";
            storePath = storePath + "borderTopRight," + dtobj.borderTopRight.x + "," + dtobj.borderTopRight.z + "~";


            if (dtobj.gmc.ruleid == "1" || dtobj.gmc.ruleid == "2")
            {
                storePath = storePath + "start," + dtobj.localagent.GetComponent<PlayerController1single>().startpos.x + "," + dtobj.localagent.GetComponent<PlayerController1single>().startpos.z + "~";
                storePath = storePath + "end," + dtobj.localagent.GetComponent<PlayerController1single>().endpos.x + "," + dtobj.localagent.GetComponent<PlayerController1single>().endpos.z + "~";

            }
            else if (dtobj.gmc.ruleid == "3" || dtobj.gmc.ruleid == "4")
            {
                storePath = storePath + "start," + dtobj.localagent.GetComponent<HeliControlMulti>().startpos.x + "," + dtobj.localagent.GetComponent<HeliControlMulti>().startpos.z + "~";
                storePath = storePath + "end," + dtobj.localagent.GetComponent<HeliControlMulti>().endpos.x + "," + dtobj.localagent.GetComponent<HeliControlMulti>().endpos.z + "~";
            }

            url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/store_path.php";

            foreach (Pos pos in dtobj.pathpositions)
            {
                storePath = storePath + "pos," + pos.x + "," + pos.z + "~";
            }

            Debug.Log(storePath.Length);
            loginForm = new WWWForm();

            loginForm.AddField("gameplayid", gameplayid, System.Text.Encoding.UTF8);
            loginForm.AddField("userid", GameObject.Find("UserData").GetComponent<UserDataScript>().uid, System.Text.Encoding.UTF8);


            //toBytes = Encoding.ASCII.GetBytes(storePath);

            loginForm.AddField("querystring", storePath);

            www1 = new WWW(url, loginForm);
            coroutine = WaitForRequest(www1);
            StartCoroutine(coroutine);
        }
        else
        {
            //mytext.text="WWW Error: "+ www.error;
        }
    }
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            output = www.text;
            Debug.Log(output);
            Debug.Log("passme" + www.text);
            //mytext.text="WWW Ok!: " + www.data;
        }
        else
        {
            Debug.Log("fail" + www.error);
            //mytext.text="WWW Error: "+ www.error;
        }
    }
}
