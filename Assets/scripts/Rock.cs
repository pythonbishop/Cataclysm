using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite rockAngry;
    public Sprite rockSad;
    public float detectionRadius;
    GameObject player;
    SpriteRenderer spriteRenderer;
    RockGlockController[] gunControllers;
    DamageController damageController;
    bool playerInBounds;
    Vector3 selfToPlayer;
    SpawnManager spawnManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunControllers = GetComponentsInChildren<RockGlockController>();
        damageController = GetComponent<DamageController>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        selfToPlayer = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        selfToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        if (selfToPlayer.magnitude < detectionRadius && !playerInBounds)
        {
            enrage();
        }
        if (selfToPlayer.magnitude > detectionRadius+20 && playerInBounds)
        {
            sleep();
        }
    }

    void enrage()
    {
        foreach (RockGlockController controller in gunControllers)
        {
            controller.wake();
        }
        spriteRenderer.sprite = rockAngry;
        damageController.wake();
        playerInBounds = true;
    }

    void sleep()
    {
        foreach (RockGlockController controller in gunControllers)
        {
            controller.sleep();
        }
        spriteRenderer.sprite = rockSad;
        spriteRenderer.color = Color.white;
        damageController.sleep();
        playerInBounds = false;
    }

}
