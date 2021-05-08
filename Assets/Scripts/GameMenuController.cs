using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour, ISlideTransitionable
{
    public GameObject titleObject;
    public GameObject classicButton;
    public GameObject infiniteButton;
    public GameObject settingsButton;

    public void SlideIntoView()
    {
        Debug.Log("Sliding Game Menu INTO view");
        GetComponent<SlideController>().LerpToPosition(new Vector3(0, 0, 0), GameSettings.Instance.TransitionSpeed);

        return;
        // TODO: having issues with recttransform here:
        titleObject.GetComponent<SlideController>().LerpToPosition(new Vector3(0, 60, 0), GameSettings.Instance.TransitionSpeed);
        classicButton.GetComponent<SlideController>().LerpToPosition(new Vector3(0, 0, 0), GameSettings.Instance.TransitionSpeed);
        infiniteButton.GetComponent<SlideController>().LerpToPosition(new Vector3(0, -32, 0), GameSettings.Instance.TransitionSpeed);
        settingsButton.GetComponent<SlideController>().LerpToPosition(new Vector3(60, -140, 0), GameSettings.Instance.TransitionSpeed);
    }

    public void SlideOutOfView()
    {
        Debug.Log("Sliding Game Menu OUT OF view");
        GetComponent<SlideController>().LerpToPosition(new Vector3(0, 320, 0), GameSettings.Instance.TransitionSpeed);

        return;
        // TODO: having issues with recttransform here:
        titleObject.GetComponent<SlideController>().LerpToPosition(new Vector3(0, 180, 0), GameSettings.Instance.TransitionSpeed);
        classicButton.GetComponent<SlideController>().LerpToPosition(new Vector3(180, 0, 0), GameSettings.Instance.TransitionSpeed);
        infiniteButton.GetComponent<SlideController>().LerpToPosition(new Vector3(-180, -32, 0), GameSettings.Instance.TransitionSpeed);
        settingsButton.GetComponent<SlideController>().LerpToPosition(new Vector3(60, -180, 0), GameSettings.Instance.TransitionSpeed);
    }
}
