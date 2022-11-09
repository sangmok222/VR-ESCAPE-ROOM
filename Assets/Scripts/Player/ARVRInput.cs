#define PC
//#define Oculus
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//static Ŭ������ ����
//static Ŭ������ ��� �Ϲ����� Ŭ������ ����������
//new Ű����� ��ü ������ �� ���� = �ʿ䰡 ����
public static class ARVRInput
{
    //Ŭ���� ��ü�� ���� Ŭ������ ������� ������
    //�Ϲ� ���� ���� ���� ���� / �޼ҵ�� ��������� ��
#if PC
    public enum ButtonTarget
    {
        Fire1,//���콺 �� Ŭ��
        Fire2,//���콺 �� Ŭ��
        Fire3,//���콺 �� ��ư
        Jump//Ű���� �����̽� Ű
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

#if Oculus //��ŧ���� ����� �� ������ Ʈ������ ��������
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

    static Transform lHand; //���� ��Ʈ�ѷ�
    public static Transform LHand //���� ��Ʈ�ѷ� ������Ƽ
    {
        get
        {
            if (lHand == null)
            {
#if PC//��ó�� ������ = ��ǻ�� ���忡�� ��ó�� �ܰ迡�� �̸� �ν��ϴ� �κ�
                //���� ����̽��� ��ü�ϴ� �κ��̳�, �׽�Ʈ � ����ϱ⵵ ��

                //LHand��� �̸��� ���ӿ�����Ʈ(�������Ʈ) ����
                GameObject handObj = new GameObject("LHand");
                //������� ������Ʈ�� Ʈ�������� lHand ������ �Ҵ�
                lHand = handObj.transform;
                //��Ʈ�ѷ��� ī�޶��� �ڽ� ������Ʈ�� ���
                lHand.parent = Camera.main.transform;
#elif Oculus
                lHand = GameObject.Find("LeftControllerAnchor").transform;
#endif
            }
            return lHand;
        }
    }

    static Transform rHand; //������ ��Ʈ�ѷ�
    public static Transform RHand //������ ��Ʈ�ѷ� ������Ƽ
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
            //��Ʈ�ѷ��� ȸ���� ������
            //���� ȸ������ ���ʹϾ� �̹Ƿ� forward�� ���ؼ� ������� ����
            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            //���� ���� ȸ������ ���� ȸ�������� ��ȯ
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
        //virtualMask�� ���� ���� ButtonTarget �ڷ������� ����ȯ
        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
#elif Oculus
        return OVRInput.Get((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
#endif
    }
    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask�� ���� ���� ButtonTarget �ڷ������� ����ȯ
        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
#elif Oculus
        return OVRInput.GetDown((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
#endif 
    }
    public static bool GetUp(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask�� ���� ���� ButtonTarget �ڷ������� ����ȯ
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
        //���۸� PC������ ������ ǥ�� �Ҽ� �����Ƿ�
    }
    public static void Recenter(Transform target, Vector3 direction)
    {
        //���ϴ� ����(������ �ϴ¹���)���� Ÿ���� ���͸� ����
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

    //���̰� ��� �κп� ũ�ν���� ǥ���ϴ� �޼ҵ�
    public static void DrawCrossHair(Transform crosshair,
                                     bool isHand = true,
                                     Controller hand = Controller.RTouch)
    {
        Ray ray;


        if (isHand)//��Ʈ�ѷ��� ������ ���
        {
#if PC
            //��Ʈ�ѷ��� �������� �ڵ���ġ�� ���� ���
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
        else//��Ʈ�ѷ� ������ �þ� ���� ���� ���
        {
            //���� ī�޶��� �߽ɿ��� �������� ���� ����
            ray = new Ray(Camera.main.transform.position,
                          Camera.main.transform.forward);
        }
        //���̶� �浹�� �ٴ� ����
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;
        if (plane.Raycast(ray, out distance))
        {
            //�浹 ���� ��ġ ��������
            crosshair.position = ray.GetPoint(distance);
            //ī�޶��� �ٶ󺸴� ������ �ݴ����(���� ���ֺ�����)
            crosshair.forward = -Camera.main.transform.forward;
            //�Ÿ��� ���� ũ�ν���� ũ�� Ŀ������
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
        else
        {
            //�浹�� ���� ��� ������ ���� ��ġ���� 100���� �Ÿ��� ����
            crosshair.position = ray.origin + ray.direction * 100;
            //������ ���������� ī�޶� �Ĵٺ�����
            crosshair.forward = -Camera.main.transform.forward;
            //������ �����ϱ� ���� �Ÿ� ���
            distance = (crosshair.position - ray.origin).magnitude;
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
    }
}