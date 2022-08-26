using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public new string name;
    public int type;



    public void Change(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("Button CLick");
    }
}
