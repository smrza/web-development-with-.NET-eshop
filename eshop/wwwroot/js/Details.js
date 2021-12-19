function ViewHideDetails(groupElementId, buttonElementId) {

    var orderItemsGroup = document.getElementById(groupElementId);
    var orderItemsButton = document.getElementById(buttonElementId);

    if (orderItemsGroup && orderItemsButton) {
        if (orderItemsGroup.style.display === "none") {
            orderItemsGroup.style.display = "block";
            orderItemsButton.textContent = "Hide Details";
        }
        else {
            orderItemsGroup.style.display = "none";
            orderItemsButton.textContent = "View Details";
        }
    }
}