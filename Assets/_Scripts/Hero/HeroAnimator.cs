using System;
using System.Collections;
using UnityEngine;

public class HeroAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int LandHash = Animator.StringToHash("Land");

    private readonly int _idleStateHash = Animator.StringToHash("Armature|Idle");
    private readonly int _jumpStateHash = Animator.StringToHash("Base Layer.Jump.Armature|Jump");
    private readonly int _landStateHash = Animator.StringToHash("Base Layer.Jump.Armature|Land");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    public Animator Animator;

    public void PlayJump() => Animator.SetTrigger(JumpHash);

    public void PlayLand() => Animator.SetTrigger(LandHash);

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _jumpStateHash)
            state = AnimatorState.Jump;
        else if (stateHash == _landStateHash)
            state = AnimatorState.Land;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}

