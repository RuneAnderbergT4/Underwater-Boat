﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Underwater_Boat
{
    public class Turnbase
    {
        public Team t1;
        public Team t2;
        int currentteam;
        int t1sub;
        int t2sub;
        KeyboardState pks;
        public Sub currentSub;
        bool shooting;
        float power;
        int upDown = 1;
        int nrOfShots;
        int firerate = 5;
        private bool fire;

        public Turnbase(string team1, string team2)
        {
            t1 = new Team(team1);
            t2 = new Team(team2);
            currentteam = 1;
            t1sub = 0;
            t2sub = 0;
            firerate = 1;
        }
        public bool AddSub(SubType st, bool isbot, string teamname)
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
                currentSub = t1.members[0];
                return true;
            }
            else
            {
                if (t1.teamname == teamname)
                {
                    Sub sub = new Sub(t1, Ship.ship(st), isbot);
                    sub.isboat = true;
                    t1.members.Add(sub);
                }
                else if (t2.teamname == teamname)
                {
                    Sub sub = new Sub(t2, Ship.ship(st), isbot);
                    sub.isboat = true;
                    t2.members.Add(sub);
                }
                else
                    return false;
                currentSub = t1.members[0];
                return true;
            }
        }

        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();

            if (currentteam == 1 && t1.members.Count != 0)
            {
                if (t1sub == t1.members.Count)
                    t1sub = 0;
                t1.members[t1sub].color = Color.Salmon;
                t1.members[t1sub].Update();
                currentSub = t1.members[t1sub];

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
                if (t2sub == t2.members.Count)
                    t2sub = 0;
                t2.members[t2sub].color = Color.Salmon;
                t2.members[t2sub].Update();
                currentSub = t2.members[t2sub];

                if (ks.IsKeyDown(Keys.Enter) && pks.IsKeyUp(Keys.Enter))
                {
                    t2.members[t2sub].ResetVel();

                    t2sub++;
                    currentteam *= -1;
                }
            }
            else if (currentteam == -1 && t2.members.Count == 0)
                currentteam *= -1;
            Shoot();
            pks = ks;
        }
        public void Draw()
        {

            foreach (var s in t1.members)
            {
                s.Draw();
            }
            foreach (var s in t2.members)
            {
                s.Draw();
            }

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
        public void Shoot()
        {
            Sub sub = Game1.currentSub;
            KeyboardState ks = Keyboard.GetState();

            if (sub.CurrentWeapon().CurrentAmmo > 0)
            {
                if (ks.IsKeyDown(Keys.Space) && !shooting)
                {

                    nrOfShots = sub.CurrentWeapon().ShotsFired * firerate;
                    if (power >= 10 && upDown == 1)
                        upDown = -1;
                    else if (power <= 1 && upDown == -1)
                        upDown = 1;
                    power += 0.1f * upDown;

                    Debug.WriteLine("Power: " + power);

                }
                else if (ks.IsKeyUp((Keys.Space)) && power != 0)
                    fire = true;
                if (fire)
                {
                    shooting = true;
                    if (power > 10)
                        power = 10;

                    else if (power < 1)
                        power = 1;

                    if (nrOfShots > 0 && nrOfShots % firerate == firerate - 1)
                        Projectiles.Add(new Shot(sub.Position, new Vector2(power , 0), sub.CurrentWeapon().weapon));


                    else if (nrOfShots <= 0)
                    {
                        fire = false;
                        power = 0;
                        upDown = 1;
                        shooting = false;
                        sub.CurrentWeapon().CurrentAmmo--;
                        //sub.Weapons[sub.CurrentWeapon.Name].CurrentAmmo--;
                    }
                    nrOfShots--;
                }
            }
        }
    }
}

