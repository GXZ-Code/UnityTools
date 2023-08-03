using UnityEngine;
using UnityEngine.AI;

public class SetObj : MonoBehaviour
{
    public GameObject objectPrefab; // 生成的物体预制体
    public Material greenMaterial; // 绿色材质
    public Material newMaterial;   // 新的材质
    public float moveSpeed = 5f;   // 角色移动速度
    public LineRenderer lineRenderer;
    public LayerMask collisionLayer;  // 指定的层

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
        // 按下数字键1生成物体，位置为鼠标实时的世界投射位置，使用绿色材质
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

        // 如果物体存在，按照鼠标实时的世界投射位置进行更新
        if (updatePos)
        {
            mouseWorldPos = GetMouseWorldPosition();
            currentObject.transform.position = mouseWorldPos;
        }

        // 按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            currentObject = null;
            // 获取鼠标投射到世界的位置
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            updatePos = false;

            // 如果距离小于1f，则切换材质
            if (Vector3.Distance(transform.position, mouseWorldPos) < setDis)
            {
                agent.enabled = false;
                if (currentObject != null)
                {
                    currentObject.GetComponent<Renderer>().material = newMaterial;
                    Debug.Log(" 如果距离小于1f，则切换材质");
                }
            }
            else
            {
                Debug.Log(" 如果距离大于1f");
                agent.enabled = true;    
                // 设置角色移动的目标点
                targetPosition = mouseWorldPos;

                // 设置NavMeshAgent的目标位置
                agent.SetDestination(targetPosition);

                // 隐藏LineRenderer
                lineRenderer.enabled = false;
            }
        }
        if (currentObject!=null)
        {
            // 如果距离小于1f，则切换材质
            if (Vector3.Distance(transform.position, mouseWorldPos) < setDis)
            {
                agent.enabled = false;
                if (currentObject != null)
                {
                    currentObject.GetComponent<Renderer>().material = newMaterial;
                    Debug.Log(" 如果距离小于1f，则切换材质");
                }
            }
            else
            {
                currentObject.GetComponent<Renderer>().material = greenMaterial;
            }
        }
       
        // 更新角色的移动路径
        UpdateMovePath();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, collisionLayer))
        {
            return hit.point;
        }
        return Vector3.zero; // 或者其他默认位置
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
