using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AnimalBehaviours
{
    public class IncidentWorker_SummonMechataur : IncidentWorker
    {




        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            List<Thing> things = new List<Thing>();
            PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named("MM_Mechataur"), null, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
            things.Add(pawn);
            IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
            DropPodUtility.DropThingsNear(intVec, map, things, 110, false, true, true, true);
            base.SendStandardLetter("MM_LetterLabelMechataurCargoPodCrash".Translate(), "MM_MechataurCargoPodCrash".Translate(), LetterDefOf.PositiveEvent, parms, new TargetInfo(intVec, map, false), Array.Empty<NamedArgument>());
            return true;
        }
    }
}