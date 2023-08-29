using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float defaultMoveSpeed;
    public float multiplicatorRunMoveSpeed;
    private float currentMoveSpeed;
    private bool isMoving;
    private bool isRuning;
    private Vector2 input;
    private GameManager gameManager;
    private MainAudioSource mainAudioSource;
    private Animator animator;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        mainAudioSource = FindObjectOfType<MainAudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }

    private void Update()
    {
        SetRuning();
        Movement();
        ModifyVolume();
        ExitGame();
    }

    private void SetRuning()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed = defaultMoveSpeed * multiplicatorRunMoveSpeed;
            animator.speed = multiplicatorRunMoveSpeed;
            if (!isRuning)
            {
                SwitchIsRuning();
                gameManager.ConsoleLog($"Player Start Runing, CurrentSpeed = {currentMoveSpeed:F1}");
            }
        }
        else
        {
            currentMoveSpeed = defaultMoveSpeed;
            animator.speed = 1;
            if (isRuning)
            {
                SwitchIsRuning();
                gameManager.ConsoleLog($"Player Stop Runing, CurrentSpeed = {currentMoveSpeed:F1}");
            }
        }
    }

    private void SwitchIsRuning()
    {
        isRuning = !isRuning;
    }

    private void Movement()
    {
        if (!isMoving)
        {
            GetMovementInputs();
            if (input != Vector2.zero)
            {
                SetAnimatorMoveXY();
                StartCoroutine(Move(ObtainTargetPos()));
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    private void GetMovementInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x != 0) input.y = 0;
    }

    private void SetAnimatorMoveXY()
    {
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        gameManager.ConsoleLog($"Player Move By ({input.x},{input.y}).", 5);
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private Vector3 ObtainTargetPos()
    {
        var targetPos = transform.position;
        targetPos.x += input.x;
        targetPos.y += input.y;
        return targetPos;
    }

    private void ModifyVolume()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            mainAudioSource.ModifyVolume(true);
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            mainAudioSource.ModifyVolume(false);
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.ExitGame();
    }
}