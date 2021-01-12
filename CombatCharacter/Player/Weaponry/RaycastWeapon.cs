using UnityEngine;
using System.Collections.Generic;
using CombatCharacter.Enemy;

namespace CombatCharacter.Player.Weaponry
{
public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    [SerializeField] bool isFiring;

    [SerializeField] int fireRate = 25;

    [SerializeField] float bulletSpeed = 1000.0f;
    [SerializeField] float bulletDrop = .0f;

    [SerializeField] ParticleSystem[] muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] TrailRenderer tracerEffect;

    [SerializeField] Transform raycastOrigin;
    [SerializeField] Transform raycastDestination;

    [SerializeField] string weaponName;
    [SerializeField] GameObject magazine;   

    [SerializeField] float damage = 10f;

    Ray ray;
    RaycastHit hit;
    float accumulatedTime;

    List<Bullet> bullets = new List<Bullet>();

    float maxLifetime = 3.0f;

    WeaponRecoil recoil;

    # region public variables
    public WeaponRecoil Recoil {get{return recoil;} set{recoil = value;}}
    public string WeaponName { get{return weaponName;} set{weaponName = value;}}
    public float Damage {get {return damage;}}
    # endregion

    private void Awake() 
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time + .5f * gravity * bullet.time * bullet.time);
        // p + v0 * t + 2 * g * t * t
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = .0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public bool IsFiring {get {return isFiring;}}

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1f / fireRate;

        while(accumulatedTime >= 0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet => {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            // p0 and p1 is for direction and distance for speed
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifetime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hit, distance))
        {
            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hit.point;
            bullet.time = maxLifetime;

            // collision impulse
            // var rb2d = hit.collider.GetComponent<Rigidbody>();
            // if (rb2d)
            //     rb2d.AddForceAtPosition(ray.direction * 20, hit.point, ForceMode.Impulse);

            // hitbox
            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
                hitBox.OnRaycastHit(this, ray.direction);

            // Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);
            // Debug.Log("Hit " + hit.transform.name);
        }
        else
        {
            if(bullet.tracer)
                bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet()
    {
        // emit muzzle flashes
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        // setup velocity
        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        if(recoil) recoil.GenerateRecoil(weaponName);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
}