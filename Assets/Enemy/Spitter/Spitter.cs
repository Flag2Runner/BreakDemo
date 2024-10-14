using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform launchTransform;
    public void Shoot()
    {
        if(Target)
        {
            Projectile newProjectile = Instantiate(projectilePrefab, launchTransform.position, launchTransform.rotation);
            newProjectile.Launch(Target.transform.position, gameObject);
        }
    }
}
