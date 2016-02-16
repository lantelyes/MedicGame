using UnityEngine;
using System.Collections;

public class InjuredSoldier : MonoBehaviour {

    public PlayerController playerController;
    public GameObject healthBarObject;
    public LineRenderer lineRenderer;

    Renderer healthBarRenderer;

    public bool isStableized;
    public float health = 100.0f;
    float healthDecreaseSpeed = 5.0f;


    void Die()  {
        playerController.injuredSoldiers.Remove(this);
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        healthBarRenderer = healthBarObject.GetComponent<Renderer>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        isStableized = false;

    }

    public void Stableize() {
        isStableized = true;
        healthBarRenderer.material.color = Color.green;

    }
	
	// Update is called once per frame
	void Update () {

        lineRenderer.SetPosition(0, transform.position);


        if (!isStableized) {
            health = health - Time.deltaTime * healthDecreaseSpeed;
            healthBarObject.transform.localScale = new Vector3(health / 100.0f, 0.25f, 0.1f);
        }

        if(health <= 0) {
            Die();
        }
	
	}
}
