namespace OptimaTech.BuildingManager.User.Core.Entities;


public enum ApprovalStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
}

public enum Religion
{
    Unknown = 0,
    Islam = 1,
    Katolik = 2,
    Protestan = 3,
    Budha = 4,
    Hindu = 5,
    Konghucu = 6,
}

public enum UserStatus
{
    Owner = 1,
    Occupant = 2, 
    Renter = 3, 
    Agent = 4,
}

public enum UnitStatus
{
    Available = 1,
    Inhabited = 2,
    Leased = 3,
    FitOut = 4,
}

public enum RelationStatus
{
    NotActive = 0,
    Active = 1,
    Inactive = 2,
}

public enum RelationType
{
    Owner = 1,
    Occupant = 2, 
    Renter = 3, 
    Agent = 4,
}
