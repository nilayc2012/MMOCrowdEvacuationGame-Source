using UnityEngine;
using System.Collections;

public class HeliControl : MonoBehaviour {

    public float speed = 10.0F;
    public GameObject helicam;
    public GameObject maincam;
    public Rigidbody rigidbody;
    public GameObject panel;
    // Use this for initialization
    void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        panel.SetActive(true);
        maincam = GameObject.FindGameObjectWithTag("MainCamera");
        maincam.SetActive(false);
        helicam.SetActive(true);

            if (Input.GetKey(KeyCode.Space))
            {
                this.gameObject.GetComponent<AH_AnimationHelper>().engineOn=true;

                if(this.gameObject.GetComponent<AH_AnimationHelper>().currentRPM>=0.8)
                    transform.position += Vector3.up * 5.0f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += Vector3.down * 5.0f * Time.deltaTime;
            }


        if (transform.position.y >= 3.0f)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

                 if(moveVertical<0)
                  {
                      moveVertical = 0;
                  }


            Vector3 movement = new Vector3(moveVertical * Mathf.Sin(Mathf.Deg2Rad*transform.eulerAngles.y), 0.0f,moveVertical* Mathf.Cos(Mathf.Deg2Rad *transform.eulerAngles.y));
            rigidbody.velocity = movement * speed;

            if(rigidbody.rotation.eulerAngles.y<90 && rigidbody.rotation.eulerAngles.y>-90)
            {
                if (rigidbody.rotation.eulerAngles.y <0 && rigidbody.rotation.eulerAngles.y > -90)
                    rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * 1.5f, rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x * 3.0f);
                else
                    rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z, rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x * -3.0f * Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * rigidbody.rotation.eulerAngles.y)));

            }
            else
            {
                rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * -1.5f * Mathf.Abs(1/Mathf.Cos(Mathf.Deg2Rad * rigidbody.rotation.eulerAngles.y)), rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x);

            }

        }
	
	}
}
