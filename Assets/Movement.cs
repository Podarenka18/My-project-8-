using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f; // Дополнительно: скорость спринта
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Компоненты")]
    [SerializeField] private Rigidbody2D rb; // Для физического движения
    [SerializeField] private Animator animator; // Опционально: анимация

    private Vector2 movementInput;
    private bool isSprinting;

    private void Update()
    {
        // Получаем ввод с клавиатуры (WASD)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize(); // Чтобы диагональное движение не было быстрее

        // Проверка на спринт (зажатый Shift)
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Анимация (если есть Animator)
        if (animator != null)
        {
            animator.SetBool("IsMoving", movementInput.magnitude > 0.1f);
        }
    }

    private void FixedUpdate()
    {
        // Рассчитываем скорость
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        rb.velocity = movementInput * currentSpeed;

        // Поворот персонажа в сторону движения (если нужно)
        if (movementInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}