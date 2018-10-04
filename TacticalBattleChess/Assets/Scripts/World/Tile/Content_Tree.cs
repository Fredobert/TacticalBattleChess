using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Tree: Content
{

    private void OnEnable()
    {
        type = GameHelper.ContentType.Tree;
    }

    public override void Effect(int z, GameHelper.AbilityType type)
    {
        if (type == GameHelper.AbilityType.Fire)
        {
            Remove();
        }
        base.Effect(z, type);
    }
}
