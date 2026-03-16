using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isPaused;

    [SerializeField] private float speed;// encaps. serialize faz aparecer no inspector
    private Rigidbody2D rig;
    private Vector2 _direction;
    [SerializeField] private float runSpeed; 
    private float initial_Speed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    private int handlingObj;
    private PlayerItens playerItens;


   

    public Vector2 direction
    {
        get {  return _direction; }
        set { _direction = value; }
    } //encapsulamento

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool IsCutting { get => _isCutting; set => _isCutting = value; }
    public bool IsDigging { get => _isDigging; set => _isDigging = value; }
    public bool IsWatering { get => _isWatering; set => _isWatering = value; }
    public int HandlingObj { get => handlingObj; set => handlingObj = value; }
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initial_Speed = speed;
        playerItens = GetComponent<PlayerItens>();
    }

    private void Update()
    {
        if (!_isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 2;
            }
            OnInput();
            OnRun();
            OnRolling();
            OnCutting();
            OnDig();
            OnWatering();

        }


        
    }
    private void FixedUpdate()// serve pra mexer na fisica
    {
        if (!_isPaused)
        {
            OnMove();
        }

    }

    #region Movement
    void OnMove() {

        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);

    }
    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initial_Speed;
            _isRunning = false;
            
        }
    }
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))//direito
        {
            _isRolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isRolling = false;
        }
       
    }
    void OnDig()
    {
        if (handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))//esquerdo
            {
                _isDigging = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDigging = false;
                speed = initial_Speed;
            }
        }
        else
        {
            IsDigging = false;
        }


    }
    void OnCutting()
    {
        if (handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))//esquerdo
            {
                _isCutting = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isCutting = false;
                speed = initial_Speed;
            }
        }
        else
        {
            IsCutting = false;
        }


    }
    void OnWatering()
    {
        if (handlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItens.TotalWater > 0)//esquerdo
            {
                _isWatering = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0)||playerItens.TotalWater <= 0.00001f)
            {
                _isWatering = false;
                speed = initial_Speed;
            }
            if (IsWatering)
            {
                playerItens.TotalWater -= 0.1f;
            }
        }
        else
        {
            IsWatering = false;
        }
    }




    #endregion
}
