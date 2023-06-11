using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool allowPause = true;
    public GameObject obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && allowPause)
        {
            if (obj.activeInHierarchy)
            {
                Unpause();
            }
            else Pause();
        }
    }

    public void SetPause(bool pause) => allowPause = pause;

    public void ResetTimeScale() => Time.timeScale = 1f;

    void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        obj.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        obj.SetActive(false);
    }
}
