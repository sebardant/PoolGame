using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CueBehaviourSettings", menuName = "ScriptableObjects/CueBehaviourSettings")]
public class CueBehaviourSettings : ScriptableObject
{
    [Tooltip("loading speed of the cue")]
    public float loadCueSpeed = 0.005f;
    [Tooltip("shooting speed of the cue")]
    public float releaseCueSpeed = 0.005f;
    [Tooltip("Animation curve of the loading ")]
    public AnimationCurve loadAnimationCurve;
    [Tooltip("Animation curve of the shooting ")]
    public AnimationCurve releaseAnimationCurve;
}
