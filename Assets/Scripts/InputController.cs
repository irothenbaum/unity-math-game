using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
TODO:
- Better Interface slide transitions
    - currently only moving entire objects as one, would be cool to get dynamic down to the individual button / element
 */

public class InputController : MonoBehaviour, ISlideTransitionable
{
    private ArrayList buffer;
    private Text display;

    // Start is called before the first frame update
    void Start()
    {
        buffer = new ArrayList();

        // grab our text display component
        display = transform.GetChild(0).gameObject.GetComponentInChildren<Text>();

        // start off with an update
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public void HandleButtonInput(string value)
    {
        if (value == "CLEAR")
        {
            buffer.Clear();
        }
        else
        {
            // if they first value they enter is a 0
            if (buffer.Count == 0 && value == "0")
            {
                // you can't add a 0 to nothing
                return;
            }

            // if they first value they enter is a .
            if (buffer.Count == 0 && value == ".")
            {
                // add a 0 first, then add the value
                buffer.Add("0");
            }

            // Now add the value they clicked
            buffer.Add(value);

        }

        this.UpdateDisplay();
    }

    public float GetInputValue()
    {
        return float.Parse(BufferToString());
    }

    public void ClearAnswer()
    {
        buffer.Clear();
        UpdateDisplay();
    }

    public void SlideIntoView()
    {
        GetComponent<SlideController>().LerpToPosition(new Vector3(0, -50, 0), GameSettings.Instance.TransitionSpeed);
    }

    public void SlideOutOfView()
    {
        GetComponent<SlideController>().LerpToPosition(new Vector3(0, -300, 0), GameSettings.Instance.TransitionSpeed);
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private string BufferToString()
    {
        if (buffer.Count == 0)
        {
            return "0";
        }

        string joined = "";

        for (int i = 0; i < buffer.Count; i++)
        {
            joined = joined + buffer[i];
        }

        return joined;
    }

    private void UpdateDisplay()
    {
        string displayStr;

        // if the user has nothing typed, show a 0
        if (buffer.Count == 0)
        {
            displayStr = "0";
        }
        else
        {
            displayStr = BufferToString();
        }

        // write the string to our display component
        display.text = displayStr;
    }


    
}
