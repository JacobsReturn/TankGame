using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    /// <summary>
    /// Bullet to fire at objects.
    /// </summary>
    class Bullet : GameObject
    {
        public bool toRemove = false; // To remove? (when it goes out of border or finishes exploding)
        public bool explode = false; // About to explode?
        public bool bounce = false; // Should bounce?
        public int bounces = 0; // 3 then stop bouncing.
        public bool justShot = true; // Did the bullet just shoot?

        public Bullet()
        {
        }

        /// <summary>
        /// Handling explosions and bullet travelling.
        /// </summary>
        /// <param name="deltaTime">Speed of frames.</param>
        public override void OnUpdate(float deltaTime)
        {
            if (!explode)
            {
                Vector3 facing = new Vector3(globalTransform.m1, globalTransform.m2, 1) * deltaTime * 500;
                Translate(facing.x, facing.y);

                Vector3 pos = globalTransform.ToVector3();

                if (pos.x - 10 > GetScreenWidth() || pos.y - 10 > GetScreenHeight() || pos.x + 10 < 0 || pos.y + 10 < 0)
                {
                    toRemove = true;
                }
            }
            else
            {
                
            }
        }

        /// <summary>
        /// Checking if it collided, so we can explode bullet or bounce.
        /// </summary>
        /// <param name="hit">The object tht hit<param>
        public override void OnCollide(GameObject hit)
        {
            if (hit == Game.tankObject | hit == Game.turretObject)
            {
                if (!bounce | bounces > 2) isColliding = false;
            }
            
            if (isColliding)
            {
                //toRemove = true;

                // Bad bouncing method, change later.
                if (bounce)
                {
                    //Vector3 self = globalTransform.ToVector3();
                    //Vector3 location = hit.GlobalTransform.ToVector3();

                    //double angle = Math.Atan2((location.y - self.y), (location.x - self.x));

                    //globalTransform.RotateZ(360-angle + globalTransform.GetRotation());

                    globalTransform.RotateZ(globalTransform.GetRotation() + 360); // Flipping rotation.

                    //bounces += 1;

                    //if (bounces >= 3)
                    //{
                        //bounce = false;
                    //}
                }
                else
                {
                    explode = true; // Making it explode.
                    canCollide = false; // Making it so nothing collides with it anymore.
                }
            }
        }

        float explosionLerp = 0; // Explosion animation speed. (increase frames, and it gets fasttteerrr)

        public override void OnDraw()
        {
            if (explode)
            {
                explosionLerp = Lerp(explosionLerp, 1f, 0.05f); // Lerping explosion animation, for nice smooth animation.

                if (explosionLerp > 0.9) toRemove = true; // Setting the bullet to remove when basically done.

                // Drawing the explosion animation.

                Vector3 pos = globalTransform.ToVector3();
                DrawCircle((int)pos.x + (int)(25 * explosionLerp), (int)pos.y, 5, Color.ORANGE);
                DrawCircle((int)pos.x + (int)(-25 * explosionLerp), (int)pos.y, 5, Color.ORANGE);

                DrawCircle((int)pos.x, (int)pos.y + (int)(25 * explosionLerp), 5, Color.ORANGE);
                DrawCircle((int)pos.x, (int)pos.y + (int)(-25 * explosionLerp), 5, Color.ORANGE);

                DrawCircle((int)pos.x + (int)(25 * explosionLerp), (int)pos.y + (int)(25 * explosionLerp), 5, Color.ORANGE);
                DrawCircle((int)pos.x + (int)(25 * explosionLerp), (int)pos.y + (int)(-25 * explosionLerp), 5, Color.ORANGE);

                DrawCircle((int)pos.x + (int)(-25 * explosionLerp), (int)pos.y + (int)(25 * explosionLerp), 5, Color.ORANGE);
                DrawCircle((int)pos.x + (int)(-25 * explosionLerp), (int)pos.y + (int)(-25 * explosionLerp), 5, Color.ORANGE);
            }
            else
            {
                // Moving the bullet.
                Vector3 pos = globalTransform.ToVector3();
                if (bounce) DrawCircle((int)pos.x, (int)pos.y, 10, Color.PURPLE);
                else DrawCircle((int)pos.x, (int)pos.y, 10, Color.BLACK);
            }

        }
    }
}
