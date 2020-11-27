using System;

[Serializable]
public class Detachment
{
    public int Amount;
    public EnemiesTypes Monster;

    /// <summary>
    /// Uses while calculating battle
    /// </summary>
    /// <returns>Power of current detachment</returns>
    public DetachmentPower GetDetachmentPower()
    {
        return new DetachmentPower(Monster, Amount);
    }
}