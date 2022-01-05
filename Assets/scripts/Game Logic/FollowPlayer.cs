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
    GameObject spawnManagerObject;
    SpawnManager spawnManager;
    SpriteRenderer spriteRenderer;
    Rigidbody2D selfRigidbody;
    Collider2D selfCollider;
    float angle;
    void Start()
    {
        selfToPlayer = new Vector3();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        selfToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0); //vector from self to player
        angle = Vector3.Angle(Vector3.right, selfToPlayer); //player direction in degrees

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