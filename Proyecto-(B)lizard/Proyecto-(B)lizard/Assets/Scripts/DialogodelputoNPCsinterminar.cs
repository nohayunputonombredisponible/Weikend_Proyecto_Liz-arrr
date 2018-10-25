using UnityEngine;
using System.Collections;

public class DialogodelputoNPCsinterminar : MonoBehaviour
{

    public bool inTrigger;

    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                
            }
        }
    }

    void OnGUI()
    {
        if (inTrigger)
        {
            GUI.Box(new Rect(0, 100, 500, 25), "Hola, el ruben es un puto subnormal por no tener ni idea de como se llama al discord");
            
        }
    }
}