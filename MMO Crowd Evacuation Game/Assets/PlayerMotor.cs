using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
	}
	
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

	// Update is called once per frame
	void FixedUpdate () {

        PerformMovement();
	}

    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
