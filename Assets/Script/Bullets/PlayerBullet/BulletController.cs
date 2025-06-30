using UnityEngine;

public class BulletController
{
    private BulletModel bulletModel;
    private BulletView bulletView;

    public BulletController(BulletModel model, BulletView prefab, Vector3 direction, Vector3 spawnPos)
    {
        this.bulletModel = model;
        this.bulletView = GameObject.Instantiate(prefab, spawnPos, Quaternion.LookRotation(direction));
        bulletModel.SetController(this);
        bulletView.SetController(this);

        bulletView.Launch(direction, model.GetSpeed());
    }

    public void OnHit()
    {        
        bulletView.Explode(bulletModel.GetDamage()); ;
    }
}
