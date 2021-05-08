using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeController : MonoBehaviour
{
    public abstract void StartGame();
    public abstract void HandleCorrect(EquationController equation);
    public abstract void HandleIncorrect(EquationController equation);
    public abstract PlayResult EndGame();
}
