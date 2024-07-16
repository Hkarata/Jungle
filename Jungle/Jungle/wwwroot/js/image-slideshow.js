var images = ['/Images/shirts.png', '/Images/beats.png', '/Images/shoes.png']; // Add your image paths here
var index = 0;

function changeImage() {
    var imgElement = document.getElementById('slideshow');
    imgElement.src = images[index];
    index = (index + 1) % images.length;
}

window.onload = function () {
    setInterval(changeImage, 3000); // Change image every 3 seconds
};
