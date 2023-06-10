using System;
using System.Collections.Generic;
using UnityEngine;

namespace GXZ_Library
{
    public class BaseTool
    {
        #region 鼠标点击事件，无论左键按下或者右键按下，返回鼠标发出射线碰撞到的点
        public static Transform GetMouseClickPos()
        {
            if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
            {
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray,out RaycastHit hit))
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
                if (Physics.Raycast(ray, out RaycastHit hit,layer))
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
                if (Physics.Raycast(ray, out RaycastHit hit,layer))
                {
                    return hit.collider.transform;
                }
            }
            return null;
        }
        public static Transform GetClickedObjPos(LayerMask layer,float distance)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, distance,layer))
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
            if (Physics.Raycast(ray, out RaycastHit hit,layer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
        public static Vector3 GetWorldPosOfMouse(LayerMask layer,float distance)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance,layer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
        #endregion
        #region 返回抛物线在某一时间t上的点
        /// <summary>
        /// 返回曲线在某一时间t上的点
        /// </summary>
        /// <param name="_p0">起始点</param>
        /// <param name="_p1">中间点</param>
        /// <param name="_p2">终止点</param>
        /// <param name="t">当前时间t(0.0~1.0)</param>
        /// <returns></returns>
        public static Vector3 GetCurvePoint(Vector3 _p0, Vector3 _p1, Vector3 _p2, float t)
        {
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            float x = ((1 - t) * (1 - t)) * _p0.x + 2 * t * (1 - t) * _p1.x + t * t * _p2.x;
            float y = ((1 - t) * (1 - t)) * _p0.y + 2 * t * (1 - t) * _p1.y + t * t * _p2.y;
            float z = ((1 - t) * (1 - t)) * _p0.z + 2 * t * (1 - t) * _p1.z + t * t * _p2.z;
            Vector3 pos = new Vector3(x, y, z);
            return pos;
        }
        #endregion
        #region 绘制抛物线
        //根据两点绘制一条抛物线
        public static void ParabolaBetween2Points(LineRenderer lineRenderer, float height, int numPoints, Transform startPoint, Transform endPoint)
        {
            lineRenderer.positionCount = numPoints;
            // Calculate the midpoint of the two points
            Vector3 midPoint = (startPoint.position + endPoint.position) / 2f;
            midPoint.y += height;

            // Calculate the points on the quadratic Bezier curve
            for (int i = 0; i < numPoints; i++)
            {
                float t = (float)i / (numPoints - 1);
                Vector3 point = (1 - t) * (1 - t) * startPoint.position
                              + 2 * (1 - t) * t * midPoint
                              + t * t * endPoint.position;
                lineRenderer.SetPosition(i, point);
            }
        }
        #endregion
        #region 物体旋转朝向某一目标
        public static void RotateToTarget(Transform current,Transform target,float speed)
        {
            Vector3 targetDir = target.position - current.position;
            Vector3 newDir = Vector3.MoveTowards(current.forward, targetDir, speed * Time.deltaTime);
            current.rotation = Quaternion.LookRotation(newDir);
        }
        
        #endregion
        #region 鼠标双击
        private float Scale = 0.2f;     //鼠标前后点击的间隔

        private double lastKickTime; // 上一次鼠标抬起的时间（用来处理双击）

        void Start()
        {
            lastKickTime = Time.realtimeSinceStartup;
        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {

                //检测上次点击的时间和当前时间差 在一定范围内认为是双击
                if (Time.realtimeSinceStartup - lastKickTime < Scale)
                {
                    //在这里写入双击所要做的事情

                }

                lastKickTime = Time.realtimeSinceStartup;//重新设置上次点击的时间

            }

        }
        #endregion
    }
}
