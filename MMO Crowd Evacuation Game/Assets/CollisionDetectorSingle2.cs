using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CollisionDetectorSingle2 : MonoBehaviour {
    // Use this for initialization

    public GameObject prize;

    public GameObject spawnGround;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    // For Mazerunner game to decide when an agent collides with the prize
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("multiplayer"))
        {
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag.Equals("outerwall") || other.gameObject.tag.Equals("prize"))
        {
            GameObject prizeobj = Instantiate(prize);
            float x = Random.Range(spawnGround.transform.position.x - spawnGround.transform.localScale.x / 2 + 20, spawnGround.transform.position.x + spawnGround.transform.localScale.x / 2 - 20);
            float z = Random.Range(spawnGround.transform.position.z - spawnGround.transform.localScale.z / 2 + 20, spawnGround.transform.position.z + spawnGround.transform.localScale.z / 2 - 20);
            prizeobj.transform.position = new Vector3(x, prizeobj.transform.position.y, z);
            prizeobj.name = "prize";
            prizeobj.SetActive(true);
            Destroy(this.gameObject);
        }
    }


}
