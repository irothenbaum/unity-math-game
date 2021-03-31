using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }
    void Update()
    {
        // global game update logic goes here
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float GetUserAnswer()
    {
        GameObject userAnswer = GameObject.FindGameObjectWithTag("UserAnswer");
        InputController userInput = userAnswer.GetComponent<InputController>();
        return userInput.GetInputValue();
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE


}