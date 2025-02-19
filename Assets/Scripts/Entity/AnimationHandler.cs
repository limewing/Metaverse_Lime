using System.Collections;
using System.Collections.Generic;
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
        if (animator == null || animator == currentAnimator) return; // 같은 애니메이터거나 null이면 변경하지 않음
        currentAnimator = animator;
    }

    public void Move(Vector2 obj)
    {
        if (currentAnimator == null)
        {
            Debug.LogError("No Animator Activated.");
            return;
        }

        bool isMovingState = obj.magnitude > 0.1f;
        Debug.Log($" {currentAnimator.gameObject.name} 애니메이션: IsMove = {isMovingState}");
        currentAnimator.SetBool(isMoving, isMovingState);
    }
}
