using UnityEngine;

public class Spell
{
    public string spellName;
    public int damage;
    public int manaCost;

    // Constructor
    public Spell(string name, int dmg, int mana)
    {
        spellName = name;
        damage = dmg;
        manaCost = mana;
    }

    // Method to cast the spell
    public bool Cast(Unit caster, Unit target)
    {
        // Check if the caster has enough mana
        if (caster.currentMana < manaCost)
        {
            // Handle insufficient mana (optional)
            return false;
        }

        // Reduce caster's mana
        caster.currentMana -= manaCost;

        // Apply damage to the target
        bool isDead = target.TakeDamage(damage);

        return isDead;
    }
}
