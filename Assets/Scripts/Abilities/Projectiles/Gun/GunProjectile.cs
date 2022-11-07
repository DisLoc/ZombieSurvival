using UnityEngine;

public class GunProjectile : Projectile
{
    [SerializeField] private ParticleSystem _sparkParticle;

    public override void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        base.Initialize(stats, weapon);

        if (_sparkParticle != null)
        {
            _sparkParticle.Stop();

            var main = _sparkParticle.main;

            main.startLifetime = _releaseDelay.Value;
            main.duration = _releaseDelay.Value;

            _sparkParticle.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
            _sparkParticle.Play();
        }
        else if (_isDebug) Debug.Log("Missing spark particle");
    }
}
