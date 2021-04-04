using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameObject equationPrefab;
    private Color bgColor;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
        CreateNewEquation();
        GetBackgroundColorController().SetColor(RandomizeBackgroundColor());
    }
    void Update()
    {
        // GameObject existing = GameObject.FindWithTag("Equation");
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float GetUserAnswer()
    {
        return GetUserInputController().GetInputValue();
    }

    public void CheckAnswerToEquation(EquationController equation)
    {
        float userAnswer = GetUserAnswer();
        float correctAnswer = equation.GetAnswer();
        if (userAnswer == correctAnswer)
        {
            HandleCorrect(equation);
        }
        else
        {
            HandleIncorrect(equation);
        }

        GetUserInputController().ClearAnswer();
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private InputController GetUserInputController()
    {
        GameObject userAnswer = GameObject.FindGameObjectWithTag("UserAnswer");
        return userAnswer.GetComponent<InputController>();
    }

    private ColorController GetBackgroundColorController()
    {
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        return background.GetComponent<ColorController>();
    }

    private void HandleCorrect(EquationController equation)
    {
        equation.HandleCorrect();
        GetBackgroundColorController().SetColors(new Color[] {
            GetBackgroundColorController().GetCurrentColor(),
            RandomizeBackgroundColor()
    }, 1f);
    }

    private void HandleIncorrect(EquationController equation)
    {
        equation.HandleIncorrect();
        GetBackgroundColorController().SetColors(new Color[] {
            new Color(255, 0, 0),
            bgColor
        }, GameSettings.Instance.TransitionSpeed);
    }

    private void CreateNewEquation()
    {
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }

    private Color RandomizeBackgroundColor()
    {
        return GameSettings.Instance.BGColors[(int) Mathf.Round(Random.Range(0, GameSettings.Instance.BGColors.Length))];
    }
}