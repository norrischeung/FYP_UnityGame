using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class LogoutSystem : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;
    [SerializeField]
    GameObject logoutPanel;

    [SerializeField]
    Text textEmail;

    [SerializeField]
    Text textRecord;

    void Start() {
        //firebaseManager.auth.StateChanged += AuthStateChanged;
        logoutPanel.SetActive(false);
    }

    //Click Window Icon to Logout
    public void OpenPanel() {
        logoutPanel.SetActive(true);
        textEmail.text = firebaseManager.user.Email;
        StartCoroutine(loadRecord());
        
    }

    public void ClosePanel() {
        logoutPanel.SetActive(false);
    }

    public void Logout() {
        firebaseManager.Logout();
        //textHistory.text = "";
        SceneManager.LoadScene(1);
    }

    IEnumerator loadRecord()
    {
        var task = firebaseManager.GetUserReference().Child("Passed Level").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            int record = int.Parse(snapshot.Value.ToString());
            if(record==0 || record == 1)
            {
                textRecord.text = "You have passed " + record + " level !";
            }
            else
            {
                textRecord.text = "You have passed " + record + " levels !";
            }
            
        }
    }
}
