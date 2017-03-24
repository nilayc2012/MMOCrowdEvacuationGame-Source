using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalOutcome : MonoBehaviour {

    public GameObject successPanel;
    public GameObject diffusedPanel;
    public bool finish;
	// Use this for initialization
	void Start () {

        finish = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(GameObject.FindGameObjectsWithTag("bomb").Length==0)
        {
            diffusedPanel.SetActive(false);
            successPanel.SetActive(true);

            foreach(GameObject panelobj in GameObject.FindGameObjectsWithTag("panel"))
            {
                if(panelobj!=successPanel)
                {
                    panelobj.SetActive(false);
                }
            }
            finish = true;
        }
	
	}

    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void exit()
    {
        Application.Quit();
    }
}
