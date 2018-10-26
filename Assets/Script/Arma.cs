using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public enum TypeAttack
    {
        Target,
        Area
    }
    public class Arma 
    {
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Range { get; set; }
        public TypeAttack  TypeAttack { get; set; }
        public ColoredArea EfectArea { get; set; }
    }
}
