using UnityEngine;
using System.Collections;

public class SwitchObject : MonoBehaviour {

    public Transform target;

    private void OnCollisionEnter (Collision col)
    {
        if (target != null)
            Destroy(target.gameObject);
    }
}
