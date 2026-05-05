# ShoppingCart API

## Overview

A backend service for managing a shopping cart in an online store.
Built using .NET 8 Minimal API with a Clean Architecture approach.

## Features

* Create a shopping cart
* Retrieve a shopping cart by user
* Update cart items (replace full state)
* Delete a shopping cart
* In-memory data storage
* Swagger UI for testing

## Architecture

This project follows Clean Architecture principles:

* Domain: core business logic (Cart, CartItem, Product)
* Application: use cases (CreateCart, UpdateCart, etc.)
* Infrastructure: in-memory data and repository
* API: Minimal API endpoints

## API Endpoints

| Method | Endpoint       | Description   |
| ------ | -------------- | ------------- |
| GET    | /cart/{userId} | Get user cart |
| POST   | /cart          | Create cart   |
| PUT    | /cart/{userId} | Update cart   |
| DELETE | /cart/{userId} | Delete cart   |

## Example Request (Update Cart)

```json
{
  "items": [
    { "productId": "p1", "quantity": 2 },
    { "productId": "p2", "quantity": 1 }
  ]
}
```

## Getting Started

### Prerequisites

* .NET 8 SDK

### Run the project

```bash
dotnet run
```

### Open Swagger UI

```
http://localhost:<port>/swagger
```
