using DG.Tweening;
using System.Linq;
using TweaxxGames.JamRaid;
using UnityEngine;

public class MeleeKillAbility : MonoBehaviour
{
    [SerializeField] private float struggleFreeTime = 1f;
    [SerializeField] private float usageRadius = 3f;
    [SerializeField] private int damage = 1;

    [Space]
    [SerializeField] private Vector3 targetOffset = new Vector2(0.382f - 0.256f, -1.243f + 1.228f);

    [Space]
    [SerializeField] private bool useInput;
    [SerializeField] private bool isKilling;

    [Space]
    [SerializeField] private Unit owner;
    [SerializeField] private Unit target;
    
    private Animator _animator;
    private Sequence _struggleFreeTween;

    private const string KILL_START = "KillStart";
    private const string KILL = "Kill";
    private const string IS_KILLING = "IsKilling";

    private void Awake()
    {
        useInput = GetComponent<Player>() != null;
        owner = GetComponent<Unit>();
        _animator = GetComponent<Animator>();

        isKilling = false;
    }

    private void Update()
    {
        if (useInput && Input.GetKeyDown(KeyCode.Space))
        {
            Use();
        }

        isKilling = IsInRange(target);
        _animator.SetBool(IS_KILLING, isKilling);

        if (!isKilling)
        {
            ResetTarget();
        }
    }

    public void Use()
    {
        if (!IsInRange(target))
        {
            var enemies = UnitManager.Instance.Units.
                FindAll(v => v != null && v != owner && v.Health != null &&
                v.Health.IsAlive && v.gameObject.activeInHierarchy);
            var closest = enemies.OrderBy(v => Vector2.Distance(v.transform.position, transform.position)).FirstOrDefault();

            target = closest != null && IsInRange(closest) ? closest : null;
        }

        var wasKilling = isKilling;
        isKilling = IsInRange(target);

        //Debug.Log($"{name} used {nameof(MeleeKillAbility)}, target = {target}");

        if (isKilling)
        {
            target.Health.TakeDamage(damage);

            target.SetMovementState(false);
            owner.SetMovementState(false);

            var flipX = GetComponent<SpriteRenderer>().flipX;
            target.transform.position = transform.position + (flipX ? -targetOffset : targetOffset);

            if (!wasKilling)
                _animator.SetTrigger(KILL_START);
            else
                _animator.SetTrigger(KILL);

            _struggleFreeTween?.Kill();
            _struggleFreeTween = Utilities.DoActionDelayed(ResetTarget, struggleFreeTime);
        }
    }

    private void ResetTarget()
    {
        isKilling = false;

        if (target == null)
            return;

        target.SetMovementState(true);
        owner.SetMovementState(true);

        Debug.Log($"{target} struggled free");

        target = null;
    }

    private bool IsInRange(Unit unit)
    {
        if (unit == null || unit.Health.IsDead)
            return false;

        return Vector2.Distance(unit.transform.position, transform.position) <= usageRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, usageRadius);
    }
}
