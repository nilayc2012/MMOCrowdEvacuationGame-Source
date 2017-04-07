using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReachedCheckerMulti : NetworkBehaviour {

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (this.GetComponent<NavigationControllerBSMulti>().isHere && this.GetComponent<NavigationControllerBSMulti>().enabled)
            {
                this.GetComponent<NavigationControllerBSMulti>().enabled = false;
                this.GetComponent<NavigationControllerBSMulti>().isHere = false;
            }
        }
    }
}
