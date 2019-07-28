using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Equipment
{
    public int atk = 30;

    public int ReduceWeaponEndurance(Fighter h)//减少武器耐久
    {
        h.WeaponEndurance -= 1;
        return h.WeaponEndurance;
    }

    public void LoseWeapon(Fighter h)//失去武器
    {
        if (h.WeaponEndurance == 0)
        {
            Console.WriteLine(h.name + "武器损坏,攻击力降低30点");
            h.addEquipment(null);
        }
    }
}