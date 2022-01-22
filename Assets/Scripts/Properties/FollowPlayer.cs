using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float soi;
    public float moveForce;
    public float engageDistance;
    GameObject player;
    Vector3 selfToPlayer;
    Rigidbody2D selfRigidbody;
    void Start()
    {
        selfToPlayer = new Vector3();
        player = GameObject.FindGameObjectWithTag("Player");
        selfRigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        selfToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0); //vector from self to player
    }
    private void FixedUpdate()
    {
        if (selfToPlayer.magnitude < soi)
        {
            if (selfToPlayer.magnitude > engageDistance)
            {
                selfRigidbody.AddForce(Vector3.Normalize(selfToPlayer) * moveForce);
            }
        }
    }
}