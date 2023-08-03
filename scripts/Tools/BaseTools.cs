using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseTools
{
    #region ������¼�������������»����Ҽ����£�������귢��������ײ���ĵ�
    public static RaycastHit GetMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit;
        }
        return default;
    }
    public static Transform Get_L_MouseClickPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.transform;
            }
        }
        return null;
    }
    public static Vector3 Get_L_MouseClickPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }
    public static Transform Get_R_MouseClickPos()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.transform;
            }
        }
        return null;
    }
    public static RaycastHit Get_R_MouseClickPoint()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit;
            }
        }
        return default(RaycastHit);
    }
    public static Transform GetMouseClickPos()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.transform;
            }
        }
        return null;
    }
    public static Transform GetMouseClickPos(LayerMask layer)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layer))
            {
                return hit.transform;
            }
        }
        return null;
    }
    #endregion;
    #region �����������������λ��
    public static Transform GetClickedObjPos()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }
    public static Transform GetClickedObjPos(LayerMask layer)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layer))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }
    public static Transform GetClickedObjPos(LayerMask layer, float distance)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layer))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }
    #endregion
    #region �����귢�����ߵ���ײλ��
    public static Vector3 GetWorldPosOfMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    public static Vector3 GetWorldPosOfMouse(LayerMask layer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            return hit.point;
        }
        return Vector3.zero; // ��������Ĭ��λ��
    }
    public static Vector3 GetWorldPosOfMouse(LayerMask layer, float distance)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layer))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    #endregion

    #region ������ת����ĳһĿ��
    //��������
    public static void RotateTowardsTarget(Transform source, Transform target, float rotationSpeed)
    {
        Debug.Log("RotateTowardsTarget");
        Vector3 targetDirection = target.position - source.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        source.rotation = Quaternion.Lerp(source.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public static void RotateToTarget(Transform current, Transform target, float speed)
    {
        Vector3 targetDir = target.position - current.position;
        Vector3 newDir = Vector3.MoveTowards(current.forward, targetDir, speed * Time.deltaTime);
        newDir.y = 0;
        current.rotation = Quaternion.LookRotation(newDir);
    }
    public static void RotateToTarget(Vector3 current, Vector3 target, float speed,ref Transform trans)
    {
        Vector3 targetDir = target - current;
        Vector3 newDir = Vector3.MoveTowards(current, targetDir, speed * Time.deltaTime);
        trans.rotation = Quaternion.LookRotation(newDir);
    }
    //���̳���
    public static void FaceTarget(Transform source, Transform target)
    {
        Vector3 direction = target.position - source.position;
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, Vector3.up);
        Quaternion targetRotation = Quaternion.LookRotation(projectedDirection, Vector3.up);
        source.rotation = targetRotation;
    }
    #endregion
    public static void UpdateMovePath(NavMeshAgent agent,LineRenderer lineRenderer)
    {
        if (agent.path != null && agent.path.corners.Length > 1)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = agent.path.corners.Length;
            lineRenderer.SetPositions(agent.path.corners);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    #region �ж���������ļн�
    // �ж�����������ǰ����н��Ƿ�С�ڸ����ĽǶ�
    public static bool CompareAngle(Transform obj1, Transform obj2, float maxAngle)
    {
        // ��ȡ��һ���������ǰ��������
        Vector3 forward1 = obj1.forward.normalized;

        // ��ȡ�ڶ�����������ڵ�һ�����������
        Vector3 direction = obj2.position - obj1.position;
        direction.y = forward1.y;
        // ��һ��������ȷ������Ϊ1
        direction.Normalize();

        // ����нǵ�����ֵ
        float dotProduct = Vector3.Dot(forward1, direction);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg; // ������ֵת��Ϊ�Ƕ�
        //Debug.Log(angle);
        // �жϼн��Ƿ�С�ڸ����ĽǶ�
        return angle < maxAngle;
    }
    #endregion
    #region ����Ŀ����Ұ������ĵ���
    public static Transform DetectView(List<Transform> enemy, Transform npc, float viewDis = 10, float viewAngle = 60)
    {
        Transform target = null;
        //��̾���
        float shortestDis = Mathf.Infinity;
        //�������е���
        for (int i = 0; i < enemy.Count; i++)
        {
            //�õ�npc��ǰ�������˵ľ���
            float currentDis = Vector3.Distance(npc.position, enemy[i].transform.position);
            Debug.Log(currentDis);
            if (currentDis <= viewDis && Vector3.Angle(npc.forward, enemy[i].transform.position - npc.position) <= viewAngle)
            {
                Ray ray = new Ray(npc.position, enemy[i].position - npc.position);
                if (Physics.Raycast(ray, out RaycastHit hit, viewDis))
                {
                    //�����ǰ����������npc��Ұ�ڲ���û���ϰ���
                    if (hit.transform.GetInstanceID() == enemy[i].GetInstanceID())
                    {
                        //�������С����̾���
                        if (currentDis <= shortestDis)
                        {
                            target = enemy[i];
                            //��ô��ǰ���˾�����npc����ĵ���
                            shortestDis = currentDis;

                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            else
            {
                continue;
            }

        }
        return target;
    }
    #endregion
}
