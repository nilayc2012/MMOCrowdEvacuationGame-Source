using UnityEngine;
using System.Collections;

public class CameraChoice : MonoBehaviour {

    public GameObject camera1, camera2, camera3,camera4;

	// Use this for initialization
	void Start () {

        camera1.SetActive(true);
	}

    void OnEnable()
    {

        camera1.SetActive(true);
    }
    // Update is called once per frame
    void Update () {

        if(Input.GetKey(KeyCode.C))
        {
            if(camera1.activeSelf)
            {
                camera1.SetActive(false);
                camera2.SetActive(true);
            }
            else if (camera2.activeSelf)
            {
                camera2.SetActive(false);
                camera3.SetActive(true);
            }
            else if (camera3.activeSelf)
            {
                camera3.SetActive(false);
                camera4.SetActive(true);
            }
            else if (camera4.activeSelf)
            {
                camera4.SetActive(false);
                camera1.SetActive(true);
            }
        }
	
	}
}
