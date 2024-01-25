using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] string currentAnim;

    [SerializeField] Animator anim;
    public void SetAnimAtk()
    {
        SetAnim(Constant.ANIM_ATTACK);
    }
    public void SetAnimMove()
    {
        SetAnim(Constant.ANIM_RUN);

    }
    public void SetAnimDie()
    {
        SetAnim(Constant.ANIM_DIE);

    }
    public void SetAnimIdle()
    {
        SetAnim(Constant.ANIM_IDLE);

    }
    void SetAnim(string animStr)
    {
        if(currentAnim != animStr)
        {
            anim.SetTrigger(animStr);
        }
    }
}
