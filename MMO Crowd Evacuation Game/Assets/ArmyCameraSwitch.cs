using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyCameraSwitch : MonoBehaviour {

    public GameObject maincam;
    public int index;
    public int armyindex;
    public GameObject gamecontroller;
    public Button mainbutton;
    //public static bool clicked;

    // Use this for initialization
    void Start()
    {
        index = 0;
        armyindex = 0;
        //clicked = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Camera.main == null)
        {
            maincam.SetActive(true);
        }

            //clicked = false;
            if (Camera.main.gameObject.transform.parent == null)
            {
                mainbutton.interactable = false;
            }
            else
            {
                mainbutton.interactable = true;
            }
        
    }

    public void mainCameraSelect()
    {
        if (Camera.main.gameObject.transform.parent.GetComponent<ArmyCameraChoice>() != null)
        {
            Camera.main.gameObject.transform.parent.GetComponent<ArmyCameraChoice>().enabled = false;
        }
        Camera.main.gameObject.SetActive(false);
        maincam.SetActive(true);
    }

    public void armyCameraSwitch()
    {
        Camera.main.gameObject.SetActive(false);
        if (armyindex >= gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones.Count)
        {
            armyindex = 0;
            //gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones.Count - 1].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = false;
            gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones.Count - 1].GetComponent<ArmyCameraChoice>().enabled = false;
        }

        if (armyindex > 0)
        {
            //gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[armyindex-1].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = false;
            gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[armyindex - 1].GetComponent<ArmyCameraChoice>().enabled = false;
        }

        //gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[armyindex++].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = true;
        gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones[armyindex++].GetComponent<ArmyCameraChoice>().enabled = true;
       // Debug.Log("end " + armyindex + " " + gamecontroller.GetComponent<ArmyCameraSwitchActivator>().selectedarmydrones.Count);
    }
}
