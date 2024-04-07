using UnityEditor;

namespace Frag
{

    public class DamageInfo
    {

        public Fighter creator;
        public Fighter target;

        public int value;
        public float percentage;
        public int damage;
        public DamageInfo(Fighter creator,Fighter Target ,int damage, int value = 0, float percentage = 100f)
        {
            this.damage = damage;
            this.value = value;
            this.percentage = percentage;
        }

        public int GetDamage()
        {
            damage += this.value;
            damage *= (int)(this.percentage - 100f);
            return damage;
        }

    }

}