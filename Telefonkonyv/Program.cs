using System;
using System.IO;

namespace Telefonkonyv;

class Program {
    static void Main()
    {
        Menu();
    }

    static void Menu()
    {
        int choice;
        Console.WriteLine("1. Add record");
        Console.WriteLine("2. List record");
        Console.WriteLine("3. Search record");
        Console.WriteLine("4. Delete record");
        Console.WriteLine("5. Modify record");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
        choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                AddRecord();
                break;
            case 2:
                ListRecord();
                break;
            case 3:
                SearchRecord();
                break;
            case 4:
                DeleteRecord();
                break;
            case 5:
                ModifyRecord();
                break;
            case 6:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void AddRecord()
    {
        Person p;
        Console.Write("\nEnter name: ");
        p.name = Console.ReadLine();
        Console.Write("Enter address: ");
        p.address = Console.ReadLine();
        Console.Write("Enter father name: ");
        p.father_name = Console.ReadLine();
        Console.Write("Enter mother name: ");
        p.mother_name = Console.ReadLine();
        Console.Write("Enter mobile no: ");
        p.mobile_no = long.Parse(Console.ReadLine());
        Console.Write("Enter sex: ");
        p.sex = Console.ReadLine();
        Console.Write("Enter e-mail: ");
        p.email = Console.ReadLine();
        Console.Write("Enter citizen no: ");
        p.citizen_no = Console.ReadLine();

        using (FileStream fs = new FileStream("project.dat", FileMode.Append, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(p.name);
            writer.Write(p.address);
            writer.Write(p.father_name);
            writer.Write(p.mother_name);
            writer.Write(p.mobile_no);
            writer.Write(p.sex);
            writer.Write(p.email);
            writer.Write(p.citizen_no);
        }

        Console.WriteLine("\nRecord saved");
        Console.WriteLine("\n\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void ListRecord()
    {
        if (!File.Exists("project.dat"))
        {
            Console.WriteLine("\nFile opening error in listing :");
            return;
        }

        using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            while (fs.Position < fs.Length)
            {
                Person p = new Person
                {
                    name = reader.ReadString(),
                    address = reader.ReadString(),
                    father_name = reader.ReadString(),
                    mother_name = reader.ReadString(),
                    mobile_no = reader.ReadInt64(),
                    sex = reader.ReadString(),
                    email = reader.ReadString(),
                    citizen_no = reader.ReadString()
                };

                Console.WriteLine("\n\n\n YOUR RECORD IS\n\n ");
                Console.WriteLine($"Name={p.name}");
                Console.WriteLine($"Address={p.address}");
                Console.WriteLine($"Father name={p.father_name}");
                Console.WriteLine($"Mother name={p.mother_name}");
                Console.WriteLine($"Mobile no={p.mobile_no}");
                Console.WriteLine($"Sex={p.sex}");
                Console.WriteLine($"E-mail={p.email}");
                Console.WriteLine($"Citizen no={p.citizen_no}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        Menu();
    }

    static void SearchRecord()
    {
        if (!File.Exists("project.dat"))
        {
            Console.WriteLine("\nError in opening file");
            return;
        }

        Console.Write("\nEnter name of person to search: ");
        string name = Console.ReadLine();

        using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            bool found = false;
            while (fs.Position < fs.Length)
            {
                Person p = new Person
                {
                    name = reader.ReadString(),
                    address = reader.ReadString(),
                    father_name = reader.ReadString(),
                    mother_name = reader.ReadString(),
                    mobile_no = reader.ReadInt64(),
                    sex = reader.ReadString(),
                    email = reader.ReadString(),
                    citizen_no = reader.ReadString()
                };

                if (p.name == name)
                {
                    found = true;
                    Console.WriteLine($"\n\tDetail Information About {name}");
                    Console.WriteLine($"Name: {p.name}");
                    Console.WriteLine($"Address: {p.address}");
                    Console.WriteLine($"Father name: {p.father_name}");
                    Console.WriteLine($"Mother name: {p.mother_name}");
                    Console.WriteLine($"Mobile no: {p.mobile_no}");
                    Console.WriteLine($"Sex: {p.sex}");
                    Console.WriteLine($"E-mail: {p.email}");
                    Console.WriteLine($"Citizen no: {p.citizen_no}");
                    break;
                }
            }
            if (!found)
                Console.WriteLine("File not found");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void DeleteRecord()
    {
        if (!File.Exists("project.dat"))
        {
            Console.WriteLine("CONTACT'S DATA NOT ADDED YET.");
            return;
        }

        Console.Write("Enter CONTACT'S NAME: ");
        string name = Console.ReadLine();
        bool flag = false;

        using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))
        using (FileStream tempFs = new FileStream("temp.dat", FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(tempFs))
        {
            while (fs.Position < fs.Length)
            {
                Person p = new Person
                {
                    name = reader.ReadString(),
                    address = reader.ReadString(),
                    father_name = reader.ReadString(),
                    mother_name = reader.ReadString(),
                    mobile_no = reader.ReadInt64(),
                    sex = reader.ReadString(),
                    email = reader.ReadString(),
                    citizen_no = reader.ReadString()
                };

                if (p.name != name)
                {
                    writer.Write(p.name);
                    writer.Write(p.address);
                    writer.Write(p.father_name);
                    writer.Write(p.mother_name);
                    writer.Write(p.mobile_no);
                    writer.Write(p.sex);
                    writer.Write(p.email);
                    writer.Write(p.citizen_no);
                }
                else
                    flag = true;
            }
        }

        if (flag)
        {
            File.Delete("project.dat");
            File.Move("temp.dat", "project.dat");
            Console.WriteLine("RECORD DELETED SUCCESSFULLY.");
        }
        else
        {
            File.Delete("temp.dat");
            Console.WriteLine("NO CONTACT'S RECORD TO DELETE.");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void ModifyRecord()
    {
        if (!File.Exists("project.dat"))
        {
            Console.WriteLine("CONTACT'S DATA NOT ADDED YET.");
            return;
        }

        Console.Write("\nEnter CONTACT'S NAME TO MODIFY: ");
        string name = Console.ReadLine();
        bool flag = false;

        using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.ReadWrite))
        using (BinaryReader reader = new BinaryReader(fs))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            while (fs.Position < fs.Length)
            {
                long position = fs.Position;

                Person p = new Person
                {
                    name = reader.ReadString(),
                    address = reader.ReadString(),
                    father_name = reader.ReadString(),
                    mother_name = reader.ReadString(),
                    mobile_no = reader.ReadInt64(),
                    sex = reader.ReadString(),
                    email = reader.ReadString(),
                    citizen_no = reader.ReadString()
                };

                if (p.name == name)
                {
                    Console.Write("\nEnter name: ");
                    p.name = Console.ReadLine();
                    Console.Write("Enter address: ");
                    p.address = Console.ReadLine();
                    Console.Write("Enter father name: ");
                    p.father_name = Console.ReadLine();
                    Console.Write("Enter mother name: ");
                    p.mother_name = Console.ReadLine();
                    Console.Write("Enter mobile no: ");
                    p.mobile_no = long.Parse(Console.ReadLine());
                    Console.Write("Enter sex: ");
                    p.sex = Console.ReadLine();
                    Console.Write("Enter e-mail: ");
                    p.email = Console.ReadLine();
                    Console.Write("Enter citizen no: ");
                    p.citizen_no = Console.ReadLine();

                    fs.Seek(position, SeekOrigin.Begin);
                    writer.Write(p.name);
                    writer.Write(p.address);
                    writer.Write(p.father_name);
                    writer.Write(p.mother_name);
                    writer.Write(p.mobile_no);
                    writer.Write(p.sex);
                    writer.Write(p.email);
                    writer.Write(p.citizen_no);
                    flag = true;
                    break;
                }
            }
        }

        if (flag)
        {
            Console.WriteLine("\nYour data is modified");
        }
        else
        {
            Console.WriteLine("\nData not found");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }
}
