using UnityEngine;

public class AnimationGearController : MonoBehaviour
{
    [SerializeField] private Animator gearAnimator;
    [SerializeField] private Animator dropdownAnimator;
    public void ExplosionGearOn()
    {
        if (!gearAnimator.GetBool("Explosion"))
        {
            gearAnimator.SetBool("Explosion", true);
            dropdownAnimator.SetBool("Dropdown", true);
        }
        else
        {
            gearAnimator.SetBool("Explosion", false);
            dropdownAnimator.SetBool("Dropdown", false);
        }
    }
}
