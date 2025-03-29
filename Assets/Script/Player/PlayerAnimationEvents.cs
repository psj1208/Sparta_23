using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    void AnimationEnd()
    {
        (player.PlayerStateMachine.CurrentState as BaseState).AnimationEndEvent();
    }
}
