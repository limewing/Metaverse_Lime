using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CaneraFollow : MonoBehaviour
{
    public Transform target;          // ���� ���(�÷��̾� ��)
    public Vector3 offset;           // ī�޶� ������
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�

    public TilemapCollider2D tilemapCollider;

    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    // Tilemap Collider�� ���
    private Vector2 minBoundary;
    private Vector2 maxBoundary;

    void Start()
    {
        cam = GetComponent<Camera>();

        // OrthographicSize�� ȭ�� ������ ���� ī�޶� �ݳʺ�, �ݳ��� ���
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;

        // Tilemap Collider�� �ִٸ�, Bounds���� min, max ���� ����
        if (tilemapCollider != null)
        {
            Bounds bounds = tilemapCollider.bounds;

            Vector3 adjustedMin = new Vector3(bounds.min.x + 1.0f, bounds.min.y + 1.0f);
            Vector3 adjustedMax = new Vector3(bounds.max.x - 1.0f, bounds.max.y - 1.0f);

            minBoundary = adjustedMin;
            maxBoundary = adjustedMax;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // �÷��̾� ��ġ + ������
        Vector3 desiredPosition = target.position + offset;

        // ī�޶� �� ������ ������ �ʵ��� Ŭ����
        float clampedX = Mathf.Clamp(desiredPosition.x,
            minBoundary.x + camHalfWidth,
            maxBoundary.x - camHalfWidth
        );

        float clampedY = Mathf.Clamp(desiredPosition.y,
            minBoundary.y + camHalfHeight,
            maxBoundary.y - camHalfHeight
        );

        // ī�޶� z���� ����
        Vector3 clampedPos = new Vector3(clampedX, clampedY, transform.position.z);

        // �ε巯�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPos, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
