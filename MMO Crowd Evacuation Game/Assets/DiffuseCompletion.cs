using UnityEngine;
using System.Collections;

public class DiffuseCompletion : MonoBehaviour {

    public GameObject panel;
    public GameObject panel2;
    public GameObject[] statusbars;
    int index;
    int count;
    public bool complete;
	// Use this for initialization
	void Start () {
        complete = true;
        index = 0;
        count = 0;
    }

    void OnEnable()
    {
        complete = true;
        index = 0;
        count = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (complete)
        {
            count++;
            if (index < statusbars.Length && count == 30)
            {
                count = 0;
                statusbars[index++].SetActive(true);
            }
            else if (index == statusbars.Length)
            {
                foreach (GameObject bar in GameObject.FindGameObjectsWithTag("status"))
                {
                    bar.SetActive(false);
                }
                complete = false;

                panel.SetActive(true);
                panel2.GetComponent<DiffuseCompletion>().enabled = false;

            }
        }
	
	}
}
