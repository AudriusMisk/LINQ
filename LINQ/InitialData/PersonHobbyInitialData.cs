using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.InitialData
{
    public class PersonHobbyInitialData
    {
        public static List<PersonHobby> DataSeed => new List<PersonHobby>
        {
            new  PersonHobby { FirstName = "Laurynas", Text="Astrology"  },
            new  PersonHobby { FirstName = "Laurynas", Text="Art"  },
            new  PersonHobby { FirstName = "Antanas", Text="Animation"  },
            new  PersonHobby { FirstName = "Adomas", Text="Card games"  },
            new  PersonHobby { FirstName = "Adomas", Text="Photograpy"  },
            new  PersonHobby { FirstName = "Vilte",Text="Movies watching"  },
            new  PersonHobby { FirstName = "Vilte", Text="Running"  },
            new  PersonHobby { FirstName = "Julija", Text="Football"  },
            new  PersonHobby { FirstName = "Rasa", Text="Tennis"  },
            new  PersonHobby { FirstName = "Rasa", Text="Museums"  },
            new  PersonHobby { FirstName = "Matas ", Text="Music"  },

        };
    }
}
