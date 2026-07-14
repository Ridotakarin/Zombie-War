using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PooledParticle : PoolObject
{
    private ParticleSystem ps;

    private void Awake() => ps = GetComponent<ParticleSystem>();

    public override void OnSpawn()
    {
        base.OnSpawn();
        ps.Clear(true);
        ps.Play(true);
    }

    public override void OnRelease()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        base.OnRelease();
    }
}