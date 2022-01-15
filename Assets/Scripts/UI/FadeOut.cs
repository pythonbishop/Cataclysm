using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasRenderer canvasRenderer;
    public float lifetime;
    public float timer;
    float totalTime;
    public bool active;
    float alpha;
    void Start()
    {
        active = false;
        alpha = 0;

        canvasRenderer = GetComponent<CanvasRenderer>();
        canvasRenderer.SetAlpha(alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (alpha > 1)
                {
                    active = false;
                    SceneManager.LoadScene("Level 1");
                }

                alpha = fadeFunction(totalTime, Mathf.Pow(lifetime, 2));

                canvasRenderer.SetAlpha(alpha);

                lifetime -= Time.deltaTime;
                totalTime += Time.deltaTime;
            }
        }
    }

    float fadeFunction(float x, float t)
    {
        return (x * x / t);
    }

    void startFade()
    {
        active = true;
    }
}
