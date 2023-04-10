using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class RunCode_1 : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    Statistic statistic;

    public Animator animator;
    public Dropdown Drop;
    public int opt;

    private int numOfAttempt=0;
    private string level = "Level 1";

    public AudioSource Click;

    public void playAnimation() {
        Click.Play();
        numOfAttempt++;

        opt = Drop.value;
        //red or Yellow
        if(opt==0 || opt==1) {
            animator.ResetTrigger("ani.red");
            animator.SetTrigger("ani.red");
            Debug.Log("red or Yellow");
        }
        //yellow
        if(opt==2) {
            animator.ResetTrigger("ani.yellow");
            animator.SetTrigger("ani.yellow");
            Debug.Log("yellow");
            //Save in Database
            firebaseManager.SaveAttempt(level, numOfAttempt);
            firebaseManager.SavePassedLevel(1);
        }

        //Save Statistic
        if (opt == 2)
        {
            //op1 correct
            statistic.saveStat("Conditional Statement", true);
        }
        else
        {
            //opt1 incorrect
            statistic.saveStat("Conditional Statement", false);
        }
    }   
}
