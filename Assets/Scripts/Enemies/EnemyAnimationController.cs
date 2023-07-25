using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;

    public string deathAnimationName = "Death";
    public string walkAnimationName = "Walk";
    public string idleAnimationName = "Idle";
    public string attackAnimationName = "Attack";

    public bool deathAnimationIsTrigger = true;
    public bool walkAnimationIsTrigger = false;
    public bool idleAnimationIsTrigger = false;
    public bool attackAnimationIsTrigger = false;

    // Método para configurar los nombres y tipos de animaciones desde el Inspector
    public void SetAnimationParameters(string deathName, bool deathIsTrigger,
                                        string walkName, bool walkIsTrigger,
                                        string idleName, bool idleIsTrigger,
                                        string attackName, bool attackIsTrigger)
    {
        deathAnimationName = deathName;
        deathAnimationIsTrigger = deathIsTrigger;
        walkAnimationName = walkName;
        walkAnimationIsTrigger = walkIsTrigger;
        idleAnimationName = idleName;
        idleAnimationIsTrigger = idleIsTrigger;
        attackAnimationName = attackName;
        attackAnimationIsTrigger = attackIsTrigger;
    }

    private void Start()
    {
        if (animator == null)
        {
            // Obtener el componente Animator del GameObject si no se asigna uno en el Inspector
            animator = GetComponent<Animator>();
        }
    }

    public void PlayDeathAnimation()
    {
        if (deathAnimationIsTrigger)
        {
            animator.SetTrigger(deathAnimationName);
        }
        else
        {
            animator.Play(deathAnimationName);
        }
    }

    public void PlayWalkAnimation()
    {
        if (walkAnimationIsTrigger)
        {
            animator.SetTrigger(walkAnimationName);
        }
        else
        {
            animator.SetBool(walkAnimationName, true);
        }

        // Desactivar otras animaciones de movimiento (Idle y Attack)
        animator.SetBool(idleAnimationName, false);
        animator.SetBool(attackAnimationName, false);
    }

    public void PlayIdleAnimation()
    {
        if (idleAnimationIsTrigger)
        {
            animator.SetTrigger(idleAnimationName);
        }
        else
        {
            animator.SetBool(idleAnimationName, true);
        }

        // Desactivar otras animaciones de movimiento (Walk y Attack)
        animator.SetBool(walkAnimationName, false);
        animator.SetBool(attackAnimationName, false);
    }

    public void PlayAttackAnimation()
    {
        if (attackAnimationIsTrigger)
        {
            animator.SetTrigger(attackAnimationName);
        }
        else
        {
            animator.SetBool(attackAnimationName, true);
        }

        // Desactivar otras animaciones de movimiento (Walk e Idle)
        animator.SetBool(walkAnimationName, false);
        animator.SetBool(idleAnimationName, false);
    }
}