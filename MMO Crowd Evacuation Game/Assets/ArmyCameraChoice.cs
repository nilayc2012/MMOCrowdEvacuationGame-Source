using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCameraChoice : MonoBehaviour {

    public GameObject camera1;
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
		
	}
}
