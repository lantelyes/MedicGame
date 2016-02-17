using UnityEngine;
using System.Collections;

public class NameReader : MonoBehaviour {

    public TextAsset nameList;
    public float scrollSpeed;

    TextMesh textMesh;

	// Use this for initialization
	void Start () {

        textMesh = GetComponent<TextMesh>();

        textMesh.text = nameList.text;
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position -= new Vector3(0.0f, 0.0f, scrollSpeed) * Time.deltaTime;
	
	}
}
