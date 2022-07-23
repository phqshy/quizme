using QuizMe.Containers;

namespace QuizMe
{
    class QuizMe
    {

        public static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a Quizlet URL.");
            Console.Write("Enter URL > ");
            string url = Console.ReadLine();
            Console.WriteLine("Loading data...");
            QuizletData data = new QuizletData(url);
            Console.WriteLine($"Loaded a Quizlet resource with {data.Data.Keys.Count} terms!");
            findMode(data);
        }

        static void doLearnMode(QuizletData data)
        {
            Console.WriteLine($"Initializing LEARN with {data.Data.Keys.Count()} terms.");
            Console.WriteLine("----------------\n\n\n");

            List<string> incorrect = new List<string>();
            List<string> masteryOne = new List<string>();
            List<string> masteryTwo = new List<string>();

            Console.WriteLine("Welcome to LEARN mode!");
            Console.WriteLine($"Please select if you want terms ({data.Data.Keys.First()}) or definitions ({data.Data.Values.First()}) to be shown.");
            Console.Write("Mode > ");

            string mode = Console.ReadLine();
            if (mode.ToLower() == "terms")
            {
                Console.WriteLine("You have selected terms.");

                Console.WriteLine("Would you like to start with a multiple choice segment? (y/n)");
                Console.Write("Multiple Choice > ");
                string multipleChoice = Console.ReadLine();
                if (multipleChoice == "y" || multipleChoice == "yes")
                {
                    learnMultipleChoice(data);
                }

                var randomized = data.Data.Keys.OrderBy(x => rnd.Next()).ToList();

                foreach (var term in randomized)
                {
                    Console.WriteLine($"The definition of {term} is what?");
                    Console.Write("Answer > ");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == data.Data[term].ToLower())
                    {
                        Console.WriteLine("Correct!\n");
                        masteryOne.Add(term);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect, the correct answer is " + data.Data[term] + ".\n");
                        Console.Write("Press enter to continue, or type 'correct' to mark as correct. ");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "correct")
                        {
                            masteryOne.Add(term);
                        }
                        else
                        {
                            incorrect.Add(term);
                        }
                    }
                }

                Console.WriteLine("\n\n\n");
                Console.WriteLine("You have completed the first part of the lesson!");
                Console.WriteLine("We will be reviewing the ones that you got correct.");
                Console.WriteLine("----------------\n\n\n");

                if (masteryOne.Count != 0)
                {
                    foreach (var term in masteryOne)
                    {
                        Console.WriteLine($"The definition of {term} is what?");
                        Console.Write("Answer > ");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == data.Data[term].ToLower())
                        {
                            Console.WriteLine("Correct!\n");
                            masteryTwo.Add(term);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect, the correct answer is " + data.Data[term] + ".\n");
                            Console.Write("Press enter to continue, or type 'correct' to mark as correct. ");
                            string input = Console.ReadLine();
                            if (input.ToLower() == "correct")
                            {
                                masteryTwo.Add(term);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have no correct terms to review.");
                    Console.WriteLine("Press enter to restart.");
                    Console.ReadLine();
                    findMode(data);
                    return;
                }

                Console.WriteLine("\n\n\n");
                Console.WriteLine("You have completed the second part of the lesson!");
                Console.WriteLine("We will be reviewing the ones that you got incorrect the first time.");
                Console.WriteLine("----------------\n\n\n");

                if (incorrect.Count != 0)
                {
                    foreach (var term in incorrect)
                    {
                        Console.WriteLine($"The definition of {term} is what?");
                        Console.Write("Answer > ");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == data.Data[term].ToLower())
                        {
                            Console.WriteLine("Correct!\n");
                            masteryOne.Add(term);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect, the correct answer is " + data.Data[term] + ".\n");
                            Console.Write("Press enter to continue, or type 'correct' to mark as correct. ");
                            string input = Console.ReadLine();
                            if (input.ToLower() == "correct")
                            {
                                masteryOne.Add(term);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have no incorrect terms to review.");
                }
            }
            else if (mode.ToLower() == "definitions")
            {
                Console.WriteLine("You have selected definitions.");

            }
            else
            {
                Console.WriteLine("Invalid selection.");
                doLearnMode(data);
            }
        }

        static void learnMultipleChoice(QuizletData data)
        {
            Console.WriteLine("\n\nWelcome to multiple choice mode!");
            Console.WriteLine("Here we will do a quick refresher on the terms you will be studying.");
            Console.WriteLine("----------------\n\n\n");

            var randomizedList = data.Data.Keys.OrderBy(x => rnd.Next()).ToList();

            foreach (var term in randomizedList)
            {
                Console.WriteLine($"Which of the following is the definition of {term}?");
                int pos = rnd.Next(1, 5);
                string answer = data.Data[term];

                switch (pos)
                {
                    case 1:
                        Console.WriteLine($"1. {answer}");
                        Console.WriteLine($"2. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"3. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"4. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        break;
                    case 2:
                        Console.WriteLine($"1. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"2. {answer}");
                        Console.WriteLine($"3. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"4. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        break;
                    case 3:
                        Console.WriteLine($"1. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"2. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"3. {answer}");
                        Console.WriteLine($"4. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        break;
                    case 4:
                        Console.WriteLine($"1. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"2. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"3. {data.Data.Values.ElementAt(rnd.Next(0, data.Data.Values.Count))}");
                        Console.WriteLine($"4. {answer}");
                        break;
                }

                Console.Write("Answer > ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == pos)
                {
                    Console.WriteLine("Correct!\n");
                }
                else
                {
                    Console.WriteLine($"Incorrect, the correct answer is {pos}. " + data.Data[term] + ".\n");
                }
            }
        }

        static void findMode(QuizletData data)
        {
            Console.WriteLine("What mode would you like to use? Our current options are: LEARN");
            Console.Write("Enter mode > ");
            string mode = Console.ReadLine();
            switch (mode.ToLower().Replace(" ", ""))
            {
                case "learn":
                    doLearnMode(data);
                    break;
                default:
                    Console.WriteLine("That's not a valid mode!");
                    findMode(data);
                    break;
            }
        }
    }
}