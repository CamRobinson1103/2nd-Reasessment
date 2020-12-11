using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames3D
{
    enum Shape
    {
        SPHERE,
        CUBE
    }

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected Matrix4 _globalTransform = new Matrix4();
        protected Matrix4 _localTransform = new Matrix4();
        protected Matrix4 _rotation = new Matrix4();
        protected Matrix4 _translation = new Matrix4();
        protected Matrix4 _scale = new Matrix4();
        protected Actor[] _children = new Actor[0];
        protected Vector3 _velocity;
        protected ConsoleColor _color;
        protected Color _rayColor;
        private float _collisionRadius;
        private float _radians;
        private float rotation;
        private Shape _shape;

        public bool Started { get; private set; }
        public Actor Parent { get; private set; }

        public Vector3 Forward
        {
            get
            {
                return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33).Normalized;
            }
            //set
            //{
            //    Vector3 lookPosition = WorldPosition + value.Normalized;
            //    LookAt(lookPosition);
            //}
        }

        public Vector3 WorldPosition
        {
            get
            {
                return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34);
            }
        }

        public Vector3 LocalPosition
        {
            get
            {
                return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34);
            }
            set
            {
                _translation.m14 = value.X;
                _translation.m24 = value.Y;
                _translation.m34 = value.Z;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            LocalPosition = new Vector3(x, y, z);
            _velocity = new Vector3();
            _color = color;
            _collisionRadius = collisionRadius;
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, z, collisionRadius, icon, color)
        {
            _rayColor = rayColor;
            _shape = shape;
        }

        public virtual void Start()
        {
            Started = true;
        }

        public void AddChild(Actor child)
        {
            //Create a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_children.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                appendedArray[i] = _children[i];
            }
            child.Parent = this;

            //Set the last value in the new array to be the actor we want to add
            appendedArray[_children.Length] = child;
            //Set old array to hold the values of the new array
            _children = appendedArray;

        }

        public bool RemoveChild(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _children.Length)
            {
                return false;
            }

            bool actorRemoved = false;

            //Create a new array with a size one less than our old array 
            Actor[] newArray = new Actor[_children.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            _children[index].Parent = null;
            //Set the old array to be the tempArray
            _children = newArray;
            return actorRemoved;
        }

        public bool RemoveChild(Actor child)
        {
            //Check to see if the actor was null
            if (child == null)
            {
                return false;
            }

            bool actorRemoved = false;
            //Create a new array with a size one less than our old array
            Actor[] newArray = new Actor[_children.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            child.Parent = null;
            //Set the old array to the new array
            _children = newArray;
            //Return whether or not the removal was successful
            return actorRemoved;
        }

        public void SetScale(Vector3 scale)
        {
            _scale.m11 = scale.X;
            _scale.m22 = scale.Y;
            _scale.m33 = scale.Z;
        }

        public void Scale(Vector3 scale)
        {
            if (scale.X != 0)
                _scale.m11 *= scale.X;
            if (scale.Y != 0)
                _scale.m22 *= scale.Y;
            if (scale.Z != 0)
                _scale.m33 *= scale.Z;
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the X axis
        /// </summary>
        /// <param name="radians">The angle to se the transform's rotation to</param>
        public void SetRotationX(float radians)
        {
            _radians = radians;
            _rotation.m22 = (float)Math.Cos(_radians);
            _rotation.m32 = -(float)Math.Sin(_radians);
            _rotation.m23 = (float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the Y axis
        /// </summary>
        /// <param name="radians">The angle to be the transform's rotation to</param>
        public void SetRotationY(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m31 = (float)Math.Sin(_radians);
            _rotation.m13 = -(float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the Z axis
        /// </summary>
        /// <param name="radians">The angle to se the transform's rotation to</param>
        public void SetRotationZ(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m12 = (float)Math.Sin(_radians);
            _rotation.m21 = -(float)Math.Sin(_radians);
            _rotation.m22 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Increases the angle of rotation by the given amount.
        /// </summary>
        /// <param name="radians">The amount of radians to increase the rotation by</param>
        public void Rotate(float radians)
        {
            _radians += radians;
            SetRotationY(_radians);
        }


        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be facing</param>
        //public void LookAt(Vector3 position)
        //{
        //    //Find the direction that the actor should look in
        //    Vector3 direction = (position - WorldPosition).Normalized;

        //    //Use the dotproduct to find the angle the actor needs to rotate
        //    float dotProd = Vector3.DotProduct(Forward, direction);
        //    if (Math.Abs(dotProd) > 1)
        //        return;
        //    float angle = (float)Math.Acos(dotProd);

        //    //Find a perpindicular vector to the direction
        //    Vector3 perp = new Vector3(direction.Y, -direction.X);

        //    //Find the dot product of the perpindicular vector and the current forward
        //    float perpDot = Vector3.DotProduct(perp, Forward);

        //    //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
        //    if (perpDot != 0)
        //        angle *= -perpDot / Math.Abs(perpDot);

        //    Rotate(angle);
        //}

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        protected void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            //Forward = Velocity.Normalized;
        }


        /// <summary>
        /// Updates the global transform to be the combination of the paernt and local
        /// transforms. Updates the transforms for all children of this actor
        /// </summary>
        private void UpdateGlobalTransform()
        {
            if (Parent != null)
                _globalTransform = Parent._globalTransform * _localTransform;
            else
                _globalTransform = _localTransform;

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].UpdateGlobalTransform();
            }
        }

        public bool CheckCollision(Actor other)
        {
            float distance = (other.WorldPosition - WorldPosition).Magnitude;
            return distance <= _collisionRadius + other._collisionRadius;
        }

        public virtual void OnCollision(Actor other)
        {

        }

        public virtual void Update(float deltaTime)
        {

            _localTransform = _translation * _rotation * _scale;

            UpdateGlobalTransform();

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;


            SetRotationY(rotation += (float)(Math.PI / 2) * deltaTime);
        }

        private void DrawShape()
        {
            switch (_shape)
            {
                case Shape.SPHERE:
                    Raylib.DrawSphere(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), 5, _rayColor);
                    break;
                case Shape.CUBE:
                    Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), 5, 5, 5, _rayColor);
                    break;
            }
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window
            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X + Forward.X) * 32),
                (int)((WorldPosition.Y + Forward.Y) * 32),
                Color.WHITE
            );
            Console.ForegroundColor = _color;
            DrawShape();
        }

        public virtual void Debug()
        {
            if (Parent != null)
                Console.WriteLine("Velocity: " + Velocity.X + ", " + Velocity.Y);
        }

        public void Destroy()
        {

            if (Parent != null)
                Parent.RemoveChild(this);

            foreach (Actor child in _children)
                child.Destroy();

            End();
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
