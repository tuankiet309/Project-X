using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_Behavior_Tree : Behavior_Tree
{
    protected override void ConstructTree(out BT_Node root_Node)
    {
        Selector RootSelector = new Selector();
        RootSelector.AddChild(new BTTask_AttackTargetGroup(this,9f,10f,4f));
        RootSelector.AddChild(new BTTask_MoveToLastLocationGroup(this));
        RootSelector.AddChild(new BTTask_PatrollingGroup(this));
        root_Node = RootSelector;
    }
}
