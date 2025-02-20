using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CaneraFollow : MonoBehaviour
{
    public Transform target;          // 따라갈 대상(플레이어 등)
    public Vector3 offset;           // 카메라 오프셋
    public float smoothSpeed = 0.125f; // 부드러운 이동 속도

    public TilemapCollider2D tilemapCollider;

    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    // Tilemap Collider의 경계
    private Vector2 minBoundary;
    private Vector2 maxBoundary;

    void Start()
    {
        cam = GetComponent<Camera>();

        // OrthographicSize와 화면 비율을 통해 카메라 반너비, 반높이 계산
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;

        // Tilemap Collider가 있다면, Bounds에서 min, max 값을 얻어옴
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

        // 플레이어 위치 + 오프셋
        Vector3 desiredPosition = target.position + offset;

        // 카메라가 맵 밖으로 나가지 않도록 클램프
        float clampedX = Mathf.Clamp(desiredPosition.x,
            minBoundary.x + camHalfWidth,
            maxBoundary.x - camHalfWidth
        );

        float clampedY = Mathf.Clamp(desiredPosition.y,
            minBoundary.y + camHalfHeight,
            maxBoundary.y - camHalfHeight
        );

        // 카메라 z축은 유지
        Vector3 clampedPos = new Vector3(clampedX, clampedY, transform.position.z);

        // 부드러운 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPos, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
