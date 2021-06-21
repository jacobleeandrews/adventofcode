
namespace DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            const int DESIRED_OUTCOME = 2020;
            System.Random rnd = new System.Random();

            var intList = new System.Collections.Generic.List<int>();
            using (var myFile = new System.IO.StreamReader("puzzleInput.txt"))
            {
                string line;
                while ((line = myFile.ReadLine()) != null)
                {
                    var intParsedLine = int.Parse(line);

                    if (intParsedLine >= 2020) continue;

                    intList.Add(intParsedLine);
                }
            }

            //sorts list ascending
            intList.Sort();

            var selectedIndices = new System.Collections.Generic.List<int>();

            var firstNumber = 0;
            var secondNumber = 0;
            try
            {
                //we aren't leaving this loop without a valid outcome
                while (firstNumber + secondNumber != DESIRED_OUTCOME)
                {
                    //pick any number at random
                    int index = rnd.Next(0, intList.Count);
                    while (selectedIndices.Contains(index))
                    {
                        //we've already picked this number -- try again
                        if (selectedIndices.Count == intList.Count) throw new System.Exception("We have ran out of numbers! Something went wrong");
                        index = rnd.Next(0, intList.Count);
                    }

                    firstNumber = intList[index];

                    selectedIndices.Add(index);

                    var addIndex = 0;
                    secondNumber = 0;

                    //as we are working with an ordered list, we can just keep working our way through our list until we exceed or meet our desired outcome
                    while (firstNumber + secondNumber < DESIRED_OUTCOME)
                    {
                        secondNumber = intList[addIndex];
                        addIndex += 1;
                    }
                    
                }
            }
            catch
            {
                throw;
            }

            if (firstNumber + secondNumber == DESIRED_OUTCOME) System.Console.WriteLine($@"The answer is {firstNumber * secondNumber}");

        }
    }
}
