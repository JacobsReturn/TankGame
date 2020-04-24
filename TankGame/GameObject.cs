using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Raylib;

namespace TankGame
{
    /// <summary>
    /// Creating a game object.
    /// </summary>
    class GameObject
    {
        protected GameObject parent = null; // The parent of the game object.

        protected List<GameObject> children = new List<GameObject>(); // A list of children.

        protected Matrix3 localTransform = new Matrix3(); // The local transform.
        protected Matrix3 globalTransform = new Matrix3(); // The global transform.

        public AABB bounds = new AABB(); // Bounds for later.
        public float w = 0; 
        public float h = 0;

        public bool isColliding = false; // Is it colliding?
        public bool canCollide = true; // Can it collide?

        // Getting the local transform.
        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }

        // Getting the global transform.
        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }

        // Getting the object parent.
        public GameObject Parent
        {
            get { return parent; }
        }

        public GameObject()
        {
            
        }

        /// <summary>
        /// Get the count of the children.
        /// </summary>
        /// <returns>A count of the children.</returns>
        public int GetChildCount()
        {
            return children.Count;
        }

        /// <summary>
        /// Grabbing an indexed child.
        /// </summary>
        /// <param name="index">Child Index</param>
        /// <returns>The Game Object.</returns>
        public GameObject GetChild(int index)
        {
            return children[index];
        }

        /// <summary>
        /// Adding a child to the children stack.
        /// </summary>
        /// <param name="child">The child game object.</param>
        public void AddChild(GameObject child)
        {
            Debug.Assert(child.parent == null);
            child.parent = this;
            children.Add(child);
        }

        /// <summary>
        /// Removing a child from the children stack.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        public void RemoveChild(GameObject child)
        {
            if(children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        /// <summary>
        /// An update to be overrided and called later.
        /// </summary>
        /// <param name="deltaTime">Speed in which it updated.</param>
        public virtual void OnUpdate(float deltaTime)
        {

        }

        /// <summary>
        /// A drawing function to be overrided and run after the current draw..
        /// </summary>
        public virtual void OnDraw()
        {

        }

        /// <summary>
        /// To be overrided, it runs when collided.
        /// </summary>
        /// <param name="hit"></param>
        public virtual void OnCollide(GameObject hit)
        {

        }

        /// <summary>
        /// When it doesn't collide do this, to be overrided.
        /// </summary>
        public virtual void OnNotCollide()
        {

        }

        /// <summary>
        /// On collision.
        /// </summary>
        /// <param name="hit">The object that was hit.</param>
        public void Collide(GameObject hit)
        {
            isColliding = true;
            
            OnCollide(hit); // Runs our override function.

            if (isColliding) bounds.drawCol = Color.RED; // If it collided, set the colour to red.
        }

        /// <summary>
        /// When it does't collide.
        /// </summary>
        public void NotCollide()
        {
            isColliding = false;

            OnNotCollide(); // Runs our override function.

            if (!isColliding) bounds.drawCol = Color.BLACK; // If it's not collided, go back to original colour.
        }

        /// <summary>
        /// On update to run constant stuff..
        /// </summary>
        /// <param name="deltaTime">the time speed.</param>
        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime); // Updating our overrided function.

            Vector3 pos = globalTransform.ToVector3(); // Grabbing the position of the GameObject.

            bounds.SetBounds(pos + new Vector3(-(w / 2), -(h / 2), 0), pos + new Vector3(w/2,h/2,0)); // Making sure the bounds are in the right position. (centered)

            // Updated the children too. (which inside of the child, will do the same too, etc etc)
            foreach(GameObject child in children)
            {
                child.Update(deltaTime);
            }
        }

        /// <summary>
        /// Drawing the objects.
        /// </summary>
        public void Draw()
        {
            OnDraw(); // Running our overrided function.

            bounds.Draw(); // Drawing the AABB bounds.

            // Drawing the children too. (which inside fo the child, will do the same too, etc etc)
            foreach(GameObject child in children)
            {
                child.Draw();
            }
        }

        /// <summary>
        /// Updating the transfomrs position and the childrens positions.
        /// </summary>
        public void UpdateTransform()
        {
            if (parent != null) globalTransform = parent.globalTransform * localTransform;
            else globalTransform = localTransform;

            foreach (GameObject child in children) child.UpdateTransform();
        }

        /// <summary>
        /// Setting the position of the transform.
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }

        /// <summary>
        /// Setting the rotation of the transform.
        /// </summary>
        /// <param name="radians">Radian of rotation</param>
        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }

        /// <summary>
        /// Setting the scale of the transform. (does not effect image size)
        /// </summary>
        /// <param name="width">The width of the transform.</param>
        /// <param name="height">The height of the transform.</param>
        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);
            UpdateTransform();
        }

        /// <summary>
        /// Translating the transform (moving it).
        /// </summary>
        /// <param name="x">X to move to.</param>
        /// <param name="y">Y to move to.</param>
        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);
            UpdateTransform();
        }

        /// <summary>
        /// Rotating the transform. Kinda like SetRotate.
        /// </summary>
        /// <param name="radians">The rotation radian to set too.</param>
        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }

        /// <summary>
        /// Scaling the transform. Basically the same as SetScaled.
        /// </summary>
        /// <param name="width">Width to.</param>
        /// <param name="height">Height to.</param>
        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);
            UpdateTransform();
        }

        /// <summary>
        /// Facing the object towards a supplied Vector.
        /// </summary>
        /// <param name="location">A Vector location to face.</param>
        public void FaceVector3(Vector3 location)
        {
            Vector3 self = globalTransform.ToVector3(); // Getting the turrets Matrice's as a Vector.

            float rotation = 0; 
            if (parent != null) rotation = parent.GlobalTransform.GetRotation(); // Grabbing the rotation of the parent.

            double angle = Math.Atan2((location.y - self.y), (location.x - self.x)); // Grabbing the angle of the x, y of the turret and the mouse.

            localTransform.AbsoluteRotate(angle - rotation); // Absolutely rotating (doesn't add).
            UpdateTransform();
        }

        /// <summary>
        /// Is it visible? (unfinished)
        /// </summary>
        public void IsVisible()
        {
            Vector3 pos = globalTransform.ToVector3();
        }

        /// <summary>
        /// When the object is removed.
        /// </summary>
        ~GameObject()
        {
            if(parent != null)
            {
                parent.RemoveChild(this);
            }

            foreach(GameObject so in children)
            {
                so.parent = null;
            }
        }
    }
}
