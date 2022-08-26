using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start15Puzzle : MonoBehaviour
{
    public GameObject SceneManagement;
    SceneManagement sceneManagement;

    bool inStartZone = false;

    private void Start()
    {
        sceneManagement = SceneManagement.GetComponent<SceneManagement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inStartZone)
        {
            sceneManagement.SceneChange("15Puzzle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inStartZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inStartZone = false;
    }
}
