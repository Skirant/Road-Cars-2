using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFinishBehavior : MonoBehaviour
{
    private void Update()
    {
        float x = Mathf.MoveTowards(transform.position.x, 0, Time.deltaTime * 2f);
        float z = transform.position.z + 16f * Time.deltaTime;
        transform.position = new Vector3(x, 0, z);

        float rot = Mathf.MoveTowardsAngle(transform.eulerAngles.y, 0, Time.deltaTime * 100f);
        transform.localEulerAngles = new Vector3(0, rot, 0);
    }
}
