using Car.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Application.Services
{
    public interface ICarService
    {
        public string TokenGenerator(CarModel model);
    }

}
