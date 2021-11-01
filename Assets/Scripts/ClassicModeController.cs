using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicModeController : ModeController
{
    public GameObject equationPrefab;
    private int equationsLeft;

    private PlayResult result;
    private GameObject equation;

    private bool isGameOn = false;

    public void Update()
    {
        if (this.isGameOn && this.equationsLeft == 0)
        {

            HandleEndGame();
        }
    }

    public override void HandleCorrect(EquationController equation)
    {
        equation.DisplayCorrect();
        result.ApplyEquationResult(EquationResult.CreateFromEquation(equation, true));
        equation.SelectNewAnswer();
        equationsLeft--;
    }

    public override void HandleIncorrect(EquationController equation)
    {
        // This is their guess if the've guessed the max number of times allowed
        bool noMoreGuessesLeft = equation.GetNumberOfGuesses() == GameSettings.Instance.ClassicMode_Guesses;

        // we destroy this equation if it's their last guess
        equation.DisplayIncorrect(noMoreGuessesLeft);

        if (noMoreGuessesLeft)
        {
            result.ApplyEquationResult(EquationResult.CreateFromEquation(equation, false));
            equation.SelectNewAnswer();
            equationsLeft--;
        }
    }

    public override void StartGame()
    {
        this.isGameOn = true;
        this.equationsLeft = GameSettings.Instance.ClassicMode_Questions;

        // construct our play result container
        result = new PlayResult();

        // create new equation
        this.equation = Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }

    public void HandleEndGame()
    {
        GameController.Instance.EndGame();
        Destroy(this.equation);
        this.isGameOn = false;
    }

    public override PlayResult GetPlayResult()
    {
        return result;
    }
}
