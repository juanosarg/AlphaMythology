using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace AnimalBehaviours;

public class DeathActionWorker_ExplodeAndSpawnEggs : DeathActionWorker
{
	public override void PawnDied(Corpse corpse, Lord prevLord)
	{
		if (corpse.Map != null)
		{
			float radius = ((corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0) ? 3.9f : ((corpse.InnerPawn.ageTracker.CurLifeStageIndex != 1) ? 5.9f : 4.9f));
			int stackCount = 1;
			if ((double)Rand.Value <= 0.3)
			{
				stackCount = 2;
			}
			Thing thing = ThingMaker.MakeThing(ThingDef.Named("MM_EggPhoenixFertilized"));
			thing.stackCount = stackCount;
			GenPlace.TryPlaceThing(thing, corpse.Position, corpse.Map, ThingPlaceMode.Near);
			List<Thing> list = new List<Thing>();
			list.Add(thing);
			GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1, -1f, null, null, null, null, null, 0f, 1, null);
		}
	}
}
