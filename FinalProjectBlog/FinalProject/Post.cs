//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Post1 { get; set; }
        public System.DateTime Date { get; set; }
        public string ImagePath { get; set; }
        public string postedBy { get; set; }
    
        public virtual myUser myUser { get; set; }
    }
}
