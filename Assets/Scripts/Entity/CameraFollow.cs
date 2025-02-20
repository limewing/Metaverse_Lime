using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CaneraFollow : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;          // ���� ���(�÷��̾� ��)
    public Vector3 offset;           // ī�޶� ������
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�

    [Header("Tilemap Collider (��� ����)")]
    public TilemapCollider2D tilemapCollider; // ���� TilemapCollider2D �Ҵ�

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
            minBoundary = bounds.min;  // (xMin, yMin)
            maxBoundary = bounds.max;  // (xMax, yMax)
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
