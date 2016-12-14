using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour {

    [SyncVar]
    private Quaternion syncPlayerRotation;

    [SerializeField]
    private float lerpRate = 15;

    [SerializeField]
    private Transform playerTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitRotations();
        LerpRotation();
	}

    void LerpRotation()
    {
        if(!isLocalPlayer)
        {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot)
    {
        syncPlayerRotation = playerRot;
    }

    [Client]
    void TransmitRotations()
    {
        if(isLocalPlayer)
        {
            CmdProvideRotationsToServer(playerTransform.rotation);
        }
    }
}
