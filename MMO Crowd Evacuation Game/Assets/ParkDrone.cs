using UnityEngine;
using System.Collections;

public class ParkDrone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 temp = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
        this.transform.position = Vector3.Slerp(this.transform.position,temp,5*Time.deltaTime);
        this.transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);

        if(transform.rotation.y<=0.2f)
        {
            this.GetComponent<ParkDrone>().enabled = false;
        }

    }
}
