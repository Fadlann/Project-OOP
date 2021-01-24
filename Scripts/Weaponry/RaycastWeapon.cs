using UnityEngine;
using System.Collections.Generic;
using CombatCharacter;
using Manager;
using System;

namespace Weaponry
{
public class RaycastWeapon : MonoBehaviour, IWeapon
{
    private class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    [Header("IWeapon Specs")]
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType type;
    [SerializeField] private float damage = 10f;

    [Header("Raycast Weapon Specs")]
    [SerializeField] private int fireRate = 25;
    [SerializeField] private float bulletSpeed = 1000.0f;
    [SerializeField] float bulletDrop = .0f;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private TrailRenderer bulletTracer;

    [HideInInspector] public Transform RaycastDestination;
    [HideInInspector] public Animator RigController;
    [HideInInspector] public CombatEntity Entity;
    
    private Ray ray;
    private RaycastHit hit;
    private float accumulatedTime;
    private List<Bullet> bullets = new List<Bullet>();
    private float maxLifetime = 3.0f;
    private bool isFiring;

    public string WeaponName { get{return weaponName;} }
    public float Damage { get {return damage;} }
    public WeaponType Type { get {return type;} }
    public bool IsAttacking {get {return isFiring;}}
    public Transform Transform {get {return transform;}}

    public event Action<Transform> OnAttack;
    public event Action<RaycastHit> OnHit;

    public delegate bool FiringAction();
    public event FiringAction OnFiring;

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time + .5f * gravity * bullet.time * bullet.time);
        // pt = p0 + v0 * t + 2 * g * t * t
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = .0f;
        bullet.tracer = Instantiate(bulletTracer, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void StartAttack() { StartFiring(); }
    public void UpdateAttack() { UpdateFiring(); }
    public void StopAttack() { StopFiring(); }

    private void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();
    }

    private void UpdateFiring()
    {
        accumulatedTime += Time.deltaTime;
        float fireInterval = 1f / fireRate;

        while(accumulatedTime >= 0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets()
    {
        SimulateBullets(Time.deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet => {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            // p0 and p1 is for direction and distance for speed
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifetime);
    }

    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.tag != "Enemy" && hit.transform.tag != "Player")
            {
                if (OnHit != null)
                    OnHit(hit);                
            }

            bullet.tracer.transform.position = hit.point;
            bullet.time = maxLifetime;

            var target = hit.collider.GetComponent<CombatEntity>();
            if (target && target != Entity) target.TakeDamage(damage);
        }
        else if(bullet.tracer) 
            bullet.tracer.transform.position = end;
    }

    private void FireBullet()
    {
        if (OnFiring != null && !OnFiring())
            return;
        
        if (OnAttack != null)
            OnAttack(raycastOrigin);
                    
        Vector3 velocity = (RaycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

    }

    private void StopFiring()
    {
        isFiring = false;
    }

    private void LateUpdate() 
    {
        UpdateBullets();
    }
}
}