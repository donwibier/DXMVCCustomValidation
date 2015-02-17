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
		  public string Name { get; set; }

		  [Required]
		  public string ShippingAddress { get; set; }

		  [Required]
		  public string ShippingCity { get; set; }
		  [RequiredWhen("ShippingCountry", "USA", ErrorMessage = "State is required for United States")]
		  public string ShippingState { get; set; }

		  [Required]
		  public string ShippingCountry { get; set; }
		  [RequiredWhen("BillingSameAsShipping", false)]
		  public string BillingAddress { get; set; }
		  [RequiredWhen("BillingSameAsShipping", false)]
		  public string BillingCity { get; set; }
		  [RequiredWhen("BillingSameAsShipping", false)]
		  [RequiredWhen("BillingCountry", "USA", ErrorMessage = "State is required for United States")]
		  public string BillingState { get; set; }
		  [RequiredWhen("BillingSameAsShipping", false)]
		  public string BillingCountry { get; set; }

		  public bool BillingSameAsShipping { get; set; }
	 }
}