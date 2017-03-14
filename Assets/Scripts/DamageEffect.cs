using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public float FadeRate = 5.0f;
    public bool requiresInput = false;
    public float lifeInSeconds = 1f;
    private Image image;
    private float targetAlpha;
    public bool isFadeIn;
    public bool isFadeOut;

    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }

        Color c = this.image.color;
        c.a = 0f;
        this.image.color = c;

        this.targetAlpha = this.image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
        this.targetAlpha = 1.0f;
    }
}