﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
using Previgesst.Validation;


namespace Previgesst.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingPassword")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "MotPasse")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class GenerateResetPasswordViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Lien pour réinitialiser le mot de passe")]
        public string Link { get; set; }

        public string TokenExpiryHour { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(ClientLoginRES), Name = "NewPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingNewPassword")]
        [MinLength(8, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }


        [Display(ResourceType = typeof(ClientLoginRES), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingConfirmPassword")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordMismatch")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }

    public class UserListViewModel
    {
        [Display(Name = "")]
        public string UserId { get; set; }

        [Display(Name = "Utilisateur")]
        public string UserName { get; set; }

        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Display(Name = "Inactif")]
        public bool Inactive { get; set; }
    }

    public class MyAccountViewModel
    {
        [Display(Name = "Courriel")]
        [Required(ErrorMessage = "Vous devez entrer une adresse courriel.")]
        [MaxLength(256, ErrorMessage = "Le courriel ne doit pas dépasser {1} caractères.")]
        public string Email { get; set; }

        [Display(Name = "Ancien mot de passe")]
        [MinLength(8, ErrorMessage = "Vous devez entrer un mot de passe de {1} caractères ou plus.")]
        public string OldPassword { get; set; }

        [Display(Name = "Nouveau mot de passe")]
        [MinLength(8, ErrorMessage = "Vous devez entrer un mot de passe de {1} caractères ou plus.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmer le mot de passe")]
        [Compare(nameof(NewPassword), ErrorMessage = "Le mot de passe doit être identique.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Inactif")]
        public bool Inactive { get; set; }

        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Display(Name = "Nom d'utilisateur")]
        [Required(ErrorMessage = "Vous devez entrer le nom d'utilisateur.")]
        public string UserName { get; set; }

        public bool IsCreate { get; set; }
        public bool CanDelete { get; set; }

        [Display(Name = "Rôles")]
        [NotEmpty]
        public IEnumerable<string> RoleNames { get; set; }


        [Display(Name = "Is Corporate")]
        public bool IsCorporate { get; set; }

        [Display(Name = "Clients")]
        public IEnumerable<string> CorporateClients { get; set; }
    }

    public class RoleViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}