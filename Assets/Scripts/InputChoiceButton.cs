using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChoiceButton : MonoBehaviour
{
    Controller input;
    Button button;

    public void Init(Controller _input)
    {
        button = GetComponent<Button>();
        input = _input;
        button.GetComponentInChildren<Text>().text = input.controllerType.ToString();
        button.onClick.AddListener(
            delegate 
            { 
                GameEvents.Instance.ControlSelected(input); 
            });
    }
}
