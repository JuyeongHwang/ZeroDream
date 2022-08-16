using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{

    //뒤에서 쫓아오면 도망가고, 앞에서 다가오면 따라와서 한번 밀치고 도망감

    // -> 기절 시켜야함. 퍼즐 풀면 기절 아이템 주는 식?? 한번 얻으면 계속 들고있고, 다른 행동 불가능. userstate : attackReady

    public Transform pathHolder;
    [Header("Status")]
    public float speed = 3.0f;
    public float waitTime = .3f;
    public float turnSpeed = 90;
    public float chasingTime = 5.0f;
    public float viewDistance = 10;
    [Header("Commons")]
    public Light spotlight;
    public LayerMask viewMask;
    float viewAngle;

    enum MonsterState { PATROL, CHASE, ATTACK, BACK, CATCHED };
    [SerializeField] MonsterState monsterState = MonsterState.PATROL;
    [SerializeField] private int targetWayPointIndex = 0;

    private Transform player;
    private Animator anim; // walkAttack  ( 0 : 공격만, 0.3 : 공격 & 걷기 0.6 이상 걷기)
    private Color originalSpotlightColor;
    private Vector3[] waypoints;
    private Coroutine follow;
    

    private void Start()
    {

        viewAngle = spotlight.spotAngle;
        player = FindObjectOfType<PlayerInput>().transform;        
        anim = GetComponent<Animator>();
        originalSpotlightColor = spotlight.color;

        waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i<waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i].y = transform.position.y;
        }

        monsterState = MonsterState.PATROL;
        follow = StartCoroutine(FollowPath(waypoints));
    }


    private void Update()
    {

        //1. 몬스터가 잡힌 경우 다 무시

        ControlMonsterState();
        //2. 몬스터가 플레이어를 볼 수 있을때,
        if (CanSeePlayer() && monsterState != MonsterState.BACK)
        {
            //2-1. 처음 발견한거라면, 패트롤 정지
            if (monsterState != MonsterState.CHASE)
            {
                StopCoroutine(follow);
            }
            //2-2. 쫓는다
            ChasePlayer();
        }
        else
        {
            if (monsterState != MonsterState.PATROL)
            {

                monsterState = MonsterState.PATROL;
                follow = StartCoroutine(FollowPath(waypoints));
                anim.SetFloat("WalkAttack", 1.0f);
                viewDistance = 10;
                spotlight.color = originalSpotlightColor;
            }

        }
    }

    void ControlMonsterState()
    {
        if (monsterState == MonsterState.CATCHED)
        {
            return;
        }

        else if (monsterState == MonsterState.CHASE)
        {
            StopCoroutine(follow);
            spotlight.color = Color.cyan;
            viewDistance = 20;

            transform.LookAt(player.position);

            Vector3 diff = player.position - transform.position;
            float distance = diff.magnitude;
            Vector3 orient = diff / distance;
            Vector3 newPos = transform.transform.position + orient * 15 * Time.deltaTime;//Vector3.Lerp(transform.transform.position, transform.transform.position + orient, speed * Time.deltaTime);//this.transform.position + direction.normalized * 10 * Time.deltaTime;

            transform.GetComponent<Rigidbody>().MovePosition(newPos);
        }
        else if(monsterState == MonsterState.ATTACK)
        {
            anim.SetTrigger("Attack");
            print("제로 밀림");

            follow = StartCoroutine(FollowPath(waypoints));
            monsterState = MonsterState.PATROL;
        }
        else if(monsterState == MonsterState.PATROL)
        {
            anim.SetFloat("WalkAttack", 1.0f);
            viewDistance = 10;
            spotlight.color = originalSpotlightColor;
        }
    }
    bool CanSeePlayer()
    {
        //1. 반경 안에 있는가
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            //2. 앞쪽 뷰 앵글 안에 있는가
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                //3. 장애물에 가려지는가 - 레이어마스크, 캐스트 활용
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;

    }



    void ChasePlayer()
    {

        spotlight.color = Color.cyan;
        viewDistance = 20;
        float animScale = 1.0f;


        if (Vector3.Distance(player.position, transform.position) < 1.0f && monsterState != MonsterState.ATTACK)        //플레이어와 가까워지면 밀어냄
        {
            monsterState = MonsterState.ATTACK;
            anim.SetTrigger("Attack");
            monsterState = MonsterState.BACK;
        }
        else //쫓아감
        {
            monsterState = MonsterState.CHASE;

            float chaseSpeed = speed * 7;

            animScale -= Time.deltaTime * 0.5f;
            Vector3 diff = player.position - transform.position;
            float distance = diff.magnitude;
            Vector3 orient = diff / distance;
            Vector3 newPos = transform.transform.position + orient * chaseSpeed * Time.deltaTime;//Vector3.Lerp(transform.transform.position, transform.transform.position + orient, speed * Time.deltaTime);//this.transform.position + direction.normalized * 10 * Time.deltaTime;
            transform.GetComponent<Rigidbody>().MovePosition(newPos);

            transform.LookAt(player.position);
            anim.SetFloat("WalkAttack", animScale);

        }
    }






    IEnumerator FollowPath(Vector3[] waypoints)
    {
        Vector3 targetWayPoint = waypoints[targetWayPointIndex];

        yield return TurnToFace(targetWayPoint); //타겟으로 회전

        while (true)
        {
            //타겟으로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            //anim.SetFloat("WalkAttack 0", 1.0f);

            //만약 그 타겟에 도착했다면,
            if ((targetWayPoint - this.transform.position).magnitude < 0.05f)
            {

                targetWayPointIndex++;
                targetWayPointIndex = (targetWayPointIndex) % waypoints.Length;
                targetWayPoint = waypoints[targetWayPointIndex];
                yield return StartCoroutine(TurnToFace(targetWayPoint));
            }

            yield return null;
        }

    }


    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - this.transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        //(this.transform.eulerAngles.y-targetAngle)
        while (Mathf.Abs(Mathf.DeltaAngle(this.transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(this.transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            this.transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "trap")
        {
            monsterState = MonsterState.CATCHED;
            anim.SetTrigger("Dizzy");
            spotlight.enabled = false;
        }
    }


    private void OnDrawGizmos()
    {
        Vector3 startPos = pathHolder.GetChild(0).position;
        Vector3 prevWayPos = startPos;

        foreach (Transform wayPoint in pathHolder)
        {
            Gizmos.DrawSphere(wayPoint.position, .3f);
            Gizmos.DrawLine(prevWayPos, wayPoint.position);
            prevWayPos = wayPoint.position;
        }
        Gizmos.DrawLine(startPos, prevWayPos);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

}
