using UnityEngine;

public class Player : Unit
{
    protected override void OnDied()
    {
        gameObject.SetActive(false);

        LevelManager.Instance.Restart();
    }
}
