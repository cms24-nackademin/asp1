window.addEventListener('resize', removeSidebarShowOnResize);
document.addEventListener('DOMContentLoaded', () => {
    initDarkMode()
    initCloseButtons()
    initMobileMenu()
    initModals()
    initProfileOptionsDropDown()
    initToggles()
})

function initMobileMenu() {
    const buttons = document.querySelectorAll('[data-type="menu"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)

            targetElement.classList.add('show')
        })  
    })
}

function initModals() {
    const buttons = document.querySelectorAll('[data-type="modal"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
            console.log('d')
            targetElement.classList.add('show')
        })
    })
}

function initCloseButtons() {
    const buttons = document.querySelectorAll('[data-type="close"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
           
            targetElement.classList.remove('show')
        })
    })
}

function initProfileOptionsDropDown() {
    const avatar = document.getElementById('profileAvatar')
    const dropdown = document.getElementById('profileDropdown')

    avatar.addEventListener('click', function () {
        dropdown.classList.toggle('show')
    });

    document.addEventListener('click', function (e) {
        if (!avatar.contains(e.target) && !dropdown.contains(e.target)) {
            dropdown.classList.remove('show')
        }
    });
}

function removeSidebarShowOnResize() {
    const sidebar = document.getElementById('sidebar')
    if (sidebar && sidebar.classList.contains('show')) {
        sidebar.classList.remove('show')
    }
}


function initDarkMode() {
    if (localStorage.getItem('theme') === 'dark') {
        document.body.classList.add('dark-mode')
        document.documentElement.setAttribute('data-mode', 'dark')

        const darkModeToggle = document.querySelector('#darkModeToggle')
        darkModeToggle.checked = true
    }
}

function initToggles() {
    const toggles = document.querySelectorAll('[data-type="toggle"]')

    toggles.forEach(toggle => {
        const togglefunction = toggle.getAttribute('data-func')

        if (togglefunction === "darkmode") {
            toggle.addEventListener('change', function () {
                if (this.checked) {
                    document.documentElement.setAttribute('data-mode', 'dark')
                    localStorage.setItem('theme', 'dark');´´
                } else {
                    document.documentElement.removeAttribute('data-mode')
                    localStorage.setItem('theme', 'light');
                }
            });
        }
    })
}
