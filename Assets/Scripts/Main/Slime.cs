using UnityEngine;

public class Slime : Enemy // INHERITANCE
{
    private int health;
    public override int Health { //ENCAPSULATION
        get => health;
        set {
            if (value > 0)
            {
                health = value;
            } else
            {
                health = 0;
            }
        }
    }
    public override float AttackDis => 1.5f; // ENCAPSULATION
    int i = 0;
    public override void Attack() //POLYMORPHISM
    {
        Debug.Log("Attack" + i);
        i++;
    }
}
