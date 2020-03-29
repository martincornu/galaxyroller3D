using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

    public float speed = 1;

    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * direction * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            if (direction == 1)
                direction = -1;
            else
                direction = 1;
        }

        if (other.tag == "player")
        {
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            other.transform.parent = null;
        }
    }
}
