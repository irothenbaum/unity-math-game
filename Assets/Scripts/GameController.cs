using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameObject equationPrefab;

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

    private void AddPoints(float basePoints, float multiplier = 1f)
    {

    }

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
        }, GameSettings.Instance.TransitionSpeed);
        AddPoints(Mathf.Abs(equation.GetAnswer()));
    }

    private void HandleIncorrect(EquationController equation)
    {
        equation.HandleIncorrect();
        GetBackgroundColorController().SetColors(new Color[] {
            new Color(1, 0, 0),
            GetBackgroundColorController().GetCurrentColor()
        }, GameSettings.Instance.TransitionSpeed);
    }

    private void CreateNewEquation()
    {
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }

    private Color RandomizeBackgroundColor()
    {
        return GameSettings.Instance.BGColors[(int) Mathf.Floor(Random.Range(0, GameSettings.Instance.BGColors.Length))];
    }
}