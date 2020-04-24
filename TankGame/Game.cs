﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    class Game
    {
        // Basic stuff for running a game.
        Stopwatch stopwatch = new Stopwatch();
        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f; // Delta time

        public static GameObject tankObject = new GameObject(); // Tank object which has controls attached to, and is controlled by the player.
        public static GameObject turretObject = new GameObject(); // Turret object that is attached to tank object.

        SpriteObject tankSprite = new SpriteObject(); // Tank sprite used for tank object.
        SpriteObject turretSprite = new SpriteObject(); // Turret sprite used for turret object.

        public static List<Bullet> bullets = new List<Bullet>(); // A list of bullets.

        public Game()
        {
        }

        /// <summary>
        /// On startup.
        /// </summary>
        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            SetTargetFPS(120);

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            // Creating the sprites & tank game objects for later use.
            tankSprite.Load("tankBlue_outline.png");
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            turretSprite.Load("barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
            tankObject.w = (int)(tankSprite.Width * 1.3);
            tankObject.h = (int)(tankSprite.Height * 1.3);
            //

            CreateMap(); // Creating the initial map.
        }

        public void Shutdown()
        {
        }
        
        /// <summary>
        /// Shooting a bullet from the tank.
        /// </summary>
        /// <param name="type">Should the bullet bounce?</param>
        public void ShootBullet(bool type)
        {
            Bullet bullet = new Bullet();
            bullet.w = 13;
            bullet.h = 13;

            bullet.bounce = type;

            float rotate = turretObject.GlobalTransform.GetRotation(); // Getting the tank turret rotation.
            bullet.SetRotate(rotate); // Rotating so it is facing the direction the turret is.

            Vector3 facing = turretObject.GlobalTransform.ToVector3() + (turretObject.GlobalTransform.GetForward() * (int)(turretSprite.Height * 1.5)); // Firing the direction.

            bullet.SetPosition(facing.x, facing.y); // Setting infront of the turret.

            bullets.Add(bullet);

            bullet = null;
        }
        
        List<GameObject> walls = new List<GameObject>(); // Wall objects.

        /// <summary>
        /// Creating a wall.
        /// </summary>
        /// <param name="pos">The Vector3 position (only need to use x, y)</param>
        /// <param name="w">The width of the wall.</param>
        /// <param name="h">The height of the wall.</param>
        public void CreateWall(Vector3 pos, float w, float h)
        {
            GameObject wall = new GameObject();
            wall.w = w;
            wall.h = h;

            wall.SetPosition(pos.x + w/2, pos.y + h/2);

            walls.Add(wall);
        }
        

        /// <summary>
        /// Creating the initial map.
        /// </summary>
        public void CreateMap()
        {
            CreateWall(new Vector3(0, 0, 0), 100, 600);
            CreateWall(new Vector3(302, 0, 0), 100, 300);

            CreateWall(new Vector3(101, 0, 0), 200, 100);
            CreateWall(new Vector3(101, 500, 0), 200, 100);

            CreateWall(new Vector3(403, 0, 0), 400, 100);
            CreateWall(new Vector3(502, 500, 0), 200, 100);
            CreateWall(new Vector3(703, 300, 0), 100, 300);

            tankObject.SetPosition(151 + tankObject.w/2, 151 + tankObject.h / 2);
        }

        /// <summary>
        /// Updating the game, this handles collisions and movement.
        /// </summary>
        public void Update()
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;
            
            int mouseX = GetMouseX();
            int mouseY = GetMouseY();

            turretObject.FaceVector3(new Vector3(mouseX, mouseY, 1));

            // Movement
            
            int mult = 1;
            if (IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT)) mult = 2;

            if (IsKeyDown(KeyboardKey.KEY_A)) tankObject.Rotate(-deltaTime * 2 * mult);

            if (IsKeyDown(KeyboardKey.KEY_D)) tankObject.Rotate(deltaTime * 2 * mult);

            if (IsKeyDown(KeyboardKey.KEY_W)) {
                Vector3 facing = tankObject.LocalTransform.GetForward() * deltaTime * 100 * mult;

                if (!tankObject.isColliding) tankObject.Translate(facing.x, facing.y);
            }
            else if (IsKeyDown(KeyboardKey.KEY_S)) { 
                Vector3 facing = tankObject.LocalTransform.GetForward() * deltaTime * -100 * mult;

                if (!tankObject.isColliding) tankObject.Translate(facing.x, facing.y);
            }

            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) ShootBullet(false);
            if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON)) ShootBullet(true);

            //

            tankObject.Update(deltaTime);

            List<GameObject> objects = new List<GameObject>();

            objects.Add(tankObject);

            // Updating Walls.
            foreach (GameObject wall in walls)
            {
                wall.Update(deltaTime);

                objects.Add(wall);
            }

            // Updating Bullets.
            foreach (Bullet bullet in bullets)
            {
                bullet.Update(deltaTime);

                objects.Add(bullet);
            }

            // Collisions
            foreach (GameObject obj in objects)
            {
                foreach (GameObject objj in objects)
                {
                    if (obj != objj & obj.canCollide & objj.canCollide)
                    {
                        if (obj.bounds.Overlaps(objj.bounds))
                        {
                            obj.Collide(objj);
                            break;
                        }
                        else
                        {
                            obj.NotCollide();
                        }
                    }
                }
            }

            // Delete unused bullets.
            foreach (Bullet bullet in new List<Bullet>(bullets))
            {
                if (bullet.toRemove)
                {
                    bullets.Remove(bullet);
                }
            }

            lastTime = currentTime;
        }
        
        /// <summary>
        /// Drawing the objects and other ui based instances.
        /// </summary>
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 12, Color.RED);
            DrawText($"{GetMouseX()} >< {GetMouseY()}", 10, 23, 12, Color.RED);

            Vector3 tankPos = tankObject.GlobalTransform.ToVector3();
            DrawText($"{tankPos.x} >< {tankPos.y}", 10, 36, 12, Color.RED);

            // Drawing the tank.
            tankObject.Draw();

            // Drawing walls.
            foreach (GameObject wall in walls)
            {
                wall.bounds.Draw();
            }

            // Drawing Bullets.
            foreach (GameObject bullet in bullets)
            {
                bullet.Draw();
            }

            // To Delete
            //Vector3 pos = tankObject.LocalTransform.ToVector3();
            //int w = (int)(tankSprite.texture.width * 1.3);
            //int h = (int)(tankSprite.texture.height * 1.3);

            //DrawRectangleLines((int)pos.x - w/2, (int)pos.y - h/2, w, h, Color.BLACK);
            //

            DrawLine((int)tankPos.x, (int)tankPos.y, GetMouseX(), GetMouseY(), Color.RED);
            DrawCircleLines(GetMouseX(), GetMouseY(), 10, Color.BLACK);
            EndDrawing();
        }

    }
}
