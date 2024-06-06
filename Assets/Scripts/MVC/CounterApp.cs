using Frag;
using QFramework;

public class CounterApp : Architecture<CounterApp>
{
    protected override void Init()
    {
        // ע�� Model
        this.RegisterModel(new BattleInfo());
        

        // ע�� System
        this.RegisterSystem(new EnemyIntentSystem());

        //ע�� Utility
        this.RegisterUtility(new Storage());
        this.RegisterUtility(new LootBag());
    }
}