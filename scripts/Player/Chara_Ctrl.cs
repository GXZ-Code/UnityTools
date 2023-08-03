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
    public bool isSelected = false;//�Ƿ�ѡ��
    public bool isMove = false;
    public bool isCanAttack = false;
    public bool autoAttack = false;
    public bool interActive = false;
    public bool throwAble = false;
    public bool setObj = false;
    public float attack_Dis = 1.5f;//��������
    public float IntA_Dis = 1f;//��������
    public GameObject clickTarget;//����Ҽ������������
    public Item equip_0;
    public Item equip_1;
    public Item equip_2;
    public Item equip_3;
    public Item usingObj;//����ʹ�õ���Ʒ
    public List<Transform> enemyList=new List<Transform>();//���˵��б�����gameManager�л�ȡ
    public float lastClickTime = 0f;
    public float doubleClickInterval = 0.3f; // ˫����ʱ��������λΪ��
    public float distanceThreshold = 0.1f; // ���������ֵ
    public Vector3 movePos = Vector3.zero;//�ƶ�����Ŀ���

    public Inventory inventory;

    //test
    //public AnimationClip clip;
    #region ������
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
        //�����ɫ��ѡ����
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
