using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings> {
    public float MaximumAnswerValue = 100.0f;
    public int MaximumNumberOfDecimals = 0;
    public Color[] BGColors = new Color[] {
        new Color(0.23f, 0.3f, 0.63f),
        new Color(0.55f, 0.23f, 0.63f),
        new Color(0.34f, 0.51f, 0.26f),
        new Color(0.45f, 0.38f, 0.2f),
        new Color(0.15f, 0.5f, 0.48f),
        new Color(0.48f, 0.18f, 0.17f),
    };
    public float TransitionSpeed = 0.5f;
    public int ClassicMode_Questions = 5;
    public int ClassicMode_Guesses = 3;
}