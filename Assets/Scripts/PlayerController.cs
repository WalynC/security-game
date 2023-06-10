using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    float rotX, rotY;
    public float sensitivity = 1000f;
    public NavMeshAgent agent;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //camera control
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * sensitivity * Time.deltaTime;
        rotX += mouseY * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -45f, 89f);

        cam.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
        //movement
        Vector3 forwardVec = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 sideVec = new Vector3(forwardVec.z, 0, -forwardVec.x);

        agent.destination = transform.position + forwardVec * Input.GetAxis("Vertical") + sideVec * Input.GetAxis("Horizontal");
    }
}
