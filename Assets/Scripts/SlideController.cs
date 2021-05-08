using UnityEngine;
using System.Collections;

public class SlideController : MonoBehaviour
{
    public void LerpToPosition(Vector3 newPos, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SlideTo(transform.position, newPos, duration));
    }

    private IEnumerator SlideTo(Vector3 startPos, Vector3 newPos, float duration)
    {
        Debug.Log("Lerping from " + startPos.ToString() + " to " + newPos.ToString());
        float t = 0f;

        while (t < duration)
        {
            float progress = t / duration;

            transform.position = Vector3.Lerp(startPos, newPos, progress);

            t += Time.deltaTime;
            yield return null;
        }

        transform.position = newPos;
    }

}