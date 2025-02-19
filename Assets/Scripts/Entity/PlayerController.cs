using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    protected override void HandleAction()
    {
        // 입력을 받으면 플레이어 이동
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 이동한 경우에만 lookDirection 갱신
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }
    }
}
