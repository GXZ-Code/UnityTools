using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chara_Ctrl : MonoBehaviour
{
    public Chara_Input input;
    public Chara_Action action;
    public BipedIK bipedIK;
    public AimIK amik;
    public LineRenderer line;
    public NavMeshAgent agent;
    public Animator anim;
    private AnimatorOverrideController overrideController;
    public bool isSelected = false;//是否被选中
    public bool isMove = false;
    public bool isCanAttack = false;
    public bool autoAttack = false;
    public bool interActive = false;
    public bool throwAble = false;
    public bool setObj = false;
    public float attack_Dis = 1.5f;//攻击距离
    public float IntA_Dis = 1f;//攻击距离
    public GameObject clickTarget;//鼠标右键点击到的物体
    public Item equip_0;
    public Item equip_1;
    public Item equip_2;
    public Item equip_3;
    public Item usingObj;//正在使用的物品
    public List<Transform> enemyList=new List<Transform>();//敌人的列表，最后从gameManager中获取
    public float lastClickTime = 0f;
    public float doubleClickInterval = 0.3f; // 双击的时间间隔，单位为秒
    public float distanceThreshold = 0.1f; // 到达距离阈值
    public Vector3 movePos = Vector3.zero;//移动到的目标点

    public Inventory inventory;

    //test
    //public AnimationClip clip;
    #region 抛物体
    public Vector3[] lineCount = new Vector3[60];
    public Transform startPoint;
    public float force;
    public float subdiLength = 0.02f;
    public float scrollSpeed = 0.1f;

    #endregion
    
    public Material greenMaterial;
    public LayerMask layer;
    private void Awake()
    {
        inventory = new Inventory();
        input=new Chara_Input(this);   
        action=new Chara_Action(this);
        agent=GetComponent<NavMeshAgent>();
        anim=GetComponentInChildren<Animator>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;
        line=GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.SetWidth(0.1f, 0.1f);
        bipedIK = GetComponentInChildren<BipedIK>();
        amik = GetComponentInChildren<AimIK>();
        //clip = Resources.Load("NearWeapon_Attack")as AnimationClip;
        //overrideController["NoWeapon_Attack"] = clip;
        
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果角色被选中了
        if (isSelected)
        {
            input.Check_Input();
            action.UpdateAciton();
        }
        
    }
    private void OnMouseDown()
    {
        isSelected = true;
    }
}
