using UnityEngine;
using System.Collections;

public class BombDetector : MonoBehaviour {

    public GameObject distantPanel;
    public GameObject panel;
    public bool detected;

    public bool isDiffused;
    // Use this for initialization
    void Start () {

        detected = false;

        isDiffused = false;
    }
	
	// Update is called once per frame
	void Update () {


	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("drone"))
        {
            distantPanel.SetActive(false);
            other.gameObject.GetComponent<HeliControl>().enabled = false;
            other.gameObject.GetComponent<AH_AnimationHelper>().engineOn = false;
            other.gameObject.GetComponent<ParkDrone>().enabled = true;
            panel.SetActive(true);
            detected = true;
            
        }

    }
}
