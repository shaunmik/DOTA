using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OutlineScript : MonoBehaviour
{
    public float FadeRate = 3.0f;
    public bool requiresInput = false;
    public float lifeInSeconds = 4f;
    private Image image;
    private float targetAlpha;
    public bool isFadeIn;
    public bool isFadeOut;

    float time = 0f;
    float multiplier = 0.4f;

    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        if (isFadeIn)
        {
            Color c = this.image.color;
            c.a = 0f;
            this.image.color = c;
        }
        this.targetAlpha = this.image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the outline up and down :)
        if (time <= 0.8)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * multiplier);
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            multiplier = -1 * multiplier;
        }

        if (isFadeIn)
        {
            FadeIn();
            if (this.image.color.a >= 0.98f)
            {
                isFadeOut = true;
                isFadeIn = false;
            }
        }

        if (!requiresInput && isFadeOut)
        {
            lifeInSeconds -= Time.deltaTime;
            if (lifeInSeconds <= 0) FadeOut();
        }

        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }

        if (this.image.color.a <= 0.01 && isFadeOut && !isFadeIn) this.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }
}