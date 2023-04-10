using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class Statistic : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    static int attempt = 0;
    [SerializeField]
    static int correct = 0;

    public void saveStat(string type, bool correct)
    {
        StartCoroutine(loadAttempt(type));
        if (correct)
        {
            StartCoroutine(loadCorrect(type));
        }
    }

    IEnumerator loadAttempt(string type)
    {
        var task = firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Attempt").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            attempt = int.Parse(snapshot.Value.ToString()) + 1;
            Debug.Log("load attempt " + attempt);
        }
        firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Attempt").SetValueAsync(attempt).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("save attempt " + attempt);
            }
        });
    }

    IEnumerator loadCorrect(string type)
    {
        var task = firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Correct").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            correct = int.Parse(snapshot.Value.ToString()) + 1;
            Debug.Log("load correct " + correct);
        }
        firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Correct").SetValueAsync(correct).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("save correct " + correct);
            }
        });
    }
}
