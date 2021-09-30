using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public GameObject WinTextGO;
    public GameObject GameOverTextGO;

    /// <summary>
    /// Initialize the panel with the right state
    /// </summary>
    /// <param name="_win">Did the player win ?</param>
    public void Init(bool _win)
    {
        WinTextGO.SetActive(_win);
        GameOverTextGO.SetActive(!_win);
        Display(true);
    }

    public void Display(bool display)
    {
        gameObject.SetActive(display);
    }

}
