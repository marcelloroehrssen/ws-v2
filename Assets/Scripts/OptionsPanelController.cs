using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelController : MonoBehaviour
{
    public Animator optionAnimator;

    public void OptionsPanelEnter()
    {
        optionAnimator.SetTrigger("OptionsPanelEnter");
    }

    public void OptionsPanelExit()
    {
        optionAnimator.SetTrigger("OptionsPanelExit");
    }
}
