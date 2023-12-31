using DG.Tweening;
using TweaxxGames.JamRaid;
using UnityEngine;

public class Policeman : Unit
{
    [SerializeField] private Visor visor;
    [SerializeField] private ParticleSystem gun;

    [Space]
    [SerializeField] private float timeToShoot = 1f;
    [SerializeField] private bool canShoot;

    private Sequence _shootTween;

    protected override void Awake()
    {
        base.Awake();
        visor.OnEnter += OnEnteredVisor;
    }

    private void OnDestroy()
    {
        visor.OnEnter -= OnEnteredVisor;
    }

    private void OnEnteredVisor(GameObject obj)
    {
        if (obj.TryGetComponent(out Player player))
        {
            _shootTween?.Kill();

            // if spotted 2nd time
            if (_shootTween != null)
                Shoot();
            else
                _shootTween = Utilities.DoActionDelayed(Shoot, timeToShoot);
        }
    }

    private void Update()
    {
        if (visor == null) return;


    }

    private void Shoot()
    {
        gun.Play();
        _shootTween = null;
    }
}
