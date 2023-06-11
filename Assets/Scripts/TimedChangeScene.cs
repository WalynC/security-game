using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedChangeScene : MonoBehaviour
{
    public string scene = "Start UI";

    IEnumerator ChangeSceneAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(scene);
    }

    private void Awake()
    {
        StartCoroutine(ChangeSceneAfterFiveSeconds());
    }
}
