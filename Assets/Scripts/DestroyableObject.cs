using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour {

    public float forceRequired = 10.0f;
    public GameObject burstPrefab;

    private void OnCollisionEnter(Collision col)
    {
        if (col.impulse.magnitude > forceRequired)
        {
            Instantiate(burstPrefab, col.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
