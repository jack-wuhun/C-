using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Monster:Fighter
{
    public Monster(string name, byte type, float hp, int atk)//设置属性
    {
        this.type = type;
        this.name = name;
        this.hp = hp;
        this.atk = atk;
    }
    //public override void beHit(Fighter f)
    //{
    //    int totlaAtk = 0;
    //    int ran = Program.RANDOM.Next(0, 2);
    //    if (ran == 0)
    //    {
    //        totlaAtk = f.skillDamage;
    //    }
    //    else if (f.equip != null)
    //    {
    //        totlaAtk = f.atk + f.equip.atk;
    //    }
    //    else
    //    {
    //        totlaAtk = atk;
    //    }
    //    this.hp -= totlaAtk;
    //    base.judgeTeam(this);
    //}
}

