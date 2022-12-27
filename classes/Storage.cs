using lab8_g.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8_g;

namespace lab8_g.classes
{
    internal static class Storage
    {
        static public int score = 0;

        // 0 - nothig
        // 1 - tree
        // 2 - bush
        // 3 - coin
        // 4 - player
        // 5 - snake
        // 6 - house
        static public int[,] field = new int[15, 15] {
            {4,3,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {0,0,1,0,0,0,0,0,0,0,0,0,0,3,0},
            {0,0,3,0,0,0,2,1,0,3,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,1,0},
            {0,0,0,0,0,0,1,0,0,0,0,3,0,0,0},
            {0,0,0,3,2,0,0,5,0,0,2,0,3,0,0},
            {0,0,3,0,0,0,0,2,0,3,0,0,0,3,0},
            {0,0,0,0,0,0,3,0,0,0,0,0,0,0,2},
            {0,3,0,0,0,2,0,1,0,0,3,0,0,0,0},
            {0,0,2,0,3,0,0,0,2,0,0,0,0,2,0},
            {0,0,1,0,0,0,1,0,0,3,0,0,3,0,0},
            {2,0,3,0,0,0,0,3,0,0,0,0,0,0,1},
            {0,0,0,1,0,3,2,0,1,0,3,0,2,0,0},
            {0,0,0,0,0,2,0,0,0,0,0,0,1,0,0},
            {1,3,0,0,0,0,0,0,0,0,0,3,0,0,6},
        };

        static public IField[,] fieldEntities = new IField[15, 15]
        {
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
            {null, null,null,null,null,null,null,null,null,null,null,null,null,null,null },
        };

    }
}
