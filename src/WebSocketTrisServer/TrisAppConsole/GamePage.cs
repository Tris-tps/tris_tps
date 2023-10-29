using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientView
{
    public class GamePage
    {
        public static void DisplayTable()
        {
            string tableDoubleLines = @"  
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║      
                                             ═══════╬═══════╬═══════
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║      
                                             ═══════╬═══════╬═══════
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║       
";

            string tableSingleLine = @"
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │      
                                             ───────┼───────┼───────
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │      
                                             ───────┼───────┼───────
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │       
";

            string tableBig = @"
                                                    |       |      
                                                    |       |      
                                                    |       |      
                                             -----------------------
                                                    |       |      
                                                    |       |      
                                                    |       |      
                                             -----------------------
                                                    |       |      
                                                    |       |      
                                                    |       |       
";
            string tableSmall = @"
                                                    |   |  
                                                 -----------
                                                    |   |  
                                                 -----------
                                                    |   |   
 
";
        }
    }
}
