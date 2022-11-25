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
    public float grabRange = 0.2f; //�׷� ��Ÿ�

    public bool isRemoteGrab = true;
    public float remoteGrabDistance = 20f;

    [Header("������ ����")]
    Vector3 prevPos; //������ġ
    public float throwPower = 10f;

    Quaternion prevRot;
    public float rotPower = 50f;

    void Start()
    {

    }

    void Update()
    {
        //��� �ִ� ��ü�� �ƹ��͵� ���� ��
        if (isGrabbing == false)
        {
            //�������� �õ��ϴ� �Լ� ȣ��
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
            //���Ÿ� �׷� Ȱ��ȭ�� ��� ����
            if (isRemoteGrab)
            {
                Ray ray = new Ray(ARVRInput.RHandPosition,
                                  ARVRInput.RHandDirection);
                RaycastHit hitInfo;
                if (Physics.SphereCast(ray, 0.5f, //0.5�� �߻��ϴ� ��ü �ݰ�
                                      out hitInfo,
                                      remoteGrabDistance,
                                      grabbedLayer))
                {
                    isGrabbing = true;
                    grabbedObject = hitInfo.transform.gameObject;
                    //��ü�� �������� �ڷ�ƾ �Լ� ȣ��
                    StartCoroutine(GrabbingAnimation());
                }
            }

            Collider[] hitObjects = Physics.OverlapSphere(ARVRInput.RHandPosition, //���� ��ġ
                                                          grabRange, //����
                                                          grabbedLayer); //�浹 ���� ���̾�
            //���� ����� ������Ʈ�� �ε��� ����� ����
            int closest = 0;

            //��ü ������Ʈ�� �����ϸ� �Ÿ��� ���ؾ� �ϹǷ�
            //foreach ������ �������� ����.
            //�켱 ��ü ������Ʈ�� �Ÿ� ��� �� �ִܰŸ� �ε��� ����
            for (int i = 1; i < hitObjects.Length; i++)
            {
                Vector3 closestPos = hitObjects[closest].transform.position;
                //�켱 ������ ������Ʈ�� ���� ��Ʈ�ѷ��� �Ÿ� ��
                float closestDistance = Vector3.Distance(closestPos,
                                                         ARVRInput.RHandPosition);
                Vector3 nextPos = hitObjects[i].transform.position;
                float nextDistance = Vector3.Distance(nextPos,
                                                      ARVRInput.RHandPosition);
                //�� �� �ε����� �ִ� ������Ʈ �Ÿ� ���Ͽ�
                //���� �ε����� �Ÿ��� ���� ����� �ִܰŸ����� ª���� �ε��� ����
                if (nextDistance < closestDistance)
                    closest = i;
            }

            //����� ������Ʈ�� �����Ѵٸ�
            if (hitObjects.Length > 0)
            {
                isGrabbing = true;
                //���� for������ ���� ���� ����� ������Ʈ�� �ε����� ���
                //���� ������ �ִ� ������Ʈ ���������
                grabbedObject = hitObjects[closest].gameObject;
                //�θ� ������Ʈ�� ������ ��Ʈ�ѷ��� ����
                grabbedObject.transform.parent = ARVRInput.RHand;

                //�տ� ��ź�� ��� ���� ��쿡 ��ź�� ������� ����
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                //��ź�� ����� �ʱ� ��ġ ����
                prevPos = ARVRInput.RHandPosition;
                //���� ��Ʈ�ѷ��� ȸ������ �ʱ� ������ ����
                prevRot = ARVRInput.RHand.rotation;
            }
        }
    }

    void TryUngrab()
    {
        //���� ���� ����
        Vector3 throwDirection = ARVRInput.RHandPosition - prevPos;
        prevPos = ARVRInput.RHandPosition;

        Quaternion deltaRotation = ARVRInput.RHand.rotation *
                                   Quaternion.Inverse(prevRot);
        prevRot = ARVRInput.RHand.rotation;

        if (ARVRInput.GetUp(ARVRInput.Button.HandTrigger,
                           ARVRInput.Controller.RTouch))
        {
            isGrabbing = false;
            //�������� ���� Gravity �� ���� �߷� �ۿ� ����
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //�θ� ������ ���������Ƿ� �ش� ��ġ���� ��������(�߷�)
            grabbedObject.transform.parent = null;

            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower;

            //���ӵ� = (1/dt) * d��Ÿ(Ư�� �� ���� ���� ���� = ����� ����)
            float angle;
            Vector3 axis;
            deltaRotation.ToAngleAxis(out angle, out axis);
            Vector3 angularVelocity = (1f / Time.deltaTime) * angle * axis;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity * rotPower;


            //��� �ִ� ������Ʈ �����Ͽ� ���� ������ ������ ��
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

        //�ð� �����
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