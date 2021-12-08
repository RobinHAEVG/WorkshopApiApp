# Workshop API Package

This project (WorkshopApi) is a package meant to facilitate app development against the 
[Workshop API](https://github.com/KaiserWerk/Workshop-API).

This package offers an easy-to-use abstraction of the Workshop API without the hassle of proper
authentication HTTP calls.

There is a Client for version 1 (ClientV1) and version 2 (ClientV2).
ClientV1 can only handle products while ClientV2 can handle reviews on top of that.

## Usage

First, create a new client:

```csharp
var clientV1 = new ClientV1("http://api-base-url.com", "your api key"); // exact same with V2
```

Then, you can call these methods on the version 1 client:

```csharp
public List<Product> GetAllProducts()
public Product GetProduct(int productId)
public void AddProduct(Product p)
public void EditProduct(Product p)
public void RemoveProduct(int id)
```

The version 2 client has additional methods to handle reviews:

```csharp
public List<Review> GetAllReviews(int productId)
public Review GetReview(int productId, int reviewId)
public void AddReview(int productId, Review r)
public void EditReview(int productId, Review r)
```