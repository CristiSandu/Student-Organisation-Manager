using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    public class User
    {
        public enum Pozition {Junior = 0, Member = 1, Mentor= 2, Admin=3}; 
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PictureURL { get; set; }
        public string Path { get; set; }
        public int Stars { get; set; }
        private int Role { get; set; }
        public List<string> Highlits {get; set;}
        public bool IsPresent { get; set; }

        public bool IsAdmin => Role > 2;
        public Pozition GetPozition()
        {
            switch (Role)
            {
                case 3:
                    return Pozition.Admin;
                case 2:
                    return Pozition.Mentor;
                case 1:
                    return Pozition.Member;
                default:
                    return Pozition.Junior;
            }
        } 

    }
}
