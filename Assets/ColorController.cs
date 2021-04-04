using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color GetCurrentColor()
    {
        return GetComponent<Renderer>().material.color;
    }

    public void SetColor(Color c)
    {
        AssignColor(c);
        // SetColors(new Color[] { GetComponent<Renderer>().material.color, c }, 0.1f);
    }

    public void SetColors(Color[] colors, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateThroughColors(colors, duration));
    }

    public IEnumerator AnimateThroughColors(Color[] colors, float duration)
    {
        AssignColor(colors[0]);
        float t = 0f;
        float stepDuration = duration / colors.Length;
        foreach(Color c in colors)
        {
            Color indexColor = Color.Lerp(GetComponent<Renderer>().material.color, c, (t % stepDuration) / stepDuration);
            AssignColor(indexColor);
            t += Time.deltaTime;

            yield return indexColor;
        }
    }

    private void AssignColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }
}
