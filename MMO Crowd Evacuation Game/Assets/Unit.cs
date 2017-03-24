using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour {

    Renderer renderer;

    void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }
	// Update is called once per frame
	void Update () {
	
        if(renderer.isVisible && Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Vector3 campos = Camera.main.WorldToScreenPoint(transform.position);
            campos.y = CameraOperator.InverseMouseY(campos.y);
            this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected = CameraOperator.selection.Contains(campos);

        }

        if(this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected)
        {
            if (this.gameObject.tag.Equals("drone"))
            {
                    this.gameObject.GetComponent<QuadCopterController>().engineStart = true;
                    this.transform.FindChild("indicator").gameObject.SetActive(true);   
            }


        }
       else
        {
            if (this.gameObject.tag.Equals("drone"))
            {
                this.transform.FindChild("indicator").gameObject.SetActive(false);
                this.gameObject.GetComponent<QuadCopterController>().engineStart = false;
            }
        }
	}
}
