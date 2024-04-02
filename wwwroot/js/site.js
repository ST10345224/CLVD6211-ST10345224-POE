// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const galleryItems = document.querySelectorAll('.gallery-item');

galleryItems.forEach(item => {
    item.addEventListener('mouseover', () => {
        item.classList.add('hover'); // Add a hover class
    });

    item.addEventListener('mouseout', () => {
        item.classList.remove('hover'); // Remove hover class
    });
});