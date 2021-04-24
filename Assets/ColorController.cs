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
        Debug.Log(colors);
        Debug.Log(colors[0]);
        Debug.Log(colors[1]);

        AssignColor(colors[0]);
        float t = 0f;
        float stepDuration = duration / colors.Length;
        Debug.Log(stepDuration);
        while (t < duration)
        {
            float progress = t / duration;
            float colorIndexFloat = progress * (colors.Length - 1);
            int colorStartIndex = (int) Mathf.Floor(colorIndexFloat);
            int colorStopIndex = colorStartIndex + 1;

            Color color1 = colors[colorStartIndex];
            Color color2 = colors[colorStopIndex];

            Color indexColor = Color.Lerp(color1, color2, colorIndexFloat - colorStartIndex);
            AssignColor(indexColor);

            t += Time.deltaTime;
            yield return null;
        }

        AssignColor(colors[colors.Length - 1]);
    }

    private void AssignColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }
}
