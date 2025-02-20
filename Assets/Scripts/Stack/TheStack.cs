using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f;
    private const float MovingBoundsSize = 3f;
    private const float StackMovingSpeed = 4.0f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin = 0.1f;

    public GameObject originBlock = null;

    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    Transform lastBlock = null;
    float blockTransition = 0f;

    int stackCount = -1;
    public int Score { get { return stackCount; } }

    int comboCount = 0;
    public int Combo { get { return comboCount; } }

    private int maxCombo = 0;
    public int MaxCombo { get => maxCombo; }

    public Color prevColor;
    public Color nextColor;

    int bestScore = 0;
    public int BestScore { get => bestScore; }

    int bestCombo = 0;
    public int BestCombo { get => bestCombo; }

    private const string BestScoreKey = "BestScore";
    private const string BestComboKey = "BestCombo";

    private bool isGameOver = true;

    void Start()
    {
        if (originBlock == null)
        {
            Debug.Log("OriginBlock is NULL");
            return;
        }

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        bestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

        prevBlockPosition = Vector3.zero;
        Spawn_Block();
    }

    void Update()
    {
        if (isGameOver)
            return;

        // 클릭 시 PlaceBlock() → 성공하면 다음 블록 스폰
        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                Spawn_Block();
            }
            else
            {
                // 게임 오버
                Debug.Log("GameOver");
                UpdateScore();
                isGameOver = true;
                GameOverEffect();
                StackUIManager.Instance.SetScoreUI();
            }
        }

        MoveBlock();

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            StackMovingSpeed * Time.deltaTime
        );
    }

    bool Spawn_Block()
    {
        // 이전 블록 위치 저장
        if (lastBlock != null)
            prevBlockPosition = lastBlock.localPosition;

        GameObject newBlock = Instantiate(originBlock);

        if (newBlock == null)
        {
            Debug.Log("NewBlock Instantiate Failed!");
            return false;
        }

        // 스프라이트가 없으면 런타임으로 생성
        ColorChange(newBlock);

        Transform newTrans = newBlock.transform;
        newTrans.parent = this.transform;
        newTrans.localPosition = prevBlockPosition + Vector3.up * 0.02f;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale = new Vector3(stackBounds.x * 10f, 10f, 1);

        stackCount++;

        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTrans;

        StackUIManager.Instance.UpdateScore();
        return true;
    }

    Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        SpriteRenderer rn = go.GetComponent<SpriteRenderer>();

        if (rn == null)
        {
            Debug.Log("Renderer is NULL!");
            return;
        }

        // 기본 스프라이트 생성
        if (rn.sprite == null)
        {
            rn.sprite = Sprite.Create(
                Texture2D.whiteTexture,
                new Rect(0, 0, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height),
                new Vector2(0.5f, 0.5f)
            );
        }

        // SpriteRenderer의 색상으로 블록 색상 적용
        rn.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if (applyColor.Equals(nextColor))
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;
        lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, 0);
    }

    bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.transform.localPosition;
        float deltaX = Mathf.Abs(prevBlockPosition.x - lastPosition.x);

        if (deltaX > ErrorMargin)
        {
            stackBounds.x -= deltaX;
            if (stackBounds.x <= 0)
                return false;

            float middle = (prevBlockPosition.x + lastPosition.x) / 2;
            lastBlock.localScale = new Vector3(stackBounds.x * 10f, 10f, 1);

            Vector3 tempPosition = lastBlock.localPosition;
            tempPosition.x = middle;
            lastBlock.localPosition = lastPosition = tempPosition;

            float rubbleHalfScale = deltaX / 2;
            CreateRubble(
                new Vector3(
                    (prevBlockPosition.x > lastPosition.x)
                        ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                        : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale,
                    lastPosition.y,
                    1),
                new Vector3(deltaX, 1, 1)
            );

            comboCount = 0;
        }
        else
        {
            ComboCheck();
            lastBlock.localPosition = prevBlockPosition + Vector3.up * 0.02f;
        }

        return true;
    }

    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        go.AddComponent<Rigidbody2D>();
        go.name = "Rubble";
    }

    void ComboCheck()
    {
        comboCount++;

        if (comboCount > maxCombo)
            maxCombo = comboCount;

        if ((comboCount % 5) == 0)
        {
            Debug.Log("5Combo Success!");
            stackBounds += new Vector3(0.5f, 0);
            stackBounds.x = (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
        }
    }

    void UpdateScore()
    {
        if (bestScore < stackCount)
        {
            Debug.Log("최고 점수 갱신");
            bestScore = stackCount;
            bestCombo = maxCombo;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }
    }

    void GameOverEffect()
    {
        int childCount = this.transform.childCount;

        for (int i = 1; i < 20; i++)
        {
            if (childCount < i)
                break;

            GameObject go = this.transform.GetChild(childCount - i).gameObject;
            if (go.name.Equals("Rubble"))
                continue;

            Rigidbody2D rigid = go.AddComponent<Rigidbody2D>();
            rigid.AddForce(
                (Vector2.up * Random.Range(0, 10f)
                 + Vector2.right * (Random.Range(0, 10f) - 5f))
                * 100f
            );
        }
    }

    public void Restart()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        isGameOver = false;

        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(BoundSize, BoundSize);

        stackCount = -1;
        blockTransition = 0f;

        comboCount = 0;
        maxCombo = 0;

        prevBlockPosition = Vector3.zero;

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        Spawn_Block();
        Spawn_Block();
    }
}