using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DXCustomValidation.Models
{
	 public class Person
	 {
		  public int Id { get; set; }

		  [Required]
		  public string FirstName { get; set; }
		  public string MiddleName { get; set; }
		  [Required]
		  public string LastName { get; set; }
		  [Required]
		  public string ShippingAddress { get; set; }

		  [RequiredWhen("BillingSameAsShipping", false)]
		  public string BillingAddress { get; set; }

		  public bool BillingSameAsShipping { get; set; }
	 }
}