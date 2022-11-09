using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunController))]       // �ڵ����� �� ��ũ��Ʈ�� Ȱ��ȭ�Ǹ� ??GunController.cs �� ������Ʈ�� �ٰ� �ȴ�. �Ǽ� ����(??GunController.cs�� �ʿ��ϴٰ� ������)
public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // ���� ��ü �ߺ� ���� ����. (True�� ���ϰ�)

    [SerializeField]
    private float changeweaponDelayTime;  // ���� ��ü ������ �ð�. ���� ������ ���� �� ���� �ִ� �� �ð�. �뷫 Weapon_Out �ִϸ��̼� �ð�.
    [SerializeField]
    private float changeweaponEndDelayTime;  // ���� ��ü�� ������ ���� ����. �뷫 Weapon_In �ִϸ��̼� �ð�.

    [SerializeField]
    private Gun[] guns;  // ��� ������ ���� ���ҷ� ������ �迭
    [SerializeField]
    private CloseWeapon[] hands;  //  Hand �� ���⸦ ������ �迭
    [SerializeField]
    private CloseWeapon[] axes;  // ���� �� ���⸦ ������ �迭
    [SerializeField]
    private CloseWeapon[] pickaxes;  // ��� �� ���⸦ ������ �迭

    // ���� �������� �̸����� ���� ���� ������ �����ϵ��� Dictionary �ڷ� ���� ���.
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField]
    private string currentWeaponType;  // ���� ������ Ÿ�� (��, ���� ���)
    public static Transform currentWeapon;  // ���� ����. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��.
    public static Animator currentWeaponAnim; // ���� ������ �ִϸ��̼�. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��.

    [SerializeField]
    private GunController theGunController;  // �� �϶� ??GunController.cs Ȱ��ȭ, �ٸ� ������ �� ??GunController.cs ��Ȱ��ȭ 
    [SerializeField]
    private HandController theHandController; // �� �϶� ??HandController.cs Ȱ��ȭ, �ٸ� ������ �� ??HandController.cs ��Ȱ��ȭ
    [SerializeField]
    private AxeController theAxeController; // ���� �϶� ??AxeController.cs Ȱ��ȭ, �ٸ� ������ �� ??AxeController.cs ��Ȱ��ȭ
    [SerializeField]
    private PickaxeController thePickaxeController; // ��� �϶� ??PickaxeController.cs Ȱ��ȭ, �ٸ� ������ �� ??PickaxeController.cs ��Ȱ��ȭ


    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
    }

    void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 ������ '�Ǽ�'���� ���� ��ü ����
                StartCoroutine(ChangeWeaponCoroutine("HAND", "�Ǽ�"));
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 ������ '���� �ӽŰ�'���� ���� ��ü ����
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 ������ '����'�� ���� ��ü ����
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            else if (Input.GetKeyDown(KeyCode.Alpha4)) // 4 ������ '���'�� ���� ��ü ����
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeweaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeweaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                theGunController.CancelFineSight();
                theGunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
        {
            theGunController.GunChange(gunDictionary[_name]);
        }
        else if (_type == "HAND")
        {
            theHandController.CloseWeaponChange(handDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            theAxeController.CloseWeaponChange(axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            thePickaxeController.CloseWeaponChange(pickaxeDictionary[_name]);
        }
    }
}