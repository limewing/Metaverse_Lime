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
        // �ִϸ����͵��� ���������� �Ҵ���� �ʾ��� ��� �ڵ����� ã��
        if (rightAnimator == null || upAnimator == null || downAnimator == null)
        {
            Debug.LogWarning("Animator�� �������� �Ҵ���� ����.");
            rightAnimator = transform.Find("RightSprite")?.GetComponent<Animator>();
            upAnimator = transform.Find("UpSprite")?.GetComponent<Animator>();
            downAnimator = transform.Find("DownSprite")?.GetComponent<Animator>();
        }

        // �⺻�� down
        currentAnimator = downAnimator;
    }

    public void SetAnimator(Animator animator)
    {
        if (animator == null || animator == currentAnimator) return; // ���� �ִϸ����Ͱų� null�̸� �������� ����
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
        Debug.Log($" {currentAnimator.gameObject.name} �ִϸ��̼�: IsMove = {isMovingState}");
        currentAnimator.SetBool(isMoving, isMovingState);
    }
}
