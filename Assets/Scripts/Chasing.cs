using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chasing : MonoBehaviour
{

    Transform Player;
    public Transform Kitten;
    public Transform[] SpawnCreatePoint;
    public Transform camTransform;
    public Player_Info pInfo;
    public float f_RotSpeed = 3.0f, f_MoveSpeed = 3.0f;
    // Use this for initialization
    public float MAXTime = 1.0f; // MAX값/ 절대값
    public float timeCount; // 시간
    public float shakeTimer = 5.0f;// 흔들림 효과 시간
    public float shakeAmount = 0.01f;//흔들림 범위
    public float saltWater = 10f; // 소금물 머금을 경우 올라고 시간량
    public float spawnDis; // 고양이와 플레이간의 거리
    public bool aShake=false; // 놀랐을 경우 화면 흔들림
    public bool spawnAction = true; // 스폰위치 변경 ON/OFF
    bool soundHeartBeat=true; // 고양이가 쫓아올때 심장소리
   

    void Start()
    {
        timeCount = MAXTime;

        InvokeRepeating("Spawn_Create", 1, 3);//1초이후 시작, 3초마다 변경

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Spawn"),
                                   LayerMask.NameToLayer("Default")
                                  );//충돌무시

       

    }

    void StartHeartBeatSound()
    {
        if(soundHeartBeat)
             StartCoroutine("LoopHeartBeatSound");
    }

    IEnumerator LoopHeartBeatSound()
    {
        soundHeartBeat = false;
        GameObject.Find("HeartBeatStrongMid").GetComponent<AudioSource>().Play();
        Debug.Log("심장소리가 재생됩니다.");
        yield return new WaitForSeconds(1f);
        soundHeartBeat = true;
    }
    void Spawn_Create()
    {

        int randomX =Random.Range(0, 4);//생성포인트 갯수
        Kitten.transform.position = new Vector3(SpawnCreatePoint[randomX].transform.position.x, SpawnCreatePoint[randomX].transform.position.y, SpawnCreatePoint[randomX].transform.position.z);
        shakeTimer = 3f;
    }

    IEnumerator StartTimeDiminution(float timeCount)
    {
            Debug.Log("게이지바가 줄어듭니다.");
            pInfo.UIUpdate("Diminution", "HP", timeCount);
            yield return null;

    }

    IEnumerator StartText(float spawnDIs)
    {
        Debug.Log("거리를 나타냅니다.");
        pInfo.TextUpdate(spawnDis);
        yield return null;

    }


    private void Awake()
    {
      //  timeCount = 15;//추후 소금물로 변경
    }

   float ActionSuprise(ref float timeCount)// 인형의 위치가 랜덤으로 이동하는 도중 플레이어가 고양이를 봤을 경우 놀란다. 동시에 시간(소금물)이 줄어든다.
    {

        aShake = true;
        Debug.Log("놀라다");

        if (aShake == true)
        {
          
            if (shakeTimer > 0.0f)
            {
                Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
                camTransform.transform.position = camTransform.transform.position + new Vector3(ShakePos.x, ShakePos.y, 0);
                shakeTimer -= Time.deltaTime;
                timeCount -= 5;
                aShake = false;
            }   
        }
            return timeCount;
       
        
    }
    private void OnDrawGizmos()//현재 고양이 위치 빨간색으로 확인
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,1f);
        //Gizmos.DrawRay(camTransform.transform.position, camTransform.transform.forward*3f);
    }

    // Update is called once per frame
    void Update()
    {
       
        StartCoroutine(StartTimeDiminution(timeCount));// 코루틴 함수

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//마우스 커서
        RaycastHit hit;
        float endingDistance = Vector3.Distance(Player.position, Kitten.position); // 고양이 거리와 플레이어 거리가 0이면 엔딩
        float maxDistance = 3;// 플레이어가 스폰위치 감지할수있는 거리
        spawnDis = Vector3.Distance(Player.position, Kitten.position);//플레이 위치랑 고양이 위치의 거리
        var layerMask = 1 << LayerMask.NameToLayer("Spawn");// 레이캐스트 무시
        Vector3 boxHalfSize = new Vector3(1.5f, 1.5f, 0f);

       
            timeCount -= Time.deltaTime;


        if (Physics.BoxCast(camTransform.transform.position,boxHalfSize,camTransform.transform.forward,out hit,camTransform.rotation,maxDistance, layerMask) && timeCount >= 0)// 박스 캐스트
       // if (Physics.SphereCast(ray,transform.lossyScale.x*3/7,out hit,maxDistance, layerMask)&&timeCount>=0) // 구 캐스트
        {
            spawnAction = false;
          
            Debug.DrawLine(ray.origin, hit.point);//플레이어와 스폰위치 선으로 연결
            Debug.DrawLine(camTransform.transform.position, hit.point);
            Debug.Log("거리에 스폰위치가 있습니다.");
           
            var distanceSpawn = hit.point;

            
            if (spawnDis <= 3)
            {
             
                ActionSuprise(ref timeCount);//화면 Shake
              
            }
            CancelInvoke("Spawn_Create");
          
        }
        if (spawnAction ==false)// 플레이어가 스폰위치를 보다가 다시 방향을 틀었을 경우
        {
            Debug.Log("다시 움직입니다.");
            spawnAction = true;
            if(!IsInvoking("Spawn_Create"))
            InvokeRepeating("Spawn_Create", 1, 3);
        }
      
     

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, layerMask))
        {
            var distanceToGround = hit.distance;
          

            // float disSpawn =Vector3.Distance(Player.)
        }
     /*   hits = Physics.RaycastAll(transform.position, transform.forward, layerMask);
        for (var i = 0; i < hits.Length; i++)
        {
            var distanceSpqwn = hits[i].point;
            Debug.Log("좌표" + hits[i].point);
            Debug.DrawLine(ray.origin, hits[i].point);
        }
        */
        if (timeCount < 0) // 추후 시간을 소금물로 변경

        {
            
            StartCoroutine(StartText(spawnDis));
            CancelInvoke("Spawn_Create");// Invoke 중지
            StartHeartBeatSound();
            transform.rotation = Quaternion.Slerp(transform.rotation
                                                  , Quaternion.LookRotation(Player.position - transform.position)
                                                  , f_RotSpeed * Time.deltaTime);
            /* Move at Player */
            transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

            if (endingDistance < 1.0)
                SceneManager.LoadScene("Over");
        }

       else if(timeCount>0)
        {
            Debug.Log("다시 움직입니다.");
            spawnAction = true;
            if (!IsInvoking("Spawn_Create"))
                InvokeRepeating("Spawn_Create", 1, 3);
        }
    }

    void LateUpdate()
    {
        StopCoroutine(StartTimeDiminution(timeCount));
    }
}