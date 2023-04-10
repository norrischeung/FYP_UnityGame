using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class GetHints : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    //public Text hintsMsg;
    public GameObject hintsImage;
    public int level=0;

    public AudioSource Click;

    public void ShowHints(int level)
    {
        Click.Play();
        //hintsMsg.enabled = true;
        hintsImage.SetActive(true);
        //save hints
        firebaseManager.GetUserReference().Child("Level " + level).Child("Hints").SetValueAsync(true);
    }
}
