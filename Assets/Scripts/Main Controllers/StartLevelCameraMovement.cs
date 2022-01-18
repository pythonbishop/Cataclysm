using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCameraMovement : MonoBehaviour
{
    GameObject player;
    public GameObject target;
    public float travelDuration; //how long camera takes to travel to target
    public float targetDuration; // how long camera stays over target
    public float returnDuration; // how long camera takes to travel back to player
    public float startUpWait;
    int state;
    Vector3 playerToTarget;
    Vector3 targetToPlayer;
    
    Vector3 travelVelocity;
    Vector3 returnVelocity;
    Vector3 currentPosition;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerInput>().enabled = false;
        GetComponent<StickToPlayer>().enabled = false;

        playerToTarget = new Vector3(target.transform.position.x - player.transform.position.x, target.transform.position.y - player.transform.position.y, 0);
        targetToPlayer = -playerToTarget;
        state = 0;
        travelVelocity = Vector3.Normalize(playerToTarget) * playerToTarget.magnitude/travelDuration;
        returnVelocity = Vector3.Normalize(targetToPlayer) * targetToPlayer.magnitude/returnDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (startUpWait < 0)
        {
            if (state == 0)
            {
                //move to target
                transform.Translate(travelVelocity * Time.deltaTime);
                travelDuration -= Time.deltaTime;
                if (travelDuration < 0)
                {
                    state = 1;
                }
            }
            if (state == 1)
            {
                // stay on target
                targetDuration -= Time.deltaTime;
                if (targetDuration < 0)
                {
                    state = 2;
                }
            }
            if (state == 2)
            {
                //move to player
                transform.Translate(returnVelocity * Time.deltaTime);
                returnDuration -= Time.deltaTime;
                if (returnDuration < 0)
                {
                    enabled = false;
                    player.GetComponent<PlayerInput>().enabled = true;
                    GetComponent<StickToPlayer>().enabled = true;
                }
            }
        }
        startUpWait -= Time.deltaTime;

    }
}
