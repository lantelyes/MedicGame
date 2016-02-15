using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


    //Public stuff
    public float walkSpeed = 10.0f;
    public float sprintSpeed = 7.0f;


    //Stuff about movement
    float lookAngle;
    float speed;
    float sprintTime = 5.0f;
    float sprintCharge = 5.0f;
    CharacterController controller;
    Quaternion heading;
    Vector3 movement;
    Vector3 mousePos;
    Vector3 lookPos;

    bool canSprint;
    bool isSprinting;

    //Draging stuff
    bool isDragging;
    bool canDrag;

    GameObject currentDragObject;
    public List<InjuredSoldier> injuredSoldiers;

    

    // Use this for initialization
    void Start () {

        speed = walkSpeed;

        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        controller = GetComponent<CharacterController>();
        movement = new Vector3(0.0f,0.0f,0.0f);

        canSprint = true;
        isSprinting = false;

        canDrag = true;
        isDragging = false;


        injuredSoldiers = new List<InjuredSoldier>();

    }
	
	// Update is called once per frame
	void Update () {

        HandleMovement();

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y; 
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        lookAngle = (Mathf.Atan2(lookPos.y, lookPos.x) + 30.0f) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);

        controller.Move(movement * Time.deltaTime);
        
        foreach(InjuredSoldier soldier in injuredSoldiers) {
            soldier.lineRenderer.SetPosition(1, gameObject.transform.position);
        }

	
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


    void HandleDragging(GameObject dragObject)
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


        if(currentDragObject) {
            currentDragObject.transform.position = transform.position;
        }

    }


    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "InjuredSoldier") { 
            HandleDragging(col.gameObject);
        }
    }


}
