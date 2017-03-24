using UnityEngine;
using System.Collections;

public class QuadracopterRayPointTracker : MonoBehaviour {

    RaycastHit hitInfo;
    RaycastHit hit;
    public bool selected;
    public static bool trackstart;
    // Use this for initialization
    void Start()
    {
        selected = false;
        hitInfo = new RaycastHit();
        hit = new RaycastHit();
        trackstart = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (trackstart)
        {
            if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 10000))
                {
                    if (hitInfo.transform.gameObject.tag.Equals("drone"))
                    {
                        if (hitInfo.transform.gameObject == this.gameObject)
                        {
                            if (selected)
                            {
                                selected = false;
                                this.gameObject.GetComponent<QuadCopterController>().engineStart = false;
                            }
                            else
                            {
                                selected = true;
                                this.gameObject.GetComponent<QuadCopterController>().engineStart = true;
                            }
                        }


                    }
                    else if (hitInfo.transform.gameObject.tag.Equals("soldier"))
                    {
                        if (hitInfo.transform.gameObject == this.gameObject)
                        {
                            if (selected)
                            {
                                selected = false;
                            }
                            else
                            {
                                selected = true;
                            }
                        }
                    }

                }
            }
            else if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 10000))
                {
                    if (hitInfo.transform.gameObject.tag.Equals("drone"))
                    {
                        if (hitInfo.transform.gameObject == this.gameObject)
                        {
                            selected = true;
                            this.gameObject.GetComponent<QuadCopterController>().engineStart = true;
                        }
                        else
                        {
                            selected = false;
                            if(this.gameObject.GetComponent<QuadCopterController>()!=null)
                            this.gameObject.GetComponent<QuadCopterController>().engineStart = false;
                        }
                    }
                    else if (hitInfo.transform.gameObject.tag.Equals("soldier"))
                    {
                        if (hitInfo.transform.gameObject == this.gameObject)
                        {
                            selected = true;
                        }
                        else
                        {
                            selected = false;
                        }
                    }

                }
            }

        }
    }
}
