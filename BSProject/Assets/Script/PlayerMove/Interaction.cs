using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public int InterractionType = 0;
    //type 0 = attack
    //type 1 = dialog
    //type 2 = itemInteraction
    //type 3 = GameStart

    public Image attackImg;
    public Image dialogImg;
    public Image itemInteractionImg;

    public GameObject SceneManagement;
    SceneManagement sceneManagement;

    public GameObject PlayerManager;
    PlayManager playManager;

    int number = 0;
    string stageStr;

    private void Start()
    {
        sceneManagement = SceneManagement.GetComponent<SceneManagement>();
        playManager = PlayerManager.GetComponent<PlayManager>();
        dialogImg.enabled = false;
        itemInteractionImg.enabled = false;
    }

    #region 상호작용 버튼 교체
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DialogZone"){
            DialogueNumber dialogueNumber = collision.GetComponent<DialogueNumber>();
            attackImg.enabled = false;
            itemInteractionImg.enabled = false;
            dialogImg.enabled = true;
            InterractionType = 1;
            number = dialogueNumber.number;
        }
        if (collision.tag == "ItemInteractionZone")
        {
            DialogueNumber dialogueNumber = collision.GetComponent<DialogueNumber>();
            attackImg.enabled = false;
            itemInteractionImg.enabled = true;
            dialogImg.enabled = false;
            InterractionType = 2;
            number = dialogueNumber.number;
        }
        if (collision.tag == "GameStartZone")
        {
            DialogueNumber dialogueNumber = collision.GetComponent<DialogueNumber>();
            attackImg.enabled = false;
            itemInteractionImg.enabled = true;
            dialogImg.enabled = false;
            InterractionType = 3;
            stageStr = dialogueNumber.sceneName;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "DialogZone")
        {
            attackImg.enabled = true;
            itemInteractionImg.enabled = false;
            dialogImg.enabled = false;
            InterractionType = 0;
        }
        if (collision.tag == "ItemInteractionZone")
        {
            attackImg.enabled = true;
            itemInteractionImg.enabled = false;
            dialogImg.enabled = false;
            InterractionType = 0;
        }
        if (collision.tag == "GameStartZone")
        {
            attackImg.enabled = true;
            itemInteractionImg.enabled = false;
            dialogImg.enabled = false;
            InterractionType = 0;
        }
    }
    #endregion

    public void OnInterractionButton()
    {
        if(InterractionType == 0)
        {
            //attack script
        }
        else if(InterractionType == 1)
        {
            //Debug.Log(temp);
            playManager.StartOnTalkButton(number);
        }
        else if(InterractionType == 2)
        {
            playManager.StartOnTalkButton(number);
        }
        else if(InterractionType == 3)
        {
            sceneManagement.SceneChange(stageStr);
        }
    }
}
