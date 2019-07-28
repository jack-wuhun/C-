using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Fighter
{
    public const int FIGHTER_MINE = 1;
    public const int FIGHTER_ENEMY = 2;
    public const int FIGHTER_MONSTER = 3;
    public byte type;
    public string name;
    public float hp;
    public int mp;
    public int atk;
    public int skillDamage;
    //public bool usedMedicine = false;
    public int WeaponEndurance;
    public Equipment equip;
    public void addEquipment(Equipment state)
    {
        this.equip = state;
    }
    public int ReduceMp(Fighter f)//使用技能消MP
    {
        this.mp -= 15;
        return this.mp;
    }
    public virtual void beHit(Fighter f)//f为攻击者
    {
        Equipment equip = new Equipment();
        int totalAtk = 0;
        if (f.type == FIGHTER_MONSTER)//检测攻击方是英雄还是中立怪
        {
            totalAtk = f.atk;
            this.hp -= totalAtk;
            while (this.hp < 0)
            {
                this.hp = 0;
            }
            Console.WriteLine(f.name + "对" + this.name + "使用了怪兽攻击,造成了" + totalAtk + "的伤害," + this.name + "剩余" + this.hp + "点HP");
        }
        else
        {
            int ran = Program.RANDOM.Next(0, 2);
            if (ran == 0)//查看英雄是否使用魔法
            {
                if(f.mp >= 15)//检测英雄是否有足够的mp施放魔法
                {
                    totalAtk = f.skillDamage;
                    ReduceMp(f);
                    this.hp -= totalAtk;
                    while (this.hp < 0)
                    {
                        this.hp = 0;
                    }
                    Console.WriteLine(f.name + "对" + this.name + "使用魔法攻击,造成了" + totalAtk + "的伤害," + this.name + "剩余" + this.hp + "点HP");
                }
                else
                {
                    Console.WriteLine(f.name + " MP不足，施放魔法失败");
                    ran = 1;
                }
            }
            if (ran == 1 && f.equip != null)//检测英雄是否装备武器
            {
                if (f.WeaponEndurance > 0 )//检测武器是否有足够的耐久度使用
                {
                    totalAtk = f.atk + equip.atk;
                    equip.ReduceWeaponEndurance(f);
                    this.hp -= totalAtk;
                    while (this.hp < 0)
                    {
                        this.hp = 0;
                    }
                    Console.WriteLine(f.name + "对" + this.name + "使用物理攻击,造成了" + totalAtk + "的伤害," + this.name + "剩余" + this.hp + "点HP");
                    equip.LoseWeapon(f);
                }
                else
                {
                    totalAtk = f.atk;
                    this.hp -= totalAtk;
                    ////while (this.hp < 0)
                    ////{
                    ////    this.hp = 0;
                    ////}
                    ////Console.WriteLine(f.name + "对" + this.name + "使用物理攻击,造成了" + totalAtk + "的伤害," + this.name + "剩余" + this.hp + "点HP");
                }
            }
            else if (ran == 1)
            {
                totalAtk = f.atk;
                this.hp -= totalAtk;
                while(this.hp < 0)
                {
                    this.hp = 0;
                }
                Console.WriteLine(f.name + "对" + this.name + "使用物理攻击,造成了" + totalAtk + "的伤害," + this.name + "剩余" + this.hp + "点HP");
            }
        }
        judgeTeam(this);
    }

        public void judgeTeam(Fighter f)//f为被攻击者
    {
        if (f.hp == 0)
        {
            if (f.type == FIGHTER_MINE)//判断是那方面的队伍
            {
                Team team = Program.bf.getTeam1();
                team.deleteFighter(f);
            }
            else if (this.type == FIGHTER_ENEMY)
            {
                Team team = Program.bf.getTeam2();
                team.deleteFighter(f);
            }
            else
            {
                Team team = Program.bf.getTeam3();
                team.deleteFighter(f);
            }
        }
        else
        {
            return;
        }
    }
}