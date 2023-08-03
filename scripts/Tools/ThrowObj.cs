using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThrowObj : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] lineCount = new Vector3[60];
    public Transform startPoint;
    public float force;
    public float subdiLength = 0.02f;
    public GameObject obj;
    public float scrollSpeed = 0.1f;
    Vector3 dir;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetWidth(0.1f, 0.1f);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 获取鼠标滚轮的滚动方向
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚动方向更新 float 值
        force += scrollInput * scrollSpeed;

        // 可选：限制 value 的范围，例如确保其不小于0
        force = Mathf.Clamp(force, 2f, 10f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            dir = (hit.point - startPoint.position).normalized;
            dir = new Vector3(dir.x, force*0.05f, dir.z);
        }
        for (int i = 0; i < lineCount.Length; i++)
        {
            lineCount[i] = startPoint.position + dir * force * i * subdiLength + Physics.gravity * (0.5f * (subdiLength * i) * (subdiLength * i));
        }
        lineRenderer.positionCount = lineCount.Length;
        lineRenderer.SetPositions(lineCount);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject throwObj = Instantiate(obj, startPoint.position, startPoint.rotation);
            throwObj.SetActive(true);
            throwObj.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.VelocityChange);
        }
    }
}

