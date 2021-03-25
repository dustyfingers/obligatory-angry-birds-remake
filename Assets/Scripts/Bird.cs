using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float launchForce = 500f;
    [SerializeField] float maxDragDistance = 5f;

    Vector2 startPosition;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    // Start is called before the first frame update
    void Start ()
    {
        startPosition = rigidbody.position;
        rigidbody.isKinematic = true;
    }

    void OnMouseDown ()
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseUp ()
    {
        Vector2 currentPosition = rigidbody.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();

        rigidbody.isKinematic = false;
        rigidbody.AddForce (direction * launchForce);
        spriteRenderer.color = Color.white;
    }

    void OnMouseDrag ()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // make sure the play cannot drag to the right of the initial starting position
        Vector2 desiredPosition = mousePos;
        
        float distance = Vector2.Distance(desiredPosition, startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > startPosition.x)
            desiredPosition.x = startPosition.x;

        rigidbody.position = desiredPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        StartCoroutine (ResetAfterDelay ());
    }

    IEnumerator ResetAfterDelay ()
    {
        yield return new WaitForSeconds (2f);
        rigidbody.position = startPosition;
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = true;
    }
}
