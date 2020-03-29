using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
    public int code;
    float disableTimer = 0;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider) { 
 
        if (collider.gameObject.name == "Player" && disableTimer<=0)
        {
            foreach (Teleporter tp in FindObjectsOfType<Teleporter>()){

                if(tp.code == code && tp != this)
                {
                    tp.disableTimer = 2;
                    Vector3 position = tp.gameObject.transform.position;
                    position.y += 2;
                    collider.gameObject.transform.position = position;
                    Rigidbody rigid = player.GetComponent<Rigidbody>();
                    rigid.velocity = Vector3.zero;
                    rigid.angularVelocity = Vector3.zero;
                }

            }
        }
    }

}
