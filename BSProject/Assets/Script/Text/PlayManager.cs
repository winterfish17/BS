using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public GameObject PlayerMoveCanvas;
    public GameObject TextBox;
    public TextManager textManager;
    public Text TalkText_1;
    public Text TalkText_2;
    public int ID;
    private int TalkIndex;
    private bool NowTalking = false;
    public Image LeftSpriteImg;
    public Image RightSpriteImg;

    public GameObject PlayerController;
    PlayerController playerController;

    int temp;

    void Start()
    {
        playerController = PlayerController.GetComponent<PlayerController>();
        TextBox.SetActive(false);
        //OnTalkButton();
    }

    public void StartOnTalkButton(int num)
    {
        playerController.moveSpeed = 0;
        temp = num;
        OnTalkButton(); 
    }

    public void OnTalkButton()
    {
        if (NowTalking == false)
        {
            NowTalking = true;
            TextBox.SetActive(true);
            PlayerMoveCanvas.SetActive(false);
        }
        Talking(temp);
    }

    void Talking(int dialogueNumber)
    {
        TalkText_1.text = " ";
        TalkText_2.text = " ";
        string text_1, text_2;
        string TextData = textManager.GetTalk_1(dialogueNumber, TalkIndex);
        if (TextData == null)
        {
            NowTalking = false;
            TextBox.SetActive(false);
            PlayerMoveCanvas.SetActive(true);
            TalkIndex = 0;
            playerController.moveSpeed = 5;
            return;
        }
        text_1 = TextData.Split(';')[0];
        LeftSpriteImg.sprite = textManager.GetLeftSprite(dialogueNumber, int.Parse(TextData.Split(';')[1]));
        TextData = textManager.GetTalk_2(dialogueNumber, TalkIndex);
        text_2 = TextData.Split(';')[0];
        RightSpriteImg.sprite = textManager.GetRightSprite(dialogueNumber, int.Parse(TextData.Split(';')[1]));
        StartCoroutine(_typing(text_1, text_2));
        TalkIndex++;
    }
    IEnumerator _typing(string text_1, string text_2)
    {
        for (int i = 0; i <= text_1.Length; i++)
        {
            TalkText_1.text = text_1.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i <= text_2.Length; i++)
        {
            TalkText_2.text = text_2.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}