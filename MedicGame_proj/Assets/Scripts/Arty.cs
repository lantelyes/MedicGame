﻿using UnityEngine;
using System.Collections;

public class Arty : MonoBehaviour {

    public PlayerController playerController;
    public GameObject explosionObject;

    public float damageAmount = 50.0f;
    public float explodeDelay = 5.0f;



    float dropTime = 0.0f;
    bool isPlayerInRadius = false;

	// Use this for initialization
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        transform.localScale = new Vector3(0.1f,0.1f,0.1f);
	
	}

    void Explode() {

        Instantiate(explosionObject, transform.position, Quaternion.identity);

        if (isPlayerInRadius) {
            playerController.Damage(damageAmount);
        }

        Destroy(gameObject);

    }
		// Update is called once per frame
	void Update () {

        if (dropTime < explodeDelay) {

            dropTime += Time.deltaTime;
            transform.localScale = new Vector3(dropTime / explodeDelay, dropTime / explodeDelay, dropTime / explodeDelay);

        }

        if (dropTime >= explodeDelay) {
            Explode();
        }
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            isPlayerInRadius = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            isPlayerInRadius = false;
        }
    }
}