using System.Collections.Generic;

class LevelsConfigurator : ILevelsConfigurator
{
    private static List<Level> _levels = new List<Level>()
    {
        // level 1
        new Level { MoverPositionX = 3, MoverPositionY = 3,
                    BoxPositions = "2,2;4,3;7,3;6,4",
                    PlatformPositions = "2,4;3,4;2,5;3,5",
                    WallPositions = "0,1,0,7;1,7,1,9;2,9,4,9;4,8,6,8;6,1,6,7;5,0,5,1;2,0,4,0;1,1,2,1;4,4,5,4;2,3,2,5" },

        // level 2
        new Level { MoverPositionX = 6, MoverPositionY = 2,
                    BoxPositions = "2,1;5,2;6,1;7,2;3,3",
                    PlatformPositions = "1,4;2,4;3,5;4,7",
                    WallPositions = "1,2,1,7;2,5,2,9;2,9,4,9;4,8,6,8;6,1,6,7;5,0,5,1;2,0,4,0;1,1,2,1;2,4,6,4;1,3,1,8;2,4,5,8;1,6,2,9" }

        // and so on...
    };

    public int Count
    {
        get { return _levels.Count; }
    }

    public Level Get(int idx)
    {
        return _levels[idx];
    }
}