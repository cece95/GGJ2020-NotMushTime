using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Vector3 initial_position;
    Quaternion initial_rotation;
    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        initial_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // float noise = Mathf.PerlinNoise(Time.timeSinceLevelLoad * 0.999f, Time.timeSinceLevelLoad * 0.999f);
        // float noise2 = Mathf.PerlinNoise(Time.timeSinceLevelLoad * 0.333f, Time.timeSinceLevelLoad * 0.666f);

        // transform.position = initial_position + Vector3.up * noise / 10.0f;
        // transform.rotation = initial_rotation * Quaternion.Euler(Vector3.forward * noise2);
    }
}
