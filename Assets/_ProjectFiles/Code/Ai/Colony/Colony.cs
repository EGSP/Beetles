using System.Collections.Generic;
using Game.Entities.Beetles;
using Game.Environment.Buildings;
using UnityEngine;

namespace Game.Ai
{
    /// <summary>
    /// Колония хранит информацию о принадлежащих ей объектах и обрабатывает их.
    /// </summary>
    public class Colony : MonoBehaviour
    {
        /// <summary>
        /// Отношения к другим колониям.
        /// </summary>
        private List<Relation> _relations = new List<Relation>();

        /// <summary>
        /// Свои жуки.
        /// </summary>
        private List<Beetle> _beetles = new List<Beetle>();
        
        /// <summary>
        /// Свои постройки.
        /// </summary>
        private List<Building> _buildings = new List<Building>();
    }
}