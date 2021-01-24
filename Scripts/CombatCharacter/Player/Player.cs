namespace CombatCharacter.Player
{
public class Player : CombatEntity 
{
    public override void TakeDamage(float amount)
    {        
        base.TakeDamage(amount);
        if (CurrentHealth <= .0f)
            Die();
    }
}
}