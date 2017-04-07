using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoldierApproachMulti : MonoBehaviour {

    public GameObject panel2;
    public GameObject[] statusbars;
    int index;

    public bool complete;

    public GameObject detectedbomb;
    public GameObject soldier;
    int multiplier;
    float dist;

    //public GameObject maincam;
    // Use this for initialization

    void Start()
    {
        complete = true;
        index = 0;

        multiplier = 9;

        dist = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position);

    }

    void OnEnable()
    {
        complete = true;
        index = 0;

        multiplier = 9;

        dist = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position);

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (complete)
        {

            if (soldier.GetComponent<NavigationControllerBSMulti>().isHere)
            {
                foreach (GameObject bar in GameObject.FindGameObjectsWithTag("status1"))
                {
                    bar.SetActive(true);
                }
                complete = false;

                foreach (GameObject bar in GameObject.FindGameObjectsWithTag("status1"))
                {
                    bar.SetActive(false);
                }

                panel2.SetActive(false);
            }
            else
            {



                float tempmaxDist = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position); 

                if (tempmaxDist <= (multiplier * (dist / 9)))
                {
                    statusbars[index++].SetActive(true);
                    multiplier--;
                    if (multiplier == 0)
                    {
                        foreach (GameObject bar in GameObject.FindGameObjectsWithTag("status1"))
                        {
                            bar.SetActive(false);
                        }
                        complete = false;

                        panel2.GetComponent<SoldierApproachMulti>().enabled = false;
                    }

                }
            }

        }

    }

    void OnDisable()
    {
        panel2.SetActive(false);
    }
}
