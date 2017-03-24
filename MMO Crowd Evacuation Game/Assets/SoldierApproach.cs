using UnityEngine;
using System.Collections;
using System.Linq;

public class SoldierApproach : MonoBehaviour {

    public GameObject panel2;
    public GameObject[] statusbars;
    int index;

    public bool complete;

    public GameObject detectedbomb;
    int multiplier;
    float maxDist;

    //public GameObject maincam;
    // Use this for initialization

    void Start()
    {
        complete = true;
        index = 0;

        multiplier = 9;

        foreach(GameObject bomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            if(bomb.GetComponent<BombDetector>().detected)
            {
                detectedbomb = bomb;
            }
        }

        float[] distances = new float[GameObject.FindGameObjectsWithTag("soldier").Length];
        int i = 0;

        foreach (GameObject soldier in GameObject.FindGameObjectsWithTag("soldier"))
        {
            distances[i++] = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position);
        }

        maxDist = distances.Max();

    }

    void OnEnable()
    {
        complete = true;
        index = 0;

        multiplier = 9;

        foreach (GameObject bomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            if (bomb.GetComponent<BombDetector>().detected)
            {
                detectedbomb = bomb;
            }
        }

        float[] distances = new float[GameObject.FindGameObjectsWithTag("soldier").Length];
        int i = 0;

        foreach (GameObject soldier in GameObject.FindGameObjectsWithTag("soldier"))
        {
            distances[i++] = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position);
        }

        maxDist = distances.Max();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (complete)
        {

            float[] distances = new float[GameObject.FindGameObjectsWithTag("soldier").Length];
            int i = 0;

            int solcount = 0;
            foreach (GameObject soldier in GameObject.FindGameObjectsWithTag("soldier"))
            {
                if (!soldier.GetComponent<NavigationControllerBS>().isHere)
                {
                    solcount++;
                    distances[i++] = Vector3.Distance(soldier.transform.position, detectedbomb.transform.position);
                }
            }
            if(solcount==0)
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

            

            float tempmaxDist = distances.Max();

                if (tempmaxDist <= (multiplier * (maxDist / 9)))
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

                        panel2.SetActive(false);
                    }

                }
            }

        }

    }
}
