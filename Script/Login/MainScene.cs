using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;
    [SerializeField]
    InputField inputEmail;
    [SerializeField]
    InputField inputPassword;

    [SerializeField]
    GameObject loginPanel;
    [SerializeField]
    GameObject infoPanel;

    [SerializeField]
    Text textEmail;

    [SerializeField]
    Text textHistory;

    void Start() {
        firebaseManager.auth.StateChanged += AuthStateChanged;
    }

    public void Register(){
        firebaseManager.Register(inputEmail.text,inputPassword.text);
        //firebaseManager.DefaultDatabase();
    }

    public void Login() {
        firebaseManager.Login(inputEmail.text,inputPassword.text);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if(firebaseManager.user == null) {
            //logout
            //textEmail.text = "";
            loginPanel.SetActive(true);
            //infoPanel.SetActive(false);
        }
        else {
            //textEmail.text = firebaseManager.user.Email;
            loginPanel.SetActive(false);
            //infoPanel.SetActive(true);
            SceneManager.LoadScene(2);
        }
    }

    void OnDestroy() {
        firebaseManager.auth.StateChanged -= AuthStateChanged;
    }
}
