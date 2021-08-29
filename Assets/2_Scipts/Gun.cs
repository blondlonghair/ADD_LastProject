using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGun
{
    protected IGun _gun;
    protected Animator _anim;
    protected float intervalTime;
    protected float timer;

    public float damage;

    public abstract void Shoot();
}

class Shotgun : IGun
{
    public Shotgun(ref Animator anim)
    {
        _anim = anim;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Shotgun");
        intervalTime = 1;
        timer = intervalTime;
        damage = 50;
    }

    public override void Shoot()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > intervalTime)
        {
            _anim.SetTrigger("Shoot");
            timer = 0;

            var hit = Physics2D.RaycastAll(new Vector2(Gun.mousePos.x, Gun.mousePos.y), Vector2.zero, 0f);

            foreach (var t in hit)
            {
                if (t.collider != null && t.collider.gameObject.CompareTag("Enemy"))
                {
                    t.collider.GetComponent<Enemy>().CurHp -= this.damage;
                }
            }
        }
    }
}

public class Bayonet : IGun
{
    public Bayonet(ref Animator anim)
    {
        _anim = anim;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Bayonet");
        intervalTime = 1;
        timer = intervalTime;
        damage = 25;
    }

    public override void Shoot()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > intervalTime)
        {
            _anim.SetTrigger("Shoot");
            timer = 0;

            var hit = Physics2D.RaycastAll(new Vector2(Gun.mousePos.x, Gun.mousePos.y), Vector2.zero, 0f);

            foreach (var t in hit)
            {
                if (t.collider != null && t.collider.gameObject.CompareTag("Enemy"))
                {
                    t.collider.GetComponent<Enemy>().CurHp -= this.damage;
                }
            }
        }
    }
}

class Pistol : IGun
{
    public Pistol(ref Animator anim)
    {
        _anim = anim;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Pistol");
        intervalTime = 0.4f;
        timer = intervalTime;
        damage = 10;
    }

    public override void Shoot()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > intervalTime)
        {
            _anim.SetTrigger("Shoot");
            timer = 0;

            var hit = Physics2D.RaycastAll(new Vector2(Gun.mousePos.x, Gun.mousePos.y), Vector2.zero, 0f);

            foreach (var t in hit)
            {
                if (t.collider != null && t.collider.gameObject.CompareTag("Enemy"))
                {
                    t.collider.GetComponent<Enemy>().CurHp -= this.damage;
                }
            }
        }
    }
}

class Chainsaw : IGun
{
    public Chainsaw(ref Animator anim)
    {
        _anim = anim;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Chainsaw");
        intervalTime = 0.1f;
        timer = intervalTime;
        damage = 10;
    }

    public override void Shoot()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            _anim.SetBool("Shoot", true);

            if (timer > intervalTime)
            {
                timer = 0;

                var hit = Physics2D.RaycastAll(new Vector2(Gun.mousePos.x, Gun.mousePos.y), Vector2.zero, 0f);

                foreach (var t in hit)
                {
                    if (t.collider != null && t.collider.gameObject.CompareTag("Enemy"))
                    {
                        t.collider.GetComponent<Enemy>().CurHp -= this.damage;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _anim.SetBool("Shoot", false);
        }
    }
}

public class Gun : MonoBehaviour
{
    public static Vector3 mousePos;

    public GameObject cursor;
    public static Gun Instance;

    Animator animator;
    IGun gun;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        // Cursor.visible = false;

        gun = new Pistol(ref animator);
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, transform.position.y, 0);
        cursor.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.RaycastAll(new Vector2(Gun.mousePos.x, Gun.mousePos.y), Vector2.zero, 0f);
            foreach (var t in hit)
            {
                if (t.collider != null && t.collider.gameObject.CompareTag("Pistol"))
                {
                    gun = new Pistol(ref animator);
                }
                if (t.collider != null && t.collider.gameObject.CompareTag("Chainsaw"))
                {
                    gun = new Chainsaw(ref animator);
                }
                if (t.collider != null && t.collider.gameObject.CompareTag("Bayonet"))
                {
                    gun = new Bayonet(ref animator);
                }
                if (t.collider != null && t.collider.gameObject.CompareTag("Shotgun"))
                {
                    gun = new Shotgun(ref animator);
                }
            }
        }

        gun.Shoot();
    }
}