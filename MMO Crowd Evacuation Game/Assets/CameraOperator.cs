using UnityEngine;
using System.Collections;

public class CameraOperator : MonoBehaviour {

    public Texture2D selectionHighlight = null;
    public static Rect selection = new Rect(0, 0, 0, 0);
    private Vector3 startClick = -Vector3.one;
	
	// Update is called once per frame
	void Update () {

        CheckCamera();
	}

    void CheckCamera()
    {
        if(Input.GetMouseButtonDown(0)&&(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)))
        {
            startClick = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if(selection.width < 0)
            {
                selection.x += selection.width;
                selection.width =- selection.width;
            }
            if(selection.height <0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }

            startClick = -Vector3.one;
        }

        if(Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            selection = new Rect(startClick.x, InverseMouseY(startClick.y), Input.mousePosition.x - startClick.x, InverseMouseY(Input.mousePosition.y) - InverseMouseY(startClick.y));
        }
    }

    public static float InverseMouseY(float y)
    {
        return Screen.height - y;
    }

    private void OnGUI()
    {
        if(startClick !=-Vector3.one)
        {
            GUI.color = new Color(0, 1, 0, 0.5f);
            GUI.DrawTexture(selection, selectionHighlight);

        }
    }
}
