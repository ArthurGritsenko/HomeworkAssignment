using HomeworkAssignment.Core;
using HomeworkAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeworkAssignment.WebAPI.ViewModels
{
    public class RecordViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string FavoriteColor { get; set; }

        public string DateOfBirth { get; set; }

        public static RecordViewModel Map(RecordModel model)
        {
            // ideally this should be done using AutoMapper, but let's keep it simple
            if (model == null)
            {
                return new RecordViewModel();
            }

            return new RecordViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender.ToString(),
                FavoriteColor = model.FavoriteColor,
                DateOfBirth = model.DateOfBirth.ToString(Constants.RecordModelDateTimeFormat)
            };
        }
    }
}