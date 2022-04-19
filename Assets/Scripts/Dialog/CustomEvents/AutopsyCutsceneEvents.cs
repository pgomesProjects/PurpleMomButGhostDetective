using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopsyCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;
    [SerializeField] private GameObject autopsyReport;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        switch (indexNumber)
        {
            case 2:
                ShowAutopsy();
                break;
        }
    }

    public void ShowAutopsy()
    {
        autopsyReport.SetActive(true);
    }

    public void HideAutopsy()
    {
        autopsyReport.SetActive(false);
    }

    public override void CustomOnEventComplete()
    {
        HideAutopsy();
    }
}
