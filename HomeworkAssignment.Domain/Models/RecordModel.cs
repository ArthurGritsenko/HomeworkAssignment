using HomeworkAssignment.Core;
using HomeworkAssignment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Domain.Models
{
    public class RecordModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        public string FavoriteColor { get; set; }

        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"{LastName}, {FirstName}, {Gender}, {FavoriteColor}, {DateOfBirth.ToString(Constants.RecordModelDateTimeFormat)}";
        }
    }
}
