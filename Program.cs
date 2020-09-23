using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "movies.csv";

            int fileLength = System.IO.File.ReadAllLines(file).Length;
            int movieID = 0; 
            string title;
            string genres = "";

            Console.WriteLine("Enter '1' to add a Movie.");
            Console.WriteLine("Enter '2' to view Movies.");
            Console.WriteLine("Enter any other key to exit.");
            string selectedOption = Console.ReadLine();

            if(selectedOption == "1")
            {   
                //enter ticket title
                StreamReader reader = new StreamReader(file);
                while(!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] lineData = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        try{movieID = Int32.Parse(lineData[0]) + 1;}
                        catch(FormatException){}
                    }
                    reader.Close();

                Console.WriteLine("Your MovieID is: " + '\n' + movieID + '\n' + 
                "Please enter the movie title:");
                title = Console.ReadLine();
                
                //enter movie genre(s)
                Console.WriteLine("How many genres does this movie fall under?");
                string numberOfGenresString = Console.ReadLine();
                int numberOfGenres = 0;
                try{numberOfGenres = Int32.Parse(numberOfGenresString);}
                catch(FormatException)
                {
                    Console.WriteLine("Invalid input." + '\n' + "Movie not saved." +
                    '\n' + "Please re-run the program.");
                    System.Environment.Exit(0);
                }
                if(numberOfGenres != 0)
                {
                    string[] theGenres = new string[numberOfGenres];
                    int i;
                    int j = 1;
                    for(i = 0; i < numberOfGenres; i++, j++)
                    {   
                        Console.WriteLine("Please enter genre number " + j + ":");
                        theGenres[i] = Console.ReadLine();
                    }
                    genres = String.Join("|", theGenres);
                }

                StreamWriter writer = new StreamWriter(file, append: true);
                writer.WriteLine(movieID + "," + title + "," + genres); 
                Console.WriteLine("Movie successfully added!" + '\n' + 
                "The following Movie data was entered:" + '\n' + movieID + "," + title + 
                "," + genres); 
                writer.Close();

            }
            else if(selectedOption == "2")
            {
                StreamReader reader = new StreamReader(file);

                if(fileLength == 1)
                {
                    reader.Close();
                    Console.WriteLine("There are no Movies.");
                }
                else
                {
                    int z = 1011; 
                    for(int i = 0; i < fileLength; i += z)
                    {
                        if(fileLength - i < z){z = fileLength - i + 10;}/*consistantly off by 10*/

                        for(int j = 1; j < z; j++)
                        {
                            string line = reader.ReadLine();
                            string[] lineData = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            string[] lineDataTwo = lineData[2].Split(new char [] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                            if(lineData[1].Length > 70){lineData[1] = lineData[1].Substring(0, 67) + "...";}
                            
                            Console.Write($"\n{lineData[0],-10}| {lineData[1],-70}|");
                            for(int k = 0; k < lineDataTwo.Count(); k++){Console.Write($" {lineDataTwo[k],1}");}
                        }
                        Console.WriteLine();
                        
                        if(z < 1011)
                        {
                            Console.WriteLine();
                            Console.WriteLine("END OF FILE");
                            System.Environment.Exit(0);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Enter '1' to keep reading.");
                        Console.WriteLine("Enter any other key to exit.");

                        string userOpt = Console.ReadLine();
                        if(userOpt == "1"){/*continue reading*/}
                        else{System.Environment.Exit(0);}
                    }
                } 
            }
        }
    }
}
