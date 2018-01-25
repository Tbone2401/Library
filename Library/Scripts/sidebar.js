function ToggleSidebar() {
    if (sessionStorage.getItem('IsActive')) {
        if (sessionStorage.getItem('IsActive') !== null) {
            if (sessionStorage.getItem('IsActive') === 'true') {
                sessionStorage.setItem('IsActive', 'false');
                $('#sidebar').removeClass('active').filter('[class=""]').removeAttr('class');;
            } else {
                sessionStorage.setItem('IsActive', 'true');
                $('#sidebar').addClass('active');

            }
        }
    } else {
        sessionStorage.setItem('IsActive', 'true');
        $('#sidebar').addClass('active');
    }
}
function CheckSideBar() {
    if (sessionStorage.getItem('IsActive')) {
        $('#sidebar').addClass('active');
    }
}
$(document).ready(function () {
    CheckSideBar();
    $('#sidebarCollapse').on('click', function () {
        ToggleSidebar();
    });
});
