using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionArguments
{
    private ActionType key;

    public ActionType Key => key;

    public ActionArguments(ActionType key)
    {
        this.key = key;
    }

}
