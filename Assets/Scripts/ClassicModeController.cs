using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicModeController : ModeController
{
    public GameObject equationPrefab;

    private PlayResult result;

    public override void HandleCorrect(EquationController equation)
    {
        equation.HandleCorrect();
        result.AddPoints(equation.GetAnswer());
    }

    public override void HandleIncorrect(EquationController equation)
    {
        equation.HandleIncorrect();
    }

    public override void StartGame()
    {
        // create new equation
        Instantiate(equationPrefab, transform.position, Quaternion.identity);
    }

    public override PlayResult EndGame()
    {
        return result;
    }
}
