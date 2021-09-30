using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControllerType
{
    MOUSE,
    KEYBOARD
}

[CreateAssetMenu(fileName = "Controller", menuName = "ScriptableObjects/Controller")]

public class Controller: ScriptableObject
{
    public ControllerType controllerType;

    public KeyCode hitButton = KeyCode.Space;
    [Header("Keyboard Control Settings")]
    public float speed;
    public KeyCode leftRotationInput = KeyCode.Q;
    public KeyCode rightRotationInput = KeyCode.D;

    /// <summary>
    /// Calculate the new rotation of the cue according to the controller type
    /// </summary>
    /// <param name="_transform"></param>
    /// <returns></returns>
    public Quaternion Rotate(Transform _transform)
    {
        if (controllerType == ControllerType.KEYBOARD)
        {
            if (Input.GetKey(leftRotationInput))
            {
                return _transform.rotation * Quaternion.Euler(- new Vector3(0, 0, 1) * speed * Time.deltaTime);
            }
            if (Input.GetKey(rightRotationInput))
            {
                return _transform.rotation * Quaternion.Euler( new Vector3(0, 0, 1) * speed * Time.deltaTime);
            }
            return _transform.rotation;
        }
        else
        {
            Vector3 targ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targ.z = 0f;

            Vector3 objectPos = _transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
