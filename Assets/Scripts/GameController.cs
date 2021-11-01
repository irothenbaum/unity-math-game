/*
TODO:
- Score
    - Displaying total score
- Classic game mode
    - {Scoring}
- Estimation game mode
    - {Scoring}
    - Interface
    - UI for equations
    - Spawn logic
- Frenzy game mode
    - {Scoring}
    - Spawn logic
- Infinite game mode
    - {Scoring}
    - UI for equations
    - Spawn logic
- Settings
    - UI
    - Settings object mutations
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject classicGame;

    private string gameMode;
    private ModeController modeController;

    const string MODE_CLASSIC = "CLASSIC";
    const string MODE_INFINITE = "INFINITE";
    const string MODE_ESTIMATE = "ESTIMATE";
    const string MODE_FRENZY = "FRENZY";

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
        GetBackgroundColorController().SetColor(RandomizeBackgroundColor());
        ShowGameMenu();
    }
    void Update()
    {
    }

    // ------------------------------------------------------------------------------------------------------
    // PUBLIC

    public float GetUserAnswer()
    {
        return GetUserInputController().GetInputValue();
    }

    public void CheckAnswerToEquation(EquationController equation)
    {
        float userAnswer = GetUserAnswer();
        float correctAnswer = equation.GetAnswer();
        if (userAnswer == correctAnswer)
        {
            HandleCorrect(equation);
        }
        else
        {
            HandleIncorrect(equation);
        }

        GetUserInputController().ClearAnswer();
    }

    // ------------------------------------------------------------------------------------------------------
    // PRIVATE   

    private void HandleCorrect(EquationController equation)
    {
        modeController.HandleCorrect(equation);
        GetBackgroundColorController().SetColors(new Color[] {
            GetBackgroundColorController().GetCurrentColor(),
            RandomizeBackgroundColor()
        }, GameSettings.Instance.TransitionSpeed);
    }

    private void HandleIncorrect(EquationController equation)
    {
        modeController.HandleIncorrect(equation);
        GetBackgroundColorController().SetColors(new Color[] {
            new Color(1, 0, 0),
            GetBackgroundColorController().GetCurrentColor()
        }, GameSettings.Instance.TransitionSpeed);
    }

    private Color RandomizeBackgroundColor()
    {
        return GameSettings.Instance.BGColors[(int) Mathf.Floor(Random.Range(0, GameSettings.Instance.BGColors.Length))];
    }

    public void ShowGameMenu()
    {
        ClearHUD();
        GetGameMenuController().SlideIntoView();
        GetUserInputController().SlideOutOfView();
        GetGameScoreDrawer().SlideOutOfView();
        GetSettingsMenuController().SlideOutOfView();
    }

    public void ShowSettingsMenu()
    {
        GetGameMenuController().SlideOutOfView();
        GetSettingsMenuController().SlideIntoView();
    }

    public void ShowGameResults(PlayResult results)
    {
        ClearHUD();
        GetUserInputController().SlideOutOfView();
        GameScoreDrawer drawer = GetGameScoreDrawer();
        drawer.RenderPlayResults(results);
        drawer.SlideIntoView();
    }

    public void ClearHUD()
    {
        SetScoreText("");
    }

    public void EndGame()
    {
        if (this.modeController != null)
        {
            PlayResult results = this.modeController.GetPlayResult();
            Debug.Log(results);
            ShowGameResults(results);
        }
        this.gameMode = null;
    }

    public void StartGame(string mode)
    {
        Debug.Log("Stating game mode " + mode);

        this.gameMode = mode;

        switch(this.gameMode)
        {
            case MODE_CLASSIC:
                // TODO: create the appropriate mode controller logic
                this.modeController = classicGame.GetComponent<ClassicModeController>();
                break;

            default:
                Debug.LogError("Invalid game mode " + mode);
                this.modeController = null;
                this.gameMode = null;
                throw new System.NullReferenceException();
        }

        this.modeController.StartGame();
        SetScoreText("0");

        GetUserInputController().SlideIntoView();
        GetGameMenuController().SlideOutOfView();
        GetSettingsMenuController().SlideOutOfView();
    }

    void OnGUI()
    {
        if (this.modeController == null || this.modeController.GetPlayResult() == null)
        {
            return;
        }

        SetScoreText("" + this.modeController.GetPlayResult().GetBaseScore());
    }

    // ----------------------------------------------------------------------------------------
    // SIMPLE GETTERS

    private InputController GetUserInputController()
    {
        GameObject userAnswer = GameObject.FindGameObjectWithTag("UserAnswer");
        return userAnswer.GetComponent<InputController>();
    }

    private GameMenuController GetGameMenuController()
    {
        GameObject gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
        return gameMenu.GetComponent<GameMenuController>();
    }

    private SettingsMenuController GetSettingsMenuController()
    {
        GameObject settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu");
        return settingsMenu.GetComponent<SettingsMenuController>();
    }

    private ColorController GetBackgroundColorController()
    {
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        return background.GetComponent<ColorController>();
    }

    private GameScoreDrawer GetGameScoreDrawer()
    {
        GameObject gameScore = GameObject.FindGameObjectWithTag("GameResults");
        return gameScore.GetComponent<GameScoreDrawer>();
    }

    private void SetScoreText(string score)
    {
        GameObject scoreHUD = GameObject.FindGameObjectWithTag("HUD_Score");
        scoreHUD.GetComponent<Text>().text = score;
    }
}