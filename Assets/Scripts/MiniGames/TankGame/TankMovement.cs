using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private int playerHp = 100;
    private int enemyHp = 100;

    [SerializeField]
    private HealthBar playerHealth;
    [SerializeField]
    private HealthBar enemyHealth;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        SetupHpBars();
    }

    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();

    }

    private void SetupHpBars(){
        playerHealth.SetMaxHealth(playerHp);
        enemyHealth.SetMaxHealth(enemyHp);
    }

    public void PlayerHit(int damage){
        playerHp -= damage;
        playerHealth.SetHealth(playerHp);
    }

    public void EnemyDamage(int damage){
        enemyHp -= damage;
        enemyHealth.SetHealth(enemyHp);
    }

    private void GetShootingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement()
    {
        OnMoveTurret?.Invoke(GetMousePositon());
    }

    private Vector2 GetMousePositon()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }
}
