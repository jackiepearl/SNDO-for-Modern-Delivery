using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Team1FinalProject.Models
{
   
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        //TODO:  Add any fields that you need for creating a new user

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Additional fields go here
        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City{ get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }




        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }
    }

    public class ChangePhoneNumberModel
    {
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "New Phone Number")]
        public string NewPhoneNumber { get; set; }

    }

    public class ChangeAddressModel
    {
     
        [Display(Name = "New Street Address")]
        public string Street { get; set; }

        
        [Display(Name = "City")]
        public string City { get; set; }

      
        [Display(Name = "State")]
        public string State { get; set; }

     
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

    }

    public class ChangeFirstNameModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "New First Name")]
        public String NewFirstName { get; set; }
    }

    public class ChangeLastNameModel
    {
        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "New Last Name")]
        public String NewLastName { get; set; }
    }


    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set;}
        public String Address { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String UserID { get; set; }
    }
}
