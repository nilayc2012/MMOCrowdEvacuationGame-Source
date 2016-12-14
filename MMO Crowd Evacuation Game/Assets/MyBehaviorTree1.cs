using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeSharpPlus;

public class MyBehaviorTree1 : MonoBehaviour
{

	private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
        this.SetStartPos();
        behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    protected void SetStartPos()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        GameObject[] planes = GameObject.FindGameObjectsWithTag("dummyplane");

        int count = 0;
        foreach (GameObject agent in agents)
        {
            UnityEngine.Random.InitState(count++);
            GameObject posPlane = planes[Convert.ToInt32(UnityEngine.Random.value * (planes.Length - 1))];

            float x = UnityEngine.Random.Range(posPlane.transform.position.x - posPlane.transform.localScale.x * 10 / 2, posPlane.transform.position.x + posPlane.transform.localScale.x * 10 / 2);

            float z = UnityEngine.Random.Range(posPlane.transform.position.z - posPlane.transform.localScale.z * 10 / 2, posPlane.transform.position.z + posPlane.transform.localScale.z * 10 / 2);

            agent.transform.position = new Vector3(x, agent.transform.position.y, z);
        }
    }
    protected Node BuildTreeRoot()
	{
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");

        GameObject[] dests = GameObject.FindGameObjectsWithTag("aidestinations");

        List<Node> agentNodes = new List<Node>();

        foreach(GameObject agent in agents)
        {
            GameObject dest = dests[Convert.ToInt32(UnityEngine.Random.value * (dests.Length - 1))];
            agentNodes.Add(
                agent.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(dest.transform.position,2));
        }

        return  new SequenceParallel(agentNodes.ToArray());
		
	}
}
