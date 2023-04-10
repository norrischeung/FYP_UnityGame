using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPop : MonoBehaviour
{
    public GameObject winpanel;
    public AudioSource audio;

    public void PopPanel() {
        Debug.Log("pop");
        winpanel.SetActive(true);
        audio.Play();
    }
}
