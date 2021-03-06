using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{

    public void StartRotating()
    {
        StopAllCoroutines();
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float duration = GameSettings.Instance.TransitionSpeed;
        float startRotation = transform.eulerAngles.x;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float xRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(xRotation, 0, 0);
            yield return null;
        }

        transform.eulerAngles = new Vector3(endRotation, 0, 0);
    }
}
