using System;

/*
 * Lab 2
 * Variant 1
 * 
 * Реалізувати задачу «Розрахунок щоденної норми споживання кілокалорій». 
 * Необхідно врахувати, що норма споживання кілокалорій залежить від 
 * типу статури, зросту, ваги, віку, статі, групи фізичної активності (низька, середня і висока активність).
 */

/*
 * Facade is a structural design pattern 
 * that provides a simplified interface to a library, a framework, or any other complex set of classes.
 */

namespace lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            // Facade is initialized with subsystem`s objects.

            CalorieCalculatorFacade calculator = 
                new CalorieCalculatorFacade
                (
                    new Weight(70),
                    new Height(175),
                    new Age(18),
                    new Gender(Genders.Male),
                    new Activity(Activities.Medium)
                );

            // The client code works with complex subsystems through a simple
            // interface provided by the Facade.

            calculator.CountCalories();
            calculator.PrintCalories();
        }
    }

    // The CalorieCalculatorFacade class provides a simple interface 
    // to the complex logic of several subsystems. 
    // This facade delegates the client requests to the appropriate objects within the subsystems. 
    // The facade is also responsible for managing their lifecycle. 
    // All of this shields the client from the undesired complexity of the subsystems.

    class CalorieCalculatorFacade
    {
        float kilocalories = 0;

        Weight weight;
        Height height;
        Age age;
        Gender gender;
        Activity activity;

        public CalorieCalculatorFacade
            (
                Weight weight,
                Height height,
                Age age,
                Gender gender,
                Activity activity
            )
        {
            this.weight = weight;
            this.height = height;
            this.age = age;
            this.gender = gender;
            this.activity = activity;
        }

        // The Facade's methods are convenient shortcuts to the sophisticated
        // functionality of the subsystems. 

        public void CountCalories()
        {
            kilocalories = weight.Calculate() + height.Calculate() + age.Calculate() + gender.Calculate();
            kilocalories *= activity.Calculate();
        }

        public void PrintCalories()
        {
            Console.WriteLine(kilocalories + " kcal");
        }
    }

    // The Subsystems can accept requests either from the facade or client
    // directly. In any case, to the Subsystem, the Facade is yet another
    // client, and it's not a part of the Subsystem.
    
    interface IValue
    {
        float Calculate();
    }

    public enum Genders
    {
        Male,
        Female
    }

    public enum Activities
    {
        Low,
        Medium,
        High
    }

    // Subsystem1
    class Weight : IValue
    {
        float kilograms;
        public Weight(float kilograms)
        {
            this.kilograms = kilograms;
        }

        public float Calculate()
        {
            return 10 * kilograms;
        }
    }

    // Subsystem2
    class Height : IValue
    {
        float centimeters;
        public Height(float centimeters)
        {
            this.centimeters = centimeters;
        }

        public float Calculate()
        {
            return 6.25f * centimeters;
        }
    }

    // Subsystem3
    class Age : IValue
    {
        int years;
        public Age(int years)
        {
            this.years = years;
        }

        public float Calculate()
        {
            return -5 * years;
        }
    }

    // Subsystem4
    class Gender : IValue
    {
        Genders gender;
        public Gender(Genders gender)
        {
            this.gender = gender;
        }

        public float Calculate()
        {
            if(gender == Genders.Female)
            {
                return -161;
            }
            else
            {
                return 5;
            }
            
        }
    }

    // Subsystem5
    class Activity : IValue
    {
        Activities activity;
        public Activity(Activities activity)
        {
            this.activity = activity;
        }

        public float Calculate()
        {
            switch(activity)
            {
                case Activities.Low:
                    return 1.3f;
                case Activities.Medium:
                    return 1.6f;
                case Activities.High:
                    return 1.9f;
                default:
                    return 1.6f;
            }
        }
    }

}
