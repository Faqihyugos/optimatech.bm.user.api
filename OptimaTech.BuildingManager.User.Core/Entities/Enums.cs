namespace OptimaTech.BuildingManager.User.Core.Entities;

public enum RequestTypeName
{
    OwnerRegistration = 1,
    RenterRegistration = 2,
    OccupantRegistration = 3,
    AgentRegistration = 4
}

public enum Status
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
}

public enum StatusUnit
{
    Empty = 1,
    Occupied = 2,
    Rented = 3,
    FittedOut = 4
}

public enum UserType
{
    Owner = 1,
    Occupant = 2,
    Renter = 3,
    Agent = 4
}