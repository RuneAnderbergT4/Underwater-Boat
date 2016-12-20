using System;
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
        Menu fightSel;
        public static int AntVarde;

        private LevelManager _lvlgen;
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

        public enum Antal
        {
            Four,
            Five,
            Six
        }
        public static Antal AT;
        public enum Graphics
        {
            set1,
            set2,
            set3,
            set4
        }
        public static Graphics GR;
        #endregion
        public MenuComponent(Game game) : base(game)
        {
            #region Meny Hantering
            _menu = new Menu();
            _activeMenu = _menu;
            var optionsMenu = new Menu();
            var graphicsMenu = new Menu();
            var soundMenu = new Menu();
            var controllMenu = new Menu();
            fightSel = new Menu();
            finalMenu = new Menu();
            var returnToMenu = new Menu();
            var exitMenu = new Menu();
            var generatingLevel = new Menu();
            var startPlaying = new Menu();

            _menu.Items = new List<MenuChoice>
            {
                new MenuChoice(null) { Text = "SEA BATTLE OF DOOOOM", IsEnabled = false , IsVisible = () => Game1.GameState == GameState.Start},
                new MenuChoice(null) { Text = "START", Selected = true, ClickAction = MoveClick, SubMenu = fightSel, IsVisible = () => Game1.GameState != GameState.Pause },
                new MenuChoice(null) { Text = "PAUSED", ClickAction = MenuStartClicked, IsVisible = () => Game1.GameState == GameState.Pause, IsEnabled = false },
                new MenuChoice(null) { Text = "ONE PLAYER", IsVisible = () => Settings.Default.TwoPlayer == true && Game1.GameState == GameState.Start, ClickAction = PlayerNum },
                new MenuChoice(null) { Text = "TWO PLAYERS", IsVisible = () => Settings.Default.TwoPlayer == false, ClickAction = PlayerNum },
                new MenuChoice(null) { Text = "BACK TO GAME", ClickAction = BackToGame, IsVisible = () => Game1.GameState == GameState.Pause},
                new MenuChoice(null) { Text = "OPTIONS", ClickAction = MoveClick, SubMenu = optionsMenu },
                new MenuChoice(null) { Text = "EXIT TO MENU", ClickAction = MoveClick, SubMenu = returnToMenu, IsVisible = () => Game1.GameState == GameState.Pause },
                new MenuChoice(null) { Text = "QUIT", ClickAction = MoveClick, SubMenu = exitMenu }
            };
            fightSel.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) { Text = "Choose the size of the teams", IsEnabled = false, ClickAction = MoveClick },
                new MenuChoice(_menu) { Text = "4 v 4", Selected = true, ClickAction = subSelection1, SubMenu = finalMenu },
                new MenuChoice(_menu) { Text = "5 v 5", ClickAction = subSelection2, SubMenu = finalMenu },
                new MenuChoice(_menu) { Text = "6 v 6", ClickAction = subSelection3, SubMenu = finalMenu },
                new MenuChoice(_menu) { Text = "Return", ClickAction = MoveUpClick }
            };
            finalMenu.Items = new List<MenuChoice>
            {
                new MenuChoice(fightSel) { Text = "Are you pleased with your selection?", IsEnabled = false },
                new MenuChoice(fightSel) { Text = "Yes", Selected = true, ClickAction = () => StartLevelGeneration(startPlaying), SubMenu = generatingLevel },
                new MenuChoice(fightSel) { Text = "", IsEnabled = false },
                new MenuChoice(fightSel) { Text = "", IsEnabled = false },
                new MenuChoice(fightSel) { Text = "", IsEnabled = false },
                new MenuChoice(fightSel) { Text = "Return", ClickAction = MoveUpClick}
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
                new MenuChoice(optionsMenu) { Text = "1920 x 1080 (Full HD)", IsVisible = () => Settings.Default.Grafik == "1920 * 1080", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1600 x 900 (HD+)", IsVisible = () => Settings.Default.Grafik == "1600 * 900", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1366 x 768", IsVisible = () => Settings.Default.Grafik == "1366 * 768", ClickAction = Grafik },
                new MenuChoice(optionsMenu) { Text = "1280 x 720 (HD)", IsVisible = () => Settings.Default.Grafik == "1280 * 720", ClickAction = Grafik },
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
                new MenuChoice(_menu) {Text = "To be started!", IsEnabled = false, SubMenu = finalMenu}
            };
            startPlaying.Items = new List<MenuChoice>
            {
                new MenuChoice(_menu) {Text = "Start Playing!", ClickAction = MenuStartClicked, Selected = true}
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
            switch (Settings.Default.Grafik)
            {
                case "1920 * 1080":
                    GR = Graphics.set1;
                    break;
                case "1600 * 900":
                    GR = Graphics.set2;
                    break;
                case "1366 * 768":
                    GR = Graphics.set3;
                    break;
                case "1280 * 720":
                    GR = Graphics.set4;
                    break;
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
            if (Settings.Default.Sound)
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
                    if (Game1.GameState == GameState.Pause && KeyboardComponent.KeyPressed(Keys.Escape))
                    {
                        var selectedChoice = _activeMenu.Items.First(c => c.Selected);
                        if (selectedChoice.ParentMenu != null)
                            _activeMenu = _menu;
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
                            choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y, (int) size.X, (int) size.Y);
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
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Rectangle(0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight), Color.White);
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
            Game1.GameState = GameState.Playing;
            gs = MenyState.Playing;
            _activeMenu = _menu;
        }
        //private void MenuShipSelClick()
        //{
        //    SP = SelShip.Ship;            
        //    finalMenu.Items[5].Text = "Ships";
        //}

        //private void MenuSubSelClick()
        //{
        //    SP = SelShip.Submarine;
        //    finalMenu.Items[5].Text = "Submarines";
        //}
        private void StartLevelGeneration(Menu returnMenu)
        {
            _lvlgen = Game1.LoadMap();

            _returnMenu = returnMenu;

            gs = MenyState.Generating;
        }

        private void BackToGame()
        {
            Game1.GameState = GameState.Playing;
            gs = MenyState.Playing;
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
                MediaPlayer.Play(_song);
                Settings.Default.Sound = true;
                Settings.Default.Save();
            }
            else
            {
                MediaPlayer.Stop();
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
            AT = Antal.Four;
            AntVarde = 4;
            finalMenu.Items[4].Text = "4 v 4";
            Game1.TB.ClearPlayers();
            Game1.TB.AddSub(SubType.Aqua, false, "t1");
            Game1.TB.AddSub(SubType.Megalodon, false, "t1");
            Game1.TB.AddSub(SubType.ShipCamoflage, false, "t1");
            Game1.TB.AddSub(SubType.ShipCarrier, false, "t1");
            Game1.TB.AddSub(SubType.X_1, false, "t2");
            Game1.TB.AddSub(SubType.YellowSubmarine, false, "t2");
            Game1.TB.AddSub(SubType.ShipTradional, false, "t2");
            Game1.TB.AddSub(SubType.ShipVintage, false, "t2");
        }

        private void subSelection2()
        {
            AT = Antal.Five;
            AntVarde = 5;
            finalMenu.Items[4].Text = "5 v 5";
            Game1.TB.ClearPlayers();
            Game1.TB.AddSub(SubType.Aqua, false, "t1");
            Game1.TB.AddSub(SubType.Megalodon, false, "t1");
            Game1.TB.AddSub(SubType.Standard, false, "t1");
            Game1.TB.AddSub(SubType.ShipCamoflage, false, "t1");
            Game1.TB.AddSub(SubType.ShipCarrier, false, "t1");
            Game1.TB.AddSub(SubType.X_1, false, "t2");
            Game1.TB.AddSub(SubType.YellowSubmarine, false, "t2");
            Game1.TB.AddSub(SubType.Standard, false, "t2");
            Game1.TB.AddSub(SubType.ShipTradional, false, "t2");
            Game1.TB.AddSub(SubType.ShipVintage, false, "t2"); 
        }
        private void subSelection3()
        {
            AT = Antal.Six;
            AntVarde = 6;
            finalMenu.Items[4].Text = "6 v 6";
            Game1.TB.ClearPlayers();
            Game1.TB.AddSub(SubType.Aqua, false, "t1");
            Game1.TB.AddSub(SubType.Megalodon, false, "t1");
            Game1.TB.AddSub(SubType.Standard, false, "t1");
            Game1.TB.AddSub(SubType.X_1, false, "t2");
            Game1.TB.AddSub(SubType.YellowSubmarine, false, "t2");
            Game1.TB.AddSub(SubType.Standard, false, "t2");
            Game1.TB.AddSub(SubType.ShipCamoflage, false, "t1");
            Game1.TB.AddSub(SubType.ShipCarrier, false, "t1");
            Game1.TB.AddSub(SubType.ShipPansar, false, "t1");
            Game1.TB.AddSub(SubType.ShipTradional, false, "t2");
            Game1.TB.AddSub(SubType.ShipVintage, false, "t2");
            Game1.TB.AddSub(SubType.ShipPansar, false, "t2");
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
            switch (GR)
            {
                case Graphics.set1:
                    GR = Graphics.set2;
                    Settings.Default.Grafik = "1600 * 900";
                    Settings.Default.Save();
                    break;
                case Graphics.set2:
                    GR = Graphics.set3;
                    Settings.Default.Grafik = "1366 * 768";
                    Settings.Default.Save();
                    break;
                case Graphics.set3:
                    GR = Graphics.set4;
                    Settings.Default.Grafik = "1280 * 720";
                    Settings.Default.Save();
                    break;
                case Graphics.set4:
                    GR = Graphics.set1;
                    Settings.Default.Grafik = "1920 * 1080";
                    Settings.Default.Save();
                    break;
            }
            (Game as Game1).Grafitti();
        }
        private void PausMenuQuitClicked()
        {
            Game1.GameState =  GameState.Start;
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