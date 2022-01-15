using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadeToTransparent : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasRenderer canvasRenderer;
    public float lifetime;
    float totalTime;
    public bool active;
    float alpha;
    void Start()
    {
        alpha = 1;

        canvasRenderer = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            if (alpha < 0)
            {
                active = false;
            }

            alpha = fadeFunction(totalTime, Mathf.Pow(lifetime, 2));

            canvasRenderer.SetAlpha(alpha);

            lifetime -= Time.deltaTime;
            totalTime += Time.deltaTime;
        }
    }

    float fadeFunction(float x, float t) 
    {
        return (x * x / -t + 1);
    }

    void startFade()
    {
        active = true;
    }
}
