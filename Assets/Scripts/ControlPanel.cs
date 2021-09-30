using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    private List<Controller> inputs;
    public GameObject layout;
    public InputChoiceButton buttonPrefab;
    public GameObject loadingAnimation;
    public GameObject panel;

    /// <summary>
    /// Initialize the panel
    /// </summary>
    /// <param name="list">List of controller avaiblable</param>
    public void Init(List<Controller> list)
    {
        Reset();
        inputs = list;
        foreach(Controller gi in inputs)
        {
            InputChoiceButton bt = Instantiate(buttonPrefab, layout.transform);
            bt.Init(gi);

        }
        Display(true);
    }

    /// <summary>
    /// Reset the panel to in initial state
    /// </summary>
    public void Reset()
    {
        panel.SetActive(true);
        loadingAnimation.SetActive(false);
        inputs = null;
        foreach(Transform go in layout.transform)
        {
            go.gameObject.SetActive(false);
            Destroy(go.gameObject);
        }
    }
    
    /// <summary>
    /// Do or do not display the panel
    /// </summary>
    /// <param name="_display"></param>
    public void Display(bool _display)
    {
        gameObject.SetActive(_display);
    }
}
