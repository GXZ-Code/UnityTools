using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool isPerformingAction = false;

    private void Update()
    {
        // ��ⰴ�µ����ּ�
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

        // �������Ҽ����
        if (Input.GetMouseButtonDown(1))
        {
            CancelActions();
            StartRightClickAction();
        }
    }
    #region ִ�в���
    private void StartAction1()
    {
        if (isPerformingAction) return;

        // ִ�в���1
        Debug.Log("ִ�в���1");

        isPerformingAction = true;
    }

    private void StartAction2()
    {
        if (isPerformingAction) return;

        // ִ�в���2
        Debug.Log("ִ�в���2");

        isPerformingAction = true;
    }

    private void StartAction3()
    {
        if (isPerformingAction) return;

        // ִ�в���3
        Debug.Log("ִ�в���3");

        isPerformingAction = true;
    }

    private void StartAction4()
    {
        if (isPerformingAction) return;

        // ִ�в���4
        Debug.Log("ִ�в���4");

        isPerformingAction = true;
    }

    private void StartAction5()
    {
        if (isPerformingAction) return;

        // ִ�в���5
        Debug.Log("ִ�в���5");

        isPerformingAction = true;
    }
    #endregion

    private void StartRightClickAction()
    {
        if (isPerformingAction) return;

        // ִ������Ҽ�����
        Debug.Log("ִ������Ҽ�����");
    }

    private void CancelActions()
    {
        if (!isPerformingAction) return;

        // ȡ������
        Debug.Log("ȡ������");

        isPerformingAction = false;
    }
}
