﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater_Boat
{
    class Menu
    {
        public List<MenuChoice> Items { get; set; }

        public Menu()
        {
            Items = new List<MenuChoice>();
        }
    }

    class MenuChoice
    {
        public float X { get; set; }
        public float Y { get; set; }

        public string Text { get; set; }
        public bool Selected { get; set; }

        public Action ClickAction { get; set; }
        public Rectangle HitBox { get; set; }

        public Func<bool> IsVisible { get; set; }

        public bool IsEnabled { get; set; }

        public Texture2D Bild { get; set; }

        public Menu ParentMenu { get; private set; }
        public Menu SubMenu { get; set; }

        public MenuChoice(Menu parentMenu)
        {
            ParentMenu = parentMenu;
            IsEnabled = true;
            IsVisible = () => true;
        }
    }
}
