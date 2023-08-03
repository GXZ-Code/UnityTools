using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseTools
{
    #region 鼠标点击事件，无论左键按下或者右键按下，返回鼠标发出射线碰撞到的点
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
    #region 获得鼠标点击到的物体的位置
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
    #region 获得鼠标发射射线的碰撞位置
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
        return Vector3.zero; // 或者其他默认位置
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

    #region 物体旋转朝向某一目标
    //缓慢朝向
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
    //立刻朝向
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

    #region 判断两个物体的夹角
    // 判断两个物体正前方向夹角是否小于给定的角度
    public static bool CompareAngle(Transform obj1, Transform obj2, float maxAngle)
    {
        // 获取第一个物体的正前方向向量
        Vector3 forward1 = obj1.forward.normalized;

        // 获取第二个物体相对于第一个物体的向量
        Vector3 direction = obj2.position - obj1.position;
        direction.y = forward1.y;
        // 归一化向量，确保长度为1
        direction.Normalize();

        // 计算夹角的余弦值
        float dotProduct = Vector3.Dot(forward1, direction);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg; // 将余弦值转换为角度
        //Debug.Log(angle);
        // 判断夹角是否小于给定的角度
        return angle < maxAngle;
    }
    #endregion
    #region 朝向目标视野内最近的敌人
    public static Transform DetectView(List<Transform> enemy, Transform npc, float viewDis = 10, float viewAngle = 60)
    {
        Transform target = null;
        //最短距离
        float shortestDis = Mathf.Infinity;
        //遍历所有敌人
        for (int i = 0; i < enemy.Count; i++)
        {
            //得到npc当前索引敌人的距离
            float currentDis = Vector3.Distance(npc.position, enemy[i].transform.position);
            Debug.Log(currentDis);
            if (currentDis <= viewDis && Vector3.Angle(npc.forward, enemy[i].transform.position - npc.position) <= viewAngle)
            {
                Ray ray = new Ray(npc.position, enemy[i].position - npc.position);
                if (Physics.Raycast(ray, out RaycastHit hit, viewDis))
                {
                    //如果当前索引敌人在npc视野内并且没有障碍物
                    if (hit.transform.GetInstanceID() == enemy[i].GetInstanceID())
                    {
                        //如果距离小于最短距离
                        if (currentDis <= shortestDis)
                        {
                            target = enemy[i];
                            //那么当前敌人就是离npc最近的敌人
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
