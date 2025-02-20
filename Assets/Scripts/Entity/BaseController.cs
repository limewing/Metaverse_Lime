using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform accessoryPivot;

    [SerializeField] private GameObject rightSprite;
    [SerializeField] private GameObject upSprite;
    [SerializeField] private GameObject downSprite;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.down;

    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();

        if (animationHandler == null)
            Debug.Log("AnimationHandler is not exist");
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        Rotate(lookDirection);
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5;
        if (direction != Vector2.zero)
        {
            lookDirection = direction.normalized; // ������ �̵� ������ �����ְ� ��
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    protected void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotZ < 0) rotZ += 360;
        GameObject newActiveSprite = null;
        Animator selectedAnimator = null;

        if (rotZ >= 315 || rotZ < 45) // ������
        {
            newActiveSprite = rightSprite;
            rightSprite.GetComponent<SpriteRenderer>().flipX = false;
            selectedAnimator = rightSprite.GetComponent<Animator>();
        }
        else if (rotZ >= 135 && rotZ < 225) // ����
        {
            newActiveSprite = rightSprite;
            rightSprite.GetComponent<SpriteRenderer>().flipX = true;
            selectedAnimator = rightSprite.GetComponent<Animator>();
        }
        else if (rotZ >= 45 && rotZ < 135) // ����
        {
            newActiveSprite = upSprite;
            selectedAnimator = upSprite.GetComponent<Animator>();
        }
        else if (rotZ >= 225 && rotZ < 315) // �Ʒ���
        {
            newActiveSprite = downSprite;
            selectedAnimator = downSprite.GetComponent<Animator>();
        }

        // �̹� ���� ��������Ʈ�� Ȱ��ȭ �Ǿ� �ִٸ� ���¸� �����Ͽ� �ִϸ��̼� ���� ����
        if (animationHandler.currentAnimator != null && animationHandler.currentAnimator.gameObject == newActiveSprite)
        {
            // ������ ��������Ʈ�� ��� flipX ���� ����� �� �����Ƿ� ������Ʈ���� �� ����
            return;
        }

        // ������ �ٲ���� ������ ��������Ʈ ��ȯ
        rightSprite.SetActive(false);
        upSprite.SetActive(false);
        downSprite.SetActive(false);

        if (newActiveSprite != null)
        {
            newActiveSprite.SetActive(true);
        }

        if (animationHandler.currentAnimator != selectedAnimator)
        {
            animationHandler.SetAnimator(selectedAnimator);
        }

        if (accessoryPivot != null)
        {
            accessoryPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

}
