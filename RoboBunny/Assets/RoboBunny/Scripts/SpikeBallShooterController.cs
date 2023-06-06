using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallShooterController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fireCooldown = 2f;
    [SerializeField] GameObject spikeBallPrefab;
    [SerializeField] float collisionCancelTime = 1.0f;
    [SerializeField] Animator anim;
    [SerializeField] float shootOffset;
    [SerializeField] float spikeBallExistDuration;
    [SerializeField] float spikeBallFadeDuration;
    private float timeTillFire;
    private UnityEngine.Collider2D shooterCollider;

    // Start is called before the first frame update
    void Start()
    { 
        timeTillFire = fireCooldown;
        shooterCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeTillFire -= Time.deltaTime; 

        RotateTowardsTarget();

        if (timeTillFire < 0)
        {
            fireSpikeBall();
            anim.SetTrigger("Fire");
            timeTillFire = fireCooldown;
        }
    }


    // https://answers.unity.com/questions/1592029/how-do-you-make-enemies-rotate-to-your-position-in.html
    private void RotateTowardsTarget()
    {
        float offsetAngle = 90.0f;

        // Calculate the direction from the current position to the target
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // Calculate the angle between the current forward direction and the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;

        // Rotate the GameObject towards the target with fixed rotation speed
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
    }

    private void fireSpikeBall()
    {
        float offsetAngle = -90.0f;


        GameObject spikeBall = Instantiate(spikeBallPrefab, transform.position, transform.rotation);

        spikeBall.transform.Translate(new Vector3(0, -shootOffset, 0));

        Rigidbody2D spikeBallRB = spikeBall.GetComponent<Rigidbody2D>();
        // Create a rotation quaternion of 90 degrees around the Z-axis
        Quaternion rotation = Quaternion.Euler(0f, 0f, offsetAngle);
        // Rotate the vector by the specified rotation
        Vector3 offsetVector = rotation * transform.right;
        spikeBallRB.velocity = offsetVector * projectileSpeed;


        SpikeBallController spikeBallController = spikeBall.GetComponent<SpikeBallController>();
        spikeBallController.DisableCollision(shooterCollider, collisionCancelTime);

        spikeBallController.FadeOut(spikeBallExistDuration, spikeBallFadeDuration);


    }
}
