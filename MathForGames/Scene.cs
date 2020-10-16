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
                    entityRemoved = true;            //says that the entity was succesfully removed
                }
            }
            _entities = appenedArray;              //sets old array equal to new array
            return entityRemoved;                  // returns true or false
        }
        public bool RemoveEntity(Entity entity)         //Takes in an entity returns a bool
        {
            if (entity == null)                        //if the entity is null
            {
                return false;                          //return false since the slot is already empty
            }
            bool entityRemoved = false;                //bool to test if an entity has been removed
            Entity[] appendedArray = new Entity[_entities.Length - 1];  //creates a temp [] thats one postion shorter
            int j = 0;                                         //placeholder var to help set old [] to new []
            for(int i = 0; i <_entities.Length; i++)       // For each slot in the _entites []
            {
                if (entity != _entities[i])                 //check to see if the entity you want to remove is not in this slot
                {
                    appendedArray[j] = _entities[i];          //if it isnt, copy the entity and paste it into the new []
                    j++;                                 //increment the placeholder val since an entity was added
                }
                else
                {
                    entityRemoved = true;                    //if it is in the slot, ignore it and set the entity removed true
                }
            }
            return entityRemoved;                           //if an entity was removed returns true else false
        }
        public bool CheckPositionAvailable(float x, float y) //takes in an x and y to see if they will land on an available slot
        {
            for (int i = 0; i < _entities.Length; i++)  //each entity in the entity []
            {
                if ( x == _entities[i].Position.X &&  y == _entities[i].Position.Y) // if they share the same postion as the x and y
                {
                    return false; //returns false for the Position is not available
                }
            }
            return true; //if it never returns false it will say the position is available.
        }
        public virtual void Start()
        {
            for (int i = 0; i < _entities.Length; i++)      //for each entity in the entities[]
            {
                _entities[i].Start();                      //call their start function
            }
        }
        public virtual void Update()
        {
            for (int i = 0; i < _entities.Length; i++)         //for each entity in the entites []
            {
                _entities[i].Update();                          //call their update function
                
            }
        }
        public virtual void Draw()
        {
            for (int i = 0; i < _entities.Length; i++)   //for each entity in the entites[]
            {
                _entities[i].Draw();                       //call their draw function
            }
        }
        public virtual void End()
        {
            for (int i = 0; i < _entities.Length; i++)   //for each entity in the entites[]
            {
                _entities[i].End();                     //call their end functions
            }
        }
    }
}
