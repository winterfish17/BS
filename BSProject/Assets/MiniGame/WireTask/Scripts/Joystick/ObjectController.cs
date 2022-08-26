using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public JoystickValue value;

    void Update()
    {
        transform.Translate(value.joyTouch);
    }
}
