using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CueBehaviour : MonoBehaviour
{
    public Vector3 newDirection;
    public Slider slider;
    private float m_value = 0;
    public CueBehaviourSettings cueBehaviourSettings;
    public AnimationCurve animationCurve1;
    public AnimationCurve animationCurve2;
    private bool m_isUsingCue;
    private float m_power = 0;
    public LineRenderer lineRenderer;
    public Transform startPoint;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        slider.value = 0;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        RaycastHit2D hit = Physics2D.Raycast(startPoint.position, transform.right);
        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    /// <summary>
    /// Update the cue state to animate it
    /// </summary>
    /// <param name="_value">Is the player using pushing the loading button</param>
    /// <returns></returns>
    public float IsUsingCue(bool _value)
    { 
        m_isUsingCue = _value;
        if (m_isUsingCue)
        {
            Load(true);
            m_power = slider.value;
        }
        else
        {
            Load(false);
            if(slider.value <= 0 && m_power != 0)
            {
                float pow = m_power;
                m_power = 0;
                return pow;
            }
        }
        return -1;
    }

    /// <summary>
    /// Animate the cue when loading and releasing the shot. Update the value of a slider. 
    /// </summary>
    /// <param name="_load">Is the cue loading and releasing the shot</param>
    public void Load(bool _load)
    {
        float value2 = 0;
        if (_load)
        {
            m_value = Mathf.Clamp(m_value + cueBehaviourSettings.loadCueSpeed, 0, 1);
            value2 = cueBehaviourSettings.loadAnimationCurve.Evaluate(m_value);
        }
        else
        {
            m_value = Mathf.Clamp(m_value - cueBehaviourSettings.releaseCueSpeed, 0, 1);
            value2 = cueBehaviourSettings.releaseAnimationCurve.Evaluate(m_value);
        }
        slider.value = value2;
    }
}
