# Console Application for Movie Functions Management

This is a .NET console application that provides a menu-driven interface to manage movie functions. Users can create, modify, delete, or list available functions.

## Features
- **Create a Function:** When creating a function, users must input:
  - Movie name (from a preloaded `movies.txt` file)
  - Director name (from a preloaded `directors.txt` file)
  - Date, time, and price of the function
- **Edit a Function:** Users can modify an existing function by providing its ID.
- **Delete a Function:** Users can remove a function by providing its ID.
- **List Functions:** Displays all existing functions.

## Business Rules
- Each movie can only be directed by one director.
- A director can direct multiple movies.
- A maximum of **10 functions per day** is allowed for each director.
- International movies can have up to **8 functions** assigned.
- National movies have no limit on the number of functions to promote local cinema.

## Requirements
- .NET SDK installed
- `movies.txt` and `directors.txt` files must be available in the application directory.

## Running the Application
1. Clone this repository:
   ```sh
   git clone https://github.com/LucianaMSanchez/PS-cine-api.git
   ```
2. Build and run the application:
   ```sh
   dotnet run
   ```

## Contributions
Feel free to fork this repository.

## License
This project is open-source and available under the MIT License.

