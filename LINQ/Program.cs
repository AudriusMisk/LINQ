using System;
using System.Collections.Generic;
using System.Linq;
using LINQ.Initial_Data;
using LINQ.InitialData;
using LINQ.Models;

namespace LINQ
{
    class Program
    {
        static List<string> miestai = new List<string> { "Vilnius", "Kaunas", "Klaipeda", "Alytus", "Vilnius" };
        static List<int> skaiciai = new List<int> { 3, 3, 5, 7, 8, 10 };
        static void Main(string[] args)
        {
            Console.WriteLine("Visi miestai {0}", string.Join(", ", miestai));

            // Sprendimas be LINQ
            foreach (var miestas in miestai)
            {
                if (miestas[0] == 'K') Console.WriteLine($"miestas is K yra {miestas}");
            }
            //Sprendimas su LINQ(filtravimo funkcija)
            Console.WriteLine("Miestai is K raides {0}", string.Join(", ", miestai.Where(miestas => miestas.StartsWith("K"))));

            //Elementu kiekio skaiciavimas su filtravimu
            Console.WriteLine("Miestai is K raides kiekis {0}", miestai.Count(miestas => miestas.StartsWith("K")));

            Console.WriteLine("Ar yra miestas Kaunas ? {0}", miestai.Any(miestas => miestas == "Kaunas") ? "Taip": "Ne");

            Console.WriteLine("Ar yra miestas Utena ? {0}", miestai.Any(miestas => miestas == "Utena") ? "Taip" : "Ne");

            var tikrinamiMiestai = new string[] { "Taurage", "Pabrade" };
            Console.WriteLine("Ar yra miestu tarp miestu is masyvo [Taurage, Pabrade] ? {0}",
                miestai.Any(miestas => tikrinamiMiestai.Contains(miestas)) ? "Taip": "Ne"
                ) ;
            
            var tikrinamiMiestai1 = new string[] { "Vilnius", "Taurage", "Pabrade" };
            Console.WriteLine("Ar yra miestu tarp miestu is masyvo [Vilnius, Taurage, Pabrade] ? {0}",
                miestai.Any(miestas => tikrinamiMiestai1.Contains(miestas)) ? "Taip" : "Ne"
                );

            //Rusiuoja pagal abecele
            Console.WriteLine("Surusiuoti nuo A iki Z miestai {0}", string.Join(", ", miestai.OrderBy(miestas => miestas)));

            //Rusiuoja atvirksciai abecele
            Console.WriteLine("Surusiuoti nuo Z iki A miestai {0}", string.Join(", ", miestai.OrderByDescending(miestas => miestas)));

            //Paima pirmus 2 masyvo elementus
            Console.WriteLine("Du miestai is saraso {0}", string.Join(", ", miestai.Take(2)));

            //Paima visus masyvo elementus isskyrus pirmus 2
            Console.WriteLine("Miestai is saraso be pirmu dvieju miestu {0}", string.Join(", ", miestai.Skip(2)));

            //Palieka tik unikalius masyvo elementus
            Console.WriteLine("Unikalus miestai {0}", string.Join(", ", miestai.Distinct()));

            Console.WriteLine("====================================================================================");

            //Norime suskaiciuoti skaiciai suma be LINQ
            int suma = 0;
            foreach (var skaicius in skaiciai)
            {
                suma+= skaicius;
            }
            Console.WriteLine("Skaicius suma {0}", suma);

            //Skaiciu suma su LINQ
            Console.WriteLine("Skaiciu suma {0}", skaiciai.Sum());

            //Maziausias skaicius
            Console.WriteLine("Skaiciu minimali reiksme {0}", skaiciai.Min());

            //Didziausias skaicius
            Console.WriteLine("Skaiciu maximali reiksme {0}", skaiciai.Max());

            //Vidurkis
            Console.WriteLine("Skaicius vidurkis {0}", skaiciai.Average());

            Console.WriteLine("Unikaliu mazesniu uz 5 skaiciu vidurkis {0}", skaiciai.Distinct().Where(s => s < 5).Average());

            List<int> skaiciai2 = new List<int> { 88, 89 };
            Console.WriteLine("Sujungti skaiciai {0}", string.Join(", ", skaiciai.Concat(skaiciai2)));

            Console.WriteLine("====================================");
            //LINQ OBJECT

            //isvesti asmenis kurie vyresni nei 20 metu
            var asmenysVyresni = PersonInitialData.DataSeed.Where(p => p.Age > 20);
            foreach (var asmuo in asmenysVyresni)
            {
                Console.WriteLine($"{asmuo.FirstName} {asmuo.LastName} amzius {asmuo.Age}");
            }


            Console.WriteLine("---------------------------------------");

            //linq refleksija
            var asmenuSarasas = PersonInitialData.DataSeed.Select(p =>
                new PersonDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                }
            );
            foreach (var asmuo in asmenuSarasas)
            {
                Console.WriteLine($"{asmuo.FirstName} {asmuo.LastName} amzius {asmuo.Age}");
            }

