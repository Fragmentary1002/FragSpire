using Frag;
using QFramework;

public class CounterApp : Architecture<CounterApp>
{
    protected override void Init()
    {
        // зЂВс Model
        this.RegisterModel(new Player());
        this.RegisterModel(new BattleInfo());
        

        // зЂВс System
        this.RegisterSystem(new EnemyIntentSystem());

        //зЂВс Utility
        this.RegisterUtility(new Storage());
    }
}