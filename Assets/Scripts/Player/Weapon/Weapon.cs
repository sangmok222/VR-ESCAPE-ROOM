using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range  }//무기 타입, 데미지, 공속, 범위, 효과 변수 생성
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    //IEnumerator 열거형 함수 클래스
    //yeid: 결과를 전달한느 키워드, 시간차 로직 작성 가능
    // WaitForSeconds: 주어진 수치만큼 기다리는 함수
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f); //0.3초 대기
        meleeArea.enabled = false;
        

        yield return new WaitForSeconds(0.3f); //0.3초 대기
        meleeArea.enabled = false;
        
    }

    //  Use() 메인루틴  -> Swing() 서브 루틴  -> Use() 메인 루틴
    /*
        코루틴 함수: 메인루틴 + 코루틴 (동시 실행)
        Use() 메인루틴 + Swing() 코루틴  
    */
}
