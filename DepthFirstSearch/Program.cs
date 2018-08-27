using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthFirstSearch
{
    class Program
    {
        public class Employee
        {
            public Employee(string name)
            {
                this.name = name;
            }

            public string name { get; set; }
            public List<Employee> Employees
            {
                get
                {
                    return EmployeeList;
                }
            }
            public void isEmployeeOf(Employee e)
            {
                EmployeeList.Add(e);
            }

            List<Employee> EmployeeList = new List<Employee>();

            public override string ToString()
            {
                return name;
            }
        }

        public class DepthFirstAlgorithm
        {
            public Employee BuildEmployeeGraph()
            {
                Employee Eva = new Employee("Eva");  // trabajamos con la de eVa
                Employee Sofia = new Employee("Sofia");
                Employee Brian = new Employee("Brian");
                Eva.isEmployeeOf(Sofia);
                Eva.isEmployeeOf(Brian);

                Employee Lisa = new Employee("Lisa");  // adyacentes creadas
                Employee Tina = new Employee("Tina");
                Employee John = new Employee("John");
                Employee Mike = new Employee("Mike");
                Sofia.isEmployeeOf(Lisa);
                Sofia.isEmployeeOf(John);
                Brian.isEmployeeOf(Tina);
                Brian.isEmployeeOf(Mike);

                return Eva;

            }

            public Employee Search(Employee root, string nameToSearchFor) //recorremos todos los empleados, va recorriendo otra vez hasta que lo encuentre, llamándose a sí mismo.
            {
                if (nameToSearchFor == root.name)
                    return root;

                Employee personFound = null;
                for (int i = 0; i < root.Employees.Count; i++) // for con el count de la lista, que es la cantidad de la lista
                {
                    personFound = Search(root.Employees[i], nameToSearchFor); // aqui hay concurrencia. Si no estoy en el nodo ctual ves al siguiente y búscate otra vez.
                    if (personFound != null)
                        break;
                }
                return personFound;
            }

            public void Traverse(Employee root)
            {
                Console.WriteLine(root.name);
                for (int i = 0; i < root.Employees.Count; i++)
                {
                    Traverse(root.Employees[i]);
                }
            }
        }

        static void Main(string[] args)
        {
            DepthFirstAlgorithm b = new DepthFirstAlgorithm();
            Employee root = b.BuildEmployeeGraph();
            Console.WriteLine("Traverse Graph\n------");
            b.Traverse(root);

            Console.WriteLine("\nSearch in graph\n------");
            Employee e = b.Search(root, "Eva"); // root es Eva con su lista de trabajadores
            Console.WriteLine(e == null ? "Employee not found" : e.name);  // ? es un if ternario, una condicion. Si se cumple la condicion, ejecútame lo de la izquierda.
            e = b.Search(root, "Brian"); // pasasmos el root como Eva, pero el name to search es Brian, tengo que llamarme a mi mismo
            Console.WriteLine(e == null ? "Employee not found" : e.name);
            e = b.Search(root, "Soni");
            Console.WriteLine(e == null ? "Employee not found" : e.name);
        }
    }
}
