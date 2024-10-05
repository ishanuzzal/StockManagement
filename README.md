# StockManagementSystem

## Functionalities

- ### Category Management
  - Create, edit, and delete product categories.
  
- ### Business Entity Management
  - Add, edit, and delete two types of business entities:
    - **Buyers**: Represent entities from whom products are purchased.
    - **Sellers**: Represent entities to whom products are sold.
  
- ### Product Management
  - Add, edit, and delete products.
  - Associate each product with a category and inventory details.

- ### Inventory Tracking
  - Track stock levels of products in real-time.
  - Automatically update inventory based on buy and sell transactions.
  - Generate inventory level reports for stock analysis.

- ### Transaction Management
  - Supports two types of transactions:
    - **Buy**: Record products purchased from buyers.
    - **Sell**: Record products sold to sellers.
  - Generate detailed reports for each transaction that can be downloaded.

- ### Reporting
  - Downloadable transaction reports.
  - Generate and view inventory level reports to monitor stock levels.

- ### User Authentication and Authorization
  - Secure login for users.
  - Role-based access control to restrict functionalities based on user roles.
  - Admin and other roles have specific permissions for controlled access.

### Admin Info
-**Email**: Safiqul@gmail.com
-**Password**: Uzzal123@

### ER Diagram: ![Blank diagram](https://github.com/ishanuzzal/StockManagement/blob/master/ER.png)


## Technologies Used
- **Backend**: ASP.NET Core Web API
- **Frontend**: Angular 17
- **ORM**: EntityFramework
- **Database**: MSSQL
- **Logging**: Serilog
- **Architecture**: 3-Layer Architecture (Presentation, Service, Data Access Layer)
  
## Setup Instructions
1. Clone the repository.
2. Set up the database connection in `appsettings.json`.
3. Run migrations to set up the database schema.
4. Run the application using `dotnet run`.
