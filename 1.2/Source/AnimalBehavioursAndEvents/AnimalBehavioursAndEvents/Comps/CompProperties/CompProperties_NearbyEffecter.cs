using Verse;


namespace AnimalBehavioursAndEvents
{
    class CompProperties_NearbyEffecter : CompProperties
    {

        public string thingToAffect = "";
        public string secondaryThingToAffect = "";


        public string thingToTurnTo = "";
        public int ticksConversionRate = 1000;


        public CompProperties_NearbyEffecter()
        {
            this.compClass = typeof(CompNearbyEffecter);
        }


    }
}