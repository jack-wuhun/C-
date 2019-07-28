using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    public static Random RANDOM = new Random(DateTime.Now.Millisecond);
    public static BattleField bf = new BattleField();
    static void Main(string[] args)
    {
        bf.team1 = new Team();
        bf.team2 = new Team();
        bf.team3 = new Team();
        for (int i = 1; i < 6; i++)
        {
            Hero h1 = new Hero("我方英雄" + i, Fighter.FIGHTER_MINE, 200, 35, 60, 40, 0);
            Equipment equip = new Equipment();
            h1.addEquipment(equip);
            h1.WeaponEndurance = 3;
            Console.WriteLine(h1.name + "获得武器，" + "攻击力提高30点");
            bf.addHeroToTeam1(h1);
            Hero h2 = new Hero("敌方英雄" + i, Fighter.FIGHTER_ENEMY, 160, 30, 70, 40, 0);
            bf.addHeroToTeam2(h2);
        }
        for (int i = 1; i < 3; i++)
        {
            Monster m = new Monster("中立怪物" + i, Fighter.FIGHTER_MONSTER, 30000, 100);
            bf.addMonsterToTeam3(m);
        }
        for (int i = 0; i < 100; i++)
        {
            int len1 = bf.team1.getFighterNum();
            int len2 = bf.team2.getFighterNum();
            if (len1 == 0 || len2 == 0)
            {
                break;
            }
            else
            {
                startFightMineRound();
            }

            len1 = bf.team1.getFighterNum();
            len2 = bf.team2.getFighterNum();
            if (len1 == 0 || len2 == 0)
            {
                break;
            }
            else
            {
                startFightEnemyRound();
            }

            len1 = bf.team1.getFighterNum();
            len2 = bf.team2.getFighterNum();
            if (len1 == 0 || len2 == 0)
            {
                break;
            }
            else
            {
                startFightMonsterRound();
            }
        }
        Console.ReadKey();
    }
    static void startFightMineRound()//我方英雄回合,A为主场，B为客场
    {
        Team teamA = bf.getTeam1();
        int ran = Program.RANDOM.Next(0, 2);
        if (ran == 0)
        {
            Team teamB = bf.getTeam2();
            doRoundLogic(teamA, teamB);
        }
        else
        {
            Team teamB = bf.getTeam3();
            doRoundLogic(teamA, teamB);
        }
    }
    static void startFightEnemyRound()//敌方英雄回合,A为主场，B为客场
    {
        Team teamA = bf.getTeam2();
        int ran = Program.RANDOM.Next(0, 2);
        if (ran == 0)
        {
            Team teamB = bf.getTeam1();
            doRoundLogic(teamA, teamB);
        }
        else
        {
            Team teamB = bf.getTeam3();
            doRoundLogic(teamA, teamB);
        }
    }
    static void startFightMonsterRound()//中立怪物回合,A为主场，B为客场
    {
        Team teamA = bf.getTeam3();
        int ran = Program.RANDOM.Next(0, 2);
        if (ran == 0)
        {
            Team teamB = bf.getTeam1();
            doRoundLogic(teamA, teamB);
        }
        else
        {
            Team teamB = bf.getTeam2();
            doRoundLogic(teamA, teamB);
        }
    }
    private static void doRoundLogic(Team teamA, Team teamB)//A为进攻方，B为防守方
    {
        int len = teamA.getFighterNum();//读取teamA的长度
        for (int i = 0; i < len; i++)
        {
            int length = teamB.getFighterNum();//读取teamB的长度
            if (length <= 0)
            {
                return;
            }
            int ran = Program.RANDOM.Next(0, length);//随机从teamB中抽取
            Fighter f1 = teamA.getFighter(i);
            Fighter f2 = teamB.getFighter(ran);
            f2.beHit(f1);
        }
    }
}