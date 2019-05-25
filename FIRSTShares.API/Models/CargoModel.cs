using FIRSTShares.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Models
{
    public class CargoModel
    {
        public int ID { get; set; }
        public CargoTipo Tipo { get; set; }
        public bool Excluido { get; set; }
    }
}
