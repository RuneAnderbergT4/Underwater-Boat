﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using UnderWater_Boat;
namespace Underwater_Boat
{
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        SpriteFont _normalFont;
        SpriteFont _selectedFont;
        MouseState _previousMouseState;
        Menu _menu;
        Menu _activeMenu;
        Song _song;
        Texture2D _background;
        Texture2D _valBild;
        Texture2D _overlay;
        Texture2D _mouse;
        Menu finalMenu;
        Menu SubSelection;
        Menu ShipSelection;
        private LevelGenerator _lvlgen;
        private Menu _returnMenu;

        #region GameStates
        public enum MenyState
        {
            MainMenu,
            Generating,
            Playing
        }
        public static MenyState gs;
        public enum Sound
        {
            On,
            Off
        }
        public static Sound SD;
        public enum Full
        {
            on,
            off
        }
        public static Full FL;
        public enum Controll
        {
            Cont,
            Key
        }
        public static Controll CL;
        public enum TwoPlayer
        {
            One,
            Two
        }
        public static TwoPlayer TP;
        public enum SelShip
        {
            Submarine,
            Ship
        }

        public static SelShip SP;

        public enum Submarine
        {
            Sub1,
            Sub2,
            Sub3,
            Sub4,
            Sub5
        }
        public static Submarine SM;
        public enum Ships
        {
            Ship1,
            Ship2,
            Ship3,
            Ship4,
            Ship5  
        }
        public static Ships SH;
        public enum Graphics
        {
            set1,
            set2,
            set3,
            set4,
            set5
        }
        public static Graphics GR;
        #endregion
        public MenuComponent(Game game) : base(game)
        {
            #region Meny Hantering
            _menu = new Menu();
            _activeMenu = _menu;
            var ShipMenu = new Menu();
            var mapMenu = new Menu();
            var optionsMenu = new Menu();
            var graphicsMenu = new Menu();
            var soundMenu = new Menu();
            var twoPlayers = new Menu();
            var controllMenu = new Menu();
            ShipSelection = new Menu();
            SubSelection = new Menu();
            finalMenu = new Menu();
            var loadingMenu = new Menu();
            var returnToMenu = new Menu();
            var exitMenu = new Menu();
            var generatingLevel = new Menu();

            _menu.Items = new List<MenuChoice>
            {
                new MenuChoice(null) { Text = "Sea battle of DOOOOM", IsEnabled = false },
                new MenuChoice(null) { Text = "START", Selected = true, ClickAction = MoveClick, SubMenu = ShipMenu, IsVisible = () => Game1.GS != GameState.Pause },
                new MenuChoice(null) { Text = "PAUSED", ClickAction = MenuStartClicked, IsVisible = () => Game1.GS == GameState.Pause, IsEnabled = false },
                new MenuChoice(null) { Text = "ONE PLAYER", IsVisible = () => Settings.Default.TwoPlayer == true, ClickAction = PlayerNum },
                new MenuChoice(null) { Text = "TWO PLAYERS", IsVisible = () => Settings.Default.TwoPlayer == false, ClickAction = PlayerNum },
                new MenuChoice(null) { Text = "OPTIONS", ClickAction = MoveClick, SubMenu = optionsMenu },
                new MenuChoice(null) { Text = "EXIT TO MENU", ClickAction = MoveClick, SubMenu = returnToMenu, IsVisible = () => Game1.GS == GameState.Pause },
                new MenuChoice(null) { Text = "QUIT", ClickAction = MoveClick, SubMenu = exitMenu }
            };
            SubSelection.Items = new List<MenuChoice>
            {
                new MenuChoice(ShipMenu) { Text = "Select your Submarine", IsEnabled = false, ClickAction = MoveClick },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("submarine"), Selected = true, ClickAction = subSelection1, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("submarine 2"), ClickAction = subSelection2, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("submarine 3"), ClickAction = subSelection3, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("submarine 4"), ClickAction = subSelection4, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("submarine 5"), ClickAction = subSelection5, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "Return", ClickAction = MoveUpClick }
            };
            ShipSelection.Items = new List<MenuChoice>
            {
                new MenuChoice(ShipMenu) { Text = "Select your Ship", IsEnabled = false, ClickAction = MoveClick },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("Ship"), Selected = true, ClickAction = shipSelection1, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("Ship 2"), ClickAction = shipSelection2, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("Ship 3"), ClickAction = shipSelection3, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("Ship 4"), ClickAction = shipSelection4, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "", Bild = Game.Content.Load<Texture2D>("Ship 5"), ClickAction = shipSelection5, SubMenu = finalMenu },
                new MenuChoice(ShipMenu) { Text = "Return", ClickAction = MoveUpClick }
            };
            ShipMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Ship battle or Submarine battle", IsEnabled = false },
                new MenuChoice(_menu) { Text = "Ships", Selected = true, ClickAction = MenuShipSelClick, SubMenu = ShipSelection },
                new MenuChoice(_menu) { Text = "Submarine", ClickAction = MenuShipSelClick, SubMenu = SubSelection },
                new MenuChoice(_menu) { Text = "Return", ClickAction = MoveUpClick }
            };
            finalMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(ShipMenu) { Text = "Are you pleased with your selection?", IsEnabled = false },
                new MenuChoice(ShipMenu) { Text = "YES", Selected = true, ClickAction = MenuStartClicked, SubMenu = loadingMenu },
                new MenuChoice(ShipMenu) { Text = "", IsEnabled = false },
                new MenuChoice(ShipMenu) { Text = "", IsEnabled = false },
                new MenuChoice(ShipMenu) { Text = "Return to Selection", ClickAction = MoveUpClick }
            };
            loadingMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(ShipMenu) { Text = "Loading Map", IsEnabled = false }
            };
            optionsMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Options Menu", ClickAction = MoveClick, IsEnabled = false },
                new MenuChoice(_menu) { Text = "Graphics Menu", Selected = true, ClickAction = MoveClick, SubMenu = graphicsMenu},
                new MenuChoice(_menu) { Text = "Control Menu", ClickAction = MoveClick, SubMenu = controllMenu},
                new MenuChoice(_menu) { Text = "Sound Menu", ClickAction = MoveClick, SubMenu = soundMenu },
                new MenuChoice(_menu) { Text = "Return to Main", ClickAction = MoveUpClick }
            };
            graphicsMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Graphics Menu", IsEnabled = false },
                new MenuChoice(optionsMenu) { Text = "Fullscreen on", Selected = true, IsVisible = () => Settings.Default.IsFullScreen == true, ClickAction = FullMenu },
                new MenuChoice(optionsMenu) { Text = "Fullscreen off", IsVisible = () => Settings.Default.IsFullScreen == false, ClickAction = FullMenu },
                new MenuChoice(optionsMenu) { Text = "1920 x 1080", IsVisible = () => Settings.Default.Grafik == "1920 * 1080" && Settings.Default.IsFullScreen == false, ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1024 x 700", IsVisible = () => Settings.Default.Grafik == "1024 * 700", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1366 x 768", IsVisible = () => Settings.Default.Grafik == "1366 * 768", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1440 x 900", IsVisible = () => Settings.Default.Grafik == "1440 * 900", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1600 x 900", IsVisible = () => Settings.Default.Grafik == "1600 * 900", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "Return to Options", ClickAction = MoveUpClick }
            };
            soundMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Sound Menu", IsEnabled = false },
                new MenuChoice(optionsMenu) { Text = "Sound on", Selected = true, IsVisible = () => Settings.Default.Sound, ClickAction = SoundMenu },
                new MenuChoice(optionsMenu) { Text = "Sound off", IsVisible = () => Settings.Default.Sound == false, ClickAction = SoundMenu },
                new MenuChoice(optionsMenu) { Text = "Return to Options", ClickAction = MoveUpClick }
            };
            controllMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Controll Menu", IsEnabled = false },
                new MenuChoice(optionsMenu) { Text = "Keyboard Active", Selected = true, IsVisible = () => Settings.Default.Keyboard == true, ClickAction = ControlMenu },
                new MenuChoice(optionsMenu) { Text = "Controll Active", IsVisible = () => Settings.Default.Keyboard == false, ClickAction = ControlMenu },
                new MenuChoice(optionsMenu) { Text = "Return to Options", ClickAction = MoveUpClick }
            };
            exitMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Are you sure?", IsEnabled = false },
                new MenuChoice(_menu) { Text = "No", Selected = true, ClickAction = MoveUpClick },
                new MenuChoice(_menu) { Text = "Yes", ClickAction = MenuQuitClicked }
            };
            returnToMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Are you sure?", IsEnabled = false },
                new MenuChoice(_menu) { Text = "No", Selected = true, ClickAction = MoveUpClick },
                new MenuChoice(_menu) { Text = "Yes", ClickAction = PausMenuQuitClicked }
            };
            generatingLevel.Items = new List<MenuChoice>
            {
                new MenuChoice(mapMenu) {Text = "To be started!", IsEnabled = false, SubMenu = twoPlayers}
            };
            #endregion
        }
        public override void Initialize()
        {
            if (Settings.Default.Keyboard == true)
            {
                CL = Controll.Key;
            }
            else
            {
                CL = Controll.Cont;
            }
            if (Settings.Default.Sound == true)
            {
                SD = Sound.On;
            }
            else
            {
                SD = Sound.Off;
            }
            if (Settings.Default.IsFullScreen == true)
            {
                FL = Full.off;
            }
            else
            {
                FL = Full.on;
            }
            if (Settings.Default.TwoPlayer == true)
            {
                TP = TwoPlayer.Two;
            }
            else
            {
                TP = TwoPlayer.One;
            }
            if (Settings.Default.Grafik == "1920 * 1080")
            {
                GR = Graphics.set1;
            }
            else if (Settings.Default.Grafik == "1024 * 700")
            {
                GR = Graphics.set2;
            }
            else if (Settings.Default.Grafik == "1366 * 768")
            {
                GR = Graphics.set3;
            }
            else if (Settings.Default.Grafik == "1440 * 900")
            {
                GR = Graphics.set4;
            }
            else if (Settings.Default.Grafik == "1600 * 900")
            {
                GR = Graphics.set5;
            }
            SP = SelShip.Ship;
            gs = MenyState.MainMenu;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _normalFont = Game.Content.Load<SpriteFont>("menuFontNormal");
            _selectedFont = Game.Content.Load<SpriteFont>("menuFontSelected");
            _background = Game.Content.Load<Texture2D>("Sjöodjur");
            _overlay = Game.Content.Load<Texture2D>("temp");
            _mouse = Game.Content.Load<Texture2D>("mouse");
            _song = Game.Content.Load<Song>("MenuMusic");
            MediaPlayer.Play(_song);
            _previousMouseState = Mouse.GetState();
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            switch (gs)
            {
                case MenyState.MainMenu:
                    if (KeyboardComponent.KeyPressed(Keys.Escape))
                    {
                        var selectedChoice = _activeMenu.Items.First(c => c.Selected);
                        if (selectedChoice.ParentMenu != null)
                            _activeMenu = selectedChoice.ParentMenu;
                    }
                    if (KeyboardComponent.KeyPressed(Keys.Down) || GamePadComponent.ButtonPressed(Buttons.LeftThumbstickDown) || KeyboardComponent.KeyPressed(Keys.S))
                    {
                        NextMenuChoice();
                    }
                    if (KeyboardComponent.KeyPressed(Keys.Up) || GamePadComponent.ButtonPressed(Buttons.LeftThumbstickUp) || KeyboardComponent.KeyPressed(Keys.W))
                    {
                        PreviousMenuChoice();
                    }
                    if (KeyboardComponent.KeyPressed(Keys.Enter) || GamePadComponent.ButtonPressed(Buttons.A) || KeyboardComponent.KeyPressed(Keys.Space))
                    {
                        var selectedChoice = _activeMenu.Items.First(c => c.Selected);
                        selectedChoice.ClickAction.Invoke();
                        if (selectedChoice.SubMenu != null)
                            _activeMenu = selectedChoice.SubMenu;
                    }
                    var mouseState = Mouse.GetState();
                    foreach (var choice in _activeMenu.Items)
                    {
                        if (choice.HitBox.Contains(mouseState.X, mouseState.Y) && choice.IsEnabled && choice.IsVisible())
                        {
                            _activeMenu.Items.ForEach(c => c.Selected = false);
                            choice.Selected = true;
                            if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed && choice.IsVisible())
                            {
                                choice.ClickAction.Invoke();
                                if (choice.SubMenu != null)
                                    _activeMenu = choice.SubMenu;
                                break;
                            }
                        }
                    }
                    _previousMouseState = mouseState;
                    float startY = 0.2f * GraphicsDevice.Viewport.Height;
                    foreach (var choice in _activeMenu.Items)
                    {
                        if (!choice.IsVisible())
                            continue;

                        Vector2 size = _normalFont.MeasureString(choice.Text);
                        choice.Y = startY;
                        choice.X = GraphicsDevice.Viewport.Width/2.0f - size.X/2;
                        if (choice.Bild != null)
                            choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y - 10, choice.Bild.Width, choice.Bild.Height - 10);
                        else if (FL == Full.off && choice.Bild == null)
                        {
                            choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y - 10, (int) size.X,
                                (int) size.Y - 10);
                        }
                        else if (FL == Full.on && choice.Bild == null)
                        {
                            choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y, (int) size.X, (int) size.Y);
                        }
                        if (choice.IsEnabled == false)
                        {
                            startY += 100;
                        }
                        else
                        {
                            startY += 70;
                        }
                    }
                    break;
                case MenyState.Generating:
                    if (_lvlgen.IsAlive)
                    {
                        _activeMenu.Items[0].Text = _lvlgen.Progress;

                        #region Text formatting
                        startY = 0.2f * GraphicsDevice.Viewport.Height;
                        foreach (var choice in _activeMenu.Items)
                        {
                            if (!choice.IsVisible())
                                continue;

                            Vector2 size = _normalFont.MeasureString(choice.Text);
                            choice.Y = startY;
                            choice.X = GraphicsDevice.Viewport.Width / 2.0f - size.X / 2;
                            if (FL == Full.off)
                            {
                                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y - 10, (int)size.X, (int)size.Y - 10);
                            }
                            else if (FL == Full.on)
                            {
                                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y, (int)size.X, (int)size.Y);
                            }
                            if (choice.IsEnabled == false)
                            {
                                startY += 100;
                            }
                            else
                            {
                                startY += 70;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        UpdateLevel();
                        _activeMenu = _returnMenu;
                        gs = MenyState.MainMenu;
                    }
                    break;
                case MenyState.Playing:
                    MediaPlayer.Stop();
                    break;
            } 
            base.Update(gameTime);
        }
        private void PreviousMenuChoice()
        {
            int selectedIndex = _activeMenu.Items.IndexOf(_activeMenu.Items.First(c => c.Selected));
            _activeMenu.Items[selectedIndex].Selected = false;
            for (int i = 0; i < _activeMenu.Items.Count; i++)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = _activeMenu.Items.Count - 1;
                if (_activeMenu.Items[selectedIndex].IsVisible() && _activeMenu.Items[selectedIndex].IsEnabled)
                {
                    _activeMenu.Items[selectedIndex].Selected = true;
                    break;
                }
            }
        }
        private void NextMenuChoice()
        {
            int selectedIndex = _activeMenu.Items.IndexOf(_activeMenu.Items.First(c => c.Selected));
            _activeMenu.Items[selectedIndex].Selected = false;
            for (int i = 0; i < _activeMenu.Items.Count; i++)
            {
                selectedIndex++;
                if (selectedIndex >= _activeMenu.Items.Count)
                    selectedIndex = 0;
                if (_activeMenu.Items[selectedIndex].IsVisible() && _activeMenu.Items[selectedIndex].IsEnabled)
                {
                    _activeMenu.Items[selectedIndex].Selected = true;
                    break;
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Rectangle(0, 0, Game1.graphics.PreferredBackBufferWidth, Game1.graphics.PreferredBackBufferHeight), Color.White);
            foreach (var choice in _activeMenu.Items)
            {               
                if (!choice.IsVisible())
                    continue;
                // HitBox Koll
                //_spriteBatch.Draw(_overlay, choice.HitBox, Color.Blue);
                if (choice.Bild != null)
                    _spriteBatch.Draw(choice.Bild, new Vector2(choice.X, choice.Y));
                else if (choice.Bild == null)
                    _spriteBatch.DrawString(choice.Selected ? _selectedFont : _normalFont, choice.Text, new Vector2(choice.X, choice.Y), Color.White);
                if (choice.IsEnabled == false)
                {
                    _spriteBatch.DrawString(_normalFont, choice.Text, new Vector2(choice.X, choice.Y), Color.Crimson);
                }                
            }
            var mp = Mouse.GetState().Position;
            _spriteBatch.Draw(_mouse, new Vector2(mp.X, mp.Y), Color.White);
            _spriteBatch.End();
        }
        #region Meny Val
        private void MenuStartClicked()
        {
            Game1 g = Game as Game1;

            _lvlgen = g.LoadMap();

            gs = MenyState.Generating;
            Game1.GS = GameState.Playing;
            gs = MenyState.Playing;
            _activeMenu = _menu;

        }

        private void MenuShipSelClick()
        {
            
        }
        private void Laddare(Menu returnMenu)
        {
            SP = (SP == SelShip.Ship) ? SelShip.Submarine : SelShip.Ship;

            Game1 g = Game as Game1;

            _lvlgen = g.LoadMap();

            _returnMenu = returnMenu;

            gs = MenyState.Generating;
        }

        private void UpdateLevel()
        {
            Game1 g = Game as Game1;
            g.UpdateLevel();
        }
        private void MoveUpClick()
        {
            var selectedChoice = _activeMenu.Items.First(c => c.Selected);
            if (selectedChoice.ParentMenu != null)
                _activeMenu = selectedChoice.ParentMenu;
        }
        private void MoveClick()
        {
            // Krav på att en ClickAction ska tillkallas. Då ingen behövs kallas en som inte gör något.
        }
        private void SoundMenu()
        {
            SD = (SD == Sound.On) ? Sound.Off : Sound.On;
            if (SD == Sound.On)
            {
                Settings.Default.Sound = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.Sound = false;
                Settings.Default.Save();
            }
        }
        private void ControlMenu()
        {
            CL = (CL == Controll.Cont) ? Controll.Key : Controll.Cont;
            if (CL == Controll.Cont)
            {
                Settings.Default.Keyboard = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.Keyboard = true;
                Settings.Default.Save();
            }
        }
        private void subSelection1()
        {
            SM = Submarine.Sub1;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("submarine");
            
        }
        private void subSelection2()
        {
            SM = Submarine.Sub2;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("submarine 2");
            
        }
        private void subSelection3()
        {
            SM = Submarine.Sub3;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("submarine 3");
        }
        private void subSelection4()
        {
            SM = Submarine.Sub4;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("submarine 4");
        }
        private void subSelection5()
        {
            SM = Submarine.Sub5;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("submarine 5");
        }
        private void shipSelection1()
        {
            SH = Ships.Ship1;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("Ship");
        }
        private void shipSelection2()
        {
            SH = Ships.Ship2;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("Ship 2");
        }
        private void shipSelection3()
        {
            SH = Ships.Ship3;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("Ship 3");
        }
        private void shipSelection4()
        {
            SH = Ships.Ship4;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("Ship 4");
        }
        private void shipSelection5()
        {
            SH = Ships.Ship5;
            finalMenu.Items[3].Bild = Game.Content.Load<Texture2D>("Ship 5");
        }
        private void PlayerNum()
        {
            TP = (TP == TwoPlayer.One) ? TwoPlayer.Two : TwoPlayer.One;
            if (TP == TwoPlayer.Two)
            {
                Settings.Default.TwoPlayer = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.TwoPlayer = false;
                Settings.Default.Save();
            }
        }
        private void FullMenu()
        {
            FL = (FL == Full.off) ? Full.on : Full.off;
            if (FL == Full.on)
            {
                GR = Graphics.set1;
                Settings.Default.Grafik = "1920 * 1080";
                Settings.Default.Save();
                Settings.Default.IsFullScreen = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.IsFullScreen = false;
                Settings.Default.Save();
            }
            (Game as Game1).FullScreen();
        }
        private void Grafik()
        {
            if (GR == Graphics.set1)
            {
                GR = Graphics.set2;
                Settings.Default.Grafik = "1024 * 700";
                Settings.Default.Save();
            }
            else if (GR == Graphics.set2)
            {
                GR = Graphics.set3;
                Settings.Default.Grafik = "1366 * 768";
                Settings.Default.Save();
            }
            else if (GR == Graphics.set3)
            {
                GR = Graphics.set4;
                Settings.Default.Grafik = "1440 * 900";
                Settings.Default.Save();
            }
            else if (GR == Graphics.set4)
            {
                GR = Graphics.set5;
                Settings.Default.Grafik = "1600 * 900";
                Settings.Default.Save();
            }
            else if (GR == Graphics.set5)
            {
                GR = Graphics.set1;
                Settings.Default.Grafik = "1920 * 1080";
                Settings.Default.Save();
            }
            (Game as Game1).Grafitti();
        }
        private void PausMenuQuitClicked()
        {
            Game1.GS =  GameState.Start;
            gs = MenyState.MainMenu;
            (Game as Game1).Restart();
            var selectedChoice = _activeMenu.Items.First(c => c.Selected);
            if (selectedChoice.ParentMenu != null)
                _activeMenu = selectedChoice.ParentMenu;
        }
        private void MenuQuitClicked()
        {
            Game.Exit();
        }
        #endregion
    }
}