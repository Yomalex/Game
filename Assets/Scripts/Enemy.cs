using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private BoxCollider2D bc2D;
    private Rigidbody2D rb2D;
    private bool toRight = false;
    public GameObject HealthBar;

    // Use this for initialization
    void Awake() {
        bc2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cp = transform.position;
        float xDir = 2;

        if (!toRight)
        {
            xDir *= -1;
        }

        Vector2 tp = cp + Vector2.right * xDir;

        if (!Physics2D.Linecast(tp, tp + Vector2.down * 10, 1 << LayerMask.NameToLayer("Ground")))
        {
            xDir *= -1;
            toRight = !toRight;

            var ls = transform.localScale;
            ls.x = (toRight) ? -1 : 1;
            transform.localScale = ls;
        }

        rb2D.MovePosition(cp + Vector2.right * xDir * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            toRight = !toRight;
            var ls = HealthBar.transform.localScale;
            ls.x -= 0.1f;
            HealthBar.transform.localScale = ls;
        }

        if(HealthBar.transform.localScale.x<Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }
}
