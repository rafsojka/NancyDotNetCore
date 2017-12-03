using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Movies")]
public class Movie
{
    [DynamoDBHashKey]
    public string MovieId { get; set; }

    public string Title { get; set; }
}