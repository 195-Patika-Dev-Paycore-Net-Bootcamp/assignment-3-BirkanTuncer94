# Birkan Tuncer Assignment - 3
<h2> Project Description </h2>

It is an API project that will enable a company working on smart waste collection systems to collect all containers by visiting all points in the shortest time by using the garbage collection vehicle in the most optimal way.

<h2> Code details of the project </h2>

Postgre sql database was used in the project. It contains two tables, the vehicle and container tables. There are 2 data in the vehicle table and 16 data in the container table. There are 2 controller classes and 10 http methods in total.
There are transaction structures, try catch methods and lists in the project.

<b> For the 1st item requested in the project: </b> Tables were created with the help of postgresql and filled in manually

<b> For the 2st item requested in the project: </b> Actual locations not used

<b> For the 3st item requested in the project: </b> The entire list is returned using the httpget method. For adding a new vehicle, httppost method used.

<b> For the 4st item requested in the project: </b> httpput method is used to update vehicle information

<b> For the 5st item requested in the project: </b> The httpdelete method is used to delete the vehicle data, but if the id of the deleted vehicle is added to any container, that container is also deleted.

<b> For the 6st item requested in the project: </b> The vehicleid property is created in the container class and the connection between the vehicle and the container is made here.

<b> For the 7st item requested in the project: </b> The entire list is returned using the httpget method. For adding a new container, httppost method used.

<b> For the 8st item requested in the project: </b> httpput method is used to update container information but its not changing the vehicle id of the container.

<b> For the 9st item requested in the project: </b> The httpdelete method is used to delete the container data

<b> For the 10st item requested in the project: </b> Using the connection created with vehicle id, all containers connected to the vehicle queried with this id are shown.

<b> For the 11st item requested in the project: </b> As a general algorithm, according to the information received from the user, how many elements each cluster will take is calculated in advance and then the containers we have are placed in the clusters according to this number. In the last set, all the missing or extra containers are added. Download the Code for more information.


<h3> Screenshots from the project </h3>

![postgresql1](https://user-images.githubusercontent.com/97250941/187081823-1f1202f6-ae36-47ef-b96c-dfe84ac7c1ca.png)
![postgresql2](https://user-images.githubusercontent.com/97250941/187081828-e8dd7c0b-0f76-451a-8669-98937b0ffc2b.png)
![postgresql3](https://user-images.githubusercontent.com/97250941/187081830-363c82e9-6fcf-42d9-9dff-bd482617a6a6.png)
![postgresql4](https://user-images.githubusercontent.com/97250941/187081839-5704e3a1-dff4-4dd6-84ab-57a449ef30ef.png)
![swaggercontainergetall](https://user-images.githubusercontent.com/97250941/187081848-84fad19b-290e-487c-b455-d5260955353d.png)
![swaggercontainergetbyidforcluster](https://user-images.githubusercontent.com/97250941/187081849-078db7f9-97b5-4819-81bd-409eab357797.png)
![swaggercontainerpost](https://user-images.githubusercontent.com/97250941/187081850-3cb45039-008d-4afb-9051-637d44c8265c.png)
![swaggercontainersgetbyvehicleid](https://user-images.githubusercontent.com/97250941/187081852-03b520d5-d704-4705-bd2f-6cc1e84200d2.png)
![swaggerput](https://user-images.githubusercontent.com/97250941/187081856-b02c8cec-b478-4ba2-936b-8b7a7fb3b854.png)
![swaggervehiclegetall](https://user-images.githubusercontent.com/97250941/187081860-48031934-3ffd-456c-bce6-925ea4bb35d8.png)
