using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    float xmove;
    float ymove;
    Vector3 dir;
    public float speed;
    Rigidbody2D rb;
    bool zoneQuit;

    Vector3 localPos;

    new SpriteRenderer renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = rb.GetComponent<SpriteRenderer>();
        StartCoroutine(move(0, 0));
    }

    void Update()
    {
        xmove = Random.Range(-1, 2);
        ymove = Random.Range(-1, 2);

        localPos = transform.localPosition;

        if(zoneQuit == true)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(0, 0), Time.deltaTime * speed);
        }
        if(localPos == new Vector3(0, 0, -1))
        {
            zoneQuit = false;
        }
    }

    IEnumerator move(float x, float y)
    {
        if(zoneQuit == false)
        {
            dir = new Vector2(x, y).normalized;

            rb.AddForce(dir * speed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(2f);
            rb.AddForce(dir * -1 * speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
        }

        if(xmove == 1)
        {
            renderer.flipX = true;
        }else if(xmove == -1)
        {
            renderer.flipX = false;
        }

        StartCoroutine(move(xmove, ymove));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyZone"))
        {
            zoneQuit = true;
        }
    }
}
