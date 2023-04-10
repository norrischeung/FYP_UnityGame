using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public AudioSource Click;
    public Animator animator;
    [SerializeField]
    GameObject startPanel;

    void Start()
    {
        startPanel.SetActive(true);
    }

    //Start -> Login
    public void Zoom_in() {
        Click.Play();
        startPanel.SetActive(false);
        animator.SetTrigger("Zoom_in");
    }
}