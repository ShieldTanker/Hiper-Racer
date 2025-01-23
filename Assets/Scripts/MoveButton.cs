using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    private bool _isDown;
    public delegate void MovebuttonDelegate();
    public event MovebuttonDelegate OnMoveButtondown;

    private void Update()
    {
        if (_isDown)
        {
            OnMoveButtondown?.Invoke();
        }
    }

    public void ButtonDown()
    {
        _isDown = true;
    }

    public void ButtonUp()
    {
        _isDown=false;
    }
}
