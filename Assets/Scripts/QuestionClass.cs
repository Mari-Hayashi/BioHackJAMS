using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionStr;
    public string[] choice;
    public int numChoices;
    public string answer;

    public Question(int numchoices)
    {
        choice = new string[numchoices];
    }
}
