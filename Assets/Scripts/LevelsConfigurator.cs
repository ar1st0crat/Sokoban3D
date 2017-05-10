using System.Collections.Generic;

static class LevelsConfigurator
{
    private static List<Level> _levels = new List<Level>()
    {
        new Level { MoverPositionX = 3, MoverPositionY = 3,
                    BoxPositions = "2,2;4,3;7,3;6,4",
                    PlatformPositions = "2,4;3,4;2,5;3,5",
                    WallPositions = "0,1,0,7;1,7,1,9;2,9,4,9;4,8,6,8;6,1,6,7;5,0,5,1;2,0,4,0;1,1,2,1;4,4,5,4;2,3,2,5" }
    };

    public static int Count
    {
        get { return _levels.Count; }
    }

    public static Level Get(int idx)
    {
        return _levels[idx];
    }
}