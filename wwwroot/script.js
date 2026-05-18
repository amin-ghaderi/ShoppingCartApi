const form = document.getElementById("cart-form");
const responseBox = document.getElementById("response");

form.addEventListener("submit", async function (event) {
  event.preventDefault();

  const userId = document.getElementById("userId").value.trim();
  const productId = document.getElementById("product").value;
  const quantity = Number(document.getElementById("quantity").value);

  if (!userId) {
    responseBox.className = "response error";
    responseBox.textContent = "Please enter a user ID.";
    return;
  }

  if (quantity < 1) {
    responseBox.className = "response error";
    responseBox.textContent = "Quantity must be at least 1.";
    return;
  }

  const url = `/cart/${encodeURIComponent(userId)}`;
  const body = {
    items: [
      {
        productId: productId,
        quantity: quantity
      }
    ]
  };

  try {
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(body)
    });

    if (response.ok) {
      const cart = await response.json();
      const item = cart.items[0];

      responseBox.className = "response success";
      responseBox.innerHTML =
        "<p><strong>Cart updated successfully</strong></p>" +
        "<p>User: " + cart.userId + "</p>" +
        "<p>Products:</p>" +
        "<ul><li>" + item.productId + " × " + item.quantity + "</li></ul>" +
        "<pre>" + JSON.stringify(cart, null, 2) + "</pre>";
      return;
    }

    if (response.status === 400) {
      const error = await response.json();
      responseBox.className = "response error";
      responseBox.textContent = error.message;
      return;
    }

    if (response.status === 500) {
      responseBox.className = "response error";
      responseBox.textContent =
        "Something went wrong. Please check quantity and try again.";
      return;
    }

    responseBox.className = "response error";
    responseBox.textContent = "Unexpected error. Status: " + response.status;
  } catch (error) {
    responseBox.className = "response error";
    responseBox.textContent =
      "Cannot connect to API. Is the server running?";
  }
});
