using UnityEngine;
using System.Collections;

public class NameReader : MonoBehaviour {

    public TextAsset nameList;

    TextMesh textMesh;

	// Use this for initialization
	void Start () {

        textMesh = GetComponent<TextMesh>();

        textMesh.text = nameList.text;
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position -= new Vector3(0.0f, 0.0f, 10.0f) * Time.deltaTime;
	
	}
}
