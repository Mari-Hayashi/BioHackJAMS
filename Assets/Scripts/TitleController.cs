using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TitleController : MonoBehaviour
{
    private const float timeToWait = 2f;

    void Start()
    {
        StartCoroutine("ShowTitle");
    }
    IEnumerator ShowTitle()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("Therapist");
    }
}
