using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

    public float duration = 1.25f;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > duration)
            Destroy(this.gameObject);
	}
}
