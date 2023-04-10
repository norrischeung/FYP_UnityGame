using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Button[] button;

    [SerializeField]
    FirebaseManager firebaseManager;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<button.Length; i++)
        {
            button[i].gameObject.SetActive(false);
        }
    }

    public void LoadPassedLevel()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        int PassedLevel = 0;
        var task = firebaseManager.GetUserReference().Child("Passed Level").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if(snapshot != null)
        {
            PassedLevel = int.Parse(snapshot.Value.ToString());
            //Debug.Log("Passed Level = " + PassedLevel);
        }
        for (int i = 0; i <= PassedLevel; i++)
        {
            //Debug.Log(button[i]);
            button[i].gameObject.SetActive(true);
        }
    }

}
