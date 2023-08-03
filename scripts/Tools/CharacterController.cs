using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool isPerformingAction = false;

    private void Update()
    {
        // 检测按下的数字键
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartAction1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartAction2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartAction3();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartAction4();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartAction5();
        }

        // 检测鼠标右键点击
        if (Input.GetMouseButtonDown(1))
        {
            CancelActions();
            StartRightClickAction();
        }
    }
    #region 执行操作
    private void StartAction1()
    {
        if (isPerformingAction) return;

        // 执行操作1
        Debug.Log("执行操作1");

        isPerformingAction = true;
    }

    private void StartAction2()
    {
        if (isPerformingAction) return;

        // 执行操作2
        Debug.Log("执行操作2");

        isPerformingAction = true;
    }

    private void StartAction3()
    {
        if (isPerformingAction) return;

        // 执行操作3
        Debug.Log("执行操作3");

        isPerformingAction = true;
    }

    private void StartAction4()
    {
        if (isPerformingAction) return;

        // 执行操作4
        Debug.Log("执行操作4");

        isPerformingAction = true;
    }

    private void StartAction5()
    {
        if (isPerformingAction) return;

        // 执行操作5
        Debug.Log("执行操作5");

        isPerformingAction = true;
    }
    #endregion

    private void StartRightClickAction()
    {
        if (isPerformingAction) return;

        // 执行鼠标右键操作
        Debug.Log("执行鼠标右键操作");
    }

    private void CancelActions()
    {
        if (!isPerformingAction) return;

        // 取消操作
        Debug.Log("取消操作");

        isPerformingAction = false;
    }
}
