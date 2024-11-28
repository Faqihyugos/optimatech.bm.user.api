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

public enum Level
{
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
    Level4 = 4,
    Level5 = 5,
    Level6 = 6
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

public enum Occupation
{
    Unknown = 0,
    PNS = 1,
    TNI = 2,
    POLRI = 3,
    LAINNYA = 4,
}

public enum TowerType
{
    Unknown = 0,
    Residential = 1,
    Hospitality = 2,
    Commercial = 3,
}