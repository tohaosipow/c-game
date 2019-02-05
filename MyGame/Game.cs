

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyGame
{
    class Game
    {
        const double MS_PER_UPDATE = 100;
        public RenderWindow renderWindow { get; protected set; }
        public GameObject[,] objects = new GameObject[40, 40];
        private Clock ClockObj = new Clock();
        public double TimeNow { private set; get; }
        public double TimeRenderFrame { private set; get; }
        public double TimeSeason { private set; get; }
        public int FrameLast { private set; get; }
        public Player p;
        public SeasonController sc;

        protected void WorldRender()
        {
            for(int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    
                    objects[i, j ] = new Wall(new Vector2f(i*30, j*30));
                }
            }
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                   if(i == 3) objects[i, j] = new Floor(new Vector2f(i*30, j*30));
                   if(j == 3) objects[i, j] = new Floor(new Vector2f(i*30, j*30));
                   if(j == 10) objects[i, j] = new Floor(new Vector2f(i*30, j*30));
                }
            }

            p = new Player(new Vector2f(90,30));

        }

        protected void Init()
        {
            sc = SeasonController.getInstance();
            TimeNow = ClockObj.ElapsedTime.AsMilliseconds();
            TimeSeason = ClockObj.ElapsedTime.AsMilliseconds();
            TimeRenderFrame = ClockObj.ElapsedTime.AsMilliseconds();
            FrameLast = (int) TimeNow;
            WorldRender();
            
           

        }

        public Game()
        {
            renderWindow = new RenderWindow(new VideoMode(1280, 720), "Hello world");
            renderWindow.SetVerticalSyncEnabled(true);
        }

        public void Run()
        {
            double lag = 0.0;
            Init();
            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();

                TimeNow = ClockObj.ElapsedTime.AsMilliseconds();
                double elapsed = TimeNow - TimeRenderFrame;
                TimeRenderFrame = TimeNow;
                lag += elapsed;
                Input();

                while(lag >= MS_PER_UPDATE)
                {
                    Update();
                    lag -= MS_PER_UPDATE;
                }
                Render(lag/MS_PER_UPDATE);
          
            }
        }

        protected void Input()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) p.Velocity = new Vector2f(30f, p.Velocity.Y+0f);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) p.Velocity = new Vector2f(-30f, p.Velocity.Y + 0f);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) p.Velocity = new Vector2f(p.Velocity.X+0f, -30f);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) p.Velocity = new Vector2f(p.Velocity.X+0f,  30f);
            else p.Velocity = new Vector2f(0f, 0f);
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) p.Velocity = new Vector2f(2f*p.Velocity.X, 2f*p.Velocity.Y);
         
        }

        protected void Physics()
        {
            bool down = false;
            foreach (GameObject obj in objects)
            {
                double height = this.renderWindow.Size.Y - obj.Position.Y;

                down = objects[(int)(p.Position.X / 30), (int) (p.Position.Y / 30)].type != GameObjectType.Wall;
                
                obj.Position = obj.Position + obj.Velocity;
                



            }
            if (down)
            {
                //Console.WriteLine(p.Position.X);
                //Console.WriteLine(p.Position.Y);
                p.Velocity = new Vector2f(p.Velocity.X * 2f, p.Velocity.Y * 2f);
            }
            else
            {
                p.Velocity = new Vector2f(0f, 0f);
            }
            p.Position = p.Position + p.Velocity;
        }


        protected void Update()
        {
             Physics();
            if(ClockObj.ElapsedTime.AsMilliseconds() - TimeSeason > 10000)
            {
                TimeSeason = ClockObj.ElapsedTime.AsMilliseconds();
                sc.nextSeason();
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        if (objects[i, j].type == GameObjectType.Wall) objects[i, j].Sprite.Texture = new Texture(SeasonController.getInstance().getSeasonTexture());
                        //renderWindow.Draw(objects[i, j]);
                    }
                }

            }
          

        }

        protected void Render(double framePosition)
        {
            renderWindow.Clear(Color.Black);
            //Sprite s = new Sprite(new Texture(@"sprites/wall.jpg"));
            //renderWindow.Draw(s);         
            //Render

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    //if(objects[i, j].type == GameObjectType.Wall) objects[i, j].Sprite.Texture = new Texture(SeasonController.getInstance().getSeasonTexture());
                    renderWindow.Draw(objects[i, j]);
                }
            }
            renderWindow.Draw(p);

            renderWindow.Display();
           
        }
    }

  
}