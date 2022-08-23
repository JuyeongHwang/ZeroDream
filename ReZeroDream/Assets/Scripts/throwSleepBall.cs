using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwSleepBall : MonoBehaviour
{
    GameObject plane;
    MeshCollider meshCollider;
    Ray ray;
    Vector3 worldPosition;
    LineRenderer line;
    public Material lineMat;

    public ParticleSystem explosion;
    public Rigidbody ball;
    public Transform target;
    public enum kind { sleep, wakeup};
    public kind myKind = kind.sleep;
    Vector3 originalPos;
    public float h = 10;
    public float gravity = -18f;
    public bool isBelongToUser = false;
    void Start()
    {

        target.gameObject.SetActive(false);
        explosion.Stop();
        originalPos = ball.transform.position;
        ball.useGravity = false;
        ball.gameObject.GetComponent<SphereCollider>().isTrigger = true;

        plane = GameObject.Find("EnjoyPlane");
        meshCollider = plane.GetComponent<MeshCollider>();

        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        line.positionCount = 30;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.useWorldSpace = true;
    }

    public bool debugPath = false;
    void Update()
    {
        if (isBelongToUser)
        {
            line.enabled = true;
            DrawPath();
        }
        else
        {
            line.enabled = false;
        }
        if (GameManager.instance.IsUserStateThrowReady())
        {
            isBelongToUser = true;
            target.gameObject.SetActive(true);
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

        isBelongToUser = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            ball.gameObject.GetComponent<SphereCollider>().isTrigger = true;
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            explosion.Play();
            StartCoroutine(reset());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isBelongToUser = false;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "NPC_Monster")
        {
            //explosion.transform.position = collision.transform.position;
            if (myKind == kind.sleep)
            {
                Instantiate(explosion, collision.transform.position, Quaternion.identity);
                explosion.Play();
                collision.transform.GetComponent<MonsterMovement>().monsterState = MonsterMovement.MonsterState.CATCHED;
                collision.transform.GetComponent<Animator>().SetBool("dizzyB", true);
                StartCoroutine(reset());
            }
            else if (myKind == kind.wakeup)
            {
                collision.transform.GetComponent<MonsterMovement>().monsterState = MonsterMovement.MonsterState.PATROL;
                Instantiate(explosion, collision.transform.position, Quaternion.identity);
                explosion.Play();
                collision.transform.GetComponent<Animator>().SetBool("dizzyB", false);
                //collision.transform.GetComponent<Animator>().SetTrigger("Dizzy");
                StartCoroutine(reset());
            }
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
            line.SetPosition(i-1, drawPoint);
 
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
