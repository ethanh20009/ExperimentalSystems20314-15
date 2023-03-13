using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    public Animator animator;
    public void SetAnimation()
    {
        if (animator.GetBool("selected"))
        {
            animator.SetBool("selected", false);
        }
        else
        {
            animator.SetBool("selected", true);
        }
    }
}
