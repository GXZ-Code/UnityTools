using Global;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RootMotion.FinalIK.InteractionTrigger.Range;

public class Chara_Input
{
    Chara_Ctrl chara;
    public RaycastHit hit;
    
    public Chara_Input(Chara_Ctrl chara_Ctrl)
    {
        chara = chara_Ctrl;
    }

    public void Check_Input()
    {
        CheckMouse();
        CheckKeyBoard();
    }

    private void CheckMouse()
    {
        //��ɫ�ƶ�
        if (Input.GetMouseButtonDown(1)) // ��������Ҽ�����¼�
        {
            if (Time.time - chara.lastClickTime <= chara.doubleClickInterval)
            {
                
                //Debug.Log("Double click");
                // ������ִ������˫������
                chara.action.Doubleclick();

            }
            else
            {
                hit = BaseTools.Get_R_MouseClickPoint();
                if (chara.usingObj!=null)
                {
                    chara.usingObj.gameObject.SetActive(false);
                }
                //�жϵ㵽��ʲô
                switch (hit.collider.gameObject.layer)
                {
                    case 6://move
                        chara.clickTarget = null;
                        chara.movePos = hit.point;
                        chara.action.Chara_Move();
                        break;
                    case 7://interAvtive
                        ResetClick(chara.IntA_Dis,chara.interActive);
                        chara.interActive = true;
                        break;
                    case 8://attack
                        ResetClick(chara.attack_Dis,chara.isCanAttack);
                        chara.isCanAttack = true;
                        break;
                }
            }
            chara.lastClickTime = Time.time;
        }
    }
    private void ResetClick(float dis,bool n)
    {
        chara.movePos = hit.collider.transform.position;
        chara.clickTarget = hit.collider.gameObject;
        if ((Vector3.Distance(chara.clickTarget.transform.position, chara.transform.position) >= dis))
        {
            chara.action.Chara_Move();
            n = true;
        }
    }
    private void CheckKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            chara.action.Exc_Input_T();
        }
        //����c����ɫ����
        if (Input.GetKeyDown(KeyCode.C))
        {
           chara.action.Exc_Input_C();
        }
        // ��ⰴ�µ����ּ�
        if (Input.GetKeyDown(KeyCode.R))
        {
            chara.action.Exc_Input_R();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chara.line.enabled= false;
            //Debug.Log("key 1");
            // �ж��Ƿ���������
            if (chara.equip_0 != null)
            {
                if (chara.equip_0==chara.usingObj)
                {
                    //chara.usingObj = chara.equip_0;
                    chara.action.StartAction();
                }
                else
                {
                    chara.anim.SetTrigger(AnimPara.unEquip);
                    chara.usingObj = chara.equip_0;
                    chara.action.StartAction();

                }
               
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chara.line.enabled = false;
            //Debug.Log("key 2");
            // �ж��Ƿ���������
            if (chara.equip_1 != null)
            {
                chara.usingObj = chara.equip_1;
                chara.action.StartAction();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chara.line.enabled = false;
            // �ж��Ƿ���������
            if (chara.equip_2 != null)
            {
                chara.usingObj = chara.equip_2;
                chara.action.StartAction();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            chara.line.enabled = false;
            // �ж��Ƿ���������
            if (chara.equip_3 != null)
            {
                chara.usingObj = chara.equip_3;
                chara.action.StartAction();
            }
        }
    }
}
