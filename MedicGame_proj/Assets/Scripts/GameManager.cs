using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int numberSaved;
    public int numberDied;
    public float injuredSoldierSpawnRate;
    public float injuredSoldierSpawnRadius;

    public InjuredSoldier injuredSoldier;
    public PlayerController playerController;


    Vector2 positionToSpawn;

	// Use this for initialization
	void Start () {

        numberSaved = 0;
        numberDied = 0;

        positionToSpawn = Random.insideUnitCircle * injuredSoldierSpawnRadius;
        InvokeRepeating("SpawnInjuredSoldier", 0.0f, injuredSoldierSpawnRate);

    }

    void SpawnInjuredSoldier() {
        InjuredSoldier soldier = (InjuredSoldier)Instantiate(injuredSoldier, new Vector3(positionToSpawn.x, positionToSpawn.y, 0.1f),Quaternion.identity);
        positionToSpawn = Random.insideUnitCircle * injuredSoldierSpawnRadius;
        playerController.injuredSoldiers.Add(soldier);
    }
	
	// Update is called once per frame
	void Update () {


	
	}
}
