using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player;
    public Transform home;
    public Vector3 lastPos;
    public void TeleToHome()
    {
        /*if (SceneManager.GetActiveScene().name == "World")
        {

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Home"));
        }*/
        lastPos = player.transform.position;
        player.transform.position = home.position;
    }
    public void TeleToWorld()
    {
        /*if (SceneManager.GetActiveScene().name == "Home")
        {
            Debug.Log("test");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("World"));
        }*/

        player.transform.position = lastPos;
    }

}
