using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun; // ���� ��� �ִ� ���� ??Gun.cs �� �Ҵ� ��.

    private float currentFireRate; // �� ���� 0 ���� ū ���ȿ��� �Ѿ��� �߻� ���� �ʴ´�. �ʱⰪ�� ���� �ӵ��� ??Gun.cs�� fireRate 

    private AudioSource audioSource;  // �߻� �Ҹ� �����

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    { 
        GunFireRateCalc();
        TryFire();
    }

    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;  // ��, 1 �ʿ� 1 �� ���ҽ�Ų��.
    }

    private void TryFire()  // �߻� �Է��� ����
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    private void Fire()  // �߻縦 ���� ����
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    private void Shoot()  // ���� �߻� �Ǵ� ����
    {
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play(); 
        Debug.Log("�Ѿ� �߻� ��");
    }
    private void PlaySE(AudioClip _clip)  // �߻� �Ҹ� ���
    {
        audioSource.clip = _clip;  // ����� Ŭ�� �Ҵ�
        audioSource.Play();       // ����� ���
    }
}