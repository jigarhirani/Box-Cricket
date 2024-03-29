// alter fade animation
setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

//Export Dropdown
<script>
    document.addEventListener("DOMContentLoaded", function() {
        var exportButton = document.getElementById('exportButton');
    var exportDropdown = document.getElementById('exportDropdown');

    exportButton.addEventListener('click', function(event) {
        event.stopPropagation();
    toggleDropdown();
        });

    document.addEventListener('click', function(event) {
            if (event.target !== exportButton && !exportDropdown.contains(event.target)) {
        hideDropdown();
            }
        });

    function toggleDropdown() {
            if (exportDropdown.style.display === 'block') {
        hideDropdown();
            } else {
        showDropdown();
            }
        }

    function showDropdown() {
        exportDropdown.style.opacity = '0';
    exportDropdown.style.display = 'block';
    setTimeout(function() {
        exportDropdown.style.opacity = '1';
            }, 10);
        }

    function hideDropdown() {
        exportDropdown.style.opacity = '0';
    setTimeout(function() {
        exportDropdown.style.display = 'none';
            }, 300);
        }
    });
</script>
