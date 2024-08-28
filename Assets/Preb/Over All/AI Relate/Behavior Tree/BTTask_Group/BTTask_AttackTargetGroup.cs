using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTTask_AttackTargetGroup : BTTask_Group
{
    [SerializeField] float moveAcceptableDistance = 2.5f;
    [SerializeField] float rotateAcceptableRadious = 10f;

    public BTTask_AttackTargetGroup(Behavior_Tree tree, float moveAcceptableDistance = 2.5f, float rotateAcceptableRadious=10f) : base(tree)
    {
        this.moveAcceptableDistance = moveAcceptableDistance;
        this.rotateAcceptableRadious = rotateAcceptableRadious;
    }

    protected override void ConstructTree(out BT_Node root)
    {
        Sequencer attackTargetSequencer = new Sequencer();
        BTTask_RotateTorwardTarget _RotateTorwardTarget = new BTTask_RotateTorwardTarget(tree, "Target", rotateAcceptableRadious);
        BTTask_MoveToTarget bTTask_MoveToTarget = new BTTask_MoveToTarget(tree, "Target", moveAcceptableDistance);
        BTTask_Attack bTTask_Attack = new BTTask_Attack(tree, "Target");
        attackTargetSequencer.AddChild(bTTask_MoveToTarget);
        attackTargetSequencer.AddChild(_RotateTorwardTarget);
        attackTargetSequencer.AddChild(bTTask_Attack);
        BlackBoard_Decorator AttackTargetDecorator = new BlackBoard_Decorator(tree, attackTargetSequencer,
                                                                              "Target",
                                                                              BlackBoard_Decorator.Condiction.KeyExist,
                                                                              BlackBoard_Decorator.NotifyRules.RunCondictionChanged,
                                                                              BlackBoard_Decorator.NotifyAbort.Both);


        root = AttackTargetDecorator;
    }
}
