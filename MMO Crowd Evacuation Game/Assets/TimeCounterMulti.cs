using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TimeCounterMulti : NetworkBehaviour {

    public Text min;
    public Text sec;

    [SyncVar]
    public int time;
    int count;


    void Awake()
    {
        time = 0;
        count = 0;

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {



        count++;
        if (count == 60)
        {
            count = 0;
            time++;
        }
        float minval = time / 60;
        float secval = time % 60;

        if (minval < 10)
        {
            min.text = "0" + minval.ToString();
        }
        else
        {
            min.text = minval.ToString();
        }

        if (secval < 10)
        {
            sec.text = "0" + secval.ToString();
        }
        else
        {
            sec.text = secval.ToString();
        }



    }
}
