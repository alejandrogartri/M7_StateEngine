using TMPro;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza del salto
    private Rigidbody _rb;
    private bool _isGrounded;

    public int health = 100;
    public TMP_Text healthText;

    public float fireRate = 5f;
    private float shootTimer;

    public GameObject bulletPrefab;
    public Transform shootPointPivot;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento en los ejes X y Z
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(moveX, _rb.velocity.y, moveZ);
        _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }

        // Shooting
        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100f));
        targetPoint.y = 1;
        shootPointPivot.LookAt(targetPoint);

        if (Input.GetMouseButton(0))
        {
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = 1f / fireRate;
            }
        }

        shootTimer -= Time.deltaTime;

        // Health text update
        healthText.text = health.ToString();
        healthText.transform.LookAt(Camera.main.transform.position);

        if (health >= 70)
        {
            healthText.color = Color.white;
        }
        else if (health >= 40)
        {
            healthText.color = Color.yellow;
        }
        else
        {
            healthText.color = Color.red;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPointPivot.GetChild(0).position, shootPointPivot.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}