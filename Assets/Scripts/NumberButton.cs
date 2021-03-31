using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButton : MonoBehaviour
{
    public string value;
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        Text txt = gameObject.GetComponentInChildren<Text>();
        txt.text = value == "CLEAR" ? "CLR" : value;
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        InputController cont = transform.parent.gameObject.GetComponent<InputController>();
        cont.HandleButtonInput(value);
    }
}
