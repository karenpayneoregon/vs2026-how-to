document.addEventListener('DOMContentLoaded', () => {

    const currentPath = location.pathname.toLowerCase();

    document.querySelectorAll('.nav-link').forEach(link => {

        const href = link.getAttribute('href');

        link.classList.remove(
            'border-bottom',
            'border-top',
            'border-start',
            'border-end',
            'border-dark',
            'bg-primary',
            'text-white',
            'fw-bold',
            'active',
            'text-dark'
        );

        link.removeAttribute('aria-current');

        if (href && href.toLowerCase() === currentPath) {
            link.classList.add(
                'border-dark',
                'border-bottom',
                'border-top',
                'border-start',
                'border-end',
                'bg-primary',
                'text-white',
                'fw-bold',
                'active'
            );

            link.setAttribute('aria-current', 'page');
        } else {
            link.classList.add('text-dark');
        }
    });
});