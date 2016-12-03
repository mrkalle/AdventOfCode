using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        private enum Direction {
            Left, Right
        }

        private enum CardinalDirection {
            North, East, South, West
        }

        private List<Tuple<Direction, int>> input = new List<Tuple<Direction, int>>();
        private List<Tuple<int,int>> historicCoordinates = new List<Tuple<int,int>>();

        private bool FirstFoundMatch = true;

        //private string inputText = "R2, L3";
        //private string inputText = "R2, R2, R2";
        //private string inputText = "R5, L5, R5, R3";
        //private string inputText = "R8, R4, R4, R8";
        private string inputText = "R5, R4, R2, L3, R1, R1, L4, L5, R3, L1, L1, R4, L2, R1, R4, R4, L2, L2, R4, L4, R1, R3, L3, L1, L2, R1, R5, L5, L1, L1, R3, R5, L1, R4, L5, R5, R1, L185, R4, L1, R51, R3, L2, R78, R1, L4, R188, R1, L5, R5, R2, R3, L5, R3, R4, L1, R2, R2, L4, L4, L5, R5, R4, L4, R2, L5, R2, L1, L4, R4, L4, R2, L3, L4, R2, L3, R3, R2, L2, L3, R4, R3, R1, L4, L2, L5, R4, R4, L1, R1, L5, L1, R3, R1, L2, R1, R1, R3, L4, L1, L3, R2, R4, R2, L2, R1, L5, R3, L3, R3, L1, R4, L3, L3, R4, L2, L1, L3, R2, R3, L2, L1, R4, L3, L5, L2, L4, R1, L4, L4, R3, R5, L4, L1, L1, R4, L2, R5, R1, R1, R2, R1, R5, L1, L3, L5, R2";
        
        public static void Main(string[] args)
        {
            new Program().CalculateDistance();
        }

        private void CalculateDistance() {
            SetInput();
            var distance = GetDistance();
            Console.WriteLine("Distance: " + distance);
        }

        private void SetInput() {
            var elements = inputText.Replace(" ", "").Split(',');
            foreach (var element in elements ) {
                Direction direction = Direction.Left;
                if (element.StartsWith("R")) {
                    direction = Direction.Right;
                }

                var nrOfSteps = int.Parse(element.Substring(1, element.Length - 1));
                input.Add(new Tuple<Direction, int>(direction, nrOfSteps));
            }
        }   

        private int GetDistance() {
            var x = 0;
            var y = 0;
            var cardinalDirection = CardinalDirection.North;
            historicCoordinates.Add(new Tuple<int, int>(x,y));
            foreach (var inElem in input) {     
                var direction = inElem.Item1;   
                var nrOfSteps = inElem.Item2;           
                switch (cardinalDirection) {
                    case CardinalDirection.North:
                        if (direction == Direction.Left) {
                            AddHistoricCoordinates(CardinalDirection.West, nrOfSteps, x, y);
                            x -= nrOfSteps;
                            cardinalDirection = CardinalDirection.West;
                        } else {                            
                            AddHistoricCoordinates(CardinalDirection.East, nrOfSteps, x, y);
                            x += nrOfSteps;
                            cardinalDirection = CardinalDirection.East;
                        }

                        break;
                    case CardinalDirection.East:
                        if (direction == Direction.Left) {
                            AddHistoricCoordinates(CardinalDirection.North, nrOfSteps, x, y);
                            y += nrOfSteps;
                            cardinalDirection = CardinalDirection.North;
                        } else {                    
                            AddHistoricCoordinates(CardinalDirection.South, nrOfSteps, x, y);        
                            y -= nrOfSteps;
                            cardinalDirection = CardinalDirection.South;
                        }

                        break;
                    case CardinalDirection.South:
                        if (direction == Direction.Left) {
                            AddHistoricCoordinates(CardinalDirection.East, nrOfSteps, x, y);
                            x += nrOfSteps;
                            cardinalDirection = CardinalDirection.East;
                        } else {               
                            AddHistoricCoordinates(CardinalDirection.West, nrOfSteps, x, y);             
                            x -= nrOfSteps;
                            cardinalDirection = CardinalDirection.West;
                        }

                        break;
                    case CardinalDirection.West:
                        if (direction == Direction.Left) {
                            AddHistoricCoordinates(CardinalDirection.South, nrOfSteps, x, y);
                            y -= nrOfSteps;
                            cardinalDirection = CardinalDirection.South;
                        } else {            
                            AddHistoricCoordinates(CardinalDirection.North, nrOfSteps, x, y);                
                            y += nrOfSteps;
                            cardinalDirection = CardinalDirection.North;
                        }

                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }  

        private void AddHistoricCoordinates(CardinalDirection directionOfWalk, int nrOfSteps, int xStart, int yStart) {
            for (var i = 1; i <= nrOfSteps; i++) {
                switch (directionOfWalk) {
                    case CardinalDirection.North: 
                        HasHistoricMatch(xStart,yStart+i);
                        historicCoordinates.Add(new Tuple<int, int>(xStart,yStart+i));
                        break;
                    case CardinalDirection.East: 
                        HasHistoricMatch(xStart+i,yStart);
                        historicCoordinates.Add(new Tuple<int, int>(xStart+i,yStart));
                        break;
                    case CardinalDirection.South: 
                        HasHistoricMatch(xStart,yStart-i);
                        historicCoordinates.Add(new Tuple<int, int>(xStart,yStart-i));
                        break;
                    case CardinalDirection.West: 
                        HasHistoricMatch(xStart-i,yStart);
                        historicCoordinates.Add(new Tuple<int, int>(xStart-i,yStart));
                        break;
                }
            }
        }

        private bool HasHistoricMatch(int x, int y) {
            foreach (var histCoord in historicCoordinates) {
                if (histCoord.Item1 == x && histCoord.Item2 == y) {
                    if (FirstFoundMatch == true) {
                        FirstFoundMatch = false;
                        Console.WriteLine("Match at: " + x + "," + y + ", abs: " + (Math.Abs(x) + Math.Abs(y)));
                    }

                    return true;
                }         
            }

            return false;
        }
    }
}