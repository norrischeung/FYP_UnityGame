using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class StatPanel : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    GameObject statPanel;

    public AudioSource Click;

    public void OpenPanel()
    {
        Click.Play();
        statPanel.SetActive(true);
    }
}
