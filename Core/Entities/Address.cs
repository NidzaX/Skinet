﻿namespace Core.Entities;

public class Address : BaseEntity
{
    public required string Line1 { get; set; }
    public required string? Line2 { get; set; }
    public required string City { get; set; } 
    public string? State { get; set; } 
    public required string PostalCode { get; set; }
    public required string Country { get; set; } 
}

