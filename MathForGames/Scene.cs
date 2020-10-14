using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        private Entity[] _entities;
        public Scene()
        {
            _entities = new Entity[0];
        }
        public void AddEntity(Entity entity)
        {
            Entity[] appendedArray = new Entity[_entities.Length + 1]; // creates new array thats 1 postions longer than entites[]
            for (int i = 0; i < _entities.Length; i++)
            {
                appendedArray[i] = _entities[i];  //copies all positions from entites to appended array
            }
            appendedArray[_entities.Length] = entity; //creates new entity at end of the appended array
            _entities = appendedArray;                //replaces _entites array value as appended arrays array;
        }
        public bool RemoveEntity(int index)
        {
            if (index < 0 || index >= _entities.Length) //checks to see if the index is outside the bounds of our array
            {
                return false;
            }
            bool entityRemoved = false;
            Entity[] appenedArray = new Entity[_entities.Length - 1]; //creates new [] that is 1 position shorter than entities[]
            int j = 0; //create a var to access app array index
            for (int i = 0; i < index; i++) //copy values from old array to new one
            {
                if (i != index)           //if the current index is not the index that needs to be removed
                {
                    appenedArray[i] = _entities[i];  //add value into the old array and increment j
                    j++;
                }
                else
                {
                    entityRemoved = true;
                }
            }
            _entities = appenedArray;
            return entityRemoved;
        }
        public bool RemoveEntity(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            bool entityRemoved = false;
            Entity[] appendedArray = new Entity[_entities.Length - 1];
            int j = 0;
            for(int i = 0; i <_entities.Length; i++)
            {
                if (entity != _entities[i])
                {
                    appendedArray[j] = _entities[i];
                    j++;
                }
                else
                {
                    entityRemoved = true;
                }
            }
            return entityRemoved;
        }
        public virtual void Start()
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i].Start();
            }
        }
        public virtual void Update()
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i].Update();
            }
        }
        public virtual void Draw()
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i].Draw();
            }
        }
        public virtual void End()
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i].End();
            }
        }
    }
}
