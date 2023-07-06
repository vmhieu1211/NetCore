using System;
using System.Collections.Generic;

namespace WebApiApp.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Body { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public int Categoryld { get; set; }

    public int Appld { get; set; }

    public int Author { get; set; }

    public byte Status { get; set; }

    public DateTime? PublishedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
