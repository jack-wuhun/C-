using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BattleField
{
    public Team team1 = new Team();
    public Team team2 = new Team();
    public Team team3 = new Team();
    public Team getTeam1()
    {
        return team1;
    }
    public Team getTeam2()
    {
        return team2;
    }
    public Team getTeam3()
    {
        return team3;
    }
    public void addHeroToTeam1(Fighter f)
    {
        team1.addCompany(f);
    }
    public void addHeroToTeam2(Fighter f)
    {
        team2.addCompany(f);
    }
    public void addMonsterToTeam3(Fighter f)
    {
        team3.addCompany(f);
    }
}

