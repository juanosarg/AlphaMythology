using Verse;

namespace AnimalBehaviours;

public class Hediff_BleedingWound : HediffWithComps
{
	private int tickMax = 64;

	private int tickCounter = 0;

	public override void Tick()
	{
		base.Tick();
		tickCounter++;
		if (tickCounter > tickMax)
		{
			pawn.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed("MM_UncontrollableBleeding"), 1f));
			tickCounter = 0;
		}
	}
}
