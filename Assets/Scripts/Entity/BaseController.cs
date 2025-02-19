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

    protected Vector2 lookDirection = Vector2.right;

    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5;
        if (direction != Vector2.zero)
        {
            lookDirection = direction.normalized; // 마지막 이동 방향을 보고있게 함
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero)  return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //  보고있는 각도

        // 왼쪽은 오른쪽 flip시켜서 사용할거임
        rightSprite.SetActive(false);
        upSprite.SetActive(false);
        downSprite.SetActive(false);

        if (Mathf.Abs(rotZ) <= 45f) // 오른쪽
        {
            rightSprite.SetActive(true);
            rightSprite.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Mathf.Abs(rotZ) > 135f) // 왼쪽
        {
            rightSprite.SetActive(true);
            rightSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rotZ > 45f && rotZ < 135f) // 위쪽
        {
            upSprite.SetActive(true);
        }
        else if (rotZ < -45f && rotZ > -135f) // 아래쪽
        {
            downSprite.SetActive(true);
        }

        if (accessoryPivot != null)
        {
            accessoryPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

}
