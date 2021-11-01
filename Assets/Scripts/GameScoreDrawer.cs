using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreDrawer : MonoBehaviour, ISlideTransitionable
{
    public GameObject titleObject;
    public GameObject scoreParts;
    public GameObject total;
    public GameObject continueButton;

    void Start()
    {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(HandleClickContinue);

    }

    public void SlideIntoView()
    {
        Debug.Log("Sliding Score Drawer INTO View");
        GetComponent<SlideController>().LerpToPosition(new Vector3(0, 0, 0), GameSettings.Instance.TransitionSpeed);
    }

    public void SlideOutOfView()
    {
        Debug.Log("Sliding Score Drawer OUT OF view");
        GetComponent<SlideController>().LerpToPosition(new Vector3(900, 0, 0), GameSettings.Instance.TransitionSpeed);
    }

    public void RenderPlayResults(PlayResult result)
    {
        Text txt = scoreParts.GetComponent<Text>();

        EquationResult[] equations = result.GetAnsweredQuestions();

        RectTransform box = scoreParts.GetComponent<RectTransform>();
        box.sizeDelta = new Vector2(box.sizeDelta.x, 20 * equations.Length);

        string buffer = "";
        for (int i = 0; i < equations.Length; i++)
        {
            buffer = buffer + GetScorePartFromEquationResult(equations[i]) + '\n';
            Debug.Log(buffer);
        }

        txt.text = buffer;      
    }

    private string GetScorePartFromEquationResult(EquationResult e)
    {
        float[] terms = e.GetTerms();
        float term1 = terms[0];
        float term2 = terms[1];

        bool term2IsNegative = term2 < 0;
        string op = term2IsNegative ? "-" : "+";

        return (e.WasCorrect() ? "✓" : "X") + " " + term1 + " " + op + " " + Mathf.Abs(term2) + " = " + e.GetAnswer();
    }

    public void HandleClickContinue()
    {
        GameController.Instance.ShowGameMenu();
    }
}
