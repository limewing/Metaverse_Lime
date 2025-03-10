using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("IsMove");

    [SerializeField] private Animator rightAnimator;
    [SerializeField] private Animator upAnimator;
    [SerializeField] private Animator downAnimator;

    public Animator currentAnimator { get; private set; }

    protected virtual void Awake()
    {
        // 애니메이터들이 정상적으로 할당되지 않았을 경우 자동으로 찾음
        if (rightAnimator == null || upAnimator == null || downAnimator == null)
        {
            Debug.LogWarning("Animator가 수동으로 할당되지 않음.");
            rightAnimator = transform.Find("RightSprite")?.GetComponent<Animator>();
            upAnimator = transform.Find("UpSprite")?.GetComponent<Animator>();
            downAnimator = transform.Find("DownSprite")?.GetComponent<Animator>();
        }

        // 기본값 down
        currentAnimator = downAnimator;
    }

    public void SetAnimator(Animator animator)
    {
        if (animator == null) return;
        if (animator == currentAnimator)
        {
            Debug.Log($"같은 Animator {animator.gameObject.name}이므로 변경하지 않음.");
            return; // 같은 애니메이터라면 변경하지 않음
        }

        Debug.Log($"애니메이터 변경: {animator.gameObject.name}");
        currentAnimator = animator;
    }

    public void Move(Vector2 obj)
    {
        if (currentAnimator == null)
        {
            Debug.LogError("No Animator Activated.");
            return;
        }

        if (!currentAnimator.parameters.Any(p => p.name == "IsMove"))
        {
            return;
        }

        bool isMovingState = obj.magnitude > 0.1f;
        currentAnimator.SetBool(isMoving, isMovingState);
    }
}
