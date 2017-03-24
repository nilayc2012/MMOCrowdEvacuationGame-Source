using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedCheckerMulti : MonoBehaviour {

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (this.GetComponent<NavigationControllerBSMulti>().isHere && this.GetComponent<NavigationControllerBSMulti>().enabled)
        {
            this.GetComponent<NavigationControllerBSMulti>().enabled = false;
            this.GetComponent<NavigationControllerBSMulti>().isHere = false;
        }
    }
}
