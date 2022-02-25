using UnityEngine;
public class TriggerAnimation : Damageable
{
    public Animator[] targetAnim;

    protected override void OnLifeLost()
    {
        base.OnLifeLost();
        foreach(Animator anim in targetAnim)
        {
            anim.SetTrigger("Start");
        }
    }
}
