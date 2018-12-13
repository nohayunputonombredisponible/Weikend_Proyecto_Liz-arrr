using UnityEngine;

public class TurnAroundScript : MonoBehaviour
{


    public float turnSpeed = 40f;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
    }
}
