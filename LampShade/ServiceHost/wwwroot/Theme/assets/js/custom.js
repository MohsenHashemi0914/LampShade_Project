const cookieName = "cart-items";

function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        currentProduct.count = parseInt(currentProduct.count) + parseInt(count);
        currentProduct.price = price;
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        };

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart-items-count").text(products.length);
    let cartItemsWrapper = $("#cart-items-wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item">
                              <a class="remove-icon" onclick="removeFromCart('${x.id}')">
                                  <i class="ion-android-close"></i>
                              </a>
                              <div class="image">
                                  <a href="single-product.html">
                                      <img src="/ProductPictures/${x.picture}"
                                           class="img-fluid" alt="">
                                  </a>
                              </div>
                              <div class="content">
                                  <p class="product-title">
                                      <a href="single-product.html">محصول : ${x.name}</a>
                                  </p>
                                  <p class="count">تعداد : ${x.count}</p>
                                  <p class="count">فیمت واحد : ${x.unitPrice}</p>
                              </div>
                          </div>`;

        cartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemForRemove = products.findIndex(x => x.id === id);
    products.splice(itemForRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalItemPriceId, count) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id === id);
    products[productIndex].count = count;
    const newTotalPrice = parseInt(products[productIndex].unitPrice) * parseInt(count);
    $(`#${totalItemPriceId}`).text(newTotalPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}