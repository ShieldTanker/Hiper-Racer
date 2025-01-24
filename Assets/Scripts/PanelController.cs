using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public delegate void StartPanelDelegate();
    public StartPanelDelegate OnButtonClick;

    public void OnClickButton()
    {
        OnButtonClick.Invoke();
    }
}
