using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombDetectorMulti : NetworkBehaviour {


    public GameObject distantPanel;
    public GameObject panel;
    public bool detected;

    public GameObject detectedDrone;

    public bool isDiffused;
    // Use this for initialization
    void Start()
    {

        detected = false;

        isDiffused = false;

    }

    // Update is called once per frame
    void Update()
    {



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("drone") && !detected)
        {
            distantPanel.SetActive(false);
            other.gameObject.GetComponent<HeliControlMulti>().enabled = false;
            other.gameObject.GetComponent<AH_AnimationHelper>().engineOn = false;
            other.gameObject.GetComponent<ParkDrone>().enabled = true;
            if (isLocalPlayer)
            {
                panel.SetActive(true);
            }
            detected = true;
            detectedDrone = other.gameObject;
            other.gameObject.GetComponent<HeliControlMulti>().detectedBomb = this.gameObject;

        }

    }
}
