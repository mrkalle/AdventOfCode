using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        private enum Direction {
            Up, Right, Down, Left
        }

        private string result = "";

        private List<List<Direction>> input = new List<List<Direction>>();

        private KeypadNr Key1 = new KeypadNr("1");
        private KeypadNr Key2 = new KeypadNr("2");
        private KeypadNr Key3 = new KeypadNr("3");
        private KeypadNr Key4 = new KeypadNr("4");
        private KeypadNr Key5 = new KeypadNr("5");
        private KeypadNr Key6 = new KeypadNr("6");
        private KeypadNr Key7 = new KeypadNr("7");
        private KeypadNr Key8 = new KeypadNr("8");
        private KeypadNr Key9 = new KeypadNr("9");

        private KeypadNr CurrentKey = null;

        /*private string inputText = @"ULL
RRDDD
LURDL
UUUUD";*/
        private string inputText = @"DUURRDRRURUUUDLRUDDLLLURULRRLDULDRDUULULLUUUDRDUDDURRULDRDDDUDDURLDLLDDRRURRUUUDDRUDDLLDDDURLRDDDULRDUDDRDRLRDUULDLDRDLUDDDLRDRLDLUUUDLRDLRUUUDDLUURRLLLUUUUDDLDRRDRDRLDRLUUDUDLDRUDDUDLLUUURUUDLULRDRULURURDLDLLDLLDUDLDRDULLDUDDURRDDLLRLLLLDLDRLDDUULRDRURUDRRRDDDUULRULDDLRLLLLRLLLLRLURRRLRLRDLULRRLDRULDRRLRURDDLDDRLRDLDRLULLRRUDUURRULLLRLRLRRUDLRDDLLRRUDUDUURRRDRDLDRUDLDRDLUUULDLRLLDRULRULLRLRDRRLRLULLRURUULRLLRRRDRLULUDDUUULDULDUDDDUDLRLLRDRDLUDLRLRRDDDURUUUDULDLDDLDRDDDLURLDRLDURUDRURDDDDDDULLDLDLU
