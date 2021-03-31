using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Only supports addition, want to extend for EMDAS

public class EquationController : MonoBehaviour
{
    private float answer;
    private float constant1;
    private float constant2;
    private Text display;

    private float padding = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Bind our click handler
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(CheckIfCorrect);

        // Pick an answer
        answer = Random.Range(0.0f, GameSettings.Instance.MaximumAnswerValue);
        answer = RoundIfNeeded(answer);

        // Determine what the constants should be
        constant1 = Random.Range(-answer, answer);
        constant1 = RoundIfNeeded(constant1);
        constant2 = answer - constant1;

        // Update our display to show the equation
        Debug.Log("Answer is " + answer.ToString());
        display = transform.GetChild(0).gameObject.GetComponentInChildren<Text>();
        display.text = constant1.ToString() + " + " + constant2.ToString();

        // Set our box width accordingly
        float width = display.preferredWidth + padding;
        float height = display.preferredHeight + (padding / 2);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public void CheckIfCorrect()
    {
        float userAnswer = GameController.Instance.GetUserAnswer();
        if (userAnswer == answer)
        {
            HandleCorrect();
        }
        else
        {
            HandleIncorrect();
        }
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private void HandleCorrect()
    {
        Debug.Log("Correct!!");
        Destroy(gameObject);
    }

    private void HandleIncorrect()
    {
        Debug.Log("Incorrect :(");
        Destroy(gameObject);
    }

    private float RoundIfNeeded(float val)
    {
        // This will round according to the decimal base
        float decimalBase = Mathf.Round(Mathf.Pow(10.0f, (float)GameSettings.Instance.MaximumNumberOfDecimals));
        return  Mathf.Round(val * decimalBase) / decimalBase;
    }
}

