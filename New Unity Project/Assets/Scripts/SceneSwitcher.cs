using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player;
    public Transform home;
    public Vector3 lastPos;
    private bool isInWorld = true;
    public void TeleToHome()
    {
        /*if (SceneManager.GetActiveScene().name == "World")
        {

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Home"));
        }*/
        this.GetComponent<CharacterController>().enabled = false;
        if (isInWorld)
        {
            lastPos = player.transform.position;
        }
        isInWorld = false;
        player.transform.position = home.position;
    }
    public void TeleToWorld()
    {
        /*if (SceneManager.GetActiveScene().name == "Home")
        {
            Debug.Log("test");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("World"));
        }*/
        isInWorld = true;
        player.transform.position = lastPos;
        this.GetComponent<CharacterController>().enabled = true;
    }

}
