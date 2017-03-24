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


public class StoreScript : MonoBehaviour
{

    public bool flag;
    private IEnumerator coroutine;
    public string gameplayid;
    public string output;
    DataTracker dtobj;
    // Use this for initialization
    void Start()
    {
        flag = false;
        dtobj = GameObject.Find("DataTracker").GetComponent<DataTracker>();
    }

    // Update is called once per frame

    public void createXML()
    {
        if (!flag)
        {

            flag = true;


            string url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/create_report.php";

            gameplayid = this.gameObject.GetComponent<GenerateGamePlayID>().gameplayID;

            WWWForm loginForm = new WWWForm();


            
            loginForm.AddField("game-id",dtobj.gmc.gid , System.Text.Encoding.UTF8);
            loginForm.AddField("gameplay-id", gameplayid, System.Text.Encoding.UTF8);
            loginForm.AddField("game-name", dtobj.gmc.gname, System.Text.Encoding.UTF8);
            loginForm.AddField("owner-id", dtobj.gmc.ownerId, System.Text.Encoding.UTF8);
            loginForm.AddField("userid", GameObject.Find("UserData").GetComponent<UserDataScript>().uid, System.Text.Encoding.UTF8);



            if (dtobj.gmc.ruleid == "1" || dtobj.gmc.ruleid == "2")
            {
                loginForm.AddField("score", dtobj.localagent.GetComponent<PlayerController1>().score.ToString(), System.Text.Encoding.UTF8);
                loginForm.AddField("stype", dtobj.localagent.GetComponent<PlayerController1>().scoretype, System.Text.Encoding.UTF8);
                loginForm.AddField("scrtype", dtobj.localagent.GetComponent<PlayerController1>().scorerType, System.Text.Encoding.UTF8);
            }
            else if (dtobj.gmc.ruleid == "3" || dtobj.gmc.ruleid == "4")
            {
                loginForm.AddField("score", dtobj.localagent.GetComponent<HeliControlMulti>().score.ToString(), System.Text.Encoding.UTF8);
                loginForm.AddField("stype", dtobj.localagent.GetComponent< HeliControlMulti> ().scoretype, System.Text.Encoding.UTF8);
                loginForm.AddField("scrtype", dtobj.localagent.GetComponent<HeliControlMulti> ().scorerType, System.Text.Encoding.UTF8);

            }

            loginForm.AddField("gtype", dtobj.goaltype, System.Text.Encoding.UTF8);
            // Sending request:
            WWW www = new WWW(url, loginForm);
            coroutine = WaitForRequest(www);
            StartCoroutine(coroutine);


            string storeGoalData = "";
            string storePath = "";

            url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/store_goal.php";

            storeGoalData = storeGoalData + "borderbottomleft," + dtobj.borderBottomLeft.x + "," + dtobj.borderBottomLeft.z + "~";
            storeGoalData = storeGoalData + "borderbottomRight," + dtobj.borderBottomRight.x + "," + dtobj.borderBottomRight.z + "~";
            storeGoalData = storeGoalData + "borderTopLeft," + dtobj.borderTopLeft.x + "," + dtobj.borderTopLeft.z + "~";
            storeGoalData = storeGoalData + "borderTopRight," + dtobj.borderTopRight.x + "," + dtobj.borderTopRight.z + "~";

            foreach (Pos pos in dtobj.goalPositions)
            {
                storeGoalData = storeGoalData + "pos," + pos.x + "," + pos.z + "~";
            }

            loginForm = new WWWForm();

            loginForm.AddField("gameplayid", gameplayid, System.Text.Encoding.UTF8);

            //byte[] toBytes = Encoding.ASCII.GetBytes(storeGoalData);
            loginForm.AddField("querystring", storeGoalData);

            www = new WWW(url, loginForm);
            coroutine = WaitForRequest(www);
            StartCoroutine(coroutine);


            //strong path details/////////////////////////////////
            storePath = storePath + "borderbottomleft," + dtobj.borderBottomLeft.x + "," + dtobj.borderBottomLeft.z + "~";
            storePath = storePath + "borderbottomRight," + dtobj.borderBottomRight.x + "," + dtobj.borderBottomRight.z + "~";
            storePath = storePath + "borderTopLeft," + dtobj.borderTopLeft.x + "," + dtobj.borderTopLeft.z + "~";
            storePath = storePath + "borderTopRight," + dtobj.borderTopRight.x + "," + dtobj.borderTopRight.z + "~";


            if (dtobj.gmc.ruleid == "1" || dtobj.gmc.ruleid == "2")
            {
                storePath = storePath + "start," + dtobj.localagent.GetComponent<PlayerController1>().startpos.x + "," + dtobj.localagent.GetComponent<PlayerController1>().startpos.z + "~";
                storePath = storePath + "end," + dtobj.localagent.GetComponent<PlayerController1>().endpos.x + "," + dtobj.localagent.GetComponent<PlayerController1>().endpos.z + "~";

            }
            else if (dtobj.gmc.ruleid == "3" || dtobj.gmc.ruleid == "4")
            {
                storePath = storePath + "start," + dtobj.localagent.GetComponent<HeliControlMulti>().startpos.x + "," + dtobj.localagent.GetComponent<HeliControlMulti>().startpos.z + "~";
                storePath = storePath + "end," + dtobj.localagent.GetComponent<HeliControlMulti>().endpos.x + "," + dtobj.localagent.GetComponent<HeliControlMulti>().endpos.z + "~";
            }


            foreach (Pos pos in dtobj.pathpositions)
            {
                storePath = storePath + "pos," + pos.x + "," + pos.z + "~";
            }

            url = "http://spanky.rutgers.edu/MMOCrowdEvacGame/store_path.php";

            loginForm = new WWWForm();

            loginForm.AddField("gameplayid", gameplayid, System.Text.Encoding.UTF8);
            loginForm.AddField("userid", GameObject.Find("UserData").GetComponent<UserDataScript>().uid, System.Text.Encoding.UTF8);

            //toBytes = Encoding.ASCII.GetBytes(storePath);

            loginForm.AddField("querystring", storePath);

            www = new WWW(url, loginForm);
            coroutine = WaitForRequest(www);
            StartCoroutine(coroutine);

        }
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            output = www.text;
            Debug.Log("pass" + www.text);
            //mytext.text="WWW Ok!: " + www.data;
        }
        else
        {
            Debug.Log("fail" + www.error);
            //mytext.text="WWW Error: "+ www.error;
        }
    }
}
