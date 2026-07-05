namespace HotSpotsGenerator;

public static class Tables
{
    public static class ContractTermsTable
    {
        public static Dictionary<int, string> BasePaySteps => new()
        {
            {1, "50%"},
            {2, "55%"},
            {3, "60%"},
            {4, "70%"},
            {5, "80%"},
            {6, "90%"},
            {7, "100%"},
            {8, "110%"},
            {9, "120%"},
            {10, "130%"},
            {11, "140%"},
            {12, "150%"},
            {13, "160%"},
            {14, "170%"},
            {15, "180%"},
            {16, "190%"},
            {17, "200%"}
        };

        public static Dictionary<int, string> CommandRightsSteps => new()
        {
            {3, "Integrated"},
            {7, "House"},
            {8, "Liaison"},
            {11, "Independent"}
        };

        public static Dictionary<int, string> SalvageRightsSteps => new()
        {
            {1, "None"},
            {3, "Exchange"},
            {4, "10%"},
            {5, "20%"},
            {6, "30%"},
            {7, "40%"},
            {8, "50%"},
            {9, "60%"},
            {10, "70%"},
            {11, "80%"},
            {12, "90%"},
            {13,"100%"}
        };

        public static Dictionary<int, string> SupportRightsSteps => new()
        {
            {1, "None"},
            {2, "Straight/20%"},
            {3, "Straight/40%"},
            {4, "Straight/60%"},
            {5, "Straight/70%"},
            {6, "Straight/80%"},
            {7, "Straight/90%"},
            {8, "Straight/100%"},
            {9, "Battle/10%"},
            {10, "Battle/20%"},
            {11, "Battle/30%"},
            {12, "Battle/40%"},
            {13, "Battle/50%"},
            {14, "Battle/75%"},
            {15, "Battle/100%"}
        };

        public static Dictionary<int, string> TransportationTermsSteps => new()
        {
            {5, "0%"},
            {6, "25%"},
            {7, "50%"},
            {8, "75%"},
            {9, "100%"}
        };
    }

    public static class TerrainTables
    {
        public static Dictionary<TerrainType, Dictionary<int, string>> Terrain => new()
        {
            { TerrainType.Alien, Alien },
            { TerrainType.Desert, Desert },
            { TerrainType.Grasslands, Grasslands },
            { TerrainType.Hills, Hills },
            { TerrainType.LightIndustrial, LightIndustrial },
            { TerrainType.Mountains, Mountains },
            { TerrainType.Savannahs, Savannahs },
            { TerrainType.Urban, Urban },
            { TerrainType.Wetlands, Wetlands },
            { TerrainType.Wooded, Wooded }
        };

        public static Dictionary<int, string> Alien => new()
        {
            {1, "Fungal Crevasse (MP: Alien Worlds)"},
            {2, "Caustic Valley (MP: Alien Worlds)"},
            {3, "Crystalline Canyon (MP: Alien Worlds)"},
            {4, "Lunar Base (MP: Alien Worlds)"},
            {5, "Crystalline Canyon (MP: Alien Worlds)"},
            {6, "Caustic Valley (MP: Alien Worlds)"}
        };

        public static Dictionary<int, string> Desert => new()
        {
            {1, "Badlands #1 (MP: Deserts)"},
            {2, "Badlands #2 (MP: Deserts)"},
            {3, "Desert #2 (AGOAC)"},
            {4, "Sand Drifts #1 (MP: Deserts)"},
            {5, "Barren Lands #1 (CI)"},
            {6, "Barren Lands #2 (CI)"}
        };

        public static Dictionary<int, string> Grasslands => new()
        {
            {1, "Grassland #1 (BB)"},
            {2, "Grassland #2 (AGOAC)"},
            {3, "Grassland #3 (AGOAC)"},
            {4, "Woodland (MP: Grassland)"},
            {5, "Open Terrain #2 (MP: Grassland)"},
            {6, "Open Terrain #3 (MP: Grassland)"}
        };

        public static Dictionary<int, string> Hills => new()
        {
            {1, "Hilltops #1 (CI)"},
            {2, "Foothills #2 (MP: Grassland)"},
            {3, "Rolling Hills #1 (MP: Grassland)"},
            {4, "Rolling Hills #2 (CI)"},
            {5, "Rolling Hills #3 (MP: Grassland)"},
            {6, "Rolling HIlls #4 (MP: Grassland)"}
        };

        public static Dictionary<int, string> LightIndustrial => new()
        {
            {1, "River CommCenter (MP: Grassland)"},
            {2, "Park District (MP: City)"},
            {3, "Central Park (MP: City)"},
            {4, "AeroBase #1 (MP: Deserts)"},
            {5, "AeroBase #2 (MP: Deserts)"},
            {6, "Roundabout (MP: City)"}
        };

        public static Dictionary<int, string> Mountains => new()
        {
            {1, "Foothills #1 (MP: Grassland)"},
            {2, "Foothills #2 (MP: Grassland)"},
            {3, "Washout #1 (MP: Deserts)"},
            {4, "Washout #2 (MP: Deserts)"},
            {5, "Mines #1 (MP: Deserts)"},
            {6, "Mines #2 (MP: Deserts)"}
        };

        public static Dictionary<int, string> Savannahs => new()
        {
            {1, "River Delta / Drainage Basin #1 (MP: Savannahs)"},
            {2, "River Delta / Drainage Basin #2 (MP: Savannahs)"},
            {3, "Large Lakes #1 (MP: Savannahs)"},
            {4, "Large Lakes #2 (MP: Savannahs)"},
            {5, "BattleTech (MP: Savannahs)"},
            {6, "City Ruins (MP: Savannahs)"}
        };

        public static Dictionary<int, string> Urban => new()
        {
            {1, "Corporate Campus (MP: City)"},
            {2, "Family Quarters (MP: City)"},
            {3, "HPG Heliport (MP: City)"},
            {4, "Business District (MP: City)"},
            {5, "Shopping District (MP: City)"},
            {6, "HPG Offices (MP: City)"}
        };

        public static Dictionary<int, string> Wetlands => new()
        {
            {1, "Streams (MP: Grassland)"},
            {2, "Wide River (MP: Savannahs)"},
            {3, "Lakes (MP: Grassland)"},
            {4, "Grassland #2 (AGOAC)"},
            {5, "Lake Area (Mercenaries)"},
            {6, "River Valley (Mercenaries)"}
        };

        public static Dictionary<int, string> Wooded => new()
        {
            {1, "Woodland (Mercenaries)"},
            {2, "Scattered Woods (Mercenaries)"},
            {3, "Grassland #3 (AGOAC)"},
            {4, "Rolling Hills #2 (CI)"},
            {5, "Desert #3 (AGOAC)"},
            {6, "Rolling Hills #1 (MP: Grassland)"}
        };
    }
}
