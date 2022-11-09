using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun; // 현재 들고 있는 총의 ??Gun.cs 가 할당 됨.

    private float currentFireRate; // 이 값이 0 보다 큰 동안에는 총알이 발사 되지 않는다. 초기값은 연사 속도인 ??Gun.cs의 fireRate 

    private AudioSource audioSource;  // 발사 소리 재생기

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
            currentFireRate -= Time.deltaTime;  // 즉, 1 초에 1 씩 감소시킨다.
    }

    private void TryFire()  // 발사 입력을 받음
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    private void Fire()  // 발사를 위한 과정
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    private void Shoot()  // 실제 발사 되는 과정
    {
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play(); 
        Debug.Log("총알 발사 함");
    }
    private void PlaySE(AudioClip _clip)  // 발사 소리 재생
    {
        audioSource.clip = _clip;  // 오디오 클립 할당
        audioSource.Play();       // 오디오 재생
    }
}