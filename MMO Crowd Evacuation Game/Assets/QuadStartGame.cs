using UnityEngine;
using System.Collections;

public class QuadStartGame : MonoBehaviour {

    public GameObject panel;
    public GameObject panel1;
    public GameObject mainPanel;
    public GameObject controlPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        GameObject[] drones = GameObject.FindGameObjectsWithTag("drone");

        foreach (GameObject drone in drones)
        {
            drone.GetComponent<QuadracopterRayPointTracker>().enabled = true;
        }

        GameObject.Find("GameController").GetComponent<GameControllerBS>().start = true;
        QuadracopterRayPointTracker.trackstart = true;
        Camera.main.gameObject.GetComponent<CameraMover>().enabled = true;
        Camera.main.gameObject.GetComponent<MouseLookBS>().enabled = true;
        Camera.main.gameObject.GetComponent<CameraOperator>().enabled = true;
        controlPanel.SetActive(true);
        panel1.SetActive(true);
        mainPanel.SetActive(true);
        panel.SetActive(false);
    }
}
