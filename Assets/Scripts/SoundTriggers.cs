using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueEnd : AkTriggerBase
{
    public void Trigger()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}


public class TriggerFootstep : AkTriggerBase
{
    public void Trigger()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}

public class TriggerBounce : AkTriggerBase
{
    public void Trigger()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}