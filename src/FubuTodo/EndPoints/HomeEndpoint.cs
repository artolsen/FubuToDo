using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Continuations;
using FubuTodoHelpers;
using FubuTodoHelpers.Interfaces;
using Raven.Client;
using StructureMap.Building;

namespace FubuTodo.EndPoints
{
    public class HomeEndpoint
    {
        
        private IFubuTodoService _fubuTodoService;
        public HomeEndpoint(IFubuTodoService fubuTodoService)
        {
            _fubuTodoService = fubuTodoService;
        }
        public IndexViewModel Index(IndexInputModel model)
        {
            return new IndexViewModel() {Tasks = _fubuTodoService.LoadTasks()};
        }

        public AddEditViewModel AddEdit(AddEditInputViewModel inputViewModel)
        {
            AddEditViewModel model = null;
            // we came here from an error, use the current state
            if (!string.IsNullOrEmpty(inputViewModel.ErrorMessage))
            {
                model = new AddEditViewModel()
                {
                    AssignedTo = inputViewModel.AssignedTo,
                    Description = inputViewModel.Description,
                    Id = inputViewModel.Id,
                    CompletedOn = inputViewModel.CompletedDate,
                    ErrorMessage = inputViewModel.ErrorMessage
                };
            }
            else
            {
                if (inputViewModel.Id != 0)
                {
                    var task = _fubuTodoService.LoadTask(inputViewModel.Id);
                    if (task != null)
                    {
                        model = new AddEditViewModel
                        {
                            AssignedTo = task.AssignedTo,
                            CompletedOn = task.CompletedOn,
                            Description = task.Description,
                            Id = task.Id,
                            IsCompleted = task.IsCompleted,
                        };
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
              {
                    model = new AddEditViewModel();
                }
            }

            return model;
        }

        public FubuContinuation AddUpdate(AddEditViewModel model)
        {
            string errorMessage;
            if (!ValidateData(model, out errorMessage))
            {
                AddEditInputViewModel inputModel = new AddEditInputViewModel
                {
                    AssignedTo = model.AssignedTo,
                    Description = model.Description,
                    Id = model.Id.HasValue ? model.Id.Value : 0,
                    ErrorMessage = errorMessage,
                    IsCompleted = model.IsCompleted,
                };
                return FubuContinuation.TransferTo(inputModel, "get");
            }
            int id = model.Id.HasValue ? model.Id.Value : 0;
            
            FubuTask task = new FubuTask
            {
                Description = model.Description,
                AssignedTo = model.AssignedTo,

            };
            if (id > 0)
            {
                task.Id = id;
            }
            if (!model.CompletedOn.HasValue && model.IsCompleted)
            {
                task.CompletedOn = DateTime.Now;
            }
            
            _fubuTodoService.SaveTask(task);

            return FubuContinuation.RedirectTo<IndexInputModel>();
        }

        public FubuContinuation DeleteTask(DeleteInputModel model)
        {
            _fubuTodoService.DeleteTask(model.Id);
            return FubuContinuation.RedirectTo<IndexInputModel>();
        }

        private bool ValidateData(AddEditViewModel model, out string errorMessage)
        {
            
            string message = string.Empty;
            if (string.IsNullOrEmpty(model.Description))
            {
                message += "Description cannot be empty";
            }
            errorMessage = message;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return false;
            }
            return true;
        }
    }
}