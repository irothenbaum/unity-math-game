using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private Color lastAssigned;

    public Color GetCurrentColor()
    {
        return lastAssigned != null ? lastAssigned : GetComponent<Renderer>().material.color;
    }

    public void SetColor(Color c)
    {
        SetColors(new Color[] { GetCurrentColor(), c }, 0.1f);
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

        AssignColor(colors[colors.Length - 1], true);
    }

    private void AssignColor(Color c, bool assignAsCurrent = false)
    {
        GetComponent<Renderer>().material.color = c;
        if (assignAsCurrent)
        {
            this.lastAssigned = c;
        }
    }
}
