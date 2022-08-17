using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwSleepBall : MonoBehaviour
{
    GameObject plane;
    MeshCollider meshCollider;
    Ray ray;
    Vector3 worldPosition;

    public ParticleSystem explosion;
    public Rigidbody ball;
    public Transform target;

    Vector3 originalPos;
    public float h = 10;
    public float gravity = -18f;

    void Start()
    {
        explosion.Stop();
        originalPos = ball.transform.position;
        ball.useGravity = false;
        ball.gameObject.GetComponent<SphereCollider>().isTrigger = true;

        plane = GameObject.Find("EnjoyPlane");
        meshCollider = plane.GetComponent<MeshCollider>();
    }

    public bool debugPath = false;
    void Update()
    {
        if (debugPath)
        {
            DrawPath();
        }
        if (GameManager.instance.IsUserStateThrowReady())
        {
            ScreenToWorld();
            target.position = worldPosition;
        }
    }
    void ScreenToWorld()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (meshCollider.Raycast(ray, out hit, 100))
        {
            worldPosition = hit.point;
        }
    }
    public void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CaculateLaunchData().initialVelocity;
        StartCoroutine(changeTrigger());
    }
    IEnumerator changeTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        ball.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        GameManager.instance.SetUserStateToMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if(collision.gameObject == plane)
        {

            explosion.Play();
            StartCoroutine(reset());
        }

        if(collision.gameObject.name == "NPC_Monster")
        {
            collision.transform.GetComponent<Animator>().SetTrigger("Dizzy");
            explosion.Play();
            StartCoroutine(reset());
        }
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(1.5f);
        ball.transform.position = originalPos;
        ball.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        ball.useGravity = false;
        ball.velocity = Vector3.zero;
    }

    LaunchData CaculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);

    }

    void DrawPath()
    {
        LaunchData ld = CaculateLaunchData();
        int resolution = 30;
        Vector3 previousDrawPoint = ball.position;
        for (int i= 1; i<=resolution; i++)
        {
            float simulationTime = i / (float)resolution * ld.timeToTarget;
            Vector3 displacement = ld.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);

            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initial, float ttT)
        {
            initialVelocity = initial;
            timeToTarget = ttT;
        }
    }
}
