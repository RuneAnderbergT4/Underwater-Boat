using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Underwater_Boat
{
    class Turnbase
    {
        public static Team t1;
        public static Team t2;
        int currentteam;
        int t1sub;
        int t2sub;
        KeyboardState pks;
        public Turnbase(string team1, string team2)
        {
            t1 = new Team(team1);
            t2 = new Team(team2);
            currentteam = 1;
            t1sub = 0;
            t2sub = 0;
        }
        public bool AddSub(SubType st,bool isbot,string teamname)
        {
            if ((int)st < 5)
            {
                if (t1.teamname == teamname)
                {
                    Sub sub = new Sub(t1, Submarine.Sub(st), isbot);
                    t1.members.Add(sub);
                }
                else if (t2.teamname == teamname)
                {
                    Sub sub = new Sub(t2, Submarine.Sub(st), isbot);
                    t2.members.Add(sub);
                }
                else
                    return false;
                return true;
            }
            else
            {
                if (t1.teamname == teamname)
                {
                    Sub sub = new Sub(t1, Ship.ship(st), isbot);
                    t1.members.Add(sub);
                }
                else if (t2.teamname == teamname)
                {
                    Sub sub = new Sub(t2, Ship.ship(st), isbot);
                    t2.members.Add(sub);
                }
                else
                    return false;
                return true;
            }
        }
        
        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
           
            if (currentteam == 1 && t1.members.Count != 0)
            {
                if (t1sub == t1.members.Count )
                    t1sub = 0;
                 t1.members[t1sub].color = Color.Salmon;
                 t1.members[t1sub].Update();
                   

                if (ks.IsKeyDown(Keys.Enter) && pks.IsKeyUp(Keys.Enter))
                {
                   
                    t1.members[t1sub].ResetVel();
                    t1sub++;
                    currentteam *= -1;
                }
            }
            else if (currentteam == 1 && t1.members.Count == 0)
                currentteam *= -1;
            else if (currentteam == -1 && t2.members.Count != 0)
            {
                if (t2sub == t2.members.Count )
                    t2sub = 0;
                t2.members[t2sub].color = Color.Salmon;
                t2.members[t2sub].Update();


                if (ks.IsKeyDown(Keys.Enter) && pks.IsKeyUp(Keys.Enter))
                {
                    t2.members[t2sub].ResetVel();

                    t2sub++;
                    currentteam *= -1;
                }
            }
            else if (currentteam == -1 && t2.members.Count == 0)
                currentteam *= -1;
            pks = ks;
        }
        public void Draw()
        {
            Game1.spriteBatch.Begin();
            foreach (var s in t1.members)
            {
                s.Draw();
            }
            foreach (var s in t2.members)
            {
                s.Draw();
            }
            Game1.spriteBatch.End();
        }

        internal void LoadContent(Game1 game1)
        {
            foreach (var s in t1.members)
            {
                s.LoadContent(game1);
            }
            foreach (var s in t2.members)
            {
                s.LoadContent(game1);
            }
        }
    }
}
