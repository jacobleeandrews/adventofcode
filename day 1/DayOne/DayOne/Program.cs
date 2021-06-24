
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayOne
{


    class Program
    {
        const int DESIRED_OUTCOME = 2020;
        static System.Random rnd = new System.Random();
        static System.Random rnd2 = new System.Random();

        static void Main(string[] args)
        {
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

            PartOne(intList);

            PartTwo(intList);
            
        }

        static void PartTwo(System.Collections.Generic.List<int> intList)
        {

            var selectedCombination = new Dictionary<int, Dictionary<int, int>>();

            var firstNumber = 0;
            var secondNumber = 0;
            var thirdNumber = 0;

            try
            {
                while (firstNumber + secondNumber + thirdNumber != DESIRED_OUTCOME)
                {
                    //generate initial seed of the run
                    var firstRandomNumber = intList[rnd.Next(0, intList.Count)];
                    var secondRandomNumber = intList[rnd.Next(0, intList.Count)];
                    var thirdRandomNumber = intList[rnd.Next(0, intList.Count)];

                    while(selectedCombination.ContainsKey(firstRandomNumber) &&
                          selectedCombination[firstRandomNumber].ContainsKey(secondRandomNumber) &&
                          selectedCombination[firstRandomNumber][secondRandomNumber] == thirdRandomNumber)
                    {
                        //if we have guessed this combination before, reroll
                        firstRandomNumber = intList[rnd.Next(0, intList.Count)];
                        secondRandomNumber = intList[rnd.Next(0, intList.Count)];
                        thirdRandomNumber = intList[rnd.Next(0, intList.Count)];
                    }

                    //index the combination and guess it
                    if (selectedCombination.ContainsKey(firstRandomNumber))
                    {
                        
                        if (selectedCombination[firstRandomNumber].ContainsKey(secondRandomNumber))
                        {
                           
                            selectedCombination[firstRandomNumber][secondRandomNumber] = thirdRandomNumber;

                            firstNumber = firstRandomNumber;
                            secondNumber = secondRandomNumber;
                            thirdNumber = thirdRandomNumber;
                        }
                        else
                        {
                            selectedCombination[firstRandomNumber].Add(secondRandomNumber, thirdRandomNumber);

                            firstNumber = firstRandomNumber;
                            secondNumber = secondRandomNumber;
                            thirdNumber = thirdRandomNumber;
                        }
                    }
                    else
                    {
                        selectedCombination.Add(firstRandomNumber, new Dictionary<int, int>());
                        selectedCombination[firstRandomNumber].Add(secondRandomNumber, thirdRandomNumber);

                        firstNumber = firstRandomNumber;
                        secondNumber = secondRandomNumber;
                        thirdNumber = thirdRandomNumber;
                    }
                    

                }

            }
            catch
            {
                throw;
            }
            if (firstNumber + secondNumber + thirdNumber == DESIRED_OUTCOME) System.Console.WriteLine($@"The answer is {firstNumber * secondNumber * thirdNumber}");
        }

        static void PartOne(System.Collections.Generic.List<int> intList)
        {

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
