use WebAutopark;
go

create table VehicleTypes
(
VehicleTypeId int primary key identity(1,1) not null,
Name nvarchar(50) not null,
TaxCoefficient float not null
)

go

create table Vehicles
(
VehicleId int primary key identity(1,1) not null,
VehicleTypeId int not null,
Model nvarchar(50) not null,
RegistrationNumber nvarchar(50) null,
Weight float not null,
Year datetime not null,
Mileage int not null,
Color nvarchar(50) not null,
FuelConsumption float not null
constraint FK_VehcileTypeId foreign key (VehicleTypeId) references VehicleTypes(VehicleTypeId)
)

go

create table Components
(
ComponentId int primary key identity(1,1) not null,
Name nvarchar(50) not null
)

go

create table Orders
(
OrderId int primary key identity(1,1) not null,
VehicleId int not null,
Date datetime not null,
constraint FK_VehicleId foreign key (VehicleId) references Vehicles(VehicleId)
)

go

create table OrderItems
(
OrderItemId int primary key identity(1,1) not null,
OrderId int not null,
ComponentId int not null,
Quantity int not null,
constraint FK_OrderId foreign key (OrderId) references Orders(OrderId),
constraint FK_ComponentId foreign key (ComponentId) references Components(ComponentId)
)
