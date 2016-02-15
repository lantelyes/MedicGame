using UnityEngine;
using System.Collections;

public class Tent : MonoBehaviour {

    public GameObject playerObject;
    public GameObject gameManagerObject;
    GameManager gameManager;
    PlayerController playerController;

	// Use this for initialization
	void Start () {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        playerController = playerObject.GetComponent<PlayerController>();
  
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "InjuredSoldier") {
            playerController.injuredSoldiers.Remove(col.gameObject.GetComponent<InjuredSoldier>());
            Destroy(col.gameObject);
            playerController.StopDragging();
            gameManager.numberSaved++;
        }
    }
}
