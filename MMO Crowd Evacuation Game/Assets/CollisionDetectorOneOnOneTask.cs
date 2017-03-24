using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CollisionDetectorOneOnOneTask : NetworkBehaviour
{
    GameMetaScript gmc;
    public GameObject spawnGround;
    // Use this for initialization

    void Start()
    {
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
    }

    // For Mazerunner game to decide when an agent collides with the prize
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("multiplayer"))
        {

            if (gmc.ctypeid == "1"|| gmc.ctypeid == "5")
            {
                other.gameObject.GetComponent<PrizeCounter>().ballcount++;
            }
            else if (gmc.ctypeid == "2")
            {
                if (other.gameObject.GetComponent<PrizeCounter>().teamno==1)
                {
                    GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
                }
                else
                {
                    GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
                }
                
            }
            else if (gmc.ctypeid == "3")
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag.Equals("outerwall") || other.gameObject.tag.Equals("prize"))
        {
            GameObject prizeobj = Instantiate(this.gameObject);
            float x = Random.Range(spawnGround.transform.position.x - spawnGround.transform.localScale.x / 2 + 20, spawnGround.transform.position.x + spawnGround.transform.localScale.x / 2 - 20);
            float z = Random.Range(spawnGround.transform.position.z - spawnGround.transform.localScale.z / 2 + 20, spawnGround.transform.position.z + spawnGround.transform.localScale.z / 2 - 20);
            prizeobj.transform.position = new Vector3(x, prizeobj.transform.position.y, z);
            prizeobj.name = "prize";
            prizeobj.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
