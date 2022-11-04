using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[ExecuteInEditMode]
public class Flag : MonoBehaviour
{


    [SerializeField] private string flagName = "Flag Name";
    [SerializeField] private string combineFlagName = null;
    [SerializeField] private Flag[] nextFlags = null;

    // private Color debugLineColor = Color.white;
    public Color color = Color.red;//����� ����
    public float radius = 0.5f;//������� ũ��
    public string FlagName { get { return flagName; } }

    private void Start()
    {
        //string name = "A" + "B" + "C";
        StringBuilder sb = new StringBuilder();
        sb.Append(flagName);
        if (nextFlags != null && nextFlags.Length > 0)
        {
            sb.Append("-");
            foreach (Flag flag in nextFlags)
            {
                sb.AppendFormat("{0}-", flag.FlagName);
            }
        }
        combineFlagName = sb.ToString();



    }

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        //DrawSphere(������ġ,ũ��)
        //���� ����� ����� ����
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}