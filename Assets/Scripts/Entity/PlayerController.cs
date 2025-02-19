using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    protected override void HandleAction()
    {
        // �Է��� ������ �÷��̾� �̵�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // �̵��� ��쿡�� lookDirection ����
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }
    }
}
