using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFolder : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    public AudioSource Click;

    //public LoadLevel LoadLevel;

    public void OpenPanel()
    {
        Click.Play();
        menuPanel.SetActive(true);
        //LoadLevel.LoadPassedLevel();
        Debug.Log("open");
    }

    public void ClosePanel()
    {
        Click.Play();
        menuPanel.SetActive(false);
    }
}
