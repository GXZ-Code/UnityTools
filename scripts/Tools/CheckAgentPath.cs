using UnityEngine;
using UnityEngine.AI;

public class CheckAgentPath : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 targetPosition;
    public float stoppingDistance = 0.1f; // 停止距离阈值

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                // 设置角色的目标位置
                agent.SetDestination(targetPosition);
            }
        }

        // 检查是否已经停下
        if (agent.remainingDistance <= stoppingDistance && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Agent 已经停下！");
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("目标点不可达！");
        }
    }
}
