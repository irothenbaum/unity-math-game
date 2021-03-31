using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameObject equationPrefab;
    private bool canCreate = true;

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
        canCreate = true;
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float GetUserAnswer()
    {
        GameObject userAnswer = GameObject.FindGameObjectWithTag("UserAnswer");
        InputController userInput = userAnswer.GetComponent<InputController>();
        return userInput.GetInputValue();
    }

    public void CheckAnswerToEquation(EquationController equation)
    {
        float userAnswer = GetUserAnswer();
        float correctAnswer = equation.GetAnswer();
        if (userAnswer == correctAnswer)
        {
            HandleCorrect();
        }
        else
        {
            HandleIncorrect();
        }

        Destroy(equation.gameObject);

        if (canCreate)
        {
            CreateNewEquation();
        }
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE

    private void HandleCorrect()
    {
        Debug.Log("Correct!!");
    }

    private void HandleIncorrect()
    {
        Debug.Log("Incorrect :(");
    }

    private void CreateNewEquation()
    {
        Debug.Log("CREATING...");
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
        canCreate = false;
    }
}