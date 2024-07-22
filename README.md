# ShoppingBasket

## Overview
The `ShoppingBasket` project is a simulation of a shopping basket system designed to allow users to add items to their basket and proceed to purchase them. Additionally, it includes endpoints for the admins such as creating products and applying discounts to items. The project is built using C# (.NET 8) and follows the Domain-Driven Design (DDD) methodology.

## Features
### User Features
- **Add Item to Basket**: Users can add various items to their shopping basket.
- **Buy Items**: Users can proceed to purchase the items in their basket.

### Administrative Features
- **Create Products**: Administrators can create new products to be added to the inventory.
- **Create Discounts**: Administrators can create discounts to specific items, like percentage discounts or quantity discount (2 items X give 50% off in item Y).
- **Add Discounts**: Administrators can add those discounts to the items.

## Technologies
- **Language**: C#
- **Database**: MySQL

## Prerequisites
- **Docker**: Ensure Docker is installed on your machine. You can download it from [here](https://www.docker.com/product<s/docker-desktop).
- **MySQL**: MySQL is required for the database.

## Getting Started

1. **Pull the MySQL Docker Image**:
   ```sh
   docker pull mysql:latest

2. **Clone the repository and you are ready to go**:
    ```sh
    git clone https://github.com/francisco-pinto/ShoppingBasket.git

### Happy shopping :D
