using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class RunCode : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    Statistic statistic;

    public Animator animator;
    public Dropdown Drop1,Drop2;
    public int opt1,opt2;
    public Text errorMsg;

    private int numOfAttempt = 0;
    private string level = "Level 2";

    public AudioSource Click,Error;

    void Start() {
        errorMsg.enabled = false;
    }

    public void playAnimation() {
        Click.Play();
        numOfAttempt++;

        opt1 = Drop1.value;
        opt2 = Drop2.value;
        Debug.Log(opt1 + " " + opt2);
        errorMsg.enabled = false;

        //close open
        if(opt1==0 && opt2==0) {
            animator.ResetTrigger("ani.1");
            animator.SetTrigger("ani.1");
            Debug.Log("(==)(==)");
        }

        //open open
        if(opt1==1 && opt2==0) {
            animator.ResetTrigger("ani.0");
            animator.SetTrigger("ani.0");
            Debug.Log("(!=)(==)");
            //Save in Database
            firebaseManager.SaveAttempt(level, numOfAttempt);
            firebaseManager.SavePassedLevel(2);
        }

        //close close
        if(opt1==0 && opt2==1) {
            animator.ResetTrigger("ani.2");
            animator.SetTrigger("ani.2");
            Debug.Log("(==)(!=)");
        }

        //open close
        if(opt1==1 && opt2==1) {
            animator.ResetTrigger("ani.3");
            animator.SetTrigger("ani.3");
            Debug.Log("(!=)(!=)");
        }

        if(opt1==2 || opt2==2) {
            errorMsg.enabled = true;
            Error.Play();
        }

        //Save Statistic
        if (opt1 == 1)
        {
            //op1 correct
            statistic.saveStat("Conditional Operator", true);
        }
        else
        {
            //opt1 incorrect
            statistic.saveStat("Conditional Operator", false);
        }
        if (opt2 == 0)
        {
            //op2 correct
            statistic.saveStat("Conditional Operator", true);
        }
        else
        {
            //opt2 incorrect
            statistic.saveStat("Conditional Operator", false);
        }
    }
}
