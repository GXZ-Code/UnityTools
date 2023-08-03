using Global;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static RootMotion.FinalIK.InteractionTrigger.Range;
using static UnityEditor.Progress;

public class Chara_Action 
{
    Chara_Ctrl chara;
    Vector3 dir;
    public delegate void Set_Item();
    Set_Item set_Item;
    private GameObject temp;

    public Chara_Action(Chara_Ctrl chara_Ctrl)
    {
        chara = chara_Ctrl;
       
    }
    public void StartAction()
    {
        switch (chara.usingObj.GetComponent<Item>().type)
        {
            case ItemType.EquipWeapon:
                ReSetTrigger();
                if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("crouch") || chara.anim.GetCurrentAnimatorStateInfo(0).IsName("stand"))
                {
                    Exc_EquipWeapon();
                } 
                break;
            case ItemType.Setable:
                ReSetTrigger();
                if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("crouch") || chara.anim.GetCurrentAnimatorStateInfo(0).IsName("stand"))
                {
                   
                    chara.usingObj.GetComponent<Renderer>().material = chara.greenMaterial;
                    chara.usingObj.GetComponent<Collider>().enabled = false;
                    temp = GameObject.Instantiate(chara.usingObj.gameObject, chara.movePos, Quaternion.identity);
                    chara.setObj = true;
                    set_Item = null;
                    set_Item = Exc_SetObj_Pos;
                }

