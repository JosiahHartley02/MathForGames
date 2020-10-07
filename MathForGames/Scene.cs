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
            Entity[] appendedArray = new Entity[_entities.Length + 1]; // creates new array thats 1 postions longer than entites
            for(int i = 0; i <_entities.Length; i++)
            {
                appendedArray[i] = _entities[i];  //copies all positions from entites to appended array
            }
            appendedArray[_entities.Length] = entity; //creates new entity at end of the appended array
            _entities = appendedArray;                //replaces _entites array value as appended arrays array;
        }
        public void Start()
        {

        }
        public void Update()
        {

        }
        public void Draw()
        {

        }
        public void End()
        {

        }
    }
}
