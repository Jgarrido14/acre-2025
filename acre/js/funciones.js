 
    // Muestra el modal
    function showModal() {
        document.getElementById("myModal").style.display = "block";
          }

    // Cierra el modal
    function closeModal() {
        document.getElementById("myModal").style.display = "none";
          }

    // Cierra el modal cuando el usuario hace clic fuera de él
    window.onclick = function (event) {
              var modal = document.getElementById("myModal");
    if (event.target == modal) {
        modal.style.display = "none";
              }
          }
 