using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

    public float speed = 1.5f;
    public bool ortho;

    void Start()
    {
        ortho = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed;
        }
        if(Input.GetKey(KeyCode.W))
        {
            if (transform.position.y >=5f)
                transform.position += Vector3.down * speed/2;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * speed/2;
        }
    
        if(Input.GetKey(KeyCode.O))
        {
            ortho = !ortho;
            if (!ortho)
            {
                Camera.main.transform.Rotate(new Vector3(90, Camera.main.transform.eulerAngles.y,Camera.main.transform.eulerAngles.z));
            }
            else
            {
                Camera.main.transform.Rotate(new Vector3( 30,Camera.main.transform.eulerAngles.y,Camera.main.transform.eulerAngles.z));
            }
        }
        
    }
}
