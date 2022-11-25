using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{
    bool isGrabbing = false;
    GameObject grabbedObject;
    public LayerMask grabbedLayer;
    public float grabRange = 0.2f; //그랩 사거리

    public bool isRemoteGrab = true;
    public float remoteGrabDistance = 20f;

    [Header("던지기 변수")]
    Vector3 prevPos; //이전위치
    public float throwPower = 10f;

    Quaternion prevRot;
    public float rotPower = 50f;

    void Start()
    {

    }

    void Update()
    {
        //잡고 있는 물체가 아무것도 없을 때
        if (isGrabbing == false)
        {
            //잡으려고 시도하는 함수 호출
            TryGrab();
        }
        else
        {
            TryUngrab();
        }
    }

    void TryGrab()
    {
        if (ARVRInput.GetDown(ARVRInput.Button.HandTrigger,
                             ARVRInput.Controller.RTouch))
        {
            //원거리 그랩 활성화일 경우 동작
            if (isRemoteGrab)
            {
                Ray ray = new Ray(ARVRInput.RHandPosition,
                                  ARVRInput.RHandDirection);
                RaycastHit hitInfo;
                if (Physics.SphereCast(ray, 0.5f, //0.5는 발사하는 구체 반경
                                      out hitInfo,
                                      remoteGrabDistance,
                                      grabbedLayer))
                {
                    isGrabbing = true;
                    grabbedObject = hitInfo.transform.gameObject;
                    //물체가 끌려오는 코루틴 함수 호출
                    StartCoroutine(GrabbingAnimation());
                }
            }

            Collider[] hitObjects = Physics.OverlapSphere(ARVRInput.RHandPosition, //생성 위치
                                                          grabRange, //범위
                                                          grabbedLayer); //충돌 감지 레이어
            //가장 가까운 오브젝트의 인덱스 저장용 변수
            int closest = 0;

            //전체 오브젝트를 경유하며 거리를 비교해야 하므로
            //foreach 구문은 적합하지 않음.
            //우선 전체 오브젝트의 거리 계산 후 최단거리 인덱스 저장
            for (int i = 1; i < hitObjects.Length; i++)
            {
                Vector3 closestPos = hitObjects[closest].transform.position;
                //우선 감지된 오브젝트와 우측 컨트롤러의 거리 비교
                float closestDistance = Vector3.Distance(closestPos,
                                                         ARVRInput.RHandPosition);
                Vector3 nextPos = hitObjects[i].transform.position;
                float nextDistance = Vector3.Distance(nextPos,
                                                      ARVRInput.RHandPosition);
                //앞 뒤 인덱스에 있는 오브젝트 거리 비교하여
                //다음 인덱스의 거리가 현재 저장된 최단거리보다 짧으면 인덱스 변경
                if (nextDistance < closestDistance)
                    closest = i;
            }

            //검출된 오브젝트가 존재한다면
            if (hitObjects.Length > 0)
            {
                isGrabbing = true;
                //윗쪽 for문에서 계산된 가장 가까운 오브젝트의 인덱스를 사용
                //가장 가까이 있는 오브젝트 가지고오기
                grabbedObject = hitObjects[closest].gameObject;
                //부모 오브젝트를 오른쪽 컨트롤러로 지정
                grabbedObject.transform.parent = ARVRInput.RHand;

                //손에 폭탄을 쥐고 있을 경우에 폭탄의 물리기능 해제
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                //폭탄을 쥐고나서 초기 위치 설정
                prevPos = ARVRInput.RHandPosition;
                //우측 컨트롤러의 회전값을 초기 값으로 지정
                prevRot = ARVRInput.RHand.rotation;
            }
        }
    }

    void TryUngrab()
    {
        //던질 방향 설정
        Vector3 throwDirection = ARVRInput.RHandPosition - prevPos;
        prevPos = ARVRInput.RHandPosition;

        Quaternion deltaRotation = ARVRInput.RHand.rotation *
                                   Quaternion.Inverse(prevRot);
        prevRot = ARVRInput.RHand.rotation;

        if (ARVRInput.GetUp(ARVRInput.Button.HandTrigger,
                           ARVRInput.Controller.RTouch))
        {
            isGrabbing = false;
            //물리엔진 가동 Gravity 에 의해 중력 작용 받음
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //부모 설정을 해지했으므로 해당 위치에서 자유낙하(중력)
            grabbedObject.transform.parent = null;

            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower;

            //각속도 = (1/dt) * d시타(특정 축 기준 변위 각도 = 변경된 각도)
            float angle;
            Vector3 axis;
            deltaRotation.ToAngleAxis(out angle, out axis);
            Vector3 angularVelocity = (1f / Time.deltaTime) * angle * axis;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity * rotPower;


            //쥐고 있던 오브젝트 해제하여 추후 문제가 없도록 함
            grabbedObject = null;
        }
    }
    IEnumerator GrabbingAnimation()
    {
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        prevPos = ARVRInput.RHandPosition;
        prevRot = ARVRInput.RHand.rotation;
        Vector3 startLocation = grabbedObject.transform.position;
        Vector3 targetLocation = ARVRInput.RHandPosition +
                                 ARVRInput.RHandDirection * 0.1f;

        float currentTime = 0f;
        float finishTime = 0.2f;

        //시간 경과율
        float elapsedRate = currentTime / finishTime;
        while (elapsedRate < 1)
        {
            currentTime += Time.deltaTime;
            elapsedRate = currentTime / finishTime;
            grabbedObject.transform.position = Vector3.Lerp(startLocation,
                                                            targetLocation,
                                                            elapsedRate);
            yield return null;
        }
        grabbedObject.transform.position = targetLocation;
        grabbedObject.transform.parent = ARVRInput.RHand;
    }
}