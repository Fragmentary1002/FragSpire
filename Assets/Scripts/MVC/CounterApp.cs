using Frag;
using QFramework;

public class CounterApp : Architecture<CounterApp>
{
    protected override void Init()
    {
        // ע�� Model
        this.RegisterModel(new Enemy());
        this.RegisterModel(new Player());
        this.RegisterModel(new BattleInfo());
   

        // ע�� System
        this.RegisterSystem(new FightTestSystem());

        //ע�� Utility
        this.RegisterUtility(new Storage());
    }
}