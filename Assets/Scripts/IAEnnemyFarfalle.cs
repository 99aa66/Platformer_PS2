using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class IAEnnemyFarfalle : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    public Transform target;
    public float height;
    Transform mob_transform;
    [SerializeField] bool is_attacking;

    [Header("Attaque")]
    [SerializeField] int damage_point = 5;
    [SerializeField] Transform attack_point;
    [SerializeField] LayerMask enemy_layers;
    float attack_range = 1.2f;
    float next_attack_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mob_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, height), speed * Time.deltaTime); // Suivre le joueur
        }
    }

    void Attack()
    {
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(attack_point.position, attack_range, enemy_layers); // Liste des ennemis
        foreach (Collider2D player in hit_player) // Si joueur est touché
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage_point); // Faire des dégâts au player
        }
        next_attack_time = Time.time + 2f; // Limitation d'attaque (4 par secondes)
        is_attacking = false;
    }
}