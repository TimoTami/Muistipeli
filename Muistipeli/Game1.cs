using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Muistipeli
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game

    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int tileSize = 48;
        Vector2 position = new Vector2(208, 48);

        bool ekaKertaMeniJo=false;
        bool ekaKertaMeniJo2 = false;
        bool ekaKerta = true;
        bool painettu = false;
        bool onAvattu = false;
        bool ekaAvattu = false;
        bool tokaAvattu = false;
        bool laitaKiinni = false;
        bool odotusOhi = true;
        bool odota = false;
        bool oikeaPariOdotus = false;
        bool pari = false;
        bool etVoiKlikkaa = false;
        bool arvausBooli = false;
        bool tasoLoppu = false;

        List<Texture2D> kuvat = new List<Texture2D>();
        List<Texture2D> tuplaKuvat = new List<Texture2D>();
        List<Texture2D> kopio = new List<Texture2D>();
        List<Texture2D> kannet = new List<Texture2D>();
        List<Vector2> koopa = new List<Vector2>();
        List<Vector2> kielletytKlikit = new List<Vector2>();
        List<Vector2> viimeksAvattuKoordinaatti = new List<Vector2>();
        List<string> nimet = new List<string>();

        List<Koordit> koordit = new List<Koordit>();
        //List<Koordit> kielletytKlikit = new List<Koordit>();

        Koordit ekaKuvaKoordit = new Koordit();
        Koordit tokaKuvaKoordit = new Koordit();
        Texture2D tokaKuva;

        int avattuPala;

        Koordit taa = new Koordit();

        private MouseState oldState;

        Vector2 kliikkaa;
        Vector2 hiiriPaikka;

        private SpriteFont Hiiri;
        private SpriteFont Klikkaus;
        private SpriteFont TextuurinPaikka;
        private SpriteFont Arvaukset;
        private SpriteFont KaikkiArvaukset;

        int arvaukset = 0;
        int kaikkiArvaukset = 0;



        Texture2D kansi;
        Texture2D tyhjäKansi;
        Texture2D tyhjäRuutu;

        Vector2 klikkauspaikka;
        Vector2 hiiriAlueMin;
        Vector2 hiiriAlueMax;
        

        int[,] map = new int[,]
        {
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},
            //{1, 1, 1, 1, 1, 1, 1, 1},

            {1, 1, 1, 1,},
            {1, 1, 1, 1,},
            {1, 1, 1, 1,},
            {1, 1, 1, 1,},
        };


        private static readonly TimeSpan odotusAika = TimeSpan.FromMilliseconds(3000);
        private TimeSpan ajastin;

        float aika1 = 0;
        float mennytAika1;
        float odotusAika1 = 2000;

        float aika2 = 0;
        float mennytAika2;
        float odotusAika2 = 2000;

        float aika3 = 0;
        float mennytAika3;
        float odotusAika3 = 5000;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Rectangle t = graphics.GraphicsDevice.Viewport.Bounds;
            //graphics.PreferredBackBufferWidth = 100;  // set this value to the desired width of your window
            //graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            hiiriAlueMin = position;
            hiiriAlueMax.X = tileSize * map.GetLength(0) +position.X;
            hiiriAlueMax.Y = tileSize * map.GetLength(1) + position.Y;
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Hiiri = Content.Load<SpriteFont>("Hiiri");
            Klikkaus = Content.Load<SpriteFont>("Klikkaus");
            TextuurinPaikka = Content.Load<SpriteFont>("TextuurinPaikka");
            Arvaukset = Content.Load<SpriteFont>("Arvaukset");
            KaikkiArvaukset = Content.Load<SpriteFont>("KaikkiArvaukset");

            kansi = Content.Load<Texture2D>("TestiMuistipala48");
            tyhjäKansi = Content.Load<Texture2D>("Tyhjä48");
            tyhjäRuutu = Content.Load<Texture2D>("Tyhjä48Toka");

            kuvat = new List<Texture2D>();

            kuvat.Add(Content.Load<Texture2D>("Ajonenuvo2"));
            kuvat.Add(Content.Load<Texture2D>("Ajoneuvo1"));
            kuvat.Add(Content.Load<Texture2D>("Eläin1"));
            kuvat.Add(Content.Load<Texture2D>("Ihminen1"));
            kuvat.Add(Content.Load<Texture2D>("Kukka1"));
            kuvat.Add(Content.Load<Texture2D>("Kukka2"));
            kuvat.Add(Content.Load<Texture2D>("Puu1"));
            kuvat.Add(Content.Load<Texture2D>("Puu2"));

            if (ekaKertaMeniJo2 == false)
            {
                tuplaKuvat = kuvat.SelectMany(t => Enumerable.Repeat(t, 2)).ToList();
                tuplaKuvat.Shuffle();

                kopio.AddRange(tuplaKuvat);
            //foreach(var kuva in tuplaKuvat)
            //kannet.Add(Content.Load<Texture2D>("TestiMuistiPala48"));
            }

            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //foreach(var kielletty in tuplaKuvat.Where(t => t.Name == "käytetty"))
            //{
            //    kielletytKlikit.Add(kielletty);
            //}
            

            if (arvausBooli==true)
            {
                arvaukset += 1;
                kaikkiArvaukset += 1;
                arvausBooli = false;
            }

            MouseState newState = Mouse.GetState();

            int x = newState.X;
            int y = newState.Y;


            //foreach(var kielletty in kielletytKlikit.Where(h=>h.koordinaatit))

            //if (kielletytKlikit.Contains(new Vector2(x, y)))

            etVoiKlikkaa = false;

            if (x >= hiiriAlueMin.X && x <= hiiriAlueMax.X && y >= hiiriAlueMin.Y && y <= hiiriAlueMax.Y)
            {
                foreach (var koordinaatti in kielletytKlikit)
                {
                    if (x>=koordinaatti.X && x <= koordinaatti.X + 48 && y>=koordinaatti.Y && y<=koordinaatti.Y+48)
                    {
                        etVoiKlikkaa = true;
                    }
                }


                //if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && odota == true )
                //{
                //    odota = false;
                //    odotusOhi = true;
                //}
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && odotusOhi == true && etVoiKlikkaa==false && ekaAvattu==false)
                {

                    kliikkaa = new Vector2(x, y);
                    painettu = true;


                }
                var vv = viimeksAvattuKoordinaatti.FirstOrDefault();
                if (x >= vv.X && x <= vv.X + 48 && y >= vv.Y && y <= vv.Y + 48 && ekaAvattu == true)
                {
                    etVoiKlikkaa = true;
                    
                }
                else if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && odotusOhi == true && etVoiKlikkaa == false&&onAvattu==true)
                {

                    kliikkaa = new Vector2(x, y);
                    painettu = true;


                }


            }
            

            oldState = newState; // this reassigns the old state so that it is ready for next time
            hiiriPaikka = new Vector2(x, y);

            if (tokaAvattu == true)
            {
                mennytAika1 = gameTime.ElapsedGameTime.Milliseconds;
                aika1 = aika1 + mennytAika1;

                if (aika1 >= odotusAika1)
                {
                    odota = false;
                    odotusOhi = true;
                    tokaAvattu = false;
                    aika1 = 0.00f;
                }
            }
            if (oikeaPariOdotus==true)
            {
                mennytAika2 = gameTime.ElapsedGameTime.Milliseconds;
                aika2 = aika2 + mennytAika2;

                if (aika2 >= odotusAika2)
                {

                    oikeaPariOdotus = false;
                    aika2 = 0.00f;
                }
            }
            if (tasoLoppu == true)
            {
                mennytAika3 = gameTime.ElapsedGameTime.Milliseconds;
                aika3 = aika3 + mennytAika2;

                if (aika3 >= odotusAika3)
                {

                    tasoLoppu = false;
                    aika3 = 0.00f;
                }
            }



            //MouseState ms = Mouse.GetState();

            //if (ms.LeftButton == ButtonState.Pressed)
            //{
            //    int x = Convert.ToInt32(ms.X);
            //    int y = Convert.ToInt32(ms.Y);

            //    Vector2 klikkauspaikka = new Vector2(x, y);




            //    //map[x, y] =
            //    //spriteBatch.Draw(null, hiiripaikka, Color.White); //Left button Clicked, Change Texture here!

            //}


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var mm = kannet.Count();

            for (var i = 0; i < mm; i++)
            {
                var ll = kannet.FirstOrDefault();
                kannet.Remove(ll);
            }




            if (ekaKertaMeniJo == true)
            {
                tuplaKuvat.AddRange(kopio);

            }

            if (tuplaKuvat.TrueForAll(n => n.Name == "käytetty"))
            {
                
                arvaukset = 0;
                foreach (var tuplakuva in tuplaKuvat)
                {
                    var ekaNimi = nimet.FirstOrDefault();
                    tuplakuva.Name = ekaNimi;
                    nimet.Remove(ekaNimi);
                    var kopit = kopio.FirstOrDefault();
                    kopio.Remove(kopit);
                }

                var kk = kielletytKlikit.Count();

                for (var i = 0; i < kk; i++)
                {
                    var oo = kielletytKlikit.FirstOrDefault();
                    kielletytKlikit.Remove(oo);
                }

                var viim = viimeksAvattuKoordinaatti.FirstOrDefault();
                viimeksAvattuKoordinaatti.Remove(viim);
                ekaKertaMeniJo = false;
                odota = false;
                tasoLoppu = true;

                tuplaKuvat.Shuffle();

                kopio.AddRange(tuplaKuvat);
            }
            if (ekaKertaMeniJo == false)
            {
                foreach (var tt in tuplaKuvat)
                {
                    var nimiTalteen = tt.Name;
                    nimet.Add(nimiTalteen);

                }
            }
            GraphicsDevice.Clear(Color.CadetBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            var num = koopa.LastOrDefault();

            spriteBatch.DrawString(Hiiri, "Hiiri: " + hiiriPaikka, new Vector2(20, 50), Color.Black);
            spriteBatch.DrawString(Klikkaus, "Klikkaus: " + kliikkaa, new Vector2(20, 80), Color.Black);
            spriteBatch.DrawString(TextuurinPaikka, "TexPaikka: " + num, new Vector2(20, 110), Color.Black);
            spriteBatch.DrawString(Arvaukset, "Arvaukset: " + arvaukset, new Vector2(20, 140), Color.Black);
            spriteBatch.DrawString(KaikkiArvaukset, "Kaikki arvaukset: " + kaikkiArvaukset, new Vector2(20, 290), Color.Black);

            //if (onAvattu == false)
            //{

            //}

            if (tasoLoppu ==false)
            {
                for (int i = 0; i <= map.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= map.GetUpperBound(1); j++)
                    {
                        int textureId = map[i, j];
                        if (textureId != 0)
                        {
                            if (ekaKerta == false && map[i, j] != avattuPala)
                            {
                                return;
                            }
                            //avattuPala = map[i,j];
                            Vector2 texturePosition = new Vector2(i * tileSize, j * tileSize) + position;

                            //Tokan palan avaus
                            if (odota == true && texturePosition == ekaKuvaKoordit.koordinaatit && tokaAvattu == true || odota == true && texturePosition == tokaKuvaKoordit.koordinaatit && tokaAvattu == true)
                            {
                                spriteBatch.Draw(tuplaKuvat.FirstOrDefault(), texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                                avattuPala = map[i, j];
                            }
                            //Ekan palan avaus
                            else if (kliikkaa.X >= texturePosition.X && kliikkaa.X <= texturePosition.X + 46 && kliikkaa.Y >= texturePosition.Y && kliikkaa.Y <= texturePosition.Y + 46 && ekaAvattu == true)
                            {
                                //klikkauspaikka = Vector2.Zero;

                                spriteBatch.Draw(tyhjäKansi, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.11f);
                                avattuPala = map[i, j];

                                onAvattu = true;
                            }
                            else
                            {
                                    spriteBatch.Draw(kansi, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                                    kannet.Add(Content.Load<Texture2D>("TestiMuistipala48"));
                                    //onAvattu = false;
                                
                            }



                            if (tuplaKuvat.Count != 0)
                            {
                                var eka = tuplaKuvat.FirstOrDefault();
                                if (eka.Name != "tyhjä" && eka.Name != "käytetty")
                                {
                                    if (laitaKiinni == true)
                                    {
                                        if (odotusOhi == true)
                                        {
                                            spriteBatch.Draw(kansi, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                                            odotusOhi = false;
                                            laitaKiinni = false;
                                        }
                                    }
                                    else
                                    {


                                        spriteBatch.Draw(tuplaKuvat.FirstOrDefault(), texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                                    }

                                }
                                else if (eka.Name == "käytetty" && texturePosition == ekaKuvaKoordit.koordinaatit || eka.Name == "käytetty" && texturePosition == tokaKuvaKoordit.koordinaatit)
                                {
                                    if (oikeaPariOdotus == false)
                                    {
                                        spriteBatch.Draw(tyhjäRuutu, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1f);
                                    }
                                }
                                else if (eka.Name == "käytetty")
                                {
                                    spriteBatch.Draw(tyhjäRuutu, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1f);
                                }
                                //else if(pari==true)
                                //{
                                //    pari = false;
                                //    if (odotusOhi == true)
                                //    {
                                //        spriteBatch.Draw(tyhjäRuutu, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.99f);
                                //        odotusOhi = false;
                                //    }
                                //}
                                else
                                {
                                    if (odota == true && eka.Name == "tyhjä")
                                    {
                                        spriteBatch.Draw(tuplaKuvat.FirstOrDefault(), texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                                        eka.Name = "käytetty";

                                        //Koordit kiellettyKlikki = new Koordit();
                                        //kiellettyKlikki.koordinaatit = texturePosition;
                                        //kiellettyKlikki.nimi = eka.Name;
                                        //kielletytKlikit.Add(kiellettyKlikki);

                                        oikeaPariOdotus = true;
                                    }

                                }

                                if (kliikkaa.X >= texturePosition.X && kliikkaa.X <= texturePosition.X + 46 && kliikkaa.Y >= texturePosition.Y && kliikkaa.Y <= texturePosition.Y + 46 && painettu == true)
                                {
                                    Koordit taa = new Koordit();
                                    Koordit taa1 = new Koordit();


                                    if (ekaAvattu == true)
                                    {
                                        tokaKuva = tuplaKuvat.FirstOrDefault();
                                        ekaKuvaKoordit = koordit.FirstOrDefault();
                                        tokaKuvaKoordit.nimi = tokaKuva.Name;
                                        tokaKuvaKoordit.koordinaatit = texturePosition;
                                        var t = 4;
                                        if (ekaKuvaKoordit.koordinaatit != texturePosition && ekaKuvaKoordit.nimi == tokaKuva.Name)
                                        {
                                            foreach (var textuuri in tuplaKuvat.Where(n => n.Name == ekaKuvaKoordit.nimi))
                                            {

                                                textuuri.Name = "tyhjä";
                                                pari = true;

                                                kielletytKlikit.Add(texturePosition);
                                                kielletytKlikit.Add(ekaKuvaKoordit.koordinaatit);

                                            }
                                        }
                                        else
                                        {
                                            laitaKiinni = true;

                                        }

                                        var jj = koordit.FirstOrDefault();
                                        koordit.Remove(jj);
                                        onAvattu = false;
                                        ekaAvattu = false;
                                        tokaAvattu = true;
                                        painettu = false;
                                        arvausBooli = true;
                                        odota = true;
                                    }

                                    else
                                    {
                                        laitaKiinni = false;
                                        koopa.Add(texturePosition);//näyttöä varten

                                        var gg = tuplaKuvat.FirstOrDefault();
                                        string nimi = gg.Name;

                                        taa.koordinaatit = texturePosition;
                                        taa.nimi = nimi;
                                        koordit.Add(taa);
                                        painettu = false;

                                        //var avattuListanEka = koordit.FirstOrDefault();
                                        ekaAvattu = true;
                                        var vv = viimeksAvattuKoordinaatti.FirstOrDefault();
                                        viimeksAvattuKoordinaatti.Remove(vv);
                                        viimeksAvattuKoordinaatti.Add(texturePosition);

                                    }



                                    //else
                                    //{

                                    //    koopa.Add(texturePosition);

                                    //    var af = tuplaKuvat.FirstOrDefault();
                                    //    string nimi1 = af.Name;

                                    //    taa1.koordinaatit = texturePosition;
                                    //    taa1.nimi = nimi1;
                                    //    koordit.Add(taa1);
                                    //    painettu = false;

                                    //    var avattuListanEka = koordit.LastOrDefault();
                                    //    tokaAvaus = false;

                                    //    if (taa1.koordinaatit != taa.koordinaatit && taa1.nimi == taa.nimi)
                                    //    {
                                    //        foreach (var textuuri in tuplaKuvat.Where(n => n.Name == taa1.nimi))
                                    //        {
                                    //            var hemuli = textuuri;
                                    //        }
                                    //    }
                                    //}      
                                }
                                //else
                                //{
                                //    var jj = koordit.FirstOrDefault();
                                //    koordit.Remove(jj);
                                //    onAvattu = false;
                                //}

                                var itemToRemove = tuplaKuvat.FirstOrDefault();
                                if (itemToRemove != null)
                                    tuplaKuvat.Remove(itemToRemove);
                            }

                            ekaKerta = true;
                        }
                    }
                }
            }
           
            spriteBatch.End();

            if (tuplaKuvat.Count == 0)
            {
                ekaKertaMeniJo = true;
            }

            base.Draw(gameTime);
        }


        //foreach(var kuva in tuplaKuvat)

        //for (int index = 0; index < (tuplaKuvat.Count - 1); index++)
        //{

        //    for (int i = 0; i <= map.GetUpperBound(0); i++)
        //    {
        //        for (int j = 0; j <= map.GetUpperBound(1); j++)
        //        {
        //            int textureId = map[i, j];
        //            if (textureId != 0)
        //            {
        //                Vector2 texturePosition = new Vector2(i * tileSize, j * tileSize) + position;

        //                //Here you would typically index to a Texture based on the textureId.


        //                spriteBatch.Draw(tuplaKuvat.FirstOrDefault(), texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);


        //            }
        //        }
        //    }
        //}

        //MouseState ms = Mouse.GetState();

        //    if (ms.LeftButton == ButtonState.Pressed)
        //    {
        //        int x = Convert.ToInt32(ms.X) / 16;
        //int y = Convert.ToInt32(ms.Y) / 16 + 1;

        //Vector2 hiiripaikka = new Vector2(x, y);

        ////map[x, y] =
        //spriteBatch.Draw(pala, hiiripaikka, null, Color.White); //Left button Clicked, Change Texture here!

        //    }
    ////    if (ms.RightButton == ButtonState.Pressed)
    ////    {
    ////        int x = Convert.ToInt32(ms.X) / 16;
    ////        int y = Convert.ToInt32(ms.Y) / 16 + 1;

    ////        tiles[x, y] = //Right button Clicked, Change Texture here!


    ////}

}

    internal class Koordit
    {
       public Vector2 koordinaatit { get; set; }
       public string nimi { get; set; }
    }
}
