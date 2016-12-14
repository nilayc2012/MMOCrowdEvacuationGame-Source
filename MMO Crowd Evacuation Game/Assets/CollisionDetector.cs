using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CollisionDetector : NetworkBehaviour {

    public GameObject panel;
    GameObject winner=null;
    public GameObject winnerText;

    [SyncVar]
    public bool end = false;
    // Use this for initialization

    void Awake()
    {
        end = false;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    // For Mazerunner game to decide when an agent collides with the prize
    void OnTriggerEnter(Collider other)
    {
        winner = other.gameObject;
        if (winner.GetComponent<PlayerController1>() != null)
            winnerText.GetComponent<Text>().text = winner.GetComponent<PlayerController1>().pname;
        else
            winnerText.GetComponent<Text>().text = winner.name;

        panel.SetActive(true);
        end = true;
    }


}
