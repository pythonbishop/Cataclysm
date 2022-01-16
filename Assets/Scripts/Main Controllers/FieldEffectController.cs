using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
    public float sizeSpeed;
    public Vector3 scale;
    float speed;
    float totalTime;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lifetime = 3;
        speed = sizeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, fadeFunction(totalTime, Mathf.Pow(lifetime, 2)));
        lifetime -= Time.deltaTime;
        totalTime += Time.deltaTime;
        speed -= sizeSpeed/lifetime * Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        scale.x += speed * Time.deltaTime;
        scale.y += speed * Time.deltaTime;
        transform.localScale = scale;
    }

    float fadeFunction(float x, float t) 
    {
        return (x * x / -t + 1);
    }
}
