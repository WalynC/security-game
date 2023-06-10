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
    public ParticleSystem weaponHit, enemyHit;

    public static PlayerController instance;

    Queue<ParticleSystem> weaponHits = new Queue<ParticleSystem>();
    Queue<ParticleSystem> enemyHits = new Queue<ParticleSystem>();

    public float timeBetweenShots;
    float lastFireTime;

    void Start()
    {
        instance = this;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
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

        //player looking at
        if (Input.GetMouseButton(0) && TryFire())
        {
            Fire();
        }
    }

    void Fire()
    {
        lastFireTime = Time.time;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth health))
            {
                Impact(hit.point, true);
                health.TakeDamage(5);
            } else
            {
                Impact(hit.point, false);
            }
        }
    }

    public ParticleSystem GetParticle(bool enemy)
    {
        Queue<ParticleSystem> sysQ = enemy ? enemyHits : weaponHits;
        if (sysQ.Count <= 0) sysQ.Enqueue(Instantiate(enemy ? enemyHit : weaponHit));
        return sysQ.Dequeue();
    }

    public void Impact(Vector3 pos, bool enemy)
    {
        ParticleSystem obj = GetParticle(enemy);
        obj.gameObject.SetActive(true);
        obj.Play();
        obj.transform.position = pos;
        obj.transform.forward = -cam.transform.forward;
    }

    public void ReturnParticleObject(ParticleSystem sys, bool enemy)
    {
        sys.gameObject.SetActive(false);
        Queue<ParticleSystem> sysQ = enemy ? enemyHits : weaponHits;
        sysQ.Enqueue(sys);
    }

    bool TryFire()
    {
        return (lastFireTime + timeBetweenShots < Time.time);
    }
}
