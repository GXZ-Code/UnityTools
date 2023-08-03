using UnityEngine;

public class DoubleClickHandler : MonoBehaviour
{
    private int clickCount = 0;
    private float lastClickTime = 0f;
    private float doubleClickInterval = 0.3f; // ˫����ʱ��������λΪ��

    public void DoubleClick()
    {
        if (Input.GetMouseButtonDown(0)) // ��������������¼�
        {
            if (Time.time - lastClickTime <= doubleClickInterval)
            {
                // ˫���¼�
                Debug.Log("Double click");
                // ������ִ������˫������

            }
            else
            {
                // �����¼�
                Debug.Log("Single click");
                clickCount = 1;
            }

            lastClickTime = Time.time;
        }
    }
}
