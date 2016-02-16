using UnityEngine;
using System.Collections;



public class GameManager : MonoBehaviour {

    public bool isPlaying = true;

    public int numberSaved;
    public int numberDied;
    public float injuredSoldierSpawnRate;
    public float injuredSoldierSpawnRadius;

    public float artySpawnRadius;
    public float artySpawnRate;

    public InjuredSoldier injuredSoldier;
    public PlayerController playerController;
    public Arty arty;
    public TextMesh statusTextMesh;


    Vector2 positionToSpawnSoldier;
    Vector2 positionToSpawnArty;
	// Use this for initialization
	void Start () {

        numberSaved = 0;
        numberDied = 0;

        positionToSpawnSoldier = Random.insideUnitCircle * injuredSoldierSpawnRadius;
        StartCoroutine("SpawnInjuredSoldier");

        positionToSpawnArty = Random.insideUnitCircle* injuredSoldierSpawnRadius;
        StartCoroutine("SpawnArty");

    }

    IEnumerator SpawnInjuredSoldier() {

        while (isPlaying) {
            InjuredSoldier soldierObject = (InjuredSoldier)Instantiate(injuredSoldier, new Vector3(positionToSpawnSoldier.x, positionToSpawnSoldier.y, 0.1f), Quaternion.identity);
            positionToSpawnSoldier = Random.insideUnitCircle * injuredSoldierSpawnRadius;
            playerController.injuredSoldiers.Add(soldierObject);

            yield return new WaitForSeconds(injuredSoldierSpawnRate);
        }
    }

    IEnumerator SpawnArty() {

        while (isPlaying) {

            Arty artyObject = (Arty)Instantiate(arty, new Vector3(positionToSpawnArty.x, positionToSpawnArty.y, 0.1f), Quaternion.identity);
            positionToSpawnArty = Random.insideUnitCircle * artySpawnRadius;
            yield return new WaitForSeconds(artySpawnRate);

        }


    }
	
	// Update is called once per frame
	void Update () {


        statusTextMesh.text = "Soldiers saved: " + numberSaved + "\nCausualties: " + numberDied;

	
	}
}
