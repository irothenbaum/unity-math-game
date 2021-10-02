using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicModeController : ModeController
{
    public GameObject equationPrefab;
    private int equationsLeft = 10;
    private int guessesPerEquation = 3;

    private PlayResult result;

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
        bool noMoreGuessesLeft = equation.GetNumberOfGuesses() == this.guessesPerEquation;

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
        // construct our play result container
        result = new PlayResult();

        // create new equation
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }

    public override PlayResult EndGame()
    {
        return result;
    }

    void OnGUI()
    {
        string score = "";
        if (result != null)
        {
            score = "" + result.GetBaseScore();
        }
        GUI.Label(new Rect(10, 10, 100, 20), score);
    }
}
