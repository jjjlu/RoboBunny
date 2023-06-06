using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    private float collisionDisableTime;
    private UnityEngine.Collider2D shooterCollider;
    private UnityEngine.Collider2D ballCollider;
    private bool disableCollision = false;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private float waitDuration;
    private float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (disableCollision)
        {
            collisionDisableTime -= Time.deltaTime;

            if (collisionDisableTime < 0)
            {
                Physics2D.IgnoreCollision(ballCollider, shooterCollider, false);
                disableCollision = false;
            }
        }
    }

    public void DisableCollision(UnityEngine.Collider2D collision, float duration)
    {
        collisionDisableTime = duration;
        shooterCollider = collision;
        ballCollider = GetComponent<Collider2D>();
        disableCollision = true;
        Physics2D.IgnoreCollision(ballCollider, shooterCollider, true);
    }

    public void FadeOut(float wait, float fade)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        waitDuration = wait;
        fadeDuration = fade;
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Wait for the specified duration before the object disappears
        yield return new WaitForSeconds(waitDuration);

        // Fade out the object by reducing its alpha value gradually over the fadeDuration
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the object
        Destroy(gameObject);
    }
}
