    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


class Team : Fighter
{
    private List<Fighter> allCompany = new List<Fighter>();
    public void addCompany(Fighter f)
    {
        allCompany.Add(f);
    }
    public List<Fighter> getAllCompany()
    {
        return allCompany;
    }
    public Fighter getFighter(int index)//获取单位
    {
        return allCompany[index];
    }
     
    public void deleteFighter(Fighter f)//删除单位
    {
        allCompany.Remove(f);
        Console.WriteLine(f.name + "遭受剧烈攻击,已死亡");
        if (allCompany.Count == 0)
        {
            if (f.type == FIGHTER_MINE)
            {
                Console.WriteLine("敌方英雄获胜,游戏结束");
            }
            else
            {
                Console.WriteLine("我方英雄获胜,游戏结束");
            }
        }
        else
        {
            return;
        }
    }
    public int getFighterNum()
    {
        return allCompany.Count;
    }
}