using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    public int phase;
    Animator animator;
    float currentAttack;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 1)
        {
            //spawning enemies
            //enemySpawnerScript.spawnEnemies();

        }

        if (phase == 2)
        {

        }
    }
}