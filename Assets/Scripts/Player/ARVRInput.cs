#define PC
//#define Oculus
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//static 클래스로 생성
//static 클래스의 경우 일반적인 클래스와 동일하지만
//new 키워드로 객체 생성할 수 없음 = 필요가 없음
public static class ARVRInput
{
    //클래스 자체를 정적 클래스로 만들었기 때문에
    //일반 변수 또한 정적 변수 / 메소드로 만들어져야 됨
#if PC
    public enum ButtonTarget
    {
        Fire1,//마우스 좌 클릭
        Fire2,//마우스 우 클릭
        Fire3,//마우스 휠 버튼
        Jump//키보드 스페이스 키
    }
#endif
    public enum Button
    {
#if PC
        One = ButtonTarget.Fire1,
        Two = ButtonTarget.Jump,
        Thumbstick = ButtonTarget.Fire1,
        IndexTrigger = ButtonTarget.Fire3,
        HandTrigger = ButtonTarget.Fire2
#elif Oculus
        One = OVRInput.Button.One,
        Two = OVRInput.Button.Two,
        Thumbstick = OVRInput.Button.PrimaryThumbstick,
        IndexTrigger = OVRInput.Button.PrimaryIndexTrigger,
        HandTrigger = OVRInput.Button.PrimaryHandTrigger
#endif
    }
    public enum Controller
    {
#if PC
        LTouch,
        RTouch
#elif Oculus
        LTouch = OVRInput.Controller.LTouch,
        RTouch = OVRInput.Controller.RTouch
#endif
    }

#if Oculus //오큘러스 사용할 때 제어할 트랜스폼 가져오기
    static Transform rootTransform;
    static Transform GetTransform()
    { 
        if(rootTransform == null)
        {
            rootTransform = GameObject.Find("TrackingSpace").transform;
        }
        return rootTransform;
    }
#endif

    static Transform lHand; //왼쪽 컨트롤러
    public static Transform LHand //왼쪽 컨트롤러 프로퍼티
    {
        get
        {
            if (lHand == null)
            {
#if PC//전처리 지시자 = 컴퓨터 입장에서 전처리 단계에서 미리 인식하는 부분
                //각종 디바이스를 교체하는 부분이나, 테스트 등에 사용하기도 함

                //LHand라는 이름의 게임오브젝트(빈오브젝트) 생성
                GameObject handObj = new GameObject("LHand");
                //만들어진 오브젝트의 트랜스폼을 lHand 변수에 할당
                lHand = handObj.transform;
                //컨트롤러를 카메라의 자식 오브젝트로 등록
                lHand.parent = Camera.main.transform;
#elif Oculus
                lHand = GameObject.Find("LeftControllerAnchor").transform;
#endif
            }
            return lHand;
        }
    }

    static Transform rHand; //으론쪽 컨트롤러
    public static Transform RHand //오른쪽 컨트롤러 프로퍼티
    {
        get
        {
            if (rHand == null)
            {
#if PC
                GameObject handObj = new GameObject("RHand");
                rHand = handObj.transform;
                rHand.parent = Camera.main.transform;
#elif Oculus
                rHand = GameObject.Find("RightControllerAnchor").transform;
#endif
            }
            return rHand;
        }
    }

    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            RHand.position = pos;
            return pos;
#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }
    public static Vector3 RHandDirection
    {
        get
        {
#if PC
            Vector3 direction = RHandPosition -
                                Camera.main.transform.position;
            RHand.forward = direction;
            return direction;
#elif Oculus
            //컨트롤러의 회전값 얻어오기
            //얻어온 회전값은 쿼터니언 이므로 forward를 곱해서 전방방향 얻어옴
            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            //얻어온 로컬 회전값을 월드 회전값으로 변환
            direction = GetTransform().TransformDirection(direction);
            return direction;

#endif
        }
    }

    public static Vector3 LHandPosition
    {
        get
        {
#if PC
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            LHand.position = pos;
            return pos;
#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }
    public static Vector3 LHandDirection
    {
        get
        {
#if PC
            Vector3 direction = LHandPosition -
                                Camera.main.transform.position;
            LHand.forward = direction;
            return direction;
#elif Oculus
            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
            direction = GetTransform().TransformDirection(direction);
            return direction;
#endif
        }
    }

    public static bool Get(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask에 들어온 값을 ButtonTarget 자료형으로 형변환
        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
#elif Oculus
        return OVRInput.Get((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
#endif
    }
    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask에 들어온 값을 ButtonTarget 자료형으로 형변환
        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
#elif Oculus
        return OVRInput.GetDown((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
#endif 
    }
    public static bool GetUp(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask에 들어온 값을 ButtonTarget 자료형으로 형변환
        return Input.GetButtonUp(((ButtonTarget)virtualMask).ToString());
#elif Oculus
        return OVRInput.GetUp((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
#endif 
    }
    public static float GetAxis(string axis, Controller hand = Controller.LTouch)
    {
#if PC
        return Input.GetAxis(axis);
#elif Oculus
        if(axis == "Horizontal")
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,
                               (OVRInput.Controller)hand).x;
        }
        else
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,
                               (OVRInput.Controller)hand).y;
        }
#endif
    }
    public static void PlayVibration(Controller hand)
    {
        //제작만 PC에서는 진동을 표현 할수 없으므로
    }
    public static void Recenter(Transform target, Vector3 direction)
    {
        //원하는 방향(보고자 하는방향)으로 타겟의 센터를 설정
        target.forward = target.rotation * direction;
    }
    public static void Recenter()
    {
#if Oculus
        OVRManager.display.RecenterPose();
#endif
    }

#if PC
    static Vector3 originScale = Vector3.one * 0.02f;
#elif Oculus
    static Vector3 originScale = Vector3.one * 0.005f;
#endif

    //레이가 닿는 부분에 크로스헤어 표시하는 메소드
    public static void DrawCrossHair(Transform crosshair,
                                     bool isHand = true,
                                     Controller hand = Controller.RTouch)
    {
        Ray ray;


        if (isHand)//컨트롤러가 존재할 경우
        {
#if PC
            //컨트롤러가 있을때는 핸드위치에 레이 쏘기
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif Oculus
            if(hand == Controller.RTouch)
            {
                ray = new Ray(RHandPosition, RHandDirection);
            }
            else
            {
                ray = new Ray(LHandPosition, LHandDirection);
            }
#endif
        }
        else//컨트롤러 없으면 시야 따라 레이 쏘기
        {
            //메인 카메라의 중심에서 전방으로 레이 생성
            ray = new Ray(Camera.main.transform.position,
                          Camera.main.transform.forward);
        }
        //레이랑 충돌한 바닥 설정
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;
        if (plane.Raycast(ray, out distance))
        {
            //충돌 지점 위치 가져오기
            crosshair.position = ray.GetPoint(distance);
            //카메라의 바라보는 방향의 반대방향(서로 마주보도록)
            crosshair.forward = -Camera.main.transform.forward;
            //거리에 따라 크로스헤어 크기 커지도록
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
        else
        {
            //충돌이 없을 경우 레이의 시작 위치에서 100정도 거리에 생성
            crosshair.position = ray.origin + ray.direction * 100;
            //방향은 마찬가지로 카메라 쳐다보도록
            crosshair.forward = -Camera.main.transform.forward;
            //스케일 조정하기 위한 거리 계산
            distance = (crosshair.position - ray.origin).magnitude;
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
    }
}