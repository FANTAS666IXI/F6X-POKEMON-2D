using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float defaultMoveSpeed;
    public float multiplicatorRunMoveSpeed;
    private float currentMoveSpeed;
    private bool isMoving;
    private Vector2 input;
    private GameManager gameManager;
    private MainAudioSource mainAudioSource;

    private void Start()
    {
        InitializeReferences();
        InitializeVariables();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        mainAudioSource = FindObjectOfType<MainAudioSource>();
    }

    private void InitializeVariables()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }

    private void Update()
    {
        CurrentMoveSpeed();
        Movement();
        ModifyVolume();
        QuitGame();
    }

    private void CurrentMoveSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
            currentMoveSpeed = defaultMoveSpeed * multiplicatorRunMoveSpeed;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftShift))
            currentMoveSpeed = defaultMoveSpeed;
    }

    private void Movement()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input.x != 0) input.y = 0;
            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                StartCoroutine(Move(targetPos));
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private void ModifyVolume()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            mainAudioSource.ModifyVolume(true);
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            mainAudioSource.ModifyVolume(false);
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ExitGame();
        }
    }
}