using UnityEngine;

public class DoubleClickHandler : MonoBehaviour
{
    private int clickCount = 0;
    private float lastClickTime = 0f;
    private float doubleClickInterval = 0.3f; // 双击的时间间隔，单位为秒

    public void DoubleClick()
    {
        if (Input.GetMouseButtonDown(0)) // 监听鼠标左键点击事件
        {
            if (Time.time - lastClickTime <= doubleClickInterval)
            {
                // 双击事件
                Debug.Log("Double click");
                // 在这里执行您的双击操作

            }
            else
            {
                // 单击事件
                Debug.Log("Single click");
                clickCount = 1;
            }

            lastClickTime = Time.time;
        }
    }
}
