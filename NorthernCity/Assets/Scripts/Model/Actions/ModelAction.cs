using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Invalid = -1,
    Count
}


public class ModelActionData
{
    public ActionType Key;

    public ModelActionData(ActionType key)
    {
        Key = key;
    }
}

public class ModelAction 
{
    public ActionType Key;
    
    public ModelAction(ActionType key)
    {
        Key = key;
    }

    public virtual void Act(ActionArguments actionArguments, Model model)
    {
    }

    public virtual void Expand(ExpandActionArguments expandAction)
    {
    }

    public bool EqualsData(ModelActionData data)
    {
        return true;
    }

    public virtual ModelActionData GetData() => new ModelActionData(Key);
}