LURLRUURDDLDDDLDDLULRLUUUDRDUUDDUDLDLDDLLUDURDRDRULULLRLDDUDRRDRUDLRLDDDURDUURLUURRLLDRURDRLDURUDLRLLDDLLRDRRLURLRRUULLLDRLULURULRRDLLLDLDLRDRRURUUUDUDRUULDLUDLURLRDRRLDRUDRUDURLDLDDRUULDURDUURLLUDRUUUUUURRLRULUDRDUDRLLDUDUDUULURUURURULLUUURDRLDDRLUURDLRULDRRRRLRULRDLURRUULURDRRLDLRUURUDRRRDRURRLDDURLUDLDRRLDRLLLLRDUDLULUDRLLLDULUDUULLULLRLURURURDRRDRUURDULRDDLRULLLLLLDLLURLRLLRDLLRLUDLRUDDRLLLDDUDRLDLRLDUDU
RRDDLDLRRUULRDLLURLRURDLUURLLLUUDDULLDRURDUDRLRDRDDUUUULDLUDDLRDULDDRDDDDDLRRDDDRUULDLUDUDRRLUUDDRUDLUUDUDLUDURDURDLLLLDUUUUURUUURDURUUUUDDURULLDDLDLDLULUDRULULULLLDRLRRLLDLURULRDLULRLDRRLDDLULDDRDDRURLDLUULULRDRDRDRRLLLURLLDUUUDRRUUURDLLLRUUDDDULRDRRUUDDUUUDLRRURUDDLUDDDUDLRUDRRDLLLURRRURDRLLULDUULLURRULDLURRUURURRLRDULRLULUDUULRRULLLDDDDURLRRRDUDULLRRDURUURUUULUDLDULLUURDRDRRDURDLUDLULRULRLLURULDRUURRRRDUDULLLLLRRLRUDDUDLLURLRDDLLDLLLDDUDDDDRDURRL
LLRURUDUULRURRUDURRDLUUUDDDDURUUDLLDLRULRUUDUURRLRRUDLLUDLDURURRDDLLRUDDUDLDUUDDLUUULUUURRURDDLUDDLULRRRUURLDLURDULULRULRLDUDLLLLDLLLLRLDLRLDLUULLDDLDRRRURDDRRDURUURLRLRDUDLLURRLDUULDRURDRRURDDDDUUUDDRDLLDDUDURDLUUDRLRDUDLLDDDDDRRDRDUULDDLLDLRUDULLRRLLDUDRRLRURRRRLRDUDDRRDDUUUDLULLRRRDDRUUUDUUURUULUDURUDLDRDRLDLRLLRLRDRDRULRURLDDULRURLRLDUURLDDLUDRLRUDDURLUDLLULDLDDULDUDDDUDRLRDRUUURDUULLDULUUULLLDLRULDULUDLRRURDLULUDUDLDDRDRUUULDLRURLRUURDLULUDLULLRD
UURUDRRDDLRRRLULLDDDRRLDUDLRRULUUDULLDUDURRDLDRRRDLRDUUUDRDRRLLDULRLUDUUULRULULRUDURDRDDLDRULULULLDURULDRUDDDURLLDUDUUUULRUULURDDDUUUURDLDUUURUDDLDRDLLUDDDDULRDLRUDRLRUDDURDLDRLLLLRLULRDDUDLLDRURDDUDRRLRRDLDDUDRRLDLUURLRLLRRRDRLRLLLLLLURULUURRDDRRLRLRUURDLULRUUDRRRLRLRULLLLUDRULLRDDRDDLDLDRRRURLURDDURRLUDDULRRDULRURRRURLUURDDDUDLDUURRRLUDUULULURLRDDRULDLRLLUULRLLRLUUURUUDUURULRRRUULUULRULDDURLDRRULLRDURRDDDLLUDLDRRRRUULDDD";

        public static void Main(string[] args)
        {
            var p = new Program();
            p.SetInput();
            p.SetKeyRelationships();
            p.FindKeys();
            var a = p.result;
            System.Console.WriteLine(">>>" + a);
        }

        // Läs in input
        // Loopa över input, ha en senaste-nr-variabel (start är 5)

        private void SetInput() {
            var linebreak = new char[]{'\r', '\n'};
            var lines = inputText.Split(linebreak);
            for (var i = 0; i < lines.Length; i++ ) {
                if (lines[i] == "")  continue;

                var elementList = new List<Direction>();
                var elements = lines[i].ToCharArray();
                foreach (var el in elements) {
                    switch (el) {
                        case 'U': elementList.Add(Direction.Up); break;
                        case 'R': elementList.Add(Direction.Right); break;
                        case 'D': elementList.Add(Direction.Down); break;
                        case 'L': elementList.Add(Direction.Left); break;
                    }
                }

                input.Add(elementList);
            }
        }  

        private void FindKeys() {
            CurrentKey = Key5;
            foreach (var line in input) {
                foreach (var element in line) {
                    switch (element) {
                        case Direction.Up:
                            if (CurrentKey.UpNeighbour != null) {
                                CurrentKey = CurrentKey.UpNeighbour;
                            }
                            break;
                        case Direction.Right:
                            if (CurrentKey.RightNeighbour != null) {
                                CurrentKey = CurrentKey.RightNeighbour;
                            }
                            break;
                        case Direction.Down:
                            if (CurrentKey.DownNeighbour != null) {
                                CurrentKey = CurrentKey.DownNeighbour;
                            }
                            break;
                        case Direction.Left:
                            if (CurrentKey.LeftNeighbour != null) {
                                CurrentKey = CurrentKey.LeftNeighbour;
                            }
                            break;
                    }
                }

                result += CurrentKey.Value;
            }
        }

        private void SetKeyRelationships() {
            Key1.RightNeighbour = Key2;
            Key1.DownNeighbour = Key4;

            Key2.RightNeighbour = Key3;
            Key2.DownNeighbour = Key5;
            Key2.LeftNeighbour = Key1;

            Key3.DownNeighbour = Key6;
            Key3.LeftNeighbour = Key2;

            Key4.UpNeighbour = Key1;
            Key4.RightNeighbour = Key5;
            Key4.DownNeighbour = Key7;

            Key5.UpNeighbour = Key2;
            Key5.RightNeighbour = Key6;
            Key5.DownNeighbour = Key8;
            Key5.LeftNeighbour = Key4;

            Key6.UpNeighbour = Key3;
            Key6.DownNeighbour = Key9;
            Key6.LeftNeighbour = Key5;

            Key7.UpNeighbour = Key4;
            Key7.RightNeighbour = Key8;

            Key8.UpNeighbour = Key5;
            Key8.RightNeighbour = Key9;
            Key8.LeftNeighbour = Key7;

            Key9.UpNeighbour = Key6;
            Key9.LeftNeighbour = Key8;
        }  
        
        public class KeypadNr {
            public string Value = "-"; 
            public KeypadNr UpNeighbour = null;
            public KeypadNr RightNeighbour = null;
            public KeypadNr DownNeighbour = null;
            public KeypadNr LeftNeighbour = null;

            public KeypadNr(string inValue) {
                Value = inValue;
            }
        }
    }
}