                if (chara.anim.GetBool(AnimPara.equip))
                {
                    chara.anim.ResetTrigger(AnimPara.attack);
                    chara.anim.SetBool(AnimPara.equip, false);
                }
                break;
            case ItemType.ThrowObj:
                ReSetTrigger();
                if ((chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.crouch) || chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.stand)))
                {
                    chara.throwAble = true;
                }
                if (chara.anim.GetBool(AnimPara.equip))
                {
                    chara.anim.ResetTrigger(AnimPara.attack);
                    chara.anim.SetBool(AnimPara.equip, false);
                }
                break;
        }
    }
    public void UpdateAciton()
    { 
        
        //判断角色是否到达目标位置
        if (chara.isMove)
        { 
            // 更新角色的移动路径
            BaseTools.UpdateMovePath(chara.agent, chara.line);
            if (Vector3.Distance(chara.transform.position, chara.agent.destination) <= chara.distanceThreshold)
            {
                // 角色已到达目标位置
                chara.anim.SetFloat(AnimPara.blend, 0);
                chara.isMove = false;
            }
        }
        //判断是否可交互
        if (chara.interActive)
        {
            if ((chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.crouch) || chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.stand)) &&
               (!chara.anim.GetBool(AnimPara.equip)))
            {
                //Debug.Log("interActive");
                if ((Vector3.Distance(chara.movePos, chara.transform.position) <= chara.IntA_Dis))
                {
                    chara.line.enabled = false;
                    chara.isMove = false;
                    chara.anim.SetFloat(AnimPara.blend, 0);
                    chara.agent.enabled = false;
                    chara.anim.ResetTrigger(AnimPara.moveable);
                    BaseTools.RotateToTarget(chara.transform, chara.input.hit.collider.transform, 30f);
                    if (BaseTools.CompareAngle(chara.transform, chara.clickTarget.transform, 5f))
                    {
                        //根据点击到的物体执行不同的操作
                        switch (chara.input.hit.collider.tag)
                        {
                            case "search_item"://搜索物品
                                IntA_Env_Search();
                                break;
                            case "switch"://开关
                                IntA_Env_Switch();
                                break;
                            case "pick_item"://拾取
                                IntA_Env_Pick();
                                break;
                        }
                        chara.interActive = false;
                    }
                    if (chara.anim.GetBool(AnimPara.equip))
                    {
                        chara.anim.SetBool(AnimPara.equip, false);
                    }

                }
            }
          
        }
        //判断是否可以攻击
        if (chara.isCanAttack)
        {
            BaseTools.RotateToTarget(chara.transform, chara.input.hit.collider.transform, 30f);
            if (chara.anim.GetBool(AnimPara.equip))
            {
                chara.attack_Dis = 10f;
            }
            else
            {
                
                chara.attack_Dis = 1.5f;

            }
            //Debug.Log(BaseTools.CompareAngle(chara.transform, chara.clickTarget.transform,5f));
            if ((Vector3.Distance(chara.transform.position, chara.clickTarget.transform.position) <= chara.attack_Dis))
            {
                chara.anim.ResetTrigger(AnimPara.moveable);
                chara.line.enabled = false;
                chara.isMove = false;
                chara.anim.SetFloat(AnimPara.blend, 0f);
                chara.agent.enabled = false;
                if (BaseTools.CompareAngle(chara.transform, chara.clickTarget.transform, 5f))
                {
                    chara.action.IntA_Env_Enemy();
                }
                
            }
        }
        //投掷物体
        if (chara.throwAble)
        {
            Exc_ThrowObj();
        }
        if (chara.setObj)
        {
            set_Item();
        }
        //持枪状态下是否自动攻击
        if (chara.anim.GetBool(AnimPara.equip)&&chara.autoAttack)
        {
            AutoAttack();
        }
    }

    #region 右键点击操作
    public void IntA_Env_Enemy()
    {
        if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("attack") &&
          chara.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f ||
          chara.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_crouch") &&
          chara.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            //Debug.Log("IntA_Env_Enemy ");
            // 当前正在播放指定的动画片段
            return;
        }
        else
        {
            chara.anim.SetTrigger(AnimPara.attack);
        }
        if (!chara.anim.GetBool(AnimPara.equip))
        {
            chara.isCanAttack = false;
        }

    }
    private void IntA_Env_Pick()
    {
        chara.line.enabled = false;
        chara.anim.ResetTrigger(AnimPara.moveable);
        //Debug.Log("IntA_Env_Pick");
        chara.anim.SetTrigger(AnimPara.pick);
        chara.clickTarget.GetComponent<Item>().pickable = false;
        chara.inventory.AddItem(chara.clickTarget.GetComponent<Item>());
        Debug.Log(chara.inventory.items.Count); 
    }

    private void IntA_Env_Switch()
    {
        chara.anim.ResetTrigger(AnimPara.moveable);
        throw new NotImplementedException();
    }

    private void IntA_Env_Search()
    {
        chara.anim.ResetTrigger(AnimPara.moveable);
        if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("Search") &&
          chara.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            Debug.Log("IntA_Env_Search ");
            // 当前正在播放指定的动画片段
            return;
        }
        else
        {
            chara.anim.SetTrigger(AnimPara.search);
        }
    }

    public void ReSetTrigger()
    {
        //Debug.Log("ReSetTrigger ");
        GameObject.Destroy(temp);
        temp = null;
        chara.isCanAttack = false;
        chara.interActive = false;
        chara.throwAble = false;
        chara.isMove = false;
        chara.setObj = false;
        chara.agent.enabled = false;
        //chara.usingObj = null;

        chara.anim.ResetTrigger(AnimPara.reload);
        chara.anim.ResetTrigger(AnimPara._throw);
        chara.anim.ResetTrigger(AnimPara.attack);
        chara.anim.ResetTrigger(AnimPara.pick);
        chara.anim.ResetTrigger(AnimPara.search);
        chara.anim.ResetTrigger(AnimPara.set);
        chara.anim.SetFloat(AnimPara.blend, 0);
        //chara.anim.SetTrigger(AnimPara.moveable);
    }   
    // 双击事件 跑
    public void Doubleclick()
    {
        if (chara.isMove && chara.movePos != Vector3.zero)
        {
            chara.anim.SetBool(AnimPara.crouch, false);
            chara.agent.speed = 8f;
            chara.anim.SetFloat(AnimPara.blend, 2);
        }
    }
    //角色移动
    public void Chara_Move()
    {
        ReSetTrigger();
        chara.isMove=true;
        chara.anim.SetTrigger(AnimPara.moveable);
        chara.agent.enabled = true;
        // 单击事件 走 或者蹲着走
        if (chara.anim.GetBool(AnimPara.crouch))
        {
            chara.agent.speed = 1.5f;
            chara.anim.SetFloat(AnimPara.blend, 0.5f);
        }
        else
        {
            chara.agent.speed = 2.5f;
            chara.anim.SetFloat(AnimPara.blend, 1);
        }
        if (chara.movePos != Vector3.zero)
        {   
            chara.agent.SetDestination(chara.movePos);
        }
    }
    #endregion

    #region 按键操作
    private void AutoAttack()
    {
        chara.clickTarget = BaseTools.DetectView(chara.enemyList, chara.transform).gameObject;
        Debug.Log("AutoAttack ");
    }
    public void Exc_Input_C()
    {
        chara.anim.SetBool(AnimPara.crouch, !chara.anim.GetBool(AnimPara.crouch));
        if (chara.isMove)
        {
            if (chara.anim.GetBool(AnimPara.crouch))
            {
                chara.agent.speed = 1.5f;
            }
            else
            {
                chara.agent.speed = 2.5f;
                chara.anim.SetFloat(AnimPara.blend, 1);
            }
        }
    }
    public void Exc_Input_R()
    {
        if (chara.isSelected && chara.anim.GetBool(AnimPara.equip))
        {
            chara.isMove = false;
            chara.anim.SetFloat(AnimPara.blend, 0);
            chara.agent.enabled = false;
            chara.anim.SetTrigger(AnimPara.reload);
        }
    }
    public void Exc_Input_T()
    {
        if (chara.anim.GetBool(AnimPara.equip))
        {
            chara.autoAttack = !chara.autoAttack;
        }
    }
    private void Exc_ThrowObj()
    {
   
        chara.line.enabled = true;
        chara.anim.ResetTrigger(AnimPara.moveable);
        chara.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        // 获取鼠标滚轮的滚动方向
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚动方向更新 float 值
        chara.force += scrollInput * chara.scrollSpeed;

        // 可选：限制 value 的范围，例如确保其不小于0
        chara.force = Mathf.Clamp(chara.force, 2f, 10f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            dir = (hit.point - chara.startPoint.position).normalized;
            dir = new Vector3(dir.x, chara.force * 0.05f, dir.z);
        }
        for (int i = 0; i < chara.lineCount.Length; i++)
        {
            chara.lineCount[i] = chara.startPoint.position + dir * chara.force * i * chara.subdiLength + Physics.gravity * (0.5f * (chara.subdiLength * i) * (chara.subdiLength * i));
        }
        chara.line.positionCount = chara.lineCount.Length;
        chara.line.SetPositions(chara.lineCount);
        if (Input.GetMouseButtonDown(0))
        {
            chara.anim.SetTrigger(AnimPara._throw);
            GameObject throwObj = GameObject.Instantiate(chara.usingObj.gameObject, chara.startPoint.position, chara.startPoint.rotation);
            throwObj.SetActive(true);
            throwObj.GetComponent<Rigidbody>().AddForce(dir * chara.force, ForceMode.VelocityChange);
            chara.throwAble = false;
            chara.line.enabled = false;
        }

    }

    private void Exc_SetObj_Pos()
    {
        temp.SetActive(true);
        // 如果物体存在，按照鼠标实时的世界投射位置进行更新
        temp.transform.position = BaseTools.GetWorldPosOfMouse(chara.layer);
        // 按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            temp .SetActive(false);
            // 获取鼠标投射到世界的位置
            chara.movePos = BaseTools.GetWorldPosOfMouse(chara.layer);
            if (Vector3.Distance(chara.transform.position, chara.movePos) < chara.IntA_Dis)
            {

                if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("set") &&
                    chara.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                {
                    //Debug.Log("set ");
                    // 当前正在播放指定的动画片段
                    return;
                }
                else
                {
                    Debug.Log("set ");
                    chara.usingObj = null;
                    temp = null;
                    //GameObject.Instantiate(chara.usingObj, chara.movePos, Quaternion.identity);
                    chara.line.enabled = false;
                    chara.isMove = false;
                    chara.anim.SetFloat(AnimPara.blend, 0);
                    chara.agent.enabled = false;
                    chara.anim.ResetTrigger(AnimPara.moveable);
                    chara.anim.SetTrigger(AnimPara.set);
                    set_Item -= EXC_SetObj_Action; Debug.Log("EXC_SetObj_Action");
                    chara.setObj = false;
                }

            }
            else
            {
                Chara_Move();
                chara.setObj = true;
            }
            set_Item -= Exc_SetObj_Pos;
            set_Item += EXC_SetObj_Action;
        }


    }
    private void EXC_SetObj_Action()
    {
        
        if (Vector3.Distance(chara.transform.position,chara.movePos)<chara.IntA_Dis)
        {
            if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName("set") &&
                chara.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                //Debug.Log("set ");
                // 当前正在播放指定的动画片段
                return;
            }
            else
            {
                //Debug.Log("set ");
                chara.usingObj = null;
                //GameObject.Instantiate(chara.usingObj, chara.movePos, Quaternion.identity);
                chara.line.enabled = false;
                chara.isMove = false;
                chara.anim.SetFloat(AnimPara.blend, 0);
                chara.agent.enabled = false;
                chara.anim.ResetTrigger(AnimPara.moveable);
                chara.anim.SetTrigger(AnimPara.set);
                set_Item -= EXC_SetObj_Action; 
                chara.setObj = false;
            }
           
        }
    }

    private void Exc_EquipWeapon()
    {
        if (chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.crouch) || chara.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimPara.stand))
        {
            chara.anim.ResetTrigger(AnimPara.moveable);
            chara.isMove = false;
            chara.anim.SetFloat(AnimPara.blend, 0);
            chara.agent.enabled = false;
            chara.anim.SetBool(AnimPara.equip, !chara.anim.GetBool(AnimPara.equip));
            if (chara.anim.GetBool(AnimPara.equip))
            {
                chara.anim.ResetTrigger(AnimPara.attack);
            }
        }
    }
    #endregion

    public void Equip_Weapon()
    {

    }
}
