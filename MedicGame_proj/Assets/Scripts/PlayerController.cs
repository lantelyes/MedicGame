using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


    //Public stuff
    public float walkSpeed = 10.0f;
    public float sprintSpeed = 7.0f;
    public List<InjuredSoldier> injuredSoldiers;
    public TextMesh statusTextMesh;


    //Stuff about movement
    float lookAngle;
    float speed;
    float sprintTime = 5.0f;
    float sprintCharge = 5.0f;

    CharacterController controller;
    Vector3 movement;
    Vector3 lookPos;

    bool canSprint;
    bool isSprinting;

    //Draging stuff
    bool isDragging;

    //Player stats
    float health = 100.0f;
    int numMedkits = 3;

    GameObject healthBarObject;
    Renderer[] medkitRenderers = new Renderer[3];
    GameObject currentDragObject;



    public void Damage(float damage) {
        health -= damage;

        Die();

    }

    void Die() {
        Application.LoadLevel("End");
    }

    // Use this for initialization
    void Start() {

        for (int i = 0; i < numMedkits; i++) {
            medkitRenderers[i] = transform.GetChild(i).GetComponent<Renderer>();
        }
        RenderMedkits();

        healthBarObject = transform.GetChild(3).gameObject;


        speed = walkSpeed;

        controller = GetComponent<CharacterController>();
        movement = new Vector3(0.0f,0.0f,0.0f);

        canSprint = true;
        isSprinting = false;

        isDragging = false;


        injuredSoldiers = new List<InjuredSoldier>();

    }

	
	// Update is called once per frame
	void Update () {

        HandleMovement();

        lookPos = movement + transform.position;
        lookAngle = (Mathf.Atan2(lookPos.y, lookPos.x) + 30.0f) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);

        controller.Move(movement * Time.deltaTime);
        

        healthBarObject.transform.localScale = new Vector3(.2f,health / 100.0f,.2f);
         
        
	
	}

    void HandleMovement() {


        //Sprinting Logic
        if (canSprint) { 
            if (Input.GetKey(KeyCode.LeftShift)) {
                speed = sprintSpeed;
                isSprinting = true;
                sprintCharge -= Time.deltaTime;
            } 

            if(Input.GetKeyUp(KeyCode.LeftShift)) {
                canSprint = false;
                isSprinting = false;
                speed = walkSpeed / 2.0f;
            }

        }

        if(!canSprint) {
            sprintCharge += Time.deltaTime;
        }
        if (sprintCharge >= sprintTime)
        {
            sprintCharge = sprintTime;
            canSprint = true;
            speed = walkSpeed;
        }
        if (sprintCharge <= 0)  {
            canSprint = false;
            sprintCharge = 0;
            speed = walkSpeed /2.0f;
        }

        //Movement logic
        movement = new Vector3(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = movement * speed;
    }

   public void StopDragging() {
        currentDragObject = null;
        isDragging = false;
    }

    void RenderMedkits() {
        foreach(Renderer medkitRenderer in medkitRenderers) {
            medkitRenderer.enabled = false;
        }

        for (int i = 0; i < numMedkits; i++) {
            medkitRenderers[i].enabled = true;
        }
    }

    
    void HandleInput(GameObject dragObject)
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(!isDragging) {
                currentDragObject = dragObject;
                isDragging = true;
            } else {
                currentDragObject = null;
                isDragging = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            InjuredSoldier soldier = dragObject.GetComponent<InjuredSoldier>();
            if(!soldier.isStableized && numMedkits > 0) {
                soldier.Stableize();
                numMedkits--;
                RenderMedkits();
            }
        }


        if(currentDragObject) {
            currentDragObject.transform.position = transform.position;
        }

    }


    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "InjuredSoldier") { 
            HandleInput(col.gameObject);
        }
    }


}
