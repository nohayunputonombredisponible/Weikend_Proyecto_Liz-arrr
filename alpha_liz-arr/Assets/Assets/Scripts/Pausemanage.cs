using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pausemanage : MonoBehaviour
{

    Canvas canvas;
    bool active;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            active = !active;
            canvas.enabled = active;
            Time.timeScale = (active) ? 0: 1f;
            if (active) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }

  
}