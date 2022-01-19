using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rocketExplode;
    public float lifetime;
    public float fuse;
    public int numEnemiesTrigger;
    SpawnManager spawnManager;
    public LineRenderer line;
    List<LineRenderer> activeLines;
    List<GameObject> enemiesInRange;
    Rigidbody2D rbody;
    int numEnemiesInRange;
    bool hasFuse;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        activeLines = new List<LineRenderer>();
        enemiesInRange = new List<GameObject>();
        numEnemiesInRange = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(rocketExplode, transform.position, transform.rotation);
        }
        
        for (int i = 0; i < activeLines.Count; i++)
        {
            LineRenderer line = activeLines[i];
            RocketLineEffect lineEffect = line.GetComponent<RocketLineEffect>();
            if (!enemiesInRange.Contains(lineEffect.target) && !hasFuse)
            {
                activeLines.Remove(line);
                Destroy(line);
            }
        }

        foreach (GameObject obj in spawnManager.allDynamicSprites)
        {
            Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
            if (objToSelf.magnitude < 25 && obj.tag == "enemy" && !enemiesInRange.Contains(obj))
            {
                LineRenderer newLine = Instantiate(line, transform.position, transform.rotation);
                Vector3[] linePoints = {transform.position, obj.transform.position};
                RocketLineEffect lineScript = newLine.GetComponent<RocketLineEffect>();
                lineScript.target = obj;
                lineScript.rocket = gameObject;

                newLine.GetComponent<LineRenderer>().SetPositions(linePoints);
                numEnemiesInRange += 1;
                activeLines.Add(newLine);
                enemiesInRange.Add(obj);
            }
            else if (objToSelf.magnitude > 25 && enemiesInRange.Contains(obj))
            {
                enemiesInRange.Remove(obj);
                numEnemiesInRange -= 1;
            }
        }

        if (numEnemiesInRange >= numEnemiesTrigger && !hasFuse)
        {
            lifetime = fuse;
            hasFuse = true;
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
        for (int i = 0; i < activeLines.Count; i++)
        {
            Destroy(activeLines[i]);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        GameObject obj = Instantiate(rocketExplode, transform.position, transform.rotation);
    }

}

