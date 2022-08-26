using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public NumberBox boxPrefab;
    public GameObject SceneManagement;
    SceneManagement sceneManagement;

    public NumberBox[,] boxes = new NumberBox[4, 4];

    public GameObject readySprite;

    public Sprite[] sprites;

    public SpriteRenderer Img;

    public string complete;
    public int titleNum;

    public int[,] checkBox = new int[4, 4];

    bool isShuffling = true;

    private void Start()
    {
        sceneManagement = SceneManagement.GetComponent<SceneManagement>();

        int cnt = 0;

        for (int i = 0; i < titleNum; i++)
            for (int j = 0; j < titleNum; j++)
                checkBox[i, j] = ++cnt;

        StartCoroutine(ReadyShuffle());
        Img = GameObject.FindGameObjectWithTag("Imgg").GetComponent<SpriteRenderer>();
    }

    private void Init()
    {
        int n = 0;
        for(int y = titleNum - 1; y >= 0; y--)
            for(int x = 0; x < titleNum; x++)
            {
                NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickToSwap);
                boxes[x, y] = box;
                n++;
            }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        if(isShuffling == false)
            Swap(x, y, dx, dy);
    }

    void Swap(int x, int y, int dx, int dy)
    {
        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];

        // swap this 2 boxes
        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        // update pos 2 boxes
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);

        int temp = checkBox[((titleNum - 1) - y) - dy, x + dx];
        checkBox[((titleNum - 1) - y) - dy, x + dx] = checkBox[(titleNum - 1) - y, x];
        checkBox[(titleNum - 1) - y, x] = temp;

        if(isShuffling == false)
        {
            StartCoroutine(Check());
        }
    }

    int getDx(int x, int y)
    {
        // is right empty
        if (x < (titleNum - 1) && boxes[x + 1, y].IsEmpty())
            return 1;

        // is left empty
        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;

        return 0;
    }

    int getDy(int x, int y)
    {
        // is top empty
        if (y < (titleNum - 1) && boxes[x, y + 1].IsEmpty())
            return 1;

        // is left empty
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;

        return 0;
    }

    void Shuffle()
    {
        for (int i = 0; i < titleNum; i++)
        {
            for (int j = 0; j < titleNum; j++)
            {
                if (boxes[i, j].IsEmpty()) // 비어있는 위치
                {
                    Vector2 pos = getValidMove(i, j); // 비어있는 위치에 대한 위치 값
                    Swap(i, j, (int)pos.x, (int)pos.y);
                }
            }
        }
    }

    IEnumerator ReadyShuffle()
    {
        Init();
        Instantiate(readySprite, new Vector3(1.5f, 1.5f, -1), Quaternion.Euler(0,0,0));

        yield return new WaitForSeconds(1.5f);

        float iso = 1f;
        Img.color = new Color(1, 1, 1, 1);
        while (iso >= 0)
        {
            iso -= 0.05f;
            yield return new WaitForSeconds(0.05f);
            Img.color = new Color(1, 1, 1, iso);
            Debug.Log(iso);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < titleNum * 50; i++)
            Shuffle();

        isShuffling = false;
    }

    private Vector2 lastMove;

    Vector2 getValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();
        do
        {
            int n = Random.Range(0, 4);
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2)
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(isValidRange(x + (int)pos.x) && isValidRange(y + (int)pos.y)) || isRepeatMove(pos));

        lastMove = pos;
        return pos;

        bool isValidRange(int n)
        {
            return n >= 0 && n <=3;
        }

        bool isRepeatMove(Vector2 pos)
        {
            return pos * -1 == lastMove;
        }
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);

        float iso = 0f;
        int cnt = 0;
        int checkCnt = 0;

        for (int i = 0; i < titleNum; i++)
            for (int j = 0; j < titleNum; j++)
                if (checkBox[i, j] == ++cnt) checkCnt++;

        if (checkCnt == titleNum * titleNum)
        {
            while (iso <= 1)
            {
                iso += 0.05f;
                yield return new WaitForSeconds(0.05f);
                Img.color = new Color(1, 1, 1, iso);
            }
            isShuffling = true;
            yield return new WaitForSeconds(3);
            sceneManagement.SceneChange("Game");
        }
    }
}

