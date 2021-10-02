using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationResult
{
    float[] terms;
    float answer;
    bool wasCorrect;

    public EquationResult(float[] terms, float answer, bool wasCorrect)
    {
        this.terms = terms;
        this.answer = answer;
        this.wasCorrect = wasCorrect;
    }

    public float[] GetTerms()
    {
        return this.terms;
    }

    public float GetAnswer()
    {
        return this.answer;
    }

    public bool WasCorrect()
    {
        return this.wasCorrect;
    }

    public static EquationResult CreateFromEquation(EquationController equation, bool wasCorrect)
    {
        return new EquationResult(equation.GetTerms(), equation.GetAnswer(), wasCorrect);
    }
}
