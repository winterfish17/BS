using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public int InterractionType = 0;
    //type 0 = attack
    //type 1 = dialog

    public Image attackImg;
    public Image dialogImg;

    public GameObject SceneManagement;
    SceneManagement sceneManagement;

    public GameObject PlayerManager;
    PlayManager playManager;

    int temp = 0;

    private void Start()
    {
        sceneManagement = SceneManagement.GetComponent<SceneManagement>();
        playManager = PlayerManager.GetComponent<PlayManager>();
        dialogImg.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DialogZone"){
            DialogueNumber dialogueNumber = collision.GetComponent<DialogueNumber>();
            attackImg.enabled = false;
            dialogImg.enabled = true;
            InterractionType = 1;
            temp = dialogueNumber.number;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "DialogZone")
        {
            dialogImg.enabled = false;
            attackImg.enabled = true;
            InterractionType = 0;
        }
    }

    public void OnInterractionButton()
    {
        if(InterractionType == 0)
        {
            //attack script
        }
        else if(InterractionType == 1)
        {
            //Debug.Log(temp);
            playManager.StartOnTalkButton(temp);
        }
    }
}
