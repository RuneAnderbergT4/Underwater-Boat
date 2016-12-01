using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
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
        Texture2D _overlay;
        Texture2D _mouse;

        #region GameStates

        public enum MenyState
        {
            MainMenu,
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
        public enum SelMap
        {
            Forrest,
            Stone
        }

        public static SelMap SP;

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
            var MapMenu = new Menu();
            var optionsMenu = new Menu();
            var graphicsMenu = new Menu();
            var soundMenu = new Menu();
            var TwoPlayers = new Menu();
            var controllMenu = new Menu();
            var returnToMenu = new Menu();
            var exitMenu = new Menu();
            _menu.Items = new List<MenuChoice>
            {
                new MenuChoice(null) { Text = "Submarine Battle of DOOOOM", IsEnabled = false},
                new MenuChoice(null) { Text = "START", Selected = true, ClickAction = MoveClick, SubMenu = MapMenu, IsVisible = () => Game1.GS != GameState.Pause},
                new MenuChoice(null) { Text = "PAUSED", ClickAction = MenuStartClicked, IsVisible = () => Game1.GS == GameState.Pause, IsEnabled = false},
                new MenuChoice(null) { Text = "OPTIONS", ClickAction = MoveClick, SubMenu = optionsMenu},
                new MenuChoice(null) { Text = "EXIT TO MENU", ClickAction = MoveClick, SubMenu = returnToMenu, IsVisible = () => Game1.GS == GameState.Pause},
                new MenuChoice(null) { Text = "QUIT", ClickAction = MoveClick, SubMenu = exitMenu}
            };
            TwoPlayers.Items = new List<MenuChoice>
            {
                new MenuChoice(MapMenu) { Text = "Start game", Selected = true, ClickAction = MenuStartClicked},
                new MenuChoice(MapMenu) { Text = "Twoplayer off", IsVisible = () => Settings.Default.TwoPlayer == true, ClickAction = PlayerNum },
                new MenuChoice(MapMenu) { Text = "Twoplayer on", IsVisible = () => Settings.Default.TwoPlayer == false, ClickAction = PlayerNum },
                new MenuChoice(MapMenu) { Text = "Back", ClickAction = MoveUpClick}
            };
            MapMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Select your map", IsEnabled = false},
                new MenuChoice(_menu) { Text = "ForrestMap", Selected = true, ClickAction = ForrestMap, SubMenu = TwoPlayers},
                new MenuChoice(_menu) { Text = "StoneMap", ClickAction = StoneMap, SubMenu = TwoPlayers},
                new MenuChoice(_menu) { Text = "Back", ClickAction = MoveUpClick}
            };
            optionsMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Options Menu", ClickAction = MoveClick, IsEnabled = false},
                new MenuChoice(_menu) { Text = "Grahpics Menu", Selected = true, ClickAction = MoveClick, SubMenu = graphicsMenu},
                new MenuChoice(_menu) { Text = "Controll Menu", ClickAction = MoveClick, SubMenu = controllMenu},
                new MenuChoice(_menu) { Text = "Sound Menu", ClickAction = MoveClick, SubMenu = soundMenu},
                new MenuChoice(_menu) { Text = "Back to Main", ClickAction = MoveUpClick}
            };
            graphicsMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Graphics Menu", IsEnabled = false},
                new MenuChoice(optionsMenu) { Text = "Fullscreen On", Selected = true, IsVisible = () => Settings.Default.IsFullScreen == true, ClickAction = FullMenu },
                new MenuChoice(optionsMenu) { Text = "Fullscreen Off", IsVisible = () => Settings.Default.IsFullScreen == false, ClickAction = FullMenu },
                new MenuChoice(optionsMenu) { Text = "1920 x 1080", IsVisible = () => Settings.Default.Grafik == "1920 * 1080" && Settings.Default.IsFullScreen == false, ClickAction = Grafik},
                new MenuChoice(optionsMenu) { Text = "1024 x 700", IsVisible = () => Settings.Default.Grafik == "1024 * 700", ClickAction = Grafik},
                new MenuChoice(optionsMenu) { Text = "1366 x 768", IsVisible = () => Settings.Default.Grafik == "1366 * 768", ClickAction = Grafik},
                new MenuChoice(optionsMenu) { Text = "1440 x 900", IsVisible = () => Settings.Default.Grafik == "1440 * 900", ClickAction = Grafik},
                new MenuChoice(optionsMenu) { Text = "1600 x 900", IsVisible = () => Settings.Default.Grafik == "1600 * 900", ClickAction = Grafik},
                new MenuChoice(optionsMenu) { Text = "Back to Options", ClickAction = MoveUpClick}
            };
            soundMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Sound Menu", IsEnabled = false},
                new MenuChoice(optionsMenu) { Text = "Sound On", Selected = true, IsVisible = () => Settings.Default.Sound, ClickAction = SoundMenu },
                new MenuChoice(optionsMenu) { Text = "Sound Off", IsVisible = () => Settings.Default.Sound == false, ClickAction = SoundMenu },
                new MenuChoice(optionsMenu) { Text = "Back to Options", ClickAction = MoveUpClick}
            };
            controllMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(optionsMenu) { Text = "Controll Menu", IsEnabled = false},
                new MenuChoice(optionsMenu) { Text = "Keyboard Active", Selected = true, IsVisible = () => Settings.Default.Keyboard == true, ClickAction = ControlMenu },
                new MenuChoice(optionsMenu) { Text = "Controll Active", IsVisible = () => Settings.Default.Keyboard == false, ClickAction = ControlMenu },
                new MenuChoice(optionsMenu) { Text = "Back to Options", ClickAction = MoveUpClick}
            };
            exitMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Are you sure?", IsEnabled = false},
                new MenuChoice(_menu) { Text = "No", Selected = true, ClickAction = MoveUpClick},
                new MenuChoice(_menu) { Text = "Yes", ClickAction = MenuQuitClicked}
            };
            returnToMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Are you sure?", IsEnabled = false},
                new MenuChoice(_menu) { Text = "No", Selected = true, ClickAction = MoveUpClick},
                new MenuChoice(_menu) { Text = "Yes", ClickAction = PausMenuQuitClicked}
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
            SP = SelMap.Forrest;
            gs = MenyState.MainMenu;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _normalFont = Game.Content.Load<SpriteFont>("menuFontNormal");
            _selectedFont = Game.Content.Load<SpriteFont>("menuFontSelected");
            _background = Game.Content.Load<Texture2D>("Submarine");
            _overlay = Game.Content.Load<Texture2D>("temp");
            _mouse = Game.Content.Load<Texture2D>("mouse");
            _song = Game.Content.Load<Song>("MenuMusic");
            if (gs == MenyState.MainMenu)
            MediaPlayer.Play(_song);
            else 
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
                        choice.X = GraphicsDevice.Viewport.Width / 2.0f - size.X / 2;
                        if (FL == Full.off)
                        {
                            choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y - 10, (int) size.X, (int) size.Y - 10);
                        }
                        else if (FL == Full.on)
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
                case MenyState.Playing:
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
            Game1.GS = GameState.Playing;
            gs = MenyState.Playing;
            _activeMenu = _menu;
        }
        private void ForrestMap()
        {
            SP = SelMap.Forrest;
            Game1 g = Game as Game1;
            g.LoadMap(SP);
        }
        private void StoneMap()
        {
            SP = SelMap.Stone;
            Game1 g = Game as Game1;
            g.LoadMap(SP);
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