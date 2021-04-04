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

    private float padding = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitSolution();
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float GetAnswer()
    {
        return answer;
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private void InitSolution()
    {
        // Bind our click handler
        Button btn = gameObject.GetComponentInChildren<Button>();
        btn.onClick.AddListener(HandleAnswer);

        // Pick an answer
        answer = Random.Range(0.0f, GameSettings.Instance.MaximumAnswerValue);
        answer = RoundIfNeeded(answer);

        // Determine what the constants should be
        constant1 = Random.Range(-answer, answer);
        constant1 = RoundIfNeeded(constant1);
        constant2 = RoundIfNeeded(answer - constant1);
        answer = constant1 + constant2;

        // Update our display to show the equation
        Debug.Log("Answer is: " + answer.ToString());

        display = btn.transform.GetChild(0).gameObject.GetComponentInChildren<Text>();
        display.text = constant1.ToString() + " + " + constant2.ToString();

        // Set our box width accordingly
        float equationWidth = display.preferredWidth + padding;
        float meshScale = equationWidth / 100.0f; // 100 is the default/starting ratio between mesh scale and UI width

        RectTransform uiRect = btn.gameObject.GetComponent<RectTransform>();
        uiRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, equationWidth);
        uiRect.localScale = new Vector3(1 / meshScale, 2, 1);
        gameObject.transform.localScale = new Vector3(meshScale, 0.5f, 1);
    }

    private float RoundIfNeeded(float val)
    {
        // This will round according to the decimal base
        float decimalBase = Mathf.Round(Mathf.Pow(10.0f, (float)GameSettings.Instance.MaximumNumberOfDecimals));
        return  Mathf.Round(val * decimalBase) / decimalBase;
    }

    private void HandleAnswer()
    {
        GameController.Instance.CheckAnswerToEquation(this);
    }
}

