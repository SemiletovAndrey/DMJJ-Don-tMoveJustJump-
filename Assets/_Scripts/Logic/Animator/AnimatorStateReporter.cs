using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateReporter : StateMachineBehaviour
{
    private IAnimationStateReader _stateReader;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        FindReader(animator);
        if (_stateReader == null)
        {
            Debug.Log("Null State reader");
        }
        _stateReader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        FindReader(animator);

        _stateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void FindReader(Animator animator)
    {
        if (_stateReader != null)
            return;

        _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
}
