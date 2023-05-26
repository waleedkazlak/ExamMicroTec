using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamMicroTec.DAL.Domain
{
    public class Account
    {
        [Key]
        public string Acc_Number { get; set; }

        [ForeignKey("Accounts")]
        public string? ACC_Parent { get; set; }

        public decimal? Balance { get; set; }
       
    }
}
