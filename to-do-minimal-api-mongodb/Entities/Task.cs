using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace to_do_minimal_api_mongodb.Entities;

public class Task
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name"), BsonRepresentation(BsonType.String)]
    public string? Name { get; set; }

    [BsonElement("is_done"), BsonRepresentation(BsonType.Boolean)]
    public bool IsDone { get; set; } = false;
}