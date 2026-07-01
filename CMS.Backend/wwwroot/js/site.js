// ============================================================
// ManDuyCMS PROFESSIONAL ADMIN - Site JavaScript
// Sidebar toggle, active nav, interactions
// ============================================================

document.addEventListener('DOMContentLoaded', function () {

    // --- Sidebar Toggle ---
    const sidebar = document.getElementById('cyberSidebar');
    const overlay = document.getElementById('sidebarOverlay');
    const toggleBtn = document.getElementById('sidebarToggle');

    if (toggleBtn && sidebar && overlay) {
        toggleBtn.addEventListener('click', function () {
            sidebar.classList.toggle('open');
            overlay.classList.toggle('open');
        });

        overlay.addEventListener('click', function () {
            sidebar.classList.remove('open');
            overlay.classList.remove('open');
        });
    }

    // --- Active Nav Item ---
    const currentPath = window.location.pathname.toLowerCase();
    const navItems = document.querySelectorAll('.sidebar-nav .nav-item');

    navItems.forEach(function (item) {
        const href = item.getAttribute('href');
        if (!href) return;

        const hrefPath = href.toLowerCase();

        // Exact match or starts-with for sub-pages
        if (currentPath === hrefPath ||
            (hrefPath !== '/' && currentPath.startsWith(hrefPath))) {
            item.classList.add('active');
        }
    });

    // If no item is active and we're on root, activate Home
    const anyActive = document.querySelector('.sidebar-nav .nav-item.active');
    if (!anyActive && (currentPath === '/' || currentPath === '')) {
        const homeItem = document.querySelector('.sidebar-nav .nav-item[href="/"]');
        if (homeItem) homeItem.classList.add('active');
    }

    // --- Animate content on load ---
    const content = document.querySelector('.cyber-content');
    if (content) {
        content.classList.add('animate-fade-in');
    }

    // --- Search bar interaction ---
    const searchInput = document.getElementById('topbarSearchInput');
    if (searchInput) {
        searchInput.addEventListener('keydown', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault();
                // Can be extended to support global search
            }
        });
    }
});
