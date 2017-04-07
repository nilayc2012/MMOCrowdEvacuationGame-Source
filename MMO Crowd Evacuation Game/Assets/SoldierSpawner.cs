using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoldierSpawner : NetworkBehaviour {

    public GameObject armyPrefab;
    public GameObject soldierPlane;
    // Use this for initialization
    public override void OnStartClient()
    {

        foreach (GameObject heli in GameObject.FindGameObjectsWithTag("drone"))
        {
            float x = UnityEngine.Random.Range(soldierPlane.transform.position.x - soldierPlane.transform.localScale.x / 2, soldierPlane.transform.position.x + soldierPlane.transform.localScale.x / 2);
            float z = UnityEngine.Random.Range(soldierPlane.transform.position.z - soldierPlane.transform.localScale.z / 2, soldierPlane.transform.position.z + soldierPlane.transform.localScale.z / 2);

            GameObject soldierObj = Instantiate(armyPrefab, new Vector3(x, 0.1f, z), new Quaternion(0, 0, 0, 0));

            NetworkServer.Spawn(soldierObj);

            soldierObj.GetComponent<BombDefuserMulti>().helicopter = heli;
            heli.GetComponent<HeliControlMulti>().soldierObj = soldierObj;
        }
    }
}
