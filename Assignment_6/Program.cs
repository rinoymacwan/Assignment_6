using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public class Program
    {
        IList<Employee> employeeList;
        IList<Salary> salaryList;

        public Program()
        {
            employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

            salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
        }

        public static void Main()
        {
            Program program = new Program();

            program.Task1();

            program.Task2();

            program.Task3();
        }

        public void Task1()
        {
            var query = employeeList.Join(salaryList.Where(a =>a.Type.ToString()=="Monthly"), e => e.EmployeeID, s => s.EmployeeID,
                (e, s) => new
                {
                    name = e.EmployeeFirstName,
                    sal = s.Amount
                     
                }).OrderBy(x => x.sal);
            foreach (var item in query)
            {
                Console.WriteLine($"{item.name}: {item.sal}");
            }
        }

        public void Task2()
        {
            var query = employeeList.Join(salaryList.Where(a => a.Type.ToString() == "Monthly"), e => e.EmployeeID, s => s.EmployeeID,
                (e, s) => new
                {
                    name = e.EmployeeFirstName,
                    age = e.Age,
                    sal = s.Amount

                }).OrderByDescending(x => x.age).Skip(1).Take(1);
            Console.WriteLine();
            Console.WriteLine("Second Oldest Employee");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.name}: {item.sal}");
            }
        }

        public void Task3()
        {
            var query = (from salary in salaryList
                        join emp in employeeList on salary.EmployeeID equals emp.EmployeeID
                        where emp.Age > 30
                        select new
                        {
                            name = emp.EmployeeFirstName,
                            type = salary.Type,
                            sal = salary.Amount
                        }).ToList();
            var monthly = query.Where(x => x.type.ToString() == "Monthly").Select(c => c.sal).Average();
            var performance = query.Where(x => x.type.ToString() == "Performance").Select(c => c.sal).Average();
            var bonus = query.Where(x => x.type.ToString() == "Bonus").Select(c => c.sal).Average();

            Console.WriteLine();
            Console.WriteLine("Mean of Salaries");
            Console.WriteLine($"Monthly: {monthly}");
            Console.WriteLine($"Performance: {performance}");
            Console.WriteLine($"Bonus: {bonus}");

        }
    }

    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
}
