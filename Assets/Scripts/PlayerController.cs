using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    float yaw, pitch;
    public float sensitivity = 1000f;
    public NavMeshAgent agent;
    public float minPitch = -45f;
    public float maxPitch = 89f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //camera control
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        pitch += mouseY * sensitivity * Time.deltaTime;
        yaw += mouseX * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -maxPitch, -minPitch);

        cam.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        //movement
        Vector3 forwardVec = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 sideVec = new Vector3(forwardVec.z, 0, -forwardVec.x);

        agent.destination = transform.position + forwardVec * Input.GetAxis("Vertical") + sideVec * Input.GetAxis("Horizontal");
    }
}
