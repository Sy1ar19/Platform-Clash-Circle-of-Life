/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : PlayerUpgrade
{
    private readonly DamageUpgradeConfig _config;

    private IPlayerHelth _playerHelth;


    public DamageUpgrade(DamageUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    public override string CurrentStats
    {
        get { return _config.damageTable.GetDamage(this.Level).ToString(); }
    }

    public override string NextImprovement
    {
        get { return _config.damageTable.DamageStep.ToString();
    }

    protected override void Initialize(IGameSystem , int level)
    {
        var playerService = system.GetService<IPlayerService>();
        this.player = playerService.GetPlayer();
        this.SetDamage(level);
    }

    protected override void UpdateLevel(int level)
    {
        this.SetDamage(level);
    }

    private void SetDamage(int level)
    {
        var damage = _config.damageTable.GetDamage(level);
        var damageComponent = this.player.Element<IDamageSetupComponent>();
        damageComponent.SetDamage(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/