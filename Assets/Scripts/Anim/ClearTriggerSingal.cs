using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTriggerSingal : StateMachineBehaviour {

    public string TriggerName;
    public Type type;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(type==Type.Enter)
        animator.ResetTrigger(TriggerName);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	 override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (type == Type.Update)
            animator.ResetTrigger(TriggerName);
    }

 	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (type == Type.Exit)
            animator.ResetTrigger(TriggerName);
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
    public enum Type
    {
        Enter,
        Update,
        Exit
    }
}
