using UnityEngine;
using System.Collections;

public class DraggableObject : MonoBehaviour {


    bool isDragged;

	// Use this for initialization
	void Start () {

        isDragged = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetDragged(bool state) {
        isDragged = state;
    }
    public bool GetDragged() {
        return isDragged;
    }
    
    

}
