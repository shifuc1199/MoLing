using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static bool IsAnim(this Animator anim,string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public static bool CurrentAnimComplete(this Animator anim)
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }

    public static bool CurrentAnimComplete(this Animator anim,string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
