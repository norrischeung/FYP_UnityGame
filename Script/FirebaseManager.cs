using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;

    public int totalLevel = 6;

    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
    }

    public void Register(string email, string password) {
        auth.CreateUserWithEmailAndPasswordAsync(email,password).ContinueWith(task=> {
            if(task.IsCanceled) {
                Debug.Log("Cancel");
                return;
            }
            if(task.IsFaulted) {
                Debug.Log(task.Exception.InnerException.Message);
                return;
            }
            if(task.IsCompletedSuccessfully) {
                Debug.Log("Register");
                //SaveEmail
                GetUserReference().Child("email").SetValueAsync(user.Email).ContinueWith(task=> {
                    if(task.IsCompletedSuccessfully) {
                        Debug.Log("save email!");
                    }
                DefaultDatabase();
                });
            }
        });
    }

    //Default Database
    public void DefaultDatabase()
    {
        //GetUserReference().Child("Login Times").SetValueAsync(1);
        GetUserReference().Child("Last Login").SetValueAsync(DateTime.Now.ToString());
        GetUserReference().Child("Passed Level").SetValueAsync(0);
        for (int i = 1; i <= totalLevel; i++)
        {
            GetUserReference().Child("Level " + i).Child("Best Result").SetValueAsync(0);
            GetUserReference().Child("Level " + i).Child("Hints").SetValueAsync(false);
        }
        //1. DataType
        GetUserReference().Child("Statistic").Child("Data Type").Child("Attempt").SetValueAsync(0);
        GetUserReference().Child("Statistic").Child("Data Type").Child("Correct").SetValueAsync(0);
        //2. Conditional Statement
        GetUserReference().Child("Statistic").Child("Conditional Statement").Child("Attempt").SetValueAsync(0);
        GetUserReference().Child("Statistic").Child("Conditional Statement").Child("Correct").SetValueAsync(0);
        //3. Conditional Operator
        GetUserReference().Child("Statistic").Child("Conditional Operator").Child("Attempt").SetValueAsync(0);
        GetUserReference().Child("Statistic").Child("Conditional Operator").Child("Correct").SetValueAsync(0);
        //4. Increment/Decrement
        GetUserReference().Child("Statistic").Child("Increment").Child("Attempt").SetValueAsync(0);
        GetUserReference().Child("Statistic").Child("Increment").Child("Correct").SetValueAsync(0);
        //5. Swapping
        GetUserReference().Child("Statistic").Child("Swapping").Child("Attempt").SetValueAsync(0);
        GetUserReference().Child("Statistic").Child("Swapping").Child("Correct").SetValueAsync(0);
    }


    public async void Login(string email, string password) {
        auth.SignInWithEmailAndPasswordAsync(email,password).ContinueWith(task=> {
            if(task.IsFaulted) {
                Debug.Log(task.Exception.InnerException.Message);
                return;
            }
            if(task.IsCompletedSuccessfully) {
                Debug.Log("Login");
                GetUserReference().Child("Last Login").SetValueAsync(DateTime.Now.ToString());
                GetUserReference().Child("Passed Level").SetValueAsync(0);
                for (int i = 1; i <= totalLevel; i++)
                Debug.Log("last login");
            }
        });
    }

    public void Logout() {
        auth.SignOut();
    }

        void AuthStateChanged(object sender, System.EventArgs eventArgs) {
            if(auth.CurrentUser != user) {
                user = auth.CurrentUser;
                if(user != null) {
                    Debug.Log($"Login - {user.Email}"); 
                }
            }
        }

    //Update Best Result
    public void SaveAttempt(String level,int attempt) {
        StartCoroutine(loadAttempt(level, attempt));
    }

    IEnumerator loadAttempt(String level, int attempt)
    {
        int oldAttempt;
        var task = GetUserReference().Child(level).Child("Best Result").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            oldAttempt = int.Parse(snapshot.Value.ToString());
            if(oldAttempt != 0)
            {
                if (attempt < oldAttempt)
                {
                    Debug.Log("new attempt " + attempt + " old attempt " + oldAttempt);
                    GetUserReference().Child(level).Child("Best Result").SetValueAsync(attempt);
                }
            }
            else
            {
                GetUserReference().Child(level).Child("Best Result").SetValueAsync(attempt);
            }   
        }
    }

    //Update Passed Level
    public void SavePassedLevel(int level) {
        StartCoroutine(loadPassedLevel(level));
    }

    IEnumerator loadPassedLevel(int level) 
    {
        int oldLevel;
        var task = GetUserReference().Child("Passed Level").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            oldLevel = int.Parse(snapshot.Value.ToString());
            if(level > oldLevel) 
            {
                GetUserReference().Child("Passed Level").SetValueAsync(level);
            }
        }
    }

    public DatabaseReference GetUserReference() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        return reference.Child(user.UserId);
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
    }
}

