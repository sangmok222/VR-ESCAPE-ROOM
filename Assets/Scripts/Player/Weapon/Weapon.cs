using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range  }//���� Ÿ��, ������, ����, ����, ȿ�� ���� ����
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

    //IEnumerator ������ �Լ� Ŭ����
    //yeid: ����� �����Ѵ� Ű����, �ð��� ���� �ۼ� ����
    // WaitForSeconds: �־��� ��ġ��ŭ ��ٸ��� �Լ�
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f); //0.1�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f); //0.3�� ���
        meleeArea.enabled = false;
        

        yield return new WaitForSeconds(0.3f); //0.3�� ���
        meleeArea.enabled = false;
        
    }

    //  Use() ���η�ƾ  -> Swing() ���� ��ƾ  -> Use() ���� ��ƾ
    /*
        �ڷ�ƾ �Լ�: ���η�ƾ + �ڷ�ƾ (���� ����)
        Use() ���η�ƾ + Swing() �ڷ�ƾ  
    */
}
