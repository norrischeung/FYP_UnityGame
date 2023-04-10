using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioSource Click;

    public void NextScene(int scene)
    {
        Click.Play();
        SceneManager.LoadScene(scene);
    }
}
