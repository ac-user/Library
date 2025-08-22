namespace Library.Data.Entities;

public partial class CollectionAssociation
{
    public int CollectionAssociationId { get; set; }

    public int CollectionId { get; set; }

    public string MediaType { get; set; } = null!;

    public int MediaId { get; set; }


    public Collection Collection { get; set; } 
    public Book Book { get; set; } 
    public Music Music { get; set; }
    public Movie Movie { get; set; }

}
