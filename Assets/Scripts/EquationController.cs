using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Only supports addition, want to extend for EMDAS

public class EquationController : MonoBehaviour
{
    private bool readyToSelectNewAnswer = true;
    private float answer;
    private float constant1;
    private float constant2;
    private Text display;
    private Color initialColor;
    private bool shouldDestroy;
    private float padding = 40.0f;
    private int numberOfGuesses = 0;

    public GameObject fireworks;

    // Start is called before the first frame update
    void Start()
    {
        // start off facing backwards
        // transform.rotation = Quaternion.Euler(180, 0, 0);
        InitSolution();

        // Bind our click handler
        Button btn = gameObject.GetComponentInChildren<Button>();
        btn.onClick.AddListener(HandleAnswer);

        initialColor = new Color(1f, 1f, 1f);
        GetComponent<ColorController>().SetColor(initialColor);
    }

    private void Update()
    {
        if (readyToSelectNewAnswer && IsFacingBackwards())
        {
            Debug.Log("Is facing backwards, picking new answer");
            readyToSelectNewAnswer = false;

            if (this.shouldDestroy)
            {
                Destroy(gameObject);
            } else
            {
                InitSolution();
            }
        }
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float[] GetTerms()
    {
        return new float[] { this.constant1, this.constant2 };
    }

    public float GetAnswer()
    {
        return answer;
    }

    public int GetNumberOfGuesses()
    {
        return numberOfGuesses;
    }

    public void SelectNewAnswer(bool shouldDestroy = false)
    {
        this.shouldDestroy = shouldDestroy;
        readyToSelectNewAnswer = true;
        GetComponent<RotationController>().StartRotating();
    }

    public void DisplayCorrect(bool shouldDestroy = false)
    {
        GetComponent<ColorController>().SetColors(new Color[] {
            new Color(0, 1, 0),
            initialColor
        }, GameSettings.Instance.TransitionSpeed);

        Instantiate(fireworks, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
    }

    public void DisplayIncorrect(bool shouldDestroy = false)
    {
        GetComponent<ShakeController>().StartShaking();
        GetComponent<ColorController>().SetColors(new Color[] {
            new Color(1, 0, 0),
            initialColor
        }, GameSettings.Instance.TransitionSpeed);
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private bool IsFacingBackwards()
    {
        return transform.rotation.x > 0.75;
    }

    private void InitSolution()
    {
        Button btn = gameObject.GetComponentInChildren<Button>();

        // Pick an answer
        answer = Random.Range(0.0f, GameSettings.Instance.MaximumAnswerValue);
        answer = RoundIfNeeded(answer);

        // Determine what the constants should be
        do
        {
            constant1 = Random.Range(-answer, answer);
            constant1 = RoundIfNeeded(constant1);
            // we want the first constant to be interesting
        } while (!IsConstantInteresting(constant1));
        
        constant2 = RoundIfNeeded(answer - constant1);
        answer = constant1 + constant2;

        // Update our display to show the equation
        Debug.Log("Answer is: " + answer.ToString());

        display = btn.transform.GetChild(0).gameObject.GetComponentInChildren<Text>();

        // we randomly swap the constants so that constan2 has a chance to be negative
        if (Mathf.Round(Random.Range(0,1)) == 0)
        {
            float temp = constant1;
            constant1 = constant2;
            constant2 = temp;
        }

        // Write the equation
        if (constant2 >= 0)
        {
            display.text = constant1.ToString() + " + " + constant2.ToString();
        }
        else
        {
            display.text = constant1.ToString() + " - " + Mathf.Abs(constant2).ToString();
        }

        // Set our box width accordingly
        float equationWidth = display.preferredWidth;
        // float meshScale = equationWidth * 0.5f; // 0.5f is the ratio between mesh scale and perceived UI width (now that canvas is in world space)

        RectTransform uiRect = btn.gameObject.GetComponent<RectTransform>();
        //        uiRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        //        uiRect.localScale = new Vector3(0.5f, 2, 1);
        // gameObject.transform.localScale = new Vector3(meshScale, 25f, 25f);

        numberOfGuesses = 0;
    }

    private float RoundIfNeeded(float val)
    {
        // This will round according to the decimal base
        float decimalBase = Mathf.Round(Mathf.Pow(10.0f, (float)GameSettings.Instance.MaximumNumberOfDecimals));
        return  Mathf.Round(val * decimalBase) / decimalBase;
    }

    private void HandleAnswer()
    {
        numberOfGuesses++;
        GameController.Instance.CheckAnswerToEquation(this);
    }

    private bool IsConstantInteresting(float constant)
    {
        return constant1 != 0 
            && constant1 != answer;
    }
}

