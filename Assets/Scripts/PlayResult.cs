using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayResult
{
    private ArrayList equationsArray;

    public PlayResult()
    {
        equationsArray = new ArrayList();
    }

    public void ApplyEquationResult(EquationResult equation)
    {
        equationsArray.Add(equation);
    }

    public int GetBaseScore()
    {
        float score = 0;
        for (int i = 0; i < equationsArray.Count; i++)
        {
            EquationResult thisEquation = (EquationResult) equationsArray[i];
            if (thisEquation.WasCorrect())
            {
                float[] terms = thisEquation.GetTerms();
                float thisScore = Mathf.Abs(terms[0]) + Mathf.Abs(terms[1]) + Mathf.Abs(thisEquation.GetAnswer());

                score = score + thisScore;
            }
        }

        return (int) Mathf.Ceil(score);
    }

    public EquationResult[] GetAnsweredQuestions()
    {
        return (EquationResult[]) equationsArray.ToArray();
    }
}
