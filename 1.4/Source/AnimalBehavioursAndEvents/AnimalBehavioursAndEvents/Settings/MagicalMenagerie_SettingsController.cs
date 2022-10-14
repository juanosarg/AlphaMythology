using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace AnimalBehaviours
{



   

    public class MagicalMenagerie_Mod : Mod
    {

        public static MagicalMenagerie_Settings settings;

        public MagicalMenagerie_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<MagicalMenagerie_Settings>();
        }
        public override string SettingsCategory() => "Alpha Mythology";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            MMToggleableSpawnDef toggleablespawndef = (from k in DefDatabase<MMToggleableSpawnDef>.AllDefsListForReading
                                                     where k.defName == "MM_ToggleableAnimals"
                                                     select k).RandomElement();


            if (settings.pawnSpawnStates == null) settings.pawnSpawnStates = new Dictionary<string, bool>();
            foreach (string defName in toggleablespawndef.toggleablePawns)
            {
                if (!settings.pawnSpawnStates.ContainsKey(defName))
                {
                    settings.pawnSpawnStates[defName] = false;
                }
            }



            settings.DoWindowContents(inRect);


        }
    }
}
