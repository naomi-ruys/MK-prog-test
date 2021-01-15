using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clock : MonoBehaviour
{
    public Button removeButton;

    public void RemoveClock()
    {
        ClockManager.cm.RemoveClock(this);
        Destroy(gameObject);
    }

    public void DeactiveRemoveButton()
    {
        removeButton.interactable = false;
    }
}
