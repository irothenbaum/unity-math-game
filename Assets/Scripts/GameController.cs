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

    private void HandleCorrect(EquationController equation)
    {
        equation.HandleCorrect();
    }

    private void HandleIncorrect(EquationController equation)
    {
        equation.HandleIncorrect();
    }

    private void CreateNewEquation()
    {
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }
}