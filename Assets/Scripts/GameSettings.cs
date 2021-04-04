using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings> {
    public float MaximumAnswerValue = 100.0f;
    public int MaximumNumberOfDecimals = 0;
    public Color[] BGColors = new Color[] {
        new Color(59, 78, 161),
        new Color(142, 73, 163),
        new Color(87, 122, 71),
        new Color(120, 97, 52),
        new Color(38, 122, 118),
        new Color(110, 49, 44),
    };
    public float TransitionSpeed = 0.5f;
}