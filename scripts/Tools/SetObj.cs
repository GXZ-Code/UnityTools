using UnityEngine;
using UnityEngine.AI;

public class SetObj : MonoBehaviour
{
    public GameObject objectPrefab; // ���ɵ�����Ԥ����
    public Material greenMaterial; // ��ɫ����
    public Material newMaterial;   // �µĲ���
    public float moveSpeed = 5f;   // ��ɫ�ƶ��ٶ�
    public LineRenderer lineRenderer;
    public LayerMask collisionLayer;  // ָ���Ĳ�

    private GameObject currentObject;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool updatePos;
    private float distanceToTarget;
    [SerializeField]
    private float setDis;
    private Vector3 mouseWorldPos;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // �������ּ�1�������壬λ��Ϊ���ʵʱ������Ͷ��λ�ã�ʹ����ɫ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            updatePos = true;
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            if (currentObject != null)
            {
                Destroy(currentObject);
            }
            currentObject = Instantiate(objectPrefab, mouseWorldPos, Quaternion.identity);
            currentObject.GetComponent<Renderer>().material = greenMaterial;
        }

        // ���������ڣ��������ʵʱ������Ͷ��λ�ý��и���
        if (updatePos)
        {
            mouseWorldPos = GetMouseWorldPosition();
            currentObject.transform.position = mouseWorldPos;
        }

        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            currentObject = null;
            // ��ȡ���Ͷ�䵽�����λ��
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            updatePos = false;

            // �������С��1f�����л�����
            if (Vector3.Distance(transform.position, mouseWorldPos) < setDis)
            {
                agent.enabled = false;
                if (currentObject != null)
                {
                    currentObject.GetComponent<Renderer>().material = newMaterial;
                    Debug.Log(" �������С��1f�����л�����");
                }
            }
            else
            {
                Debug.Log(" ����������1f");
                agent.enabled = true;    
                // ���ý�ɫ�ƶ���Ŀ���
                targetPosition = mouseWorldPos;

                // ����NavMeshAgent��Ŀ��λ��
                agent.SetDestination(targetPosition);

                // ����LineRenderer
                lineRenderer.enabled = false;
            }
        }
        if (currentObject!=null)
        {
            // �������С��1f�����л�����
            if (Vector3.Distance(transform.position, mouseWorldPos) < setDis)
            {
                agent.enabled = false;
                if (currentObject != null)
                {
                    currentObject.GetComponent<Renderer>().material = newMaterial;
                    Debug.Log(" �������С��1f�����л�����");
                }
            }
            else
            {
                currentObject.GetComponent<Renderer>().material = greenMaterial;
            }
        }
       
        // ���½�ɫ���ƶ�·��
        UpdateMovePath();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, collisionLayer))
        {
            return hit.point;
        }
        return Vector3.zero; // ��������Ĭ��λ��
    }

    private void UpdateMovePath()
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
}
