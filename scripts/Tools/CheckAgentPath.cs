using UnityEngine;
using UnityEngine.AI;

public class CheckAgentPath : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 targetPosition;
    public float stoppingDistance = 0.1f; // ֹͣ������ֵ

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                // ���ý�ɫ��Ŀ��λ��
                agent.SetDestination(targetPosition);
            }
        }

        // ����Ƿ��Ѿ�ͣ��
        if (agent.remainingDistance <= stoppingDistance && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Agent �Ѿ�ͣ�£�");
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Ŀ��㲻�ɴ");
        }
    }
}
