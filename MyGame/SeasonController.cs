
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyGame
{
    class SeasonController
    {
        public static SeasonController instance;
        public bool season = false;

        private SeasonController()
        {


        }
        public String getSeasonTexture()
        {
            if (season)
            {
                return @"sprites/grass.jpg";
            }
            else return @"sprites/snow.jpg";
        }

        public void nextSeason()
        {
            season = !season;
        }

        public static SeasonController getInstance()
        {
            if (instance == null) instance = new SeasonController();
            return instance;

        }
    }
}