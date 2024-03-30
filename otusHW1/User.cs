﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserID { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}
