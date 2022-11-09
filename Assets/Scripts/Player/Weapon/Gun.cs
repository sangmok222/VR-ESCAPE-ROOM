using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public string gunName;  // ���� �̸�. ���� ������ �������̱� ������.
    public float range;     // ���� ���� �Ÿ�. �Ѹ��� ���� �Ÿ� �ٸ�. �Ѿ��� �ʹ� �ָ����� ������ ���ư��� �ȵǴϱ�.
    public float accuracy;  // ���� ��Ȯ��. ���� �������� ��Ȯ���� �ٸ�.
    public float fireRate;  // ���� �ӵ�. �� �ѹ߰� �ѹ߰��� �ð� ��. ������ ���� ���� ���簡 ������. ���� �������� �ٸ�.
    public float reloadTime;// ������ �ӵ�. ���� �������� �ٸ�.

    public int damage;      // ���� ���ݷ�. ���� �������� �ٸ�.

    public int reloadBulletCount;   // ���� ������ ����. ������ �� �� �� �߾� ����. ���� �������� �ٸ�.
    public int currentBulletCount;  // ���� �� ���� źâ�� �����ִ� �Ѿ��� ����.
    public int maxBulletCount;      // �Ѿ��� �ִ� �� ������ ������ �� �ִ���. 
    public int carryBulletCount;    // ���� �� �ٱ����� �����ϰ� �ִ� �Ѿ��� �� ����.

    public float retroActionForce;  // �ݵ� ����. ���� �������� �ٸ�.
    public float retroActionFineSightForce; // �����ؽ� �ݵ� ����. ���� �������� �ٸ�.

    public Vector3 findSightOriginPos;  // �����ؽ� ���� ���� ��ġ. ������ �� �� ���� ��ġ�� ���ϴϱ� �� ���� ��ġ!
      
    public Animator anim;   // ���� �ִϸ��̼��� ����� �ִϸ����� ������Ʈ
    public ParticleSystem muzzleFlash;  // ȭ���� ����Ʈ ����� ����� ��ƼŬ �ý��� ������Ʈ
    public AudioClip fire_Sound;    // �� �߻� �Ҹ� ����� Ŭ��

}