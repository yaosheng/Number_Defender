using UnityEngine;
using System.Collections;

public class testAngle : MonoBehaviour {

    public Transform point1;
    public Transform point2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 vector1 = point1.position - transform.position;
        Vector3 vector2 = point2.position - transform.position;

        float temp = Vector3.Angle(vector1, vector2);

        Debug.Log("flaot :" + temp);
	}
}
