using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace GXZ_Library
{
    public class AI
    {
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
        #region 移动到目标点
        public static void MoveNpc2Target(Transform target, NavMeshAgent npc)
        {
            
        }
        #endregion

    }
}