            Console.WriteLine("---------------------------------------");
            //query syntax
            var personWithHobby =
                from p in PersonInitialData.DataSeed
                join h in PersonHobbyInitialData.DataSeed on p.FirstName equals h.FirstName into hobbyGroup
                select new PersonInfo
                {
                    Person = p,
                    PersonHobbies = hobbyGroup.ToList()
                };

            foreach (var asmuo in personWithHobby)
            {
                List<string> hobbies = asmuo.PersonHobbies.Select(ph => ph.Text).ToList();
                Console.WriteLine($"{asmuo.Person.FirstName} {asmuo.Person.LastName} {string.Join(", ", hobbies)}");
            }

            //extension method syntax with nesting

            var personWithHobby1 =
                PersonInitialData.DataSeed.Select(p =>
                new PersonInfo
                {
                    Person = p,
                    PersonHobbies = PersonHobbyInitialData.DataSeed.Where(h => h.FirstName == p.FirstName).ToList()
                }
            );

            //query syntax with Anonymous type
            var personWithHobbyAnonymous =
                from p in PersonInitialData.DataSeed
                join h in PersonHobbyInitialData.DataSeed on p.FirstName equals h.FirstName into hobbyGroup
                select new
                {
                    FullName = $"{p.FirstName} {p.LastName}",
                    Hobbies = hobbyGroup.Select(hg => hg.Text).ToList()
                };

            foreach (var asmuo in personWithHobbyAnonymous)
            {
                Console.WriteLine($"{asmuo.FullName} {string.Join(", ", asmuo.Hobbies)}");
            }


            // SKAICIU UZDUOTIS

            int MinSkaicius = -100;
            int MaxSkaicius = 100;
            int[] randomMasyvas = new int[20];
            Random randomNumber = new Random();
            for (int i = 0; i < randomMasyvas.Length; i++)
            {
                randomMasyvas[i] = randomNumber.Next(MinSkaicius, MaxSkaicius);
            }

            var neigiamiSkaiciai = 0;
            var teigiamiSkaiciai = 0;
            Console.WriteLine("Masyvas: {0}", string.Join(", ", randomMasyvas));

            Console.WriteLine("Neigiami skaiciai: {0}", neigiamiSkaiciai = randomMasyvas.Distinct().Where(s => s < 0).Count());

            Console.WriteLine("Teigiami skaiciai: {0}", teigiamiSkaiciai = randomMasyvas.Distinct().Where(s => s > 0).Count());

            if (neigiamiSkaiciai > teigiamiSkaiciai)
            {
                Console.WriteLine("Neigiamu skaiciu daugiau");
            }
            if (neigiamiSkaiciai < teigiamiSkaiciai)
            {
                Console.WriteLine("Teigiamu skaiciu daugiau");
            }
            if (neigiamiSkaiciai == teigiamiSkaiciai)
            {
                Console.WriteLine("Teigiamu ir neigiamu skaiciu kiekis yra vienodas");
            }


        }

    }
}
