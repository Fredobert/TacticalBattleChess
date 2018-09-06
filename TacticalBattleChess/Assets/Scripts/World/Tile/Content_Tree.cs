using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Tree: Content
{

    private void OnEnable()
    {
        type = GameHelper.ContentType.Tree;
    }

    public override void Effect(int z, GameHelper.EffectType type)
    {
        if (type == GameHelper.EffectType.Fire)
        {
            Remove();
        }
        base.Effect(z, type);
    }
}
