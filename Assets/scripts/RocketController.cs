using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rocketExplode;
    public float lifetime;
    SpawnManager spawnManager;
    Rigidbody2D rbody;
    bool fuseLit = false;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        spawnManager.allDynamicSprites.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        int enemiesInRange = 0;

        if (lifetime < 0)
        // || velocity.sqrMagnitude < 4
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(rocketExplode, transform.position, transform.rotation);
        }

        else if (!fuseLit)
        {
            foreach (GameObject obj in spawnManager.allDynamicSprites)
            {
                Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
                if (objToSelf.magnitude < 20 && obj.tag == "enemy")
                {
                    enemiesInRange += 1;
                }
            }

            if (enemiesInRange >= 3)
            {
                lifetime = 0.3f;
                fuseLit = true;
            }
        }
    }
    public void setVelocity(Vector3 v)
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
    }
    Vector3 rotateAboutOrgin(Vector3 vec, float angle)
    {
        float x = vec.x*Mathf.Cos(angle) - vec.y*Mathf.Sin(angle);
        float y = vec.y*Mathf.Cos(angle) + vec.x*Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }

    private void OnDestroy() {
        spawnManager.allDynamicSprites.Remove(gameObject);
    }
}

