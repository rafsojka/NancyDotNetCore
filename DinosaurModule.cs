using System.Collections.Generic;

using Nancy;
using Nancy.ModelBinding;

namespace WebApplication
{
    // http://engineering.laterooms.com/building-microservices-with-nancy-fx/
    public class DinosaurModule : NancyModule
    {
        class Dinosaur
        {
            public string Name { get; set; }
            public int HeightInFeet { get; set; }
            public string Status { get; set; }
        }


        private static List<Dinosaur> dinosaurs = new List<Dinosaur>()
        {
            new Dinosaur() {
            Name = "Kierkegaard",
            HeightInFeet = 6,
            Status = "Inflated"
            }
        };

        public DinosaurModule()
        {
            Get("/dinosaurs/{id}", parameters => dinosaurs[parameters.id - 1].ToString());

            Post("/dinosaurs", parameters =>
            {
                var model = this.Bind<Dinosaur>();
                dinosaurs.Add(model);
                return dinosaurs.Count.ToString();
            });
        }
    }
}