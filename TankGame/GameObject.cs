using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Raylib;

namespace TankGame
{
    class GameObject
    {
        protected GameObject parent = null;

        protected List<GameObject> children = new List<GameObject>();

        protected Matrix3 localTransform = new Matrix3();
        protected Matrix3 globalTransform = new Matrix3();

        public AABB bounds = new AABB();
        public float w = 0;
        public float h = 0;

        public bool isColliding = false;
        public bool canCollide = true;

        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }

        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }

        public GameObject Parent
        {
            get { return parent; }
        }

        public GameObject()
        {
            
        }

        public int GetChildCount()
        {
            return children.Count;
        }

        public GameObject GetChild(int index)
        {
            return children[index];
        }

        public void AddChild(GameObject child)
        {
            Debug.Assert(child.parent == null);
            child.parent = this;
            children.Add(child);
        }

        public void RemoveChild(GameObject child)
        {
            if(children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }
        public virtual void OnDraw()
        {

        }

        public virtual void OnCollide(GameObject hit)
        {

        }

        public virtual void OnNotCollide()
        {

        }

        public void Collide(GameObject hit)
        {
            isColliding = true;
            
            OnCollide(hit);

            if (isColliding) bounds.drawCol = Color.RED;
        }

        public void NotCollide()
        {
            isColliding = false;

            OnNotCollide();

            if (!isColliding) bounds.drawCol = Color.BLACK;
        }

        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);

            Vector3 pos = globalTransform.ToVector3();

            bounds.SetBounds(pos + new Vector3(-(w / 2), -(h / 2), 0), pos + new Vector3(w/2,h/2,0));

            foreach(GameObject child in children)
            {
                child.Update(deltaTime);
            }
        }

        public void Draw()
        {
            OnDraw();

            bounds.Draw();

            foreach(GameObject child in children)
            {
                child.Draw();
            }
        }

        public void UpdateTransform()
        {
            if (parent != null) globalTransform = parent.globalTransform * localTransform;
            else globalTransform = localTransform;

            foreach (GameObject child in children) child.UpdateTransform();
        }

        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }

        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }

        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);
            UpdateTransform();
        }

        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);
            UpdateTransform();
        }

        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }

        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);
            UpdateTransform();
        }

        public void FaceVector3(Vector3 location)
        {
            Vector3 self = globalTransform.ToVector3(); // Getting the turrets Matrice's as a Vector.

            float rotation = 0; 
            if (parent != null) rotation = parent.GlobalTransform.GetRotation(); // Grabbing the rotation of the parent.

            double angle = Math.Atan2((location.y - self.y), (location.x - self.x)); // Grabbing the angle of the x, y of the turret and the mouse.

            localTransform.AbsoluteRotate(angle - rotation); // Absolutely rotating (doesn't add).
            UpdateTransform();
        }

        public void IsVisible()
        {
            Vector3 pos = globalTransform.ToVector3();


        }

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
