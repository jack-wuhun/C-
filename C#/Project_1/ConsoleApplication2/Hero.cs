using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Hero : Fighter
{
    public Hero(string name, byte type, float hp, int mp, int atk, int skillDamage, int WeaponEndurance)//设置属性
    {
        this.type = type;
        this.name = name;
        this.hp = hp;
        this.mp = mp;
        this.atk = atk;
        this.skillDamage = skillDamage;
        this.WeaponEndurance = WeaponEndurance;
    }

    //public float useMedicine(Hero h)//使用药品
    //{
    //    this.hp = h.hp + hp * 0.3f;
    //    return this.hp;
    //}
}

