using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DroneCameraSwitch : MonoBehaviour {

    public GameObject maincam;
    public int index;
    public int armyindex;
    public GameObject gamecontroller;
    public Button mainbutton;
    //public static bool clicked;

	// Use this for initialization
	void Start () {
        index = 0;
        armyindex = 0;
        //clicked = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //clicked = false;
        if(Camera.main.gameObject.transform.parent==null)
        {
            mainbutton.interactable = false;
        }
        else
        {
            mainbutton.interactable = true;
        }
    }

    public void DroneCamSelect()
    {
        //clicked = true;
        Camera.main.gameObject.SetActive(false);
        if (index >= gamecontroller.GetComponent<DroneCameraSwitchActivator>().selecteddrones.Count)
        {
            index = 0;
            gamecontroller.GetComponent<DroneCameraSwitchActivator>().selecteddrones[gamecontroller.GetComponent<DroneCameraSwitchActivator>().selecteddrones.Count - 1].GetComponent<CameraChoice>().enabled = false;
        }
        if(index>0)
        {
            gamecontroller.GetComponent<DroneCameraSwitchActivator>().selecteddrones[index-1].GetComponent<CameraChoice>().enabled = false;
        }
        gamecontroller.GetComponent<DroneCameraSwitchActivator>().selecteddrones[index++].GetComponent<CameraChoice>().enabled = true;
    }
    public void mainCameraSelect()
    {
        //clicked = true;
        if (Camera.main.gameObject.transform.parent.GetComponent<CameraChoice>() != null)
        {
            Camera.main.gameObject.transform.parent.GetComponent<CameraChoice>().enabled = false;
        }
        else if(Camera.main.gameObject.transform.parent.GetComponent<ArmyCameraChoice>()!=null)
        {
            Camera.main.gameObject.transform.parent.GetComponent<ArmyCameraChoice>().enabled = false;
        }
        Camera.main.gameObject.SetActive(false);
        maincam.SetActive(true);
    }

    public void armyCameraSwitch()
    {
        Debug.Log("start "+armyindex + " " + gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones.Count);
        //clicked = true;
        Camera.main.gameObject.SetActive(false);
        if (armyindex >= gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones.Count)
        {
            armyindex = 0;
            //gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones.Count - 1].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = false;
            gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones.Count - 1].GetComponent<ArmyCameraChoice>().enabled = false;
        }

        if(armyindex>0)
        {
            //gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[armyindex-1].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = false;
            gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[armyindex - 1].GetComponent<ArmyCameraChoice>().enabled = false;
        }

        //gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[armyindex++].GetComponent<ArmyUnit>().droneCam.GetComponent<CameraChoice>().enabled = true;
        gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones[armyindex++].GetComponent<ArmyCameraChoice>().enabled = true;
        Debug.Log("end "+armyindex + " " + gamecontroller.GetComponent<DroneCameraSwitchActivator>().selectedarmydrones.Count);
    }

}
