using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    NONE,
    IN, 
    OUT
}
public class Trigger : MonoBehaviour
{
    public TriggerType triggerType = TriggerType.NONE;
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (triggerType)
        {
            case TriggerType.IN:
                GameEvents.Instance.BallIn(other.GetComponent<Ball>());
                break;
            case TriggerType.OUT:
                GameEvents.Instance.BallOut(other.GetComponent<Ball>());
                break;
            default:
                break;
        }
    }
}
